using CloudApp.Core.Dtos.CheckIn;
using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudApp.Application
{
    public class CheckInService: ICheckInService
    {
        private const int MaxStreakWindowDays = 3650;
        private readonly ICheckInRepository _repo;
        private readonly ILogger<CheckInService> _logger;
        private readonly TimeProvider _timeProvider;
        private readonly TimeZoneInfo _tz;

        public CheckInService(ICheckInRepository repo, ILogger<CheckInService> logger, TimeProvider timeProvider)
        {
            _repo = repo;
            _logger = logger;
            _timeProvider = timeProvider;
            _tz = ResolveChinaTimeZone();
        }

        /// <summary>Windows 用 "China Standard Time"，Linux 容器用 "Asia/Shanghai"；两边都试，最后兜 UTC（构造期绝不让它抛）</summary>
        private static TimeZoneInfo ResolveChinaTimeZone()
        {
            foreach (var id in new[] { "Asia/Shanghai", "China Standard Time" })
            {
                try { return TimeZoneInfo.FindSystemTimeZoneById(id); }
                catch (TimeZoneNotFoundException) { }
                catch (InvalidTimeZoneException) { }
            }
            return TimeZoneInfo.Utc;
        }

        private DateOnly GetToday()
            => DateOnly.FromDateTime(TimeZoneInfo.ConvertTime(_timeProvider.GetUtcNow(), _tz).DateTime);

        public async Task<CheckInResult> CheckInAsync(string openId, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(openId))
                throw new ArgumentException("openId 不能为空", nameof(openId));

            var today = GetToday();

            // 快路径：已签到
            if (await _repo.GetByDateAsync(openId, today, ct) != null)
                return await AlreadyAsync(openId, today, ct);

            var record = new CheckInRecord { OpenId = openId, CheckInDate = today };
            _repo.Add(record);

            try
            {
                await _repo.SaveChangeAsync();
            }
            catch (DbUpdateException ex)
            {
                // 唯一索引 (OpenId, CheckInDate) 挡下并发重复插入：
                _logger.LogWarning(ex, "并发重复签到被唯一索引拦截 openId={OpenId} date={Date}", openId, today);
                _repo.Detach(record);
                return await AlreadyAsync(openId, today, ct);
            }

            var streak = await CalcStreakAsync(openId, today, ct);
            _logger.LogInformation("签到成功 openId={OpenId} date={Date} streak={Streak}", openId, today, streak);

            return new CheckInResult
            {
                Success = true,
                AlreadyCheckIn = false,
                Streak = streak,
                Message = streak > 1 ? $"签到成功，已连续签到 {streak} 天" : "签到成功"
            };
        }

        public async Task<CheckInStatus> GetStatusAsync(string openId, CancellationToken ct = default)
        {
            if (string.IsNullOrWhiteSpace(openId))
                throw new ArgumentException("openId 不能为空", nameof(openId));

            var today = GetToday();
            var from = today.AddDays(-(MaxStreakWindowDays - 1));
            var dates = await _repo.GetDatesInRangeAsync(openId, from, today, ct);
            var set = dates.ToHashSet();

            return new CheckInStatus
            {
                CheckedInToday = set.Contains(today),
                Streak = CalcStreak(set, today),
                TotalDays = await _repo.GetTotalCountAsync(openId, ct),
                Today = today,
                LastCheckInDate = set.Count == 0 ? null : set.Max()
            };
        }

        private async Task<CheckInResult> AlreadyAsync(string openId, DateOnly today, CancellationToken ct)
            => new()
            {
                Success = true,
                AlreadyCheckIn = true,
                Streak = await CalcStreakAsync(openId, today, ct),
                Message = "今天已经签到过了"
            };

        private async Task<int> CalcStreakAsync(string openId, DateOnly today, CancellationToken ct)
        {
            var from = today.AddDays(-(MaxStreakWindowDays - 1));
            var dates = await _repo.GetDatesInRangeAsync(openId, from, today, ct);
            return CalcStreak(dates.ToHashSet(), today);
        }

        /// <summary>今天签过就从今天往前数；今天还没签就从昨天往前数（保证白天查状态也能看到连续天数）</summary>
        private static int CalcStreak(HashSet<DateOnly> dates, DateOnly today)
        {
            var cursor = dates.Contains(today) ? today : today.AddDays(-1);
            var streak = 0;
            while (dates.Contains(cursor))
            {
                streak++;
                cursor = cursor.AddDays(-1);
            }
            return streak;
        }
    }
}

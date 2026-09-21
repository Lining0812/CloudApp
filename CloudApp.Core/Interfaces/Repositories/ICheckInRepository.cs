using CloudApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Interfaces.Repositories
{
    public interface ICheckInRepository : IRepository<CheckInRecord>
    {
        /// <summary>取某人某天的签到记录（唯一索引命中的精确查询）</summary>
        Task<CheckInRecord?> GetByDateAsync(string openId, DateOnly date, CancellationToken ct = default);

        /// <summary>取区间内的签到日期（用于算连续天数）</summary>
        Task<List<DateOnly>> GetDatesInRangeAsync(string openId, DateOnly from, DateOnly to, CancellationToken ct = default);

        /// <summary>累计签到天数</summary>
        Task<int> GetTotalCountAsync(string openId, CancellationToken ct = default);

        // Add / SaveChangeAsync 由 IRepository<CheckInRecord> 提供，无需重复声明

        /// <summary>把实体从变更跟踪里摘掉（唯一索引冲突后必须调，否则上下文残留脏状态）</summary>
        void Detach(CheckInRecord entity);
    }
}

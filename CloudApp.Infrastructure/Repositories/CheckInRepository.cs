using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudApp.Infrastructure.Repositories
{
    public class CheckInRepository : BaseRepository<CheckInRecord>, ICheckInRepository
    {
        public CheckInRepository(MyDBContext dbContext, ILogger<BaseRepository<CheckInRecord>> logger)
            : base(dbContext, logger)
        {
        }

        public Task<CheckInRecord?> GetByDateAsync(string openId, DateOnly date, CancellationToken ct = default)
        => _dbSet.AsNoTracking().FirstOrDefaultAsync(x => x.OpenId == openId && x.CheckInDate == date, ct);

        public async Task<List<DateOnly>> GetDatesInRangeAsync(string openId, DateOnly from, DateOnly to, CancellationToken ct = default)
            => await _dbSet.AsNoTracking()
                .Where(x => x.OpenId == openId && x.CheckInDate >= from && x.CheckInDate <= to)
                .Select(x => x.CheckInDate)
                .ToListAsync(ct);

        public Task<int> GetTotalCountAsync(string openId, CancellationToken ct = default)
            => _dbSet.AsNoTracking().CountAsync(x => x.OpenId == openId, ct);

        public void Detach(CheckInRecord entity) => _context.Entry(entity).State = EntityState.Detached;
    }
}

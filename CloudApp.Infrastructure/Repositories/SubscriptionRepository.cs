using CloudApp.Core.Entities;
using CloudApp.Core.Enums;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudApp.Infrastructure.Repositories
{
    public class SubscriptionRepository : BaseRepository<UserSubscription>, ISubscriptionRepository
    {
        public SubscriptionRepository(MyDBContext dbContext, ILogger<BaseRepository<UserSubscription>> logger) 
            : base(dbContext, logger)
        {
        }

        public Task<List<UserSubscription>> GetAllByUserAsync(int userId, CancellationToken ct = default)
            => _dbSet.AsNoTracking()
                    .Where(s => s.UserId == userId)
                    .OrderByDescending(x => x.CreatedAt)
                    .ToListAsync(ct);

        public Task<int> GetSubscriberCountAsync(int targetId, SubscriptionTargetType targetType, CancellationToken ct = default)
            => _dbSet.AsNoTracking()
                    .CountAsync(s => s.TargetId == targetId && s.TargetType == targetType, ct);

        public Task<UserSubscription?> GetSubscriptionAsync(int userId, int targetId, SubscriptionTargetType targetType, CancellationToken ct = default)
            => _dbSet.FirstOrDefaultAsync(x=>x.UserId==userId && x.TargetId == targetId && x.TargetType==targetType,ct);


        public Task<bool> IsSubscribedAsync(int userId, int targetId, SubscriptionTargetType targetType, CancellationToken ct = default) 
            =>_dbSet.AsNoTracking()
                   .AnyAsync(s => s.UserId == userId && s.TargetId == targetId && s.TargetType == targetType, ct);
    }
}
    
using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudApp.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;
        protected readonly ILogger<BaseRepository<T>> _logger;

        public BaseRepository(MyDBContext dbContext, ILogger<BaseRepository<T>> logger)
        {
            _context = dbContext;
            _dbSet = dbContext.Set<T>();
            _logger = logger;
        }

        #region 同步方法
        public virtual IEnumerable<T> GetAll()
        {
            // 全局查询过滤器已经自动过滤IsDeleted，这里不需要手动过滤
            return _dbSet.ToList();
        }

        public virtual T? GetById(int id)
        {
            // 全局查询过滤器已经自动过滤IsDeleted，这里不需要手动过滤
            return _dbSet.FirstOrDefault(e => e.Id == id);
        }

        public virtual void Add(T entity)
        {
            var now = DateTime.UtcNow;
            entity.CreatedAt = now;
            entity.UpdatedAt = now;
            entity.IsDeleted = false;
            _dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            var now = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                entity.CreatedAt = now;
                entity.UpdatedAt = now;
                entity.IsDeleted = false;
            }
            _dbSet.AddRange(entities);
        }

        public virtual bool Exists(int id)
        {
            // 全局查询过滤器已经自动过滤IsDeleted
            return _dbSet.Any(e => e.Id == id);
        }
        

        public virtual void Update(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            var now = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                entity.UpdatedAt = now;
            }
            _dbSet.UpdateRange(entities);
        }

        public virtual void Delete(T entity)
        {
            // 统一使用软删除
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
        }

        public virtual void Delete(int id)
        {
            // 全局查询过滤器已经自动过滤IsDeleted
            var entity = _dbSet.FirstOrDefault(e => e.Id == id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            var now = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeletedAt = now;
                entity.UpdatedAt = now;
            }
            _dbSet.UpdateRange(entities);
        }

        public virtual int Count()
        {
            // 全局查询过滤器已经自动过滤IsDeleted
            return _dbSet.Count();
        }

        public virtual int SaveChange()
        {
            try
            {
                var result = _context.SaveChanges();
                _logger.LogDebug("保存更改成功，影响 {Count} 条记录", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "保存更改失败");
                throw;
            }
        }
        #endregion

        #region 异步方法
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            // 全局查询过滤器已经自动过滤IsDeleted
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(int id)
        {
            // 全局查询过滤器已经自动过滤IsDeleted
            return await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
        }

        public virtual async Task AddAsync(T entity)
        {
            var now = DateTime.UtcNow;
            entity.CreatedAt = now;
            entity.UpdatedAt = now;
            entity.IsDeleted = false;
            await _dbSet.AddAsync(entity);
        }
        
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            var now = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                entity.CreatedAt = now;
                entity.UpdatedAt = now;
                entity.IsDeleted = false;
            }
            await _dbSet.AddRangeAsync(entities);
        }

        public virtual async Task<bool> ExistsAsync(int id)
        {
            // 全局查询过滤器已经自动过滤IsDeleted
            return await _dbSet.AnyAsync(e => e.Id == id);
        }

        public virtual async Task UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
        }
        
        public virtual async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            var now = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                entity.UpdatedAt = now;
            }
            _dbSet.UpdateRange(entities);
        }
        
        public virtual async Task DeleteAsync(int id)
        {
            // 全局查询过滤器已经自动过滤IsDeleted
            var entity = await _dbSet.FirstOrDefaultAsync(e => e.Id == id);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }
        
        public virtual async Task DeleteAsync(T entity)
        {
            // 统一使用软删除
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
        }
        
        public virtual async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            var now = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeletedAt = now;
                entity.UpdatedAt = now;
            }
            _dbSet.UpdateRange(entities);
        }

        public virtual async Task<int> SaveChangeAsync()
        {
            try
            {
                var result = await _context.SaveChangesAsync();
                _logger.LogDebug("异步保存更改成功，影响 {Count} 条记录", result);
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "异步保存更改失败");
                throw;
            }
        }

        public virtual async Task<int> CountAsync()
        {
            // 全局查询过滤器已经自动过滤IsDeleted
            return await _dbSet.CountAsync();
        }
        #endregion
    }
}

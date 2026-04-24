using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace CloudApp.Infrastructure.Repositories
{
    /// <summary>
    /// 基础仓储实现，提供常用的CRUD操作
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(MyDBContext dbContext, ILogger<BaseRepository<T>> logger)
        {
            _context = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        #region 同步方法
        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T? GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
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
            entity.Delete();
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
            _dbSet.UpdateRange(entities);
        }

        public virtual int Count()
        {
            // 全局查询过滤器已经自动过滤IsDeleted
            return _dbSet.Count();
        }

        public virtual int SaveChange()
        { 
            return _context.SaveChanges();
        }

        public virtual IDbContextTransaction BeginTransaction()
        {
            return _context.Database.BeginTransaction();
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
        
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            var now = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                entity.UpdatedAt = now;
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
            entity.Delete();
            entity.UpdatedAt = DateTime.UtcNow;
            _dbSet.Update(entity);
        }
        
        public virtual async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            var now = DateTime.UtcNow;
            foreach (var entity in entities)
            {
                entity.Delete();
                entity.UpdatedAt = now;
            }
            _dbSet.UpdateRange(entities);
        }

        public virtual async Task<int> SaveChangeAsync()
        {
            var result = await _context.SaveChangesAsync();
            return result;
        }

        public virtual async Task<int> CountAsync()
        {
            return await _dbSet.CountAsync();
        }

        public virtual async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }
        #endregion
    }
}

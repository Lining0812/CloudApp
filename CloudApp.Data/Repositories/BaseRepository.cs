using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CloudApp.Data.Repositories
{
    public class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(MyDBContext dbContext)
        {
            _context = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        #region 同步方法
        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
        }

        public virtual bool Exists(int id)
        {
            return _dbSet.Any(e => e.Id == id && !e.IsDeleted);
        }
        

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
        }

        public virtual void Delete(T entity)
        {
            //硬删除
            //_dbSet.Remove(entity);

            //软删除
            entity.IsDeleted = true;
            entity.DeletedAt = DateTime.UtcNow;
        }

        public virtual void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                Delete(entity);
            }
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public virtual int SaveChange()
        {
            return _context.SaveChanges();
        }
        #endregion

        #region 异步方法
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
        
        public virtual async Task AddRangeAsync(IEnumerable<T> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.FindAsync(id) != null;
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }
        
        public virtual async Task UpdateRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }
        
        public virtual async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }
        
        public virtual async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }
        
        public virtual async Task DeleteRangeAsync(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}

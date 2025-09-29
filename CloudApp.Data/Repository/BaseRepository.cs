using CloudApp.Core.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CloudApp.Data.Repository
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(MyDBContext dbContext)
        {
            _context = dbContext;
            _dbSet = dbContext.Set<T>();
        }

        #region 同步查询方法
        public virtual IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        //public virtual IEnumerable<T> GetByCondition(Expression<Func<T, bool>> predicate)
        //{
        //    return _dbSet.Where(predicate).ToList();
        //}

        public virtual T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public bool Exists(int id)
        {
            return _dbSet.Find(id) != null;
        }

        //public bool Exists(Expression<Func<T, bool>> predicate)
        //{
        //    return _dbSet.Any(predicate);
        //}

        public int Count()
        {
            return _dbSet.Count();
        }

        //public int Count(Expression<Func<T, bool>> predicate)
        //{
        //    return _dbSet.Count(predicate);
        //}

        //public virtual IEnumerable<T> GetPaged(int pageNumber, int pageSize)
        //{
        //    return _dbSet
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();
        //}

        //public virtual IEnumerable<T> GetPagedByCondition(Expression<Func<T, bool>> predicate, int pageNumber, int pageSize)
        //{
        //    return _dbSet
        //        .Where(predicate)
        //        .Skip((pageNumber - 1) * pageSize)
        //        .Take(pageSize)
        //        .ToList();
        //}
        #endregion

        #region 同步操作方法
        public virtual void Add(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public virtual void AddRange(IEnumerable<T> entities)
        {
            _dbSet.AddRange(entities);
            _context.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public virtual void UpdateRange(IEnumerable<T> entities)
        {
            _dbSet.UpdateRange(entities);
            _context.SaveChanges();
        }

        public virtual void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public virtual void Delete(T entity)
        {
            _dbSet.Remove(entity);
            _context.SaveChanges();
        }

        public virtual void DeleteRange(IEnumerable<T> entities)
        {
            _dbSet.RemoveRange(entities);
            _context.SaveChanges();
        }
        #endregion
    }
}

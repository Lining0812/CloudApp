using CloudApp.Data;
using CloudApp.Service.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CloudApp.Service.Services
{
    public class BaseRepository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _dbSet;
        public BaseRepository(MyDBContext dbContext)
        {
            _context = dbContext;
            _dbSet = dbContext.Set<T>();
        }
        public void AddEntity(T entity)
        {
            _dbSet.Add(entity);
            _context.SaveChanges();
        }

        public void DeleteEntity(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                _context.SaveChanges();
            }
        }

        public IEnumerable<T> GetAllEntities()
        {
            return _dbSet.ToList();
        }

        public T GetEntityById(int id)
        {
            return _dbSet.Find(id);
        }

        public void UpdateEntity(T entity)
        {
            _dbSet.Update(entity);
            _context.SaveChanges();
        }

        public bool FindEntity(int id)
        {
            return _dbSet.Find(id) != null;
        }
    }
}

using Microsoft.EntityFrameworkCore;

namespace CloudApp.Data.Repository
{
    public interface IRepository<T>
    {
        public IEnumerable<T> GetAllEntities();
        public T GetEntityById(int id);
        public void AddEntity(T entity);
        public void UpdateEntity(T entity);
        public void DeleteEntity(int id);
        public bool FindEntity(int id);
    }
}

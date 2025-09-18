using Microsoft.EntityFrameworkCore;

namespace CloudApp.Service.Interfaces
{
    public interface IRepository<T>
    {
        public IEnumerable<T> GetAll();
        public T GetById(int id);
        public void Add(T entity);
        public void Update(T entity);
        public void Delete(int id);
        public bool Find(int id);
    }
}

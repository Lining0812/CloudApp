using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Service.Interfaces
{
    public interface IService<T>
    {
        void Add(T entity);
        IEnumerable<T> GetAll();
        T GetById(int id);
        void Update(T entity);
        void Delete(int id);
    }
}

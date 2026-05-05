using CloudApp.Core.Entities;

namespace CloudApp.Core.Interfaces.Repositories
{
    public interface IConcertRepository : IRepository<Concert>
    {
        Concert? FindByTitle(string title);
        bool ConcertExists(string title);
    }
}

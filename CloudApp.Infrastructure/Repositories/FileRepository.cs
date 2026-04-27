using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CloudApp.Infrastructure.Repositories
{
    public class FileRepository : BaseRepository<UploadedFile>, IFileRepository
    {
        public FileRepository(MyDBContext dbContext, ILogger<FileRepository> logger)
            : base(dbContext, logger)
        {
        }

        public Task<UploadedFile?> FindFileAsync(long size, string hash)
        {
            return _dbSet.FirstOrDefaultAsync(f =>
                f.FileSizeBytes == size && f.FileSHA256Hash == hash);
        }

        public override Task<UploadedFile?> GetByIdAsync(int id)
        {
            return _dbSet.FirstOrDefaultAsync(f => f.Id == id);
        }

        public Task<IEnumerable<UploadedFile>> SearchByFileNameAsync(string fileName, int skip = 0, int take = 20)
        {
            var query = _dbSet
                .Where(f => EF.Functions.Like(f.FileName, $"%{fileName}%"))
                .OrderByDescending(f => f.CreatedAt)
                .Skip(skip)
                .Take(take)
                .AsNoTracking();

            return Task.FromResult<IEnumerable<UploadedFile>>(query.ToList());
        }

        public Task<IEnumerable<UploadedFile>> GetPagedAsync(int skip = 0, int take = 20)
        {
            var query = _dbSet
                .OrderByDescending(f => f.CreatedAt)
                .Skip(skip)
                .Take(take)
                .AsNoTracking();

            return Task.FromResult<IEnumerable<UploadedFile>>(query.ToList());
        }
    }
}

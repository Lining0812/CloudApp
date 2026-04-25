using CloudApp.Core.Entities;
using CloudApp.Core.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Infrastructure.Repositories
{
    public class FileRepository : IFileRepository
    {
        private readonly MyDBContext _context;
        public FileRepository(MyDBContext context)
        {
            _context = context;
        }

        public Task<UploadedFile?> FindFileAsync(long size, string hash)
        {
            return _context.UploadedFiles.FirstOrDefaultAsync(f => 
            f.FileSizeBytes == size && f.FileSHA256Hash == hash);
        }
    }
}

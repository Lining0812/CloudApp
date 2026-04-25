using CloudApp.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Interfaces.Repositories
{
    public interface IFileRepository
    {
        Task<UploadedFile?> FindFileAsync(long size, string hash);
    }
}

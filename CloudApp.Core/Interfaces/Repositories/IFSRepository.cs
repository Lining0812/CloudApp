using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Core.Interfaces.Repositories
{
    public interface IFileRepository
    {
        Task<string> FindFileAsync(string filename);
    }
}

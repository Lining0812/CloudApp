using CloudApp.Core.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace CloudApp.Application
{
    public class FileService
    {
        private readonly IFileRepository _fileRepository;

        public FileService(IFileRepository fileRepository)
        {
            _fileRepository = fileRepository;
        }
    }
}

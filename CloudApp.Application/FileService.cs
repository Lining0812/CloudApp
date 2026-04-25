using CloudApp.Core.Entities;
using CloudApp.Core.Enums;
using CloudApp.Core.Interfaces.Repositories;
using CloudApp.Core.Interfaces.Services;

namespace CloudApp.Application
{
    public class FileService
    {
        private readonly IFileRepository _fileRepository;
        private readonly IStorageProvider _backupStorage;
        private readonly IStorageProvider _remoteStorage;

        public FileService(IFileRepository fileRepository, List<IStorageProvider> storages)
        {
            _fileRepository = fileRepository;
            _backupStorage = storages.First(s => s.StorageType == StorageType.Bakeup);
            _remoteStorage = storages.First(s => s.StorageType == StorageType.Public);
        }

        public async Task<UploadedFile> Upload(Stream stream, string fileName)
        {
            string hash = "adwwqe3ejugrte8";
            long fileSize = stream.Length;
            DateTime today = DateTime.Today;

            string key = $"{today.Year}/{today.Month}/{today.Day}/{hash}/{fileName}";

            var oldFile = await _fileRepository.FindFileAsync(fileSize,hash);
            if (oldFile != null) return oldFile;

            Uri backupUrl = _backupStorage.SaveFile(key, stream);
            stream.Position = 0;
            Uri remoteUrl = _remoteStorage.SaveFile(key, stream);
            stream.Position = 0;
            return UploadedFile.Create(fileName, fileSize, key, backupUrl, remoteUrl);
        }
    }
}

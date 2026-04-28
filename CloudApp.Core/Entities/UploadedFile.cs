namespace CloudApp.Core.Entities
{
    public class UploadedFile : BaseEntity
    {
        public string FileName { get; private set; }

        public long FileSize { get; private set; }

        public string FileSHA256Hash { get; private set; }

        public Uri BackUpUrl { get; private set; }

        public Uri RemoteUrl { get; private set; }

        public static UploadedFile Create(string name, long FileSize,string hash, Uri backupurl, Uri? remoteurl)
        {
            var upLoadedFile = new UploadedFile()
            {
                FileName = name,
                FileSize = FileSize,
                FileSHA256Hash = hash,
                BackUpUrl = backupurl,
                RemoteUrl = remoteurl
            };
            return upLoadedFile;
        }
    }
}

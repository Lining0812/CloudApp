namespace CloudApp.Core.Confige
{
    /// <summary>
    /// 存储配置选项
    /// </summary>
    public class StorageOptions
    {
        /// <summary>
        /// 最大文件大小（字节），默认 20MB
        /// </summary>
        public long MaxFileSize { get; set; } = 20 * 1024 * 1024;

        /// <summary>
        /// 允许的文件扩展名列表
        /// </summary>
        public string[] AllowedExtensions { get; set; } = [".jpg", ".jpeg", ".png", ".gif", ".mp3", ".mp4", ".pdf"];

        /// <summary>
        /// 本地存储根路径（相对于 WebRootPath 或 ContentRootPath）
        /// </summary>
        public string LocalStorageRoot { get; set; } = "uploads";

        /// <summary>
        /// 是否启用文件去重（基于 SHA256）
        /// </summary>
        public bool EnableDeduplication { get; set; } = true;
    }
}

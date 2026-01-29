using CloudApp.Core.Enums;

namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 媒体资源
    /// </summary>
    public class MediaResource : BaseEntity
    {
        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { get; set; } = string.Empty;
        /// <summary>
        /// 文件路径
        /// </summary>
        public string FilePath { get; set; } = string.Empty;
        /// <summary>
        /// 文件url
        /// </summary>
        public string FileUrl { get; set; } = string.Empty;
        /// <summary>
        /// 文件 MIME 类型
        /// </summary>
        public string ContentType { get; set; } = string.Empty;
        /// <summary>
        /// 资源类型
        /// </summary>
        public MediaType MediaType { get; set; }
        //public bool IsDefault { get; set; }

        /// <summary>
        /// 导航属性->MediaRelation
        /// </summary>
        public ICollection<MediaRelation> MediaRelations { get; set; } = new List<MediaRelation>();
    }
}

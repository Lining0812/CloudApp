using CloudApp.Core.Enums;

namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 实体 <-> 媒体资源
    /// </summary>
    public class MediaRelation : BaseEntity
    {
        /// <summary>
        /// 关联实体ID
        /// </summary>
        public int EntityId { get; set; }
        /// <summary>
        /// 关联资源ID
        /// </summary>
        public int MediaId { get; set; }
        /// <summary>
        /// 资源类型
        /// </summary>
        public MediaType MediaType { get; set; }
        /// <summary>
        /// 导航属性->MediaResource
        /// </summary>
        public MediaResource MediaResource { get; set; }
    }
}

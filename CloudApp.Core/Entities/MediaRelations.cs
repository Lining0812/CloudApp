using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 实体<->媒体资源
    /// </summary>
    public class MediaRelation : BaseEntity
    {
        public int EntityId { get; set; }
        public int MediaId { get; set; }
        /// <summary>
        /// 导航属性->MediaResource
        /// </summary>
        public MediaResource MediaResource { get; set; }
    }
}

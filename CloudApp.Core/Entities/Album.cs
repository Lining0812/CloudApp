using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 专辑实体类
    /// </summary>
    public class Album
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 专辑名称
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 专辑描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 艺术家
        /// </summary>
        public string Artist { get; set; }
        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// 封面图片URL
        /// </summary>
        public string CoverImageUrl { get; set; }

        /// <summary>
        /// 导航属性 - 专辑中的曲目列表
        /// </summary>
        public List<Track> Tracks { get; set; } = new List<Track>();
    }
}

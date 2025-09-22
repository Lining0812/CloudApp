using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 单曲实体类
    /// </summary>
    public class Track
    {
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 单曲标题
        /// </summary>
        public string Title { get; set; } = string.Empty;
        /// <summary>
        /// 副标题
        /// </summary>
        public string Subtitle { get; set; }
        /// <summary>
        /// 单曲描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// 发行日期
        /// </summary>
        public DateTime ReleaseDate { get; set; }
        /// <summary>
        /// 单曲链接URL
        /// </summary>
        public string URL { get; set; }

        // 创作者信息

        public string Artist { get; set; }

        /// <summary>
        /// 作曲者
        /// </summary>
        public string Composer { get; set; }
        /// <summary>
        /// 作词者
        /// </summary>
        public string Lyricist { get; set; }

        /// <summary>
        /// 外键字段 - 所属专辑
        /// </summary>
        public Album? Album { get; set; }
    }
}
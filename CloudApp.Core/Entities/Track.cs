using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CloudApp.Core.Entities
{
    public class Track
    {
        [Key]
        public int Id { get; set; }

        // 核心字段
        /// <summary>
        /// 单曲标题
        /// </summary>
        [Required]
        public string Title { get; set; } = string.Empty;
        public string? Subtitle { get; set; }
        public string? Description { get; set; }

        public Album Album { get; set; }
        // 创作者信息
        /// <summary>
        /// 作曲者
        /// </summary>
        public string? Composer { get; set; }
        /// <summary>
        /// 作词者
        /// </summary>
        public string? Lyricist { get; set; }
    }
}
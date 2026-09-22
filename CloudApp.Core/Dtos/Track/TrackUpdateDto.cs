using CloudApp.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Dtos.Track
{
    /// <summary>
    /// 更新单曲请求（PATCH 语义：为 null 的字段表示“不修改”，空字符串表示“清空”）
    /// </summary>
    public class TrackUpdateDto
    {
        [MaxLength(200, ErrorMessage = "单曲名称不能超过200个字符")]
        public string? Title { get; set; }

        [MaxLength(200, ErrorMessage = "副标题不能超过200个字符")]
        public string? Subtitle { get; set; }

        [MaxLength(1000, ErrorMessage = "描述不能超过1000个字符")]
        public string? Description { get; set; }

        /// <summary>
        /// 时长（为 null 表示不修改）
        /// </summary>
        public TimeSpan? Duration { get; set; }

        /// <summary>
        /// 发行日期（为 null 表示不修改）
        /// </summary>
        public DateTime? ReleaseDate { get; set; }

        [MaxLength(100, ErrorMessage = "原唱不能超过100个字符")]
        public string? Artist { get; set; }

        [MaxLength(100, ErrorMessage = "作曲不能超过100个字符")]
        public string? Composer { get; set; }

        [MaxLength(100, ErrorMessage = "作词不能超过100个字符")]
        public string? Lyricist { get; set; }

        [MaxLength(500, ErrorMessage = "封面地址不能超过500个字符")]
        public string? CoverUrl { get; set; }

        /// <summary>
        /// 跳转链接：null=不修改，""=清空
        /// </summary>
        [MaxLength(500, ErrorMessage = "跳转链接不能超过500个字符")]
        public string? LinkUrl { get; set; }

        /// <summary>
        /// 单曲类型（为 null 表示不修改）
        /// </summary>
        public TrackType? Type { get; set; }

        /// <summary>
        /// 所属专辑（为 null 表示不修改）
        /// </summary>
        public int? AlbumId { get; set; }

        /// <summary>
        /// 是否解绑专辑。AlbumId 为 null 时无法表达“置空”，需要解绑时置为 true
        /// </summary>
        public bool ClearAlbumId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Dtos.Album;

public class CreateAlbumRequest
{
    [Required(ErrorMessage = "专辑名不能为空")]
    [MaxLength(50, ErrorMessage = "专辑名不能超过50个字符")]
    public string Title { get; set; } = string.Empty;

    [MaxLength(500, ErrorMessage = "描述不能超过500个字符")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "艺术家不能为空")]
    [MaxLength(50, ErrorMessage = "艺术家名称不能超过50个字符")]
    public string Artist { get; set; } = string.Empty;

    [Required(ErrorMessage = "发行日期不能为空")]
    public DateTime ReleaseDate { get; set; }

    //public IFileContent? CoverImage { get; set; }

    public CreateAlbumRequest()
    {
    }

    public CreateAlbumRequest(string title, string? description, string artist, DateTime releaseDate)
    {
        Title = title;
        Description = description;
        Artist = artist;
        ReleaseDate = releaseDate;
    }
}

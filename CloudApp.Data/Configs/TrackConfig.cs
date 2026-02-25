using CloudApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudApp.Data.Configs
{
    /// <summary>
    /// 单曲配置类
    /// </summary>
    public class TrackConfig : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.ToTable("T_Tracks");

            // 主键配置
            builder.HasKey(t=>t.Id);
            // 单曲名称配置
            builder.Property(t => t.Title).IsRequired().HasMaxLength(200);
            // 副标题配置
            builder.Property(t => t.Subtitle).IsRequired(false).HasMaxLength(200);
            // 描述配置
            builder.Property(t => t.Description).IsRequired(false).HasMaxLength(1000);
            // 时长配置
            builder.Property(t => t.Duration).IsRequired().HasConversion(
                v => (int)v.TotalSeconds,
                v => TimeSpan.FromSeconds(v)
            );
            // 原唱配置
            builder.Property(t => t.Artist).IsRequired().HasMaxLength(100);
            // 作曲人配置
            builder.Property(t => t.Composer).IsRequired().HasMaxLength(100);
            // 作词人配置
            builder.Property(t => t.Lyricist).IsRequired().HasMaxLength(100);
            // 发行日期配置
            builder.Property(t => t.ReleaseDate).IsRequired().HasConversion(
                v => v,
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

            // Title字段创建索引
            builder.HasIndex(t => t.Title);

            // Track与Album的一对多关系配置
            builder.HasOne(t => t.Album).WithMany(a => a.Tracks).HasForeignKey(t => t.AlbumId).OnDelete(DeleteBehavior.SetNull);
            // Track与Concert的一对多关系配置
            builder.HasOne(t => t.Concert).WithMany(c => c.Tracks).HasForeignKey(t => t.ConcertId).OnDelete(DeleteBehavior.SetNull);

            // 资源关系配置
            builder.HasMany(t=>t.MediaRelations)
                   .WithOne()
                   .HasPrincipalKey(t => t.Id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

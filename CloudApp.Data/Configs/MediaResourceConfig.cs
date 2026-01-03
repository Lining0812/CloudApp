using CloudApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudApp.Data.Configs
{
    /// <summary>
    /// 媒体资源配置类
    /// </summary>
    public class MediaResourceConfig : IEntityTypeConfiguration<MediaResource>
    {
        public void Configure(EntityTypeBuilder<MediaResource> builder)
        {
            builder.ToTable("T_MediaResources");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.FileName).IsRequired().HasMaxLength(100);
            builder.Property(m => m.FilePath).IsRequired().HasMaxLength(200);
            builder.Property(m => m.FileUrl).IsRequired().HasMaxLength(200);
            builder.Property(m => m.ContentTpye).IsRequired().HasMaxLength(50);
            // 导航属性配置
            builder.HasMany(m=>m.MediaRelations).WithOne(m=>m.MediaResource).OnDelete(DeleteBehavior.Cascade);
        }
    }
}

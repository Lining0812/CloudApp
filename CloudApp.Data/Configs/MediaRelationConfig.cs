using CloudApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudApp.Data.Configs
{
    /// <summary>
    /// 实体 <-> 媒体资源配置类
    /// </summary>
    public class MediaRelationConfig : IEntityTypeConfiguration<MediaRelation>
    {
        public void Configure(EntityTypeBuilder<MediaRelation> builder)
        {
            builder.ToTable("T_MediaRelations");

            builder.HasKey(m => m.Id);

            builder.Property(m => m.EntityId).IsRequired();
            builder.Property(m => m.MediaId).IsRequired();
            builder.Property(m => m.MediaType).IsRequired();
            //builder.Property(m => m.OrderIndex).IsRequired().HasDefaultValue(0);
            builder.Property(m => m.IsDefault).IsRequired().HasDefaultValue(false);

            // 添加复合索引以提高查询性能
            //builder.HasIndex(m => new { m.EntityId, m.EntityType });

            // 配置与MediaResource的关系
            builder.HasOne(m => m.MediaResource)
                   .WithMany(mr => mr.MediaRelations)
                   .HasForeignKey(m => m.MediaId)
                   .OnDelete(DeleteBehavior.Restrict); 
        }
    }
}

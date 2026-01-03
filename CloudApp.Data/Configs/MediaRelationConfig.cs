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
        }
    }
}

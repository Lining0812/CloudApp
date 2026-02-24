using CloudApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudApp.Data.Configs
{
    /// <summary>
    /// 演唱会配置类
    /// </summary>
    public class ConcertConfig : IEntityTypeConfiguration<Concert>
    {
        public void Configure(EntityTypeBuilder<Concert> builder)
        {
            builder.ToTable("T_Concerts");
            // 主键配置
            builder.HasKey(c => c.Id);
            // 演出标题配置
            builder.Property(c => c.Title).IsRequired().HasMaxLength(200);
            // 描述配置
            builder.Property(c => c.Description).IsRequired(false).HasMaxLength(1000);
            // 开始时间配置
            builder.Property(c => c.StartAt).IsRequired().HasConversion(
                v => v,
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );
            // 结束时间配置
            builder.Property(c => c.EndAt).IsRequired().HasConversion(
                v => v,
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );
            // 地点配置
            builder.Property(c => c.Address).IsRequired().HasMaxLength(300);

            // 资源关系配置
            builder.HasMany(c => c.MediaRelations)
                .WithOne()
                .HasForeignKey(m => m.Id)
                .HasPrincipalKey(c => c.Id)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

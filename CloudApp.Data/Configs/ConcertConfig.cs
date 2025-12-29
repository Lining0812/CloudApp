using CloudApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudApp.Data.Configs
{
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
            builder.Property(c => c.Location).IsRequired().HasMaxLength(300);
        }
    }
}

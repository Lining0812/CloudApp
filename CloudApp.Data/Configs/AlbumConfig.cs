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
    public class AlbumConfig : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("T_Albums");

            // 主键配置
            builder.HasKey(x => x.Id);
            // 标题配置
            builder.Property(b=>b.Title)
                .IsRequired()
                .HasMaxLength(200);
            // 描述配置
            builder.Property(b => b.Description)
                .HasMaxLength(1000);
            // 艺术家配置
            builder.Property(b => b.Artist)
                .IsRequired()
                .HasMaxLength(50);
            // 发行日期配置
            builder.Property(b=>b.ReleaseDate)
                .IsRequired();
            // 封面图片URL配置
            builder.Property(b => b.CoverImageUrl)
                .HasMaxLength(500);
            // 一对多导航属性配置在TrackConfig中
        }
    }
}

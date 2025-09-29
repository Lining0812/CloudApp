﻿using CloudApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudApp.Data.Configs
{
    public class AlbumConfig : IEntityTypeConfiguration<Album>
    {
        public void Configure(EntityTypeBuilder<Album> builder)
        {
            builder.ToTable("T_Albums");

            // 主键配置
            builder.HasKey(a => a.Id);
            // 标题配置
            builder.Property(a => a.Title).IsRequired().HasMaxLength(50);
            // 描述配置
            builder.Property(a => a.Description).IsRequired(false).HasMaxLength(1000);
            // 艺术家配置
            builder.Property(a => a.Artist).IsRequired().HasMaxLength(50);
            // 发行日期配置
            builder.Property(a => a.ReleaseDate).IsRequired().HasConversion(
                v => v,
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
                );
            // 封面图片URL配置
            builder.Property(b => b.CoverImageUrl).IsRequired(false).HasMaxLength(500);
            // 一对多导航属性配置在TrackConfig中

            builder.HasIndex(a => a.Title);
        }
    }
}

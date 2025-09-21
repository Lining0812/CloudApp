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
    public class TrackConfig : IEntityTypeConfiguration<Track>
    {
        public void Configure(EntityTypeBuilder<Track> builder)
        {
            builder.ToTable("T_Tracks");

            builder.HasKey(t=>t.Id);
            builder.Property(t => t.Title).IsRequired().HasMaxLength(200);
            builder.Property(t => t.Subtitle).HasMaxLength(200);
            builder.Property(t => t.Description).HasMaxLength(1000);
            builder.Property(t => t.URL).HasMaxLength(500);
            builder.Property(t => t.Artist).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Composer).IsRequired().HasMaxLength(100);
            builder.Property(t => t.Lyricist).IsRequired().HasMaxLength(100);
            builder.Property(t => t.ReleaseDate).HasConversion(
                v => v, // 保存到数据库时的转换
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // 从数据库读取时的转换
            );

            builder.HasOne(t => t.Album).WithMany(a => a.Tracks);
        }
    }
}

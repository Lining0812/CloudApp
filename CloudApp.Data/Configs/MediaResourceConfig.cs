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
    public class MediaResourceConfig : IEntityTypeConfiguration<MediaResource>
    {
        public void Configure(EntityTypeBuilder<MediaResource> builder)
        {
            builder.ToTable("T_MediaResource");

            builder.HasKey(m => m.Id);
            builder.Property(m => m.FileName).IsRequired().HasMaxLength(100);
            builder.Property(m => m.FilePath).IsRequired().HasMaxLength(200);
            builder.Property(m => m.FileUrl).IsRequired().HasMaxLength(200);
            builder.Property(m => m.ContentTpye).IsRequired().HasMaxLength(50);
        }
    }
}

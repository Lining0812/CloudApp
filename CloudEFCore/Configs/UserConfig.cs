using CloudEFCore.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudEFCore.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");
            builder.HasKey(u => u.ID);
            builder.Property(u => u.Name).IsRequired().HasMaxLength(100);
            builder.Property(u => u.PassWord).IsRequired().HasMaxLength(100);
        }
    }
}

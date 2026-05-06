using CloudApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudApp.Infrastructure.Configs
{
    internal class ScheduleConfig : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {
            builder.ToTable("T_Schedules");

            builder.HasKey(s => s.Id);
            builder.Property(s => s.Title).IsRequired().HasMaxLength(200);
            builder.Property(s => s.Description).HasMaxLength(1000);
            builder.Property(s => s.Artist).IsRequired().HasMaxLength(100);
            builder.Property(s => s.Location).HasMaxLength(200);
            builder.Property(s => s.StartTime).IsRequired();
            builder.Property(s => s.EndTime).IsRequired();
            builder.Property(s => s.Type).IsRequired();
            builder.Property(s => s.Status).IsRequired();
            builder.Property(s => s.IsPublic).IsRequired();
        }
    }
}
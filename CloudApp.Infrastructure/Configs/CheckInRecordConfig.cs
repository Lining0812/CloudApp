using CloudApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudApp.Infrastructure.Configs
{
    public class CheckInRecordConfig : IEntityTypeConfiguration<CheckInRecord>
    {
        public void Configure(EntityTypeBuilder<CheckInRecord> builder)
        {
            builder.ToTable("T_CheckInRecords");
            builder.HasKey(c => c.Id);
            builder.Property(c => c.OpenId)
                .IsRequired()
                .HasMaxLength(100);
            builder.Property(c => c.CheckInDate)
                .IsRequired();

            builder.HasIndex(x => new { x.OpenId, x.CheckInDate }).IsUnique();
        }
    }
}

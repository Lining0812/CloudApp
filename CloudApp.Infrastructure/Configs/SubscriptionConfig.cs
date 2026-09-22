using CloudApp.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CloudApp.Infrastructure.Configs
{
    public class SubscriptionConfig : IEntityTypeConfiguration<UserSubscription>
    {
        public void Configure(EntityTypeBuilder<UserSubscription> builder)
        {
            builder.ToTable("T_UserSubscriptions");

            builder.HasKey(s=>s.Id);
            builder.Property(s => s.UserId).IsRequired();
            builder.Property(s => s.TargetId).IsRequired();
            builder.Property(s => s.TargetType).IsRequired();

            // 防止同一个用户对同一个目标重复订阅，设置联合唯一索引
            builder.HasIndex(s => new { s.UserId, s.TargetId, s.TargetType }).IsUnique();
        }
    }
}

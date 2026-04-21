using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Entities
{
    public abstract class BaseEntity
    {
        /// <summary>
        /// 主键
        /// </summary>
        [Key]
        public int Id { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; } = DateTime.UtcNow;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; private set; } = false;
        public DateTime? DeletedAt { get; private set; }

        /// <summary>
        /// 删除实体（软删除），并记录删除时间
        /// </summary>
        public void Delete()
        {
            if (IsDeleted) return;

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
        }
    }
}
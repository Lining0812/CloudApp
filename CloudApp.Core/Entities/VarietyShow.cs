using System;
using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 综艺实体类
    /// </summary>
    public class VarietyShow : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? FirstAirDate { get; set; }
        public string CoverImageUrl { get; set; }
    }
}

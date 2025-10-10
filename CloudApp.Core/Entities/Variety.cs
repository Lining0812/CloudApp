using System;
using System.ComponentModel.DataAnnotations;

namespace CloudApp.Core.Entities
{
    /// <summary>
    /// 综艺实体类
    /// </summary>
    public class Variety
    {
        [Key]
        public int Id { get; set; }
    }
}

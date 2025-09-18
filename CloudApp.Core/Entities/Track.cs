using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CloudApp.Core.Entities
{
    public class Track
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
    }
}

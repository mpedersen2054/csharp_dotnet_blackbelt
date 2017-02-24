using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace bbelt.Models
{
    public class ActivityValid : BaseEntity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        [MinLength(2)]
        public int Title { get; set; }

        // must be in the future
        [Required]
        public DateTime DateAt { get; set; }

        [Required]
        public int Duration { get; set; }
        public string DurationInc { get; set; }

        [Required]
        [MinLength(10)]
        public int Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }
    }
}

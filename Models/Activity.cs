using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace bbelt.Models
{
    public class Activity : BaseEntity
    {
        public int ActivityId { get; set; }

        public int Title { get; set; }

        public int Duration { get; set; }
        public string DurationInc { get; set; }

        public DateTime DateAt { get; set; }

        public int Description { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public List<UserActivity> Participants { get; set; }
        public Activity()
        {
            Participants = new List<UserActivity>();
        }
    }
}

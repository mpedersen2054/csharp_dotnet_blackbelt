using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace bbelt.Models
{
    public class UserActivity : BaseEntity
    {
        public int UserActivityId { get; set; }

        public int ParticipantId { get; set; }
        public User Participant { get; set; }

        public int ActivityId { get; set; }
        public Activity Activity { get; set; }
    }
}

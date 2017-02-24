using System;
using System.ComponentModel.DataAnnotations;

namespace bbelt.Models
{
    public class UserLogin : BaseEntity
    {
        [Required]
        [EmailAddress]
        [RegularExpression(@"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$")]
        public string Email { get; set; }

        [Required]
        [MinLength(8)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
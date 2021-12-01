using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VaporStore.Common;

namespace VaporStore.Data.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(GlobalConstants.USERNAME_MAXLENGTH)]
        public string Username { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [MaxLength(GlobalConstants.USER_MAX_AGE_RANGE)]
        public int Age { get; set; }

        public virtual ICollection<Card> Cards { get; set; }
    }
}

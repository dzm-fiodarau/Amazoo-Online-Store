using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace AmazooApp.Models
{
    public class UserProfile :IdentityUser
    {
        [Key]
        public int Id { get; set; }
        public string Password { get; set; }
        public string email { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public virtual User User { get; set; }


    }
}

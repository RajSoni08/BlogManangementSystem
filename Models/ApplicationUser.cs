//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class ApplicationUser : IdentityUser
    {
        
        [Required]
        public string? Name { get; set; }
        public string? Email { get; set; }
       // public string? Password { get; set; }

        public string? Role { get; set; }
    }
}

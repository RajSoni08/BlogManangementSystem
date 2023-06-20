using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNet.Identity.EntityFramework;

namespace Models
{
    public class UserDTO 

    {
        
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
        public string UserName { get; set; }
   //     public string Email { get; set; }
        public string Password { get; set; }

        public string Role { get; set; }
    }
}

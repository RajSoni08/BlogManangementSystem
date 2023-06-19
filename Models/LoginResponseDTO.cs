using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class LoginResponseDTO
    {
        public UserDTO User { get; set; }
       // public string Role { get; set; }
        public string Token { get; set; }
    }
}

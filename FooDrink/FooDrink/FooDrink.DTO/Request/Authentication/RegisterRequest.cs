using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FooDrink.DTO.Request.Authentication
{
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Fullname {  get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Address {  get; set; } = string.Empty;
    }
}

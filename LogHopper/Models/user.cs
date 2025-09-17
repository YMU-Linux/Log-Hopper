using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogHopper
{

    public enum Role
    {
        LE,
        Admin
    }

    public class User
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.LE;
        public string? Login_token { get; set; } = null;
    }
}

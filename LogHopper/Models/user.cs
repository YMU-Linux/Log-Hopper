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
        public Role Role { get; set; } = Role.LE;
        public string? Login_token { get; set; } = null;
    }
}

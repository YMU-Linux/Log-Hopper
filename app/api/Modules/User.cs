using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.Modules
{
    public class User
    {
        public int Id { get; set; }
        public string userName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        [Range(0, 999)]
        public int rolePower { get; set; }
    }
}
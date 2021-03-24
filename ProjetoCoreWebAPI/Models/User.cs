using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoCoreWebAPI.Models
{
    public class User : IdentityUser<int>
    {
        public string Name { get; set; }
        
        public List<UserRole> UserRoles { get; set; }

        public string Image { get; set; }

        public bool Ativo { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public DateTime? DeletedAt { get; set; }
    }
}

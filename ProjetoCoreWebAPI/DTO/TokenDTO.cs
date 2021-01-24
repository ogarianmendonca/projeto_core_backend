using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoCoreWebAPI.DTO
{
    public class TokenDTO
    {
        public string Token { get; set; }

        public DateTime? Expiration { get; set; }
    }
}

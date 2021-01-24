using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoCoreWebAPI.DTO
{
    public class RoleDTO
    {
        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        public string Name { get; set; }
    }
}

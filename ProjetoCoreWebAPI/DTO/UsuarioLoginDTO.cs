using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoCoreWebAPI.DTO
{
    public class UsuarioLoginDTO
    {
        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        public string Password { get; set; }
    }
}

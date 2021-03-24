using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoCoreWebAPI.DTO
{
    public class UsuarioCreateDTO
    {
        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Campo {0} obrigatório!")]
        [Compare("Password", ErrorMessage = "Senha e confirmação de senha são diferentes!")]
        public string ConfirmPassword { get; set; }

        public string Image { get; set; }

        public List<RoleDTO> Roles { get; set; }
    }
}

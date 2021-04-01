using ProjetoCoreWebAPI.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using X.PagedList;

namespace ProjetoCoreWebAPI.Interfaces
{
    public interface IUsuario
    {
        Task<PagedListDTO<UsuarioDTO>> GetAll(int? page);

        Task<UsuarioDTO> Get(int id);

        Task<UsuarioDTO> Post(UsuarioCreateDTO usuarioDTO);

        Task<UsuarioDTO> Put(int id, UsuarioDTO usuarioDTO);

        Task<UsuarioDTO> Delete(int id);

        Task<bool> AtualizaPassword(int id, PasswordDTO passwordDTO);
    }
}

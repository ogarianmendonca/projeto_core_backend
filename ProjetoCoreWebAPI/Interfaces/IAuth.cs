using ProjetoCoreWebAPI.DTO;
using System.Threading.Tasks;

namespace ProjetoCoreWebAPI.Interfaces
{
    public interface IAuth
    {
        Task<TokenDTO> GenerateToken(UsuarioLoginDTO userLoginDTO);
    }
}

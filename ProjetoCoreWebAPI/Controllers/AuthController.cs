using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProjetoCoreWebAPI.DTO;
using ProjetoCoreWebAPI.Interfaces;

namespace ProjetoCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuth _authRepository;

        public AuthController(IAuth authRepository)
        {
            _authRepository = authRepository;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult> Login(UsuarioLoginDTO userLogin)
        {
            try
            {
                var token = await _authRepository.GenerateToken(userLogin);

                if (token.Token.Length > 0)
                {
                    return Ok(token);
                }

                return Unauthorized();
            }
            catch (System.Exception ex)
            {
                return this.StatusCode(401, $"{ex.Message}");
            }
        }
    }
}

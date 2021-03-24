using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProjetoCoreWebAPI.DTO;
using ProjetoCoreWebAPI.Interfaces;
using ProjetoCoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoCoreWebAPI.Repositories
{
    public class AuthRepository : IAuth
    {
        private readonly IConfiguration _config;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _accessor;

        public AuthRepository(IConfiguration config,
                              UserManager<User> userManager,
                              SignInManager<User> signInManager,
                              IMapper mapper,
                              IHttpContextAccessor accessor
        ) {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _accessor = accessor;
        }

        public async Task<TokenDTO> GenerateToken(UsuarioLoginDTO userLoginDTO)
        {
            var user = await _userManager.FindByNameAsync(userLoginDTO.UserName);

            if (user == null)
            {
                throw new Exception("Nome de usuário ou senha inválidos!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, userLoginDTO.Password, false);

            if (!result.Succeeded)
            {
                throw new Exception("Nome de usuário ou senha inválidos!");
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email)
            };

            var roles = await _userManager.GetRolesAsync(user);
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(
                _config.GetSection("AppSettings:Token").Value
            );

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            var tokenDTO = new TokenDTO
            {
                Token = tokenHandler.WriteToken(token),
                Expiration = tokenDescriptor.Expires
            };

            return tokenDTO;
        }

        public async Task<UsuarioDTO> UsuarioLogado()
        {
            var userName = _accessor.HttpContext.User.Identity.Name;
            var usuario = await _userManager.FindByNameAsync(userName);

            List<RoleDTO> rolesDTO = new List<RoleDTO>();
            var rolesNames = await _userManager.GetRolesAsync(usuario);
            foreach (var roleName in rolesNames)
            {
                Role role = new Role();
                role.Name = roleName;
                rolesDTO.Add(_mapper.Map<RoleDTO>(role));
            }

            var usuarioLogado = _mapper.Map<UsuarioDTO>(usuario);
            usuarioLogado.Roles = rolesDTO;

            return _mapper.Map<UsuarioDTO>(usuarioLogado);
        }   
    }
}

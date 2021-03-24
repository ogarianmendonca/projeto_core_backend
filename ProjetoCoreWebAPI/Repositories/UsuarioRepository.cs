using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ProjetoCoreWebAPI.DTO;
using ProjetoCoreWebAPI.Interfaces;
using ProjetoCoreWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoCoreWebAPI.Repositories
{
    public class UsuarioRepository : IUsuario
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public UsuarioRepository(
            UserManager<User> userManager,
            IMapper mapper
        ) {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<List<UsuarioDTO>> GetAll()
        {
            var users = _userManager.Users.ToList();
            if (users == null)
            {
                return null;
            }

            List<UsuarioDTO> usuariosDTO = new List<UsuarioDTO>();

            foreach (User user in users)
            {
                var usuarioDTO = _mapper.Map<UsuarioDTO>(user);

                usuarioDTO = IncluiRoles(user);

                usuariosDTO.Add(usuarioDTO);
            }

            return await Task.Run(() => usuariosDTO);
        }

        public async Task<UsuarioDTO> Get(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return null;
            }

            UsuarioDTO usuarioDTO = IncluiRoles(user);

            return await Task.Run(() => usuarioDTO); ;
        }
        
        public async Task<UsuarioDTO> Post(UsuarioCreateDTO usuarioDTO)
        {
            try
            {
                var user = _mapper.Map<User>(usuarioDTO);
                user.CreatedAt = DateTime.Now;
                user.Ativo = true;
                
                var result = await _userManager.CreateAsync(user, usuarioDTO.Password);
                if (!result.Succeeded)
                {
                    throw new Exception("Erro ao cadastrar usuário!");
                }
                
                if (usuarioDTO.Roles.Count > 0)
                {
                    IEnumerable<string> items;
                    foreach (RoleDTO role in usuarioDTO.Roles)
                    {
                        items = new string[] { new string(role.Name) };
                        await _userManager.AddToRolesAsync(user, items);
                    }
                }

                List<RoleDTO> rolesDTO = new List<RoleDTO>();
                foreach (UserRole userRole in user.UserRoles)
                {
                    rolesDTO.Add(_mapper.Map<RoleDTO>(userRole.Role));
                }

                var usuarioCadastrado = _mapper.Map<UsuarioDTO>(user);
                usuarioCadastrado.Roles = rolesDTO;

                return usuarioCadastrado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao cadastrar usuário! {ex.Message}");
            }
        }

        public async Task<UsuarioDTO> Put(int id, UsuarioDTO usuarioDTO)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());

                user.Name = usuarioDTO.Name;
                user.Email = usuarioDTO.Email;
                user.UserName = usuarioDTO.UserName;
                user.UpdatedAt = DateTime.Now;
                user.Image = usuarioDTO.Image;

                var alteraRoles = false;
                if (usuarioDTO.Roles != null)
                {
                    var roles = _userManager.GetRolesAsync(user);
                    foreach (string role in roles.Result)
                    {
                        await _userManager.RemoveFromRoleAsync(user, role);
                    }

                    IEnumerable<string> nomeRole;
                    foreach (RoleDTO role in usuarioDTO.Roles)
                    {
                        nomeRole = new string[] { new string(role.Name) };
                        await _userManager.AddToRolesAsync(user, nomeRole);
                    }

                    alteraRoles = true;
                }

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("Erro ao editar usuário!");
                }

                var usuarioEditado = _mapper.Map<UsuarioDTO>(user);

                if (alteraRoles)
                {
                    List<RoleDTO> rolesDTO = new List<RoleDTO>();
                    foreach (UserRole userRole in user.UserRoles)
                    {
                        rolesDTO.Add(_mapper.Map<RoleDTO>(userRole.Role));
                    }

                    usuarioEditado.Roles = rolesDTO;
                }

                return usuarioEditado;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao editar usuário! {ex.Message}");
            }
        }

        public async Task<UsuarioDTO> Delete(int id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());
                user.DeletedAt = DateTime.Now;
                user.Ativo = false;

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    throw new Exception("Erro ao excluir usuário!");
                }

                var usuarioExcluidoDTO = _mapper.Map<UsuarioDTO>(user);
                return usuarioExcluidoDTO;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao excluir usuário! {ex.Message}");
            }
        }

        public async Task<bool> AtualizaPassword(int id, PasswordDTO passwordDTO)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id.ToString());

                await _userManager.RemovePasswordAsync(user);
                await _userManager.AddPasswordAsync(user, passwordDTO.Password);

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar a senha! {ex.Message}");
            }
        }

        private UsuarioDTO IncluiRoles(User user)
        {
            var usuarioDTO = _mapper.Map<UsuarioDTO>(user);

            List<RoleDTO> rolesDTO = new List<RoleDTO>();
            var roles = _userManager.GetRolesAsync(user);

            foreach (string role in roles.Result)
            {
                var roleDTO = new RoleDTO();
                roleDTO.Name = role.ToString();

                rolesDTO.Add(roleDTO);
            }

            usuarioDTO.Roles = rolesDTO;

            return usuarioDTO;
        }
    }
}

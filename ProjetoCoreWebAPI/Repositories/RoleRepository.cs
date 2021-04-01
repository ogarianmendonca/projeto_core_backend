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
    public class RoleRepository : IRole
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleRepository(RoleManager<Role> roleManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<List<RoleDTO>> GetAll()
        {
            var roles = _roleManager.Roles.ToList();
            if (roles == null)
            {
                return null;
            }

            List<RoleDTO> rolesDTO = _mapper.Map<List<RoleDTO>>(roles);

            return await Task.Run(() => rolesDTO);
        }
    }
}

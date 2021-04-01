using Microsoft.AspNetCore.Mvc;
using ProjetoCoreWebAPI.DTO;
using ProjetoCoreWebAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRole _roleRepository;

        public RoleController(IRole roleRepository)
        {
            _roleRepository = roleRepository;
        }

        // GET: api/<RoleController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            List<RoleDTO> rolesDTO = await _roleRepository.GetAll();

            if (rolesDTO == null)
            {
                return NotFound("Usuários não encontrados!");
            }

            return Ok(rolesDTO);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoCoreWebAPI.DTO;
using ProjetoCoreWebAPI.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ProjetoCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuario _usuarioRepository;

        public UsuarioController(IUsuario usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<ActionResult> Get()
        {
            List<UsuarioDTO> usuariosDTO = await _usuarioRepository.GetAll();

            if(usuariosDTO == null)
            {
                return NotFound("Usuários não encontrados!");
            }

            return Ok (usuariosDTO);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            var usuarioDTO = await _usuarioRepository.Get(id);
             
            if (usuarioDTO == null)
            {
                return NotFound("Usuário não encontrado!");
            }

            return Ok(usuarioDTO);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<ActionResult> Post(UsuarioCreateDTO usuarioCreateDTO)
        {
            try
            {
                var usuarioCadastrado = await _usuarioRepository.Post(usuarioCreateDTO);
                return Ok(usuarioCadastrado);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"{ex.Message}");
            }
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, UsuarioDTO usuarioDTO)
        {
            try
            {
                var usuarioEditadoDTO = await _usuarioRepository.Put(id, usuarioDTO);
                return Ok(usuarioEditadoDTO);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"{ex.Message}");
            }
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var usuarioExcluidoDTO = await _usuarioRepository.Delete(id);
                return Ok(usuarioExcluidoDTO);
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"{ex.Message}");
            }
        }

        // ATUALIZAR PASSWORD api/<UsuarioController>/5
        [HttpPatch("{id}")]
        public async Task<ActionResult> AtualizarPassword(int id, PasswordDTO passwordDTO)
        {
            try
            {
                if (!await _usuarioRepository.AtualizaPassword(id, passwordDTO))
                {
                    throw new Exception($"Erro ao atualizar a senha!");
                }

                return Ok("Senha atualizada com sucesso!");
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"{ex.Message}");
            }
        }
    }
}

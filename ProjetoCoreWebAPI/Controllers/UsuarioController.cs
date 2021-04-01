using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProjetoCoreWebAPI.DTO;
using ProjetoCoreWebAPI.Interfaces;
using X.PagedList;

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
        public async Task<ActionResult> Get(int? page)
        {
            PagedListDTO<UsuarioDTO> results = await _usuarioRepository.GetAll(page);

            if(results == null)
            {
                return NotFound("Usuários não encontrados!");
            }

            return Ok (results);
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
        [HttpPut("atualizar-senha/{id}")]
        public async Task<ActionResult> AtualizarPassword(int id, PasswordDTO passwordDTO)
        {
            try
            {
                if (!await _usuarioRepository.AtualizaPassword(id, passwordDTO))
                {
                    throw new Exception($"Erro ao atualizar a senha!");
                }

                return Ok(new { message = "Senha alterada com sucesso!" });
            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"{ex.Message}");
            }
        }

        [HttpPost("convert-file")]
        public ActionResult ConvertToBase64()
        {
            try
            {
                var file = Request.Form.Files[0];
                var folderName = Path.Combine("Resources", "Uploads/Perfil");
                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var filename = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
                    var fullPath = Path.Combine(pathToSave, filename.Replace("\"", " ").Trim());

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    Byte[] bytes = System.IO.File.ReadAllBytes(fullPath);
                    String fileBase64 = Convert.ToBase64String(bytes);

                    var arrayFileName = file.FileName.Split(".");
                    var imagemBase64 = "data:image/" + arrayFileName[1] + ";base64," + fileBase64;

                    System.IO.File.Delete(fullPath);

                    return Ok(new { fileBase64 = imagemBase64 });
                }

                return this.StatusCode(500, $"Ocorreu um erro ao efetuar conversão de arquivo para base64!");

            }
            catch (Exception ex)
            {
                return this.StatusCode(500, $"{ex.Message}");
            }
        }
    }
}

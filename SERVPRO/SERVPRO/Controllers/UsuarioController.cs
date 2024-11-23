using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;
using System.Globalization;

namespace SERVPRO.Controllers
{
    [Authorize(Policy = "AdministradorPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        public UsuariosController(IUsuarioRepositorio usuarioRepositorio)
        {
            _usuarioRepositorio = usuarioRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Usuario>>> BuscarTodosUsuarios()
        {
            List<Usuario> usuarios = await _usuarioRepositorio.BuscarTodosUsuarios();
            return Ok(usuarios);
        }


        [HttpGet("{TipoUsuario}")]
        public async Task<ActionResult<List<Usuario>>> BuscarPorTipoUsuario(string TipoUsuario)
        {
            List<Usuario> usuarios = await _usuarioRepositorio.BuscarPorTipoUsuario(TipoUsuario);

            if (usuarios == null || usuarios.Count == 0)
            {
                return NotFound($"Nenhum usuário encontrado com o tipo: {TipoUsuario}");
            }

            return Ok(usuarios);
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<Usuario>> BuscarPorCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                return BadRequest("O CPF não pode ser vazio.");
            }

            // Limpar o CPF, removendo caracteres não numéricos
            cpf = cpf.Replace(".", "").Replace("-", "");

            // Verificar se o CPF tem o formato correto
            if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            {
                return BadRequest("O CPF informado é inválido.");
            }

            // Buscar o usuário pelo CPF
            Usuario usuario = await _usuarioRepositorio.BuscarPorCpf(cpf);

            if (usuario == null)
            {
                return NotFound($"Nenhum usuário encontrado com o CPF: {cpf}");
            }

            return Ok(usuario);
        }

    }
}
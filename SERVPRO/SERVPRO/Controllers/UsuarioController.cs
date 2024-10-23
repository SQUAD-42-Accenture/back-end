using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;
using System.Globalization;

namespace SERVPRO.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController: ControllerBase
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



    }
}

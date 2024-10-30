using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;

namespace SERVPRO.Controllers
{
    [Authorize(Policy = "AdministradorPolicy")]
    [Route("api/[controller]")]
    [ApiController]
    public class AdministradorController : ControllerBase
    {
        private readonly IAdministradorRepositorio _administradorRepositorio;

        public AdministradorController(IAdministradorRepositorio administradorRepositorio)
        {
            _administradorRepositorio = administradorRepositorio;
        }


        [HttpGet]
        public async Task<ActionResult<List<Administrador>>> BuscarTodosAdministradores()
        {
            var administradores = await _administradorRepositorio.BuscarTodosAdministradores();
            return Ok(administradores);
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<Administrador>> BuscarPorCPF(string cpf)
        {
            var administrador = await _administradorRepositorio.BuscarPorCPF(cpf);
            if (administrador == null)
            {
                return NotFound($"Administrador com CPF {cpf} não encontrado.");
            }
            return Ok(administrador);
        }

        [HttpPost]
        public async Task<ActionResult<Administrador>> Cadastrar([FromBody] Administrador adminModel)
        {
            var administrador = await _administradorRepositorio.Adicionar(adminModel);
            return CreatedAtAction(nameof(BuscarPorCPF), new { cpf = administrador.CPF }, administrador);
        }

        [HttpPut("{cpf}")]
        public async Task<ActionResult<Administrador>> Atualizar([FromBody] Administrador adminModel, string cpf)
        {
            adminModel.CPF = cpf;
            var administrador = await _administradorRepositorio.Atualizar(adminModel, cpf);
            return Ok(administrador);
        }

        [HttpDelete("{cpf}")]
        public async Task<ActionResult<bool>> Apagar(string cpf)
        {
            bool apagado = await _administradorRepositorio.Apagar(cpf);
            return Ok(apagado);
        }
    }
}
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SERVPRO.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        [Authorize(Policy = "AdministradorPolicy")]
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> BuscarTodosClientes()
        {
            List<Cliente> clientes = await _clienteRepositorio.BuscarTodosClientes();
            return Ok(clientes);
        }

        [Authorize(Policy = "ClientePolicy")]
        [HttpGet("{cpf}")]
        
        public async Task<ActionResult<Cliente>> BuscarPorCPF(string cpf)
        {
            Cliente cliente = await _clienteRepositorio.BuscarPorCPF(cpf);
            if (cliente == null)
            {
                return NotFound($"Cliente com CPF {cpf} não encontrado.");
            }
            return Ok(cliente);
        }

        [Authorize(Policy = "AdministradorPolicy")]
        [HttpPost]
        
        public async Task<ActionResult<Cliente>> Cadastrar([FromBody] Cliente clienteModel)
        {
            Cliente cliente = await _clienteRepositorio.Adicionar(clienteModel);
            return CreatedAtAction(nameof(BuscarPorCPF), new { cpf = cliente.CPF }, cliente);
        }

        [Authorize(Policy = "AdministradorPolicy")]
        [HttpPut("{cpf}")]
        
        public async Task<ActionResult<Cliente>> Atualizar([FromBody] Cliente clienteModel, string cpf)
        {
            clienteModel.CPF = cpf;
            try
            {
                Cliente cliente = await _clienteRepositorio.Atualizar(clienteModel, cpf);
                return Ok(cliente);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        [Authorize(Policy = "AdministradorPolicy")]
        [HttpDelete("{cpf}")]
      
        public async Task<ActionResult<bool>> Apagar(string cpf)
        {
            bool apagado = await _clienteRepositorio.Apagar(cpf);
            return Ok(apagado);
        }
    }
}

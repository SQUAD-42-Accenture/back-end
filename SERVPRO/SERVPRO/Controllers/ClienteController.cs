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

    public class ClienteController : ControllerBase
    {

        private readonly IClienteRepositorio _clienteRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
        }

        [HttpGet]
        public  async Task <ActionResult<List<Cliente>>> BuscarTodosClientes()
        {
            List<Cliente> clientes = await _clienteRepositorio.BuscarTodosClientes();
            return Ok(clientes);
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<Cliente>> BuscarPorCPF(string cpf)
        {
            Cliente clientes = await _clienteRepositorio.BuscarPorCPF(cpf);

            return Ok(clientes);
        }

        [HttpPost]

        public async Task<ActionResult<Cliente>> Cadastrar([FromBody] Cliente clienteModel)
        {
            Cliente cliente = await _clienteRepositorio.Adicionar(clienteModel);

            return Ok(cliente);
        }

        [HttpPut ("{cpf}")]

        public async Task<ActionResult<Cliente>> Atualizar([FromBody] Cliente clienteModel, string cpf)
        {
            clienteModel.CPF = cpf;
            Cliente cliente = await _clienteRepositorio.Atualizar(clienteModel, cpf);

            return Ok(cliente);
        }

        [HttpDelete("{cpf}")]

        public async Task<ActionResult<Cliente>> Apagar(string cpf)
        {
            
            bool apagado = await _clienteRepositorio.Apagar(cpf);

            return Ok(apagado);
        }


    }
}

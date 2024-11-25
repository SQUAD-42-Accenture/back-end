using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SERVPRO.Models;
using SERVPRO.Repositorios;
using SERVPRO.Repositorios.interfaces;
using SERVPRO.Repositorios.Interfaces;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace SERVPRO.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        private readonly IClienteRepositorio _clienteRepositorio;
        private readonly IOrdemDeServicoRepositorio _ordemDeServicoRepositorio;

        public ClienteController(IClienteRepositorio clienteRepositorio, IOrdemDeServicoRepositorio ordemDeServicoRepositorio)
        {
            _clienteRepositorio = clienteRepositorio;
            _ordemDeServicoRepositorio = ordemDeServicoRepositorio;
        }

        //[Authorize(Policy = "AdministradorPolicy")]
        [HttpGet]
        public async Task<ActionResult<List<Cliente>>> BuscarTodosClientes()
        {
            List<Cliente> clientes = await _clienteRepositorio.BuscarTodosClientes();
            return Ok(clientes);
        }

        [HttpGet("ordens/{cpf}")]
        //[Authorize(Policy = "TecnicoPolicy", "ClientePolicy)]
        public async Task<ActionResult<List<OrdemDeServico>>> BuscarOrdensPorCpfCliente(string cpf)
        {
            var usuarioLogadoCpf = User.Claims.FirstOrDefault(c => c.Type == "cpf")?.Value;
            var tipoUsuario = User.Claims.FirstOrDefault(c => c.Type == "tipoUsuario")?.Value;

            if (tipoUsuario == "Cliente" && usuarioLogadoCpf == cpf)
            {
                var ordensDeServico = await _ordemDeServicoRepositorio.BuscarOrdensPorCpfCliente(cpf);

                if (ordensDeServico == null || ordensDeServico.Count == 0)
                {
                    return NotFound(new { mensagem = $"Nenhuma ordem de serviço encontrada para o cliente com CPF {cpf}." });
                }

                return Ok(ordensDeServico);
            }

            if (tipoUsuario == "Tecnico")
            {
                var ordensDeServico = await _ordemDeServicoRepositorio.BuscarOrdensPorCpfCliente(cpf);

                if (ordensDeServico == null || ordensDeServico.Count == 0)
                {
                    return NotFound(new { mensagem = $"Nenhuma ordem de serviço encontrada para o cliente com CPF {cpf}." });
                }

                return Ok(ordensDeServico);
            }
            return Forbid();
        }

        // [Authorize(Policy = "AdministradorPolicy")]
        [HttpGet("{cpf}")]
        public async Task<ActionResult<Cliente>> BuscarPorCPF(string cpf)
        {
            var usuarioLogadoCpf = User.Claims.FirstOrDefault(c => c.Type == "cpf")?.Value;
            var tipoUsuario = User.Claims.FirstOrDefault(c => c.Type == "tipoUsuario")?.Value;

            if (usuarioLogadoCpf == cpf || tipoUsuario == "Administrador")
            {
                Cliente cliente = await _clienteRepositorio.BuscarPorCPF(cpf);
                if (cliente == null)
                {
                    return NotFound($"Cliente com CPF {cpf} não encontrado.");
                }

                // Construa a URL pública para a foto
                if (!string.IsNullOrEmpty(cliente.FotoPath))
                {
                    var urlFoto = $"{Request.Scheme}://{Request.Host}/Fotos/{Path.GetFileName(cliente.FotoPath)}";
                    cliente.FotoPath = urlFoto; // Atualize o campo FotoPath com a URL pública
                }

                return Ok(cliente);
            }
            return Forbid();
        }


        //[Authorize(Policy = "AdministradorPolicy")]
        [HttpPost]

        public async Task<ActionResult<Cliente>> Cadastrar([FromBody] Cliente clienteModel)
        {
            if (!ModelState.IsValid)
    {
        return BadRequest(new
        {
            errors = ModelState.ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
            )
        });
    }
            Cliente cliente = await _clienteRepositorio.Adicionar(clienteModel);
            return CreatedAtAction(nameof(BuscarPorCPF), new { cpf = cliente.CPF }, cliente);
        }

        //[Authorize(Policy = "AdministradorPolicy")]
        [HttpPut("{cpf}")]
        public async Task<ActionResult<Cliente>> Atualizar(string cpf, [FromBody] JsonElement request)
        {
            // Busca o cliente pelo CPF
            var clienteExistente = await _clienteRepositorio.BuscarPorCPF(cpf);

            var clienteAtualizado = await _clienteRepositorio.Atualizar(clienteExistente, cpf);

            return Ok(clienteAtualizado);
        }



        //[Authorize(Policy = "AdministradorPolicy")]
        [HttpPost("{cpf}/upload-foto")]
        public async Task<IActionResult> UploadFoto(string cpf, IFormFile foto)
        {
            if (foto == null || foto.Length == 0)
            {
                return BadRequest("Nenhuma foto foi fornecida.");
            }

            var pastaDestino = Path.Combine(Directory.GetCurrentDirectory(), "FotosClientes");

            if (!Directory.Exists(pastaDestino))
            {
                Directory.CreateDirectory(pastaDestino);
            }

            var nomeArquivo = $"{cpf}_{Path.GetFileName(foto.FileName)}";
            var caminhoArquivo = Path.Combine(pastaDestino, nomeArquivo);

            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            var cliente = await _clienteRepositorio.BuscarPorCPF(cpf);
            if (cliente == null)
            {
                return NotFound($"Cliente com CPF {cpf} não encontrado.");
            }

            cliente.FotoPath = $"FotosClientes/{nomeArquivo}";
            await _clienteRepositorio.Atualizar(cliente, cpf);

            var urlFoto = $"{Request.Scheme}://{Request.Host}/Fotos/{nomeArquivo}";

            return Ok(new { FotoUrl = urlFoto });
        }


        //[Authorize(Policy = "AdministradorPolicy")]
        [HttpDelete("{cpf}")]

        public async Task<ActionResult<bool>> Apagar(string cpf)
        {
            bool apagado = await _clienteRepositorio.Apagar(cpf);
            return Ok(apagado);
        }
    }
}
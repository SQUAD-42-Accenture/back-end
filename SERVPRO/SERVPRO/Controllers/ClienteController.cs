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

        [Authorize(Policy = "AdministradorPolicy")]
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


        [Authorize(Policy = "AdministradorPolicy")]
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
        [HttpPost("{cpf}/upload-foto")]
        public async Task<IActionResult> UploadFoto(string cpf, IFormFile foto)
        {
            // Verifique se a foto foi recebida
            if (foto == null || foto.Length == 0)
            {
                return BadRequest("Nenhuma foto foi fornecida.");
            }

            // Local onde as fotos serão armazenadas
            var pastaDestino = Path.Combine(Directory.GetCurrentDirectory(), "FotosClientes");

            // Crie o diretório se não existir
            if (!Directory.Exists(pastaDestino))
            {
                Directory.CreateDirectory(pastaDestino);
            }

            // Gere um nome único para o arquivo da foto (por exemplo, baseado no CPF)
            var nomeArquivo = $"{cpf}_{Path.GetFileName(foto.FileName)}";
            var caminhoArquivo = Path.Combine(pastaDestino, nomeArquivo);

            // Salve o arquivo no diretório
            using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
            {
                await foto.CopyToAsync(stream);
            }

            // Obtenha o cliente e atualize o campo FotoPath
            var cliente = await _clienteRepositorio.BuscarPorCPF(cpf);
            if (cliente == null)
            {
                return NotFound($"Cliente com CPF {cpf} não encontrado.");
            }

            // Armazenar apenas o caminho relativo no banco de dados
            cliente.FotoPath = $"FotosClientes/{nomeArquivo}";
            await _clienteRepositorio.Atualizar(cliente, cpf);

            // Retorna a URL pública da foto
            var urlFoto = $"{Request.Scheme}://{Request.Host}/Fotos/{nomeArquivo}";

            return Ok(new { FotoUrl = urlFoto });
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
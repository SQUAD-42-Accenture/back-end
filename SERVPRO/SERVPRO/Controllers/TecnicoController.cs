using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;
using SERVPRO.Repositorios.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SERVPRO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] 
    public class TecnicoController : ControllerBase
    {
        private readonly ITecnicoRepositorio _tecnicoRepositorio;
        private readonly IOrdemDeServicoRepositorio _ordemDeServicoRepositorio;

        public TecnicoController(ITecnicoRepositorio tecnicoRepositorio, IOrdemDeServicoRepositorio ordemDeServicoRepositorio)
        {
            _tecnicoRepositorio = tecnicoRepositorio;
            _ordemDeServicoRepositorio = ordemDeServicoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Tecnico>>> BuscarTodosTecnicos()
        {
            List<Tecnico> tecnicos = await _tecnicoRepositorio.BuscarTodosTecnicos();
            return Ok(tecnicos);
        }

        [HttpGet("{cpf}")]
        [Authorize(Policy = "AdministradorPolicy")] 
        public async Task<ActionResult<Tecnico>> BuscarPorCPF(string cpf)
        {
            var usuarioLogadoCpf = User.Claims.FirstOrDefault(c => c.Type == "cpf")?.Value;
            var tipoUsuario = User.Claims.FirstOrDefault(c => c.Type == "tipoUsuario")?.Value;

            if (usuarioLogadoCpf == cpf || tipoUsuario == "Administrador")
            {
                Tecnico tecnico = await _tecnicoRepositorio.BuscarPorCPF(cpf);

                if (tecnico == null)
                {
                    return NotFound(new { mensagem = "Técnico não encontrado." });
                }

                return Ok(tecnico);
            }

            return Forbid(); 
        }

        [HttpPost]
        [Authorize(Policy = "AdministradorPolicy")]  
        public async Task<ActionResult<Tecnico>> Cadastrar([FromBody] Tecnico tecnicoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Tecnico tecnico = await _tecnicoRepositorio.Adicionar(tecnicoModel);

            return Ok(tecnico);
        }

        [HttpPut("{cpf}")]
        [Authorize(Policy = "AdministradorPolicy")]  
        public async Task<ActionResult<Tecnico>> Atualizar([FromBody] Tecnico tecnicoModel, string cpf)
        {
            tecnicoModel.CPF = cpf;
            Tecnico tecnico = await _tecnicoRepositorio.Atualizar(tecnicoModel, cpf);

            return Ok(tecnico);
        }

        [HttpDelete("{cpf}")]
        [Authorize(Policy = "AdministradorPolicy")]  
        public async Task<ActionResult<Tecnico>> Apagar(string cpf)
        {
            bool apagado = await _tecnicoRepositorio.Apagar(cpf);

            return Ok(apagado);
        }

        [HttpGet("ordens/{cpf}")]
        [Authorize(Policy = "TecnicoPolicy")] 
        public async Task<ActionResult<List<OrdemDeServico>>> BuscarOrdensPorCpfTecnico(string cpf)
        {
            var usuarioLogadoCpf = User.Claims.FirstOrDefault(c => c.Type == "cpf")?.Value;
            var tipoUsuario = User.Claims.FirstOrDefault(c => c.Type == "tipoUsuario")?.Value;

            if (tipoUsuario == "Tecnico" && usuarioLogadoCpf == cpf)
            {
                List<OrdemDeServico> ordensDeServico = await _ordemDeServicoRepositorio.BuscarOrdensPorCpfTecnico(cpf);

                if (ordensDeServico == null || ordensDeServico.Count == 0)
                {
                    return NotFound(new { mensagem = $"Nenhuma ordem de serviço encontrada para o técnico com CPF {cpf}." });
                }

                return Ok(ordensDeServico);
            }

            return Forbid();
        }

    }
}

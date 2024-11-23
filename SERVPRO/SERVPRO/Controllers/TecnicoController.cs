using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;
using System.Globalization;

namespace SERVPRO.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TecnicoController : ControllerBase
    {

        private readonly ITecnicoRepositorio _tecnicoRepositorio;

        public TecnicoController(ITecnicoRepositorio tecnicoRepositorio)
        {
            _tecnicoRepositorio = tecnicoRepositorio;
        }
        //[Authorize(Policy = "AdministradorPolicy")]
        [HttpGet]
        public async Task<ActionResult<List<Tecnico>>> BuscarTodosTecnicos()
        {
            List<Tecnico> tecnicos = await _tecnicoRepositorio.BuscarTodosTecnicos();
            return Ok(tecnicos);
        }
       // [Authorize(Policy = "TecnicoPolicy")]
        [HttpGet("{cpf}")]
        public async Task<ActionResult<Tecnico>> BuscarPorCPF(string cpf)
        {
            var usuarioLogadoCpf = User.Claims.FirstOrDefault(c => c.Type == "cpf")?.Value;
            var tipoUsuario = User.Claims.FirstOrDefault(c => c.Type == "tipoUsuario")?.Value;

            // Verifica se é o próprio técnico ou se é um administrador acessando
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


        //[Authorize(Policy = "AdministradorPolicy")]
        [HttpPost]

        public async Task<ActionResult<Tecnico>> Cadastrar([FromBody] Tecnico tecnicoModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna automaticamente os erros de validação
            }
            Tecnico tecnico = await _tecnicoRepositorio.Adicionar(tecnicoModel);

            return Ok(tecnico);
        }

       // [Authorize(Policy = "AdministradorPolicy")]
        [HttpPut("{cpf}")]

        public async Task<ActionResult<Tecnico>> Atualizar([FromBody] Tecnico tecnicoModel, string cpf)
        {
            tecnicoModel.CPF = cpf;
            Tecnico tecnico = await _tecnicoRepositorio.Atualizar(tecnicoModel, cpf);

            return Ok(tecnico);
        }

       // [Authorize(Policy = "AdministradorPolicy")]
        [HttpDelete("{cpf}")]

        public async Task<ActionResult<Tecnico>> Apagar(string cpf)
        {

            bool apagado = await _tecnicoRepositorio.Apagar(cpf);

            return Ok(apagado);
        }


    }
}
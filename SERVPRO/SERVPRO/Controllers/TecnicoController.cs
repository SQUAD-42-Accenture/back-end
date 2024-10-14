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
    public class TecnicoController : ControllerBase
    {

        private readonly ITecnicoRepositorio _tecnicoRepositorio;

        public TecnicoController(ITecnicoRepositorio tecnicoRepositorio)
        {
            _tecnicoRepositorio = tecnicoRepositorio;
        }

        [HttpGet]
        public  async Task <ActionResult<List<Tecnico>>> BuscarTodosTecnicos()
        {
            List<Tecnico> tecnicos = await _tecnicoRepositorio.BuscarTodosTecnicos();
            return Ok(tecnicos);
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<Tecnico>> BuscarPorCPF(string cpf)
        {
            Tecnico tecnicos = await _tecnicoRepositorio.BuscarPorCPF(cpf);

            return Ok(tecnicos);
        }

        [HttpPost]

        public async Task<ActionResult<Tecnico>> Cadastrar([FromBody] Tecnico tecnicoModel)
        {
            Tecnico tecnico = await _tecnicoRepositorio.Adicionar(tecnicoModel);

            return Ok(tecnico);
        }

        [HttpPut ("{cpf}")]

        public async Task<ActionResult<Tecnico>> Atualizar([FromBody] Tecnico tecnicoModel, string cpf)
        {
            tecnicoModel.CPF = cpf;
            Tecnico tecnico = await _tecnicoRepositorio.Atualizar(tecnicoModel, cpf);

            return Ok(tecnico);
        }

        [HttpDelete("{cpf}")]

        public async Task<ActionResult<Tecnico>> Apagar(string cpf)
        {
            
            bool apagado = await _tecnicoRepositorio.Apagar(cpf);

            return Ok(apagado);
        }


    }
}

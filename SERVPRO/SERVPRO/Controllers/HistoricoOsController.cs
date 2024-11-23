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
    public class HistoricoOsController : ControllerBase
    {

        private readonly IHistoricoOsRepositorio _historicoOsRepositorio;

        public HistoricoOsController(IHistoricoOsRepositorio historicoOsRepositorio)
        {
            _historicoOsRepositorio = historicoOsRepositorio;
        }

       // [Authorize(Policy = "AdministradorPolicy")]
        [HttpGet]
        public async Task<ActionResult<List<HistoricoOS>>> BuscarTodoshistoricos()
        {
            List<HistoricoOS> historicoOs = await _historicoOsRepositorio.BuscarTodoshistoricos();
            return Ok(historicoOs);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<HistoricoOS>> BuscarPorId(int id)
        {
            HistoricoOS historicoOS = await _historicoOsRepositorio.BuscarPorId(id);

            return Ok(historicoOS);
        }

        [HttpPost]

        public async Task<ActionResult<HistoricoOS>> Cadastrar([FromBody] HistoricoOS historicoOsModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState); // Retorna automaticamente os erros de validação
            }
            HistoricoOS historicoOS = await _historicoOsRepositorio.Adicionar(historicoOsModel);

            return Ok(historicoOS);
        }

        [HttpPut("{id}")]

        public async Task<ActionResult<HistoricoOS>> Atualizar([FromBody] HistoricoOS historicoOsModel, int id)
        {
            historicoOsModel.Id = id;
            HistoricoOS historicoOS = await _historicoOsRepositorio.Atualizar(historicoOsModel, id);

            return Ok(historicoOS);
        }

       // [Authorize(Policy = "AdministradorPolicy")]
        [HttpDelete("{id}")]

        public async Task<ActionResult<HistoricoOS>> Apagar(int id)
        {

            bool apagado = await _historicoOsRepositorio.Apagar(id);

            return Ok(apagado);
        }


    }
}
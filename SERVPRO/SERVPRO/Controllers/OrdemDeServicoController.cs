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
    public class OrdemDeServicoController : ControllerBase
    {

        private readonly IOrdemDeServicoRepositorio _ordemDeServicoRepositorio;

        public OrdemDeServicoController(IOrdemDeServicoRepositorio ordemDeServicoRepositorio)
        {
            _ordemDeServicoRepositorio = ordemDeServicoRepositorio;
        }

        [HttpGet]
        public  async Task <ActionResult<List<OrdemDeServico>>> BuscarTodasOS()
        {
            List<OrdemDeServico> ordemDeServicos = await _ordemDeServicoRepositorio.BuscarTodasOS();
            return Ok(ordemDeServicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrdemDeServico>> BuscarPorId(int id)
        {
            OrdemDeServico ordemDeServico = await _ordemDeServicoRepositorio.BuscarPorId(id);

            return Ok(ordemDeServico);
        }

        [HttpPost]

        public async Task<ActionResult<OrdemDeServico>> Cadastrar([FromBody] OrdemDeServico ordemDeServicoModel)
        {
            OrdemDeServico ordemDeServico = await _ordemDeServicoRepositorio.Adicionar(ordemDeServicoModel);

            return Ok(ordemDeServico);
        }

        [HttpPut ("{id}")]

        public async Task<ActionResult<OrdemDeServico>> Atualizar([FromBody] OrdemDeServico ordemDeServicoModel, int id)
        {
            ordemDeServicoModel.Id = id;
            OrdemDeServico ordemDeServico = await _ordemDeServicoRepositorio.Atualizar(ordemDeServicoModel, id);

            return Ok(ordemDeServico);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<OrdemDeServico>> Apagar(int id)
        {
            
            bool apagado = await _ordemDeServicoRepositorio.Apagar(id);

            return Ok(apagado);
        }


    }
}

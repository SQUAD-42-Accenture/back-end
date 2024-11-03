using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;

namespace SERVPRO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoController : ControllerBase
    {
        private readonly IServicoRepositorio _servicoRepositorio;

        public ServicoController(IServicoRepositorio servicoRepositorio)
        {
            _servicoRepositorio = servicoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Servico>>> BuscarTodosServicos()
        {
            var servicos = await _servicoRepositorio.BuscarTodosServicos();
            return Ok(servicos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Servico>> BuscarPorId(int id)
        {
            var servico = await _servicoRepositorio.BuscarPorId(id);
            if (servico == null) return NotFound();
            return Ok(servico);
        }

        [HttpPost]
        public async Task<ActionResult<Servico>> Cadastrar([FromBody] Servico servicoModel)
        {
            var servico = await _servicoRepositorio.Adicionar(servicoModel);
            return CreatedAtAction(nameof(BuscarPorId), new { id = servico.Id }, servico);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Servico>> Atualizar([FromBody] Servico servicoModel, int id)
        {
            servicoModel.Id = id;
            var servico = await _servicoRepositorio.Atualizar(servicoModel, id);
            return Ok(servico);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            var apagado = await _servicoRepositorio.Apagar(id);
            return Ok(apagado);
        }
    }
}

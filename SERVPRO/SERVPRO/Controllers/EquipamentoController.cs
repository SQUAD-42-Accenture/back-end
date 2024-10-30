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
    public class EquipamentoController : ControllerBase
    {

        private readonly IEquipamentoRepositorio _equipamentoRepositorio;

        public EquipamentoController(IEquipamentoRepositorio equipamentoRepositorio)
        {
            _equipamentoRepositorio = equipamentoRepositorio;
        }

        [HttpGet]
        public  async Task <ActionResult<List<Equipamento>>> BuscarTodosEquipamentos()
        {
            List<Equipamento> equipamentos = await _equipamentoRepositorio.BuscarTodosEquipamentos();
            return Ok(equipamentos);
        }

        [HttpGet("{serial}")]
        public async Task<ActionResult<Equipamento>> BuscarPorSerial(string serial)
        {
            Equipamento equipamento = await _equipamentoRepositorio.BuscarPorSerial(serial);

            return Ok(equipamento);
        }

        [HttpPost]

        public async Task<ActionResult<Equipamento>> Cadastrar([FromBody] Equipamento equipamentoModel)
        {
            Equipamento equipamento = await _equipamentoRepositorio.Adicionar(equipamentoModel);

            return Ok(equipamento);
        }

        [HttpPut ("{serial}")]

        public async Task<ActionResult<Equipamento>> Atualizar([FromBody] Equipamento equipamentoModel, string serial)
        {
            equipamentoModel.Serial = serial;
            Equipamento equipamento = await _equipamentoRepositorio.Atualizar(equipamentoModel, serial);

            return Ok(equipamento);
        }

        [HttpDelete("{serial}")]

        public async Task<ActionResult<Equipamento>> Apagar(string serial)
        {
            
            bool apagado = await _equipamentoRepositorio.Apagar(serial);

            return Ok(apagado);
        }


    }
}


using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SERVPRO.Models;
using SERVPRO.Repositorios;
using SERVPRO.Repositorios.interfaces;
using System;
using System.Globalization;

namespace SERVPRO.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrdemDeServicoController : ControllerBase
    {

        private readonly IOrdemDeServicoRepositorio _ordemDeServicoRepositorio;
        private readonly PdfServiceRepositorio _pdfServiceRepositorio;


        public OrdemDeServicoController(IOrdemDeServicoRepositorio ordemDeServicoRepositorio, PdfServiceRepositorio pdfServiceRepositorio)
        {
            _ordemDeServicoRepositorio = ordemDeServicoRepositorio;
            _pdfServiceRepositorio = pdfServiceRepositorio;


        }

        [HttpGet]
        public async Task<ActionResult<List<OrdemDeServico>>> BuscarTodasOS()
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

        [Authorize(Policy = "AdministradorPolicy")]
        [HttpPost]

        public async Task<ActionResult<OrdemDeServico>> Cadastrar([FromBody] OrdemDeServico ordemDeServicoModel)
        {
            OrdemDeServico ordemDeServico = await _ordemDeServicoRepositorio.Adicionar(ordemDeServicoModel);

            return Ok(ordemDeServico);
        }


        [HttpPut("{id}")]

        public async Task<ActionResult<OrdemDeServico>> Atualizar([FromBody] OrdemDeServico ordemDeServicoModel, int id)
        {
            ordemDeServicoModel.Id = id;
            OrdemDeServico ordemDeServico = await _ordemDeServicoRepositorio.Atualizar(ordemDeServicoModel, id);

            return Ok(ordemDeServico);
        }

        [Authorize(Policy = "AdministradorPolicy")]
        [HttpDelete("{id}")]

        public async Task<ActionResult<OrdemDeServico>> Apagar(int id)
        {

            bool apagado = await _ordemDeServicoRepositorio.Apagar(id);

            return Ok(apagado);
        }

        [Authorize(Policy = "AdministradorPolicy")]
        [HttpGet("{id}/gerar-pdf")]
        public async Task<IActionResult> GerarPdf(int id)
        {
            var ordemDeServico = await _ordemDeServicoRepositorio
                .BuscarPorId(id);

            if (ordemDeServico == null || ordemDeServico.Cliente == null || ordemDeServico.Tecnico == null)
                return NotFound("Ordem de serviço, cliente ou técnico não encontrados.");

            var pdf = _pdfServiceRepositorio.GeneratePdf(ordemDeServico, ordemDeServico.Cliente, ordemDeServico.Tecnico);

            return File(pdf, "application/pdf", $"ordem_servico_{id}.pdf");
        }

    }
}
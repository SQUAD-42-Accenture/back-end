using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SERVPRO.Models;
using SERVPRO.Repositorios;
using SERVPRO.Repositorios.interfaces;
using SERVPRO.Repositorios.Interfaces;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace SERVPRO.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdemDeServicoController : ControllerBase
    {
        private readonly IOrdemDeServicoRepositorio _ordemDeServicoRepositorio;
        private readonly PdfServiceRepositorio _pdfServiceRepositorio;
        private readonly EmailServiceRepositorio _emailService;
        private readonly IHistoricoOsRepositorio _historicoOsRepositorio;

        public OrdemDeServicoController(IOrdemDeServicoRepositorio ordemDeServicoRepositorio, PdfServiceRepositorio pdfServiceRepositorio, EmailServiceRepositorio emailServiceRepositorio, IHistoricoOsRepositorio historicoOsRepositorio)
        {
            _ordemDeServicoRepositorio = ordemDeServicoRepositorio;
            _pdfServiceRepositorio = pdfServiceRepositorio;
            _emailService = emailServiceRepositorio;
            _historicoOsRepositorio = historicoOsRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<OrdemDeServico>>> BuscarTodasOS()
        {
            List<OrdemDeServico> ordensDeServico = await _ordemDeServicoRepositorio.BuscarTodasOS();
            return Ok(ordensDeServico);
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

            var historicoOs = new HistoricoOS
            {
                OrdemDeServicoId = ordemDeServico.Id,
                DataAtualizacao = DateTime.Now,
                Comentario = "Ordem de serviço criada",
                TecnicoCPF = ordemDeServico.TecnicoCPF
            };

            await _historicoOsRepositorio.Adicionar(historicoOs);

            return Ok(ordemDeServico);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<OrdemDeServico>> Atualizar([FromBody] OrdemDeServico ordemDeServicoModel, int id)
        {
            OrdemDeServico ordemDeServico = await _ordemDeServicoRepositorio.Atualizar(ordemDeServicoModel, id);

            if (ordemDeServico == null)
                return NotFound();

            var historicoOs = new HistoricoOS
            {
                OrdemDeServicoId = ordemDeServico.Id,
                DataAtualizacao = DateTime.Now,
                Comentario = "Ordem de serviço atualizada",
                TecnicoCPF = ordemDeServico.TecnicoCPF
            };

            await _historicoOsRepositorio.Adicionar(historicoOs);

            return Ok(ordemDeServico);
        }

        [HttpGet("{id}/calcular-valor-total")]
        public async Task<IActionResult> CalcularValorTotal(int id)
        {
            try
            {
                var valorTotal = await _ordemDeServicoRepositorio.CalcularValorTotal(id);
                return Ok(valorTotal);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _ordemDeServicoRepositorio.Apagar(id);
            return Ok(apagado);
        }

        [HttpGet("{id}/gerar-pdf")]
        public async Task<IActionResult> GerarPdf(int id)
        {
            var ordemDeServico = await _ordemDeServicoRepositorio.BuscarPorId(id);
            if (ordemDeServico == null || ordemDeServico.Cliente == null || ordemDeServico.Tecnico == null)
                return NotFound("Ordem de serviço, cliente ou técnico não encontrados.");

            var pdf = _pdfServiceRepositorio.GeneratePdf(ordemDeServico, ordemDeServico.Cliente, ordemDeServico.Tecnico);
            return File(pdf, "application/pdf", $"ordem_servico_{id}.pdf");
        }

        [HttpPost("{id}/enviar-por-email")]
        public async Task<IActionResult> EnviarOrdemPorEmail(int id)
        {
            var ordemDeServico = await _ordemDeServicoRepositorio.BuscarPorId(id);
            if (ordemDeServico == null || ordemDeServico.Cliente == null)
                return NotFound("Ordem de serviço ou cliente não encontrados.");

            var pdfBytes = _pdfServiceRepositorio.GeneratePdf(ordemDeServico, ordemDeServico.Cliente, ordemDeServico.Tecnico);

            var destinatarioEmail = ordemDeServico.Cliente.Email;
            var assunto = $"Ordem de Serviço #{ordemDeServico.Id}";
            var conteudo = "Segue em anexo o PDF com os detalhes da sua ordem de serviço.";

            await _emailService.EnviarOrdemDeServicoPorEmail(destinatarioEmail, assunto, conteudo, pdfBytes);

            return Ok("Ordem de serviço enviada com sucesso para o e-mail do cliente.");
        }
    }
}

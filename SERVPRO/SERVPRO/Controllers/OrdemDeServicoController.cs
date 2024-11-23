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

        public OrdemDeServicoController(IOrdemDeServicoRepositorio ordemDeServicoRepositorio, PdfServiceRepositorio pdfServiceRepositorio, EmailServiceRepositorio emailServiceRepositorio)
        {
            _ordemDeServicoRepositorio = ordemDeServicoRepositorio;
            _pdfServiceRepositorio = pdfServiceRepositorio;
            _emailService = emailServiceRepositorio;
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

        //[Authorize(Policy = "AdministradorPolicy")]
        [HttpPost]
        public async Task<ActionResult<OrdemDeServico>> Cadastrar([FromBody] OrdemDeServico ordemDeServicoModel)
        {

            // Adiciona a ordem de serviço ao banco de dados
            OrdemDeServico ordemDeServico = await _ordemDeServicoRepositorio.Adicionar(ordemDeServicoModel);

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

        //[Authorize(Policy = "AdministradorPolicy")]
        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> Apagar(int id)
        {
            bool apagado = await _ordemDeServicoRepositorio.Apagar(id);
            return Ok(apagado);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<OrdemDeServico>> AtualizarStatus(int id, [FromBody] OrdemDeServico ordemDeServico)
        {
            if (ordemDeServico == null)
            {
                return BadRequest("Dados da ordem de serviço não fornecidos.");
            }

            var ordemExistente = await _ordemDeServicoRepositorio.BuscarPorId(id);
            if (ordemExistente == null)
            {
                return NotFound($"Ordem de serviço com ID {id} não encontrada.");
            }

            var statusPermitidos = new[] { "Concluido", "Em Andamento", "Pendente", "Cancelada", "Aberta" };
            if (!statusPermitidos.Contains(ordemDeServico.Status))
            {
                return BadRequest($"Status inválido. Os status permitidos são: {string.Join(", ", statusPermitidos)}.");
            }

            var ordemAtualizada = await _ordemDeServicoRepositorio.Atualizar(id, ordemDeServico);

            if (ordemAtualizada == null)
            {
                return BadRequest("Erro ao atualizar a ordem de serviço.");
            }

            return Ok(ordemAtualizada);
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

  
        //[Authorize(Policy = "AdministradorPolicy")]
        [HttpPost("{id}/enviar-por-email")]
        public async Task<IActionResult> EnviarOrdemPorEmail(int id)
        {

            var ordemDeServico = await _ordemDeServicoRepositorio.BuscarPorId(id);
            if (ordemDeServico == null || ordemDeServico.Cliente == null)
                return NotFound("Ordem de serviço ou cliente não encontrados.");

            // Gera o PDF da ordem de serviço
            var pdfBytes = _pdfServiceRepositorio.GeneratePdf(ordemDeServico, ordemDeServico.Cliente, ordemDeServico.Tecnico);

            // Configura o envio de e-mail
            var destinatarioEmail = ordemDeServico.Cliente.Email;
            var assunto = $"Ordem de Serviço #{ordemDeServico.Id}";
            var conteudo = "Segue em anexo o PDF com os detalhes da sua ordem de serviço.";

            // Envia o e-mail com o PDF anexado
            await _emailService.EnviarOrdemDeServicoPorEmail(destinatarioEmail, assunto, conteudo, pdfBytes);

            return Ok("Ordem de serviço enviada com sucesso para o e-mail do cliente.");
        }


    }
}

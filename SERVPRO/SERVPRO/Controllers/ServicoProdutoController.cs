using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SERVPRO.Models;
using SERVPRO.Repositorios.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SERVPRO.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ServicoProdutoController : ControllerBase
    {
        private readonly IServicoProdutoRepositorio _servicoProdutoRepositorio;

        public ServicoProdutoController(IServicoProdutoRepositorio servicoProdutoRepositorio)
        {
            _servicoProdutoRepositorio = servicoProdutoRepositorio;
        }

    
        [HttpGet]
        public async Task<ActionResult<List<ServicoProduto>>> BuscarTodosServicoProdutos()
        {
            List<ServicoProduto> servicoProdutos = await _servicoProdutoRepositorio.BuscarTodosServicoProdutos();
            return Ok(servicoProdutos);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ServicoProduto>> BuscarPorId(int id)
        {
            ServicoProduto servicoProduto = await _servicoProdutoRepositorio.BuscarPorId(id);

            if (servicoProduto == null)
            {
                return NotFound($"Associação de produto e serviço com ID {id} não encontrada.");
            }

            return Ok(servicoProduto);
        }

        [HttpPost]
        public async Task<ActionResult<ServicoProduto>> Adicionar([FromBody] ServicoProduto servicoProdutoModel)
        {
            try
            {
                ServicoProduto servicoProduto = await _servicoProdutoRepositorio.Adicionar(servicoProdutoModel);
                return CreatedAtAction(nameof(BuscarPorId), new { id = servicoProduto.Id }, servicoProduto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpPut("{servicoId}/{produtoId}")]
        public async Task<ActionResult<ServicoProduto>> AtualizarCustoProdutoNoServico(int servicoId, int produtoId, [FromBody] decimal novoCusto)
        {
            try
            {
                ServicoProduto servicoProduto = await _servicoProdutoRepositorio.AtualizarCustoProdutoNoServico(servicoId, produtoId, novoCusto);
                return Ok(servicoProduto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        [HttpDelete("{servicoId}/{produtoId}")]
        public async Task<ActionResult> Remover(int servicoId, int produtoId)
        {
            try
            {
                bool removido = await _servicoProdutoRepositorio.Remover(servicoId, produtoId);
                if (removido)
                {
                    return NoContent(); // No content status, pois a remoção foi bem-sucedida.
                }
                return NotFound("Associação não encontrada.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("servico/{servicoId}/produtos")]
        public async Task<ActionResult<List<Produto>>> ObterProdutosPorServico(int servicoId)
        {
            List<Produto> produtos = await _servicoProdutoRepositorio.ObterProdutosPorServico(servicoId);
            return Ok(produtos);
        }


        [HttpGet("produto/{produtoId}/servicos")]
        public async Task<ActionResult<List<Servico>>> ObterServicosPorProduto(int produtoId)
        {
            List<Servico> servicos = await _servicoProdutoRepositorio.ObterServicosPorProduto(produtoId);
            return Ok(servicos);
        }
    }
}

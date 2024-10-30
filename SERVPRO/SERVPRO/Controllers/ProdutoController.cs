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
    public class ProdutoController : ControllerBase
    {

        private readonly IProdutoRepositorio _produtoRepositorio;

        public ProdutoController(IProdutoRepositorio produtoRepositorio)
        {
            _produtoRepositorio = produtoRepositorio;
        }

        [HttpGet]
        public async Task<ActionResult<List<Produto>>> BuscarTodosProdutos()
        {
            List<Produto> produtos = await _produtoRepositorio.BuscarTodosProdutos();
            return Ok(produtos);
        }

        [HttpGet("{IdProduto}")]
        public async Task<ActionResult<Produto>> BuscarPorId(string IdProduto)
        {
            Produto produto = await _produtoRepositorio.BuscarPorId(IdProduto);

            return Ok(produto);
        }

        [HttpPost]

        public async Task<ActionResult<Produto>> Cadastrar([FromBody] Produto produtoModel)
        {
            Produto produto = await _produtoRepositorio.Adicionar(produtoModel);

            return Ok(produto);
        }

        [HttpPut("{serial}")]

        public async Task<ActionResult<Produto>> Atualizar([FromBody] Produto produtoModel, string IdProduto)
        {
            produtoModel.IdProduto = IdProduto;
            Produto produto = await _produtoRepositorio.Atualizar(produtoModel, IdProduto);

            return Ok(produto);
        }

        [HttpDelete("{IdProduto}")]

        public async Task<ActionResult<Produto>> Apagar(string IdProduto)
        {

            bool apagado = await _produtoRepositorio.Apagar(IdProduto);

            return Ok(apagado);
        }


    }
}

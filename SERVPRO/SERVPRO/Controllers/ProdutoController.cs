using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SERVPRO.Models;
using SERVPRO.Repositorios.interfaces;
using System.Globalization;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

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

        [HttpGet("{Id}")]
        public async Task<ActionResult<Produto>> BuscarPorId(int Id)
        {
            Produto produto = await _produtoRepositorio.BuscarPorId(Id);

            return Ok(produto);
        }

        [HttpPost]

        public async Task<ActionResult<Produto>> Cadastrar([FromBody] Produto produtoModel)
        {
            Produto produto = await _produtoRepositorio.Adicionar(produtoModel);

            return Ok(produto);
        }

        [HttpPut("{serial}")]

        public async Task<ActionResult<Produto>> Atualizar([FromBody] Produto produtoModel, int Id)
        {
            produtoModel.Id = Id;
            Produto produto = await _produtoRepositorio.Atualizar(produtoModel, Id);

            return Ok(produto);
        }

        [HttpDelete("{Id}")]

        public async Task<ActionResult<Produto>> Apagar(int Id)
        {

            bool apagado = await _produtoRepositorio.Apagar(Id);

            return Ok(apagado);
        }


    }
}

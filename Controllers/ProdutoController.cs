using ApiWeb.Data;
using ApiWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProdutoController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<ProdutoModel>> BuscarProdutos()
        {
            var Produtos = _context.Produtos.ToList();
            return Ok(Produtos);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ProdutoModel> BuscarProdutosPorId(int id)
        {
            var produto = _context.Produtos.Find(id);
            if(produto == null)
            {
                return NotFound("Registro não localizado !");
            }

            return Ok(produto);
        }

        [HttpPost]
        public ActionResult<ProdutoModel> CriarProduto(ProdutoModel produtoModel)
        {
            if(produtoModel == null)
            {
                return BadRequest("Ocorreu um erro na solicitação");
            }

            _context.Produtos.Add(produtoModel);
            _context.SaveChanges();

            return CreatedAtAction(nameof(BuscarProdutosPorId), new { id = produtoModel.Id }, produtoModel);
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult<ProdutoModel> EditarProduto(ProdutoModel produtoModel, int id)
        {
            var produto = _context.Produtos.Find(id);

            if (produto == null)
            {
                return NotFound("Registro não localizado");
            }

            produto.Nome = produtoModel.Nome;
            produto.Descricao = produtoModel.Descricao;
            produto.Marca = produtoModel.Marca;
            produto.QuantidadeEstoque = produtoModel.QuantidadeEstoque;
            produto.CodigoDeBarras = produtoModel.CodigoDeBarras;

            _context.Produtos.Update(produto);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeletarProduto(int id) 
        {
            var produto = _context.Produtos.Find(id);

            if (produto == null)
            {
                return NotFound("Registro não localizado");
            }

            _context.Produtos.Remove(produto);
            _context.SaveChanges();

            return NoContent();
        } // Verifique se essa chave de fechamento do método e da classe estão ok!

    }
 }

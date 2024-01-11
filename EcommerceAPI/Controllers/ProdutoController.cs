using EcommerceAPI.Data.Dtos.ProdutoDto;
using EcommerceAPI.Modelo;
using EcommerceAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutoController : ControllerBase
    {
        private readonly ProdutoService _produtoService;

        public ProdutoController(ProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpPost]
        public IActionResult CadastrarProdutos([FromBody] CriarProdutoDto dto)
        {
            LerProdutoDto lerDto = _produtoService.CadastrarProdutos(dto);
            return CreatedAtAction(nameof(RecuperarProdutosComFiltros), new { nome = lerDto.Nome }, lerDto);
        }

        [HttpGet("pesquisarfiltros")]
        public List<Produto> RecuperarProdutosComFiltros([FromQuery] FiltroProdutoDto filtroDto)
        {
            return _produtoService.RecuperarProdutosComFiltros(filtroDto);
        }

        [HttpPut("editar/{id}")]
        public Result EditarProdutos(int id, [FromBody] EditarProdutoDto produtoDto)
        {
            Result editarDto = _produtoService.EditarProdutos(id, produtoDto);
            return Result.Ok();
        }

        [HttpDelete("deletar/{id}")]
        public IActionResult DeletarProdutos(int id)
        {
            var deletarDto = _produtoService.DeletarProdutos(id);
            return NoContent();
        }
    }
}

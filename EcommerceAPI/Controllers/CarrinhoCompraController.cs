using EcommerceAPI.Data.Dtos.CarrinhoDto;
using EcommerceAPI.Modelo;
using EcommerceAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CarrinhoCompraController : ControllerBase
    {
        private readonly CarrinhoCompraService _carrinhoService;

        public CarrinhoCompraController(CarrinhoCompraService carrinhoService)
        {
            _carrinhoService = carrinhoService;
        }

        [HttpPost("criacarrinho")]
        public IActionResult CriarCarrinho([FromQuery] CriarCarrinhoDto carrinhoDto)
        {
            var criar = _carrinhoService.CriarCarrinho(carrinhoDto);
            if (criar.Result.IsFailed)
            {
                return BadRequest(criar.Result.Errors);
            }
            return Ok(carrinhoDto);
        }

        [HttpPut("adicionaproduto")]
        public IActionResult AdicionarProduto(int carrinhoId, int produtoId)
        {
            var adicionar = _carrinhoService.AdicionarProduto(carrinhoId, produtoId);
            if (adicionar.ToResult().IsFailed)
            {
                return BadRequest(adicionar.ToResult().Reasons.FirstOrDefault());
            }
            return Ok();
        }

        [HttpGet]
        public LerCarrinhoDto PesquisarCarrinho([FromQuery] LerCarrinhoDto carrinhoDto)
        {
            var pesquisar = _carrinhoService.PesquisarCarrinho(carrinhoDto);
            return pesquisar;
        }

        [HttpPatch]
        public CarrinhoDeCompra AlterarQuantidade([FromQuery] int carrinhoId, int produtoId, int quantidade)
        {
            var alterar = _carrinhoService.AlterarQuantidade(carrinhoId, produtoId, quantidade);
            return alterar;
        }

        [HttpDelete]
        public IActionResult DeletarCarrinho([FromQuery] int? id)
        {
            Result deletar = _carrinhoService.DeletarCarrinho(id);
            if (deletar.IsFailed)
            {
                return BadRequest(deletar.Errors);
            }
            return NoContent();
        }
    }
}

using AutoMapper;
using EcommerceAPI.Data.Dtos.CarrinhoDto;
using EcommerceAPI.Exceptions;
using EcommerceAPI.Modelo;
using FluentResults;
using System;
using System.Linq;

namespace EcommerceAPI.Data.Repository
{
    public class CarrinhoCompraRepository
    {
        private readonly EcommerceDbContext _context;
        private readonly IMapper _mapper;

        public CarrinhoCompraRepository(EcommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public Result CriarCarrinho(CarrinhoDeCompra carrinho, CarrinhoDeCompra endereco)
        {
            carrinho.Logradouro = endereco.Logradouro;
            carrinho.Bairro = endereco.Bairro;
            carrinho.Localidade = endereco.Localidade;
            carrinho.UF = endereco.UF;

            _context.CarrinhoDeCompras.Add(carrinho);
            _context.SaveChanges();

            var relacionamento = CriaRelacionamento(carrinho);
            if (relacionamento.IsFailed)
            {
                return Result.Fail(relacionamento.Errors);
            }
            
            return Result.Ok();
        }

        public Result CriaRelacionamento(CarrinhoDeCompra carrinho)
        {
            ProdutoCarrinho prodCarrinho = new ProdutoCarrinho();

            prodCarrinho.ProdutoId = carrinho.ProdutoId;
            prodCarrinho.CarrinhoId = carrinho.Id;

            var carrinhoCompra = _context.CarrinhoDeCompras.Where(c => c.Id == prodCarrinho.CarrinhoId).FirstOrDefault();
            var produto = _context.Produtos.Where(p => p.Id == prodCarrinho.ProdutoId).FirstOrDefault();

            carrinhoCompra.ValorTotal = carrinho.Quantidade * produto.Valor;

            prodCarrinho.Produto = produto;
            prodCarrinho.Carrinho = carrinhoCompra;
          
            if(prodCarrinho.Produto.Estoque < carrinho.Quantidade)
            {
                return Result.Fail("A quantidade de produto em estoque é menor que a quantidade solicitada!");
            }
            if(prodCarrinho.Produto.Status != true)
            {
                return Result.Fail("Não é possível adicionar um produto inativo ao carrinho!");
            }
            _context.ProdutoCarrinhos.Add(prodCarrinho);
            _context.SaveChanges();
            return Result.Ok();
        }

        public CarrinhoDeCompra AlterarQuantidade(int carrinhoId, int produtoId, int quantidade)
        {
            var carrinho = _context.CarrinhoDeCompras.Where(c => c.Id == carrinhoId).FirstOrDefault();
            var produto = _context.Produtos.Where(p => p.Id == produtoId).FirstOrDefault();

            if(carrinho == null || produto == null)
            {
                throw new NullException();
            }

            if(produto.Status == false)
            {
                throw new StatusException();
            }

            if (quantidade == 0)
            {
                _context.Remove(carrinho);
                _context.SaveChanges();
                return carrinho;
            }
            carrinho.Quantidade = quantidade;
            carrinho.ValorTotal = produto.Valor * quantidade;
            _context.Update(carrinho);
            _context.SaveChanges();
            return carrinho;
        }

        public Result AdicionarProduto(int carrinhoId, int produtoId)
        {
            ProdutoCarrinho prodCarrinho = new ProdutoCarrinho();
            var carrinho = _context.CarrinhoDeCompras.Where(c => c.Id == carrinhoId).FirstOrDefault();
            var produto = _context.Produtos.Where(p => p.Id == produtoId).FirstOrDefault();
            prodCarrinho.Carrinho = carrinho;
            prodCarrinho.Produto = produto;
            if(carrinho == null || produto == null)
            {
                throw new NullException();
            }
            _context.ProdutoCarrinhos.Add(prodCarrinho);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result DeletarCarrinho(int? id)
        {
            var carrinho = _context.CarrinhoDeCompras.FirstOrDefault(c => c.Id == id);
            if (carrinho == null)
            {
                throw new NullException();
            }
            _context.CarrinhoDeCompras.Remove(carrinho);
            _context.SaveChanges();
            return Result.Ok();
        }

        public LerCarrinhoDto PesquisarCarrinho(CarrinhoDeCompra carrinho)
        {
            var pesquisa = _context.CarrinhoDeCompras.FirstOrDefault(c => c.Id == carrinho.Id);
            var carrinhoDto = _mapper.Map<LerCarrinhoDto>(pesquisa);
            return carrinhoDto;
        }
    }
}

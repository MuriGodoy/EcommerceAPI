using AutoMapper;
using EcommerceAPI.Data;
using EcommerceAPI.Data.Dtos.ProdutoDto;
using EcommerceAPI.Modelo;
using FluentResults;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Dapper;
using EcommerceAPI.Exceptions;

namespace EcommerceAPI.Services
{
    public class ProdutoService
    {
        private readonly EcommerceDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDbConnection _dbConnection;

        public ProdutoService(EcommerceDbContext context, IMapper mapper, IDbConnection dbConnection)
        {
            _context = context;
            _mapper = mapper;
            _dbConnection = dbConnection;
        }

        public LerProdutoDto CadastrarProdutos(CriarProdutoDto dto)
        {
            if (dto.Status != true)
            {
                throw new StatusException("Não é possível cadastrar um produto com status inativo!");
            }
            Subcategoria subcat = _context.Subcategorias.FirstOrDefault(subcat => subcat.Id == dto.SubcategoriaId);
            if (subcat.Status == false)
            {
                throw new StatusException("Não é possível cadastrar produto em uma subcategoria inativa!");
            }
            Produto produto = _mapper.Map<Produto>(dto);
            produto.CategoriaId = subcat.CategoriaId;
            _context.Produtos.Add(produto);
            _context.SaveChanges();
            return _mapper.Map<LerProdutoDto>(produto);
        }

        public Result EditarProdutos(int id, EditarProdutoDto produtoDto)
        {
            if (string.IsNullOrEmpty(produtoDto.Nome) || string.IsNullOrEmpty(produtoDto.Descricao))
            {
                throw new NullException("Não é possível editar produto com nome ou descrição em branco!");
            }
            Subcategoria subcategoria = _context.Subcategorias.FirstOrDefault(subcategoria => subcategoria.Id == produtoDto.SubcategoriaId);
            if (subcategoria.Status == false)
            {
                throw new StatusException("Não é possível editar produto que pertenca a uma subcategoria inativa");
            }
            Produto produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
            if (produto == null)
            {
                throw new NullException("Não foi possível encontrar um produto com o Id informado!");
            }
            _mapper.Map(produtoDto, produto);
            _context.SaveChanges();
            return Result.Ok();
        }

        public List<Produto> RecuperarProdutosComFiltros(FiltroProdutoDto filtroDto)
        {
            var sql = "SELECT * FROM Produtos WHERE ";
            _dbConnection.Open();
            if (filtroDto.Nome != null)
            {
                sql += "Nome LIKE \"%" + filtroDto.Nome + "%\" and ";
            }

            if (filtroDto.Status != null)
            {
                sql += "Status = @status and ";
            }

            if (filtroDto.Peso != null)
            {
                sql += "Peso = @peso and ";
            }

            if (filtroDto.Altura != null)
            {
                sql += "Altura = @altura and ";
            }

            if (filtroDto.Largura != null)
            {
                sql += "Largura = @largura and ";
            }

            if (filtroDto.Comprimento != null)
            {
                sql += "Comprimento = @comprimento and ";
            }

            if (filtroDto.Valor != null)
            {
                sql += "Valor = @valor and ";
            }

            if (filtroDto.Estoque != null)
            {
                sql += "Estoque LIKE \"%" + filtroDto.Estoque + "%\" and ";
            }
            if (filtroDto.Nome == null && filtroDto.Status == null && filtroDto.Peso == null && filtroDto.Altura == null && filtroDto.Largura == null && filtroDto.Comprimento == null
                && filtroDto.Valor == null && filtroDto.Estoque == null)
            {
                var removerWhere = sql.LastIndexOf("WHERE");
                sql = sql.Remove(removerWhere);
            }
            else
            {
                var removerAnd = sql.LastIndexOf("and");
                sql = sql.Remove(removerAnd);
            }

            if (filtroDto.Ordem != null)
            {
                if (filtroDto.Ordem == "crescente")
                {
                    sql += " ORDER BY Nome";
                }
                if (filtroDto.Ordem == "decrescente")
                {
                    sql += " ORDER BY Nome DESC";
                }
            }

            var resultado = _dbConnection.Query<Produto>(sql, new
            {
                Nome = filtroDto.Nome,
                Status = filtroDto.Status,
                Peso = filtroDto.Peso,
                Altura = filtroDto.Altura,
                Largura = filtroDto.Largura,
                Comprimento = filtroDto.Comprimento,
                Valor = filtroDto.Valor,
                Estoque = filtroDto.Estoque,
            }).ToList();

            if (filtroDto.PaginaAtual > 0 && filtroDto.PorPagina > 0)
            {
                var paginacao = resultado
                    .Skip((filtroDto.PaginaAtual - 1) * filtroDto.PorPagina)
                    .Take(filtroDto.PorPagina)
                    .ToList();
                _dbConnection.Close();
                return paginacao;
            }
            _dbConnection.Close();
            return resultado;

        }

        public Result DeletarProdutos(int id)
        {
            Produto produto = _context.Produtos.FirstOrDefault(produto => produto.Id == id);
            if (produto == null)
            {
                throw new NullException("Não foi possível encontrar um produto com o Id informado!");
            }
            _context.Remove(produto);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}

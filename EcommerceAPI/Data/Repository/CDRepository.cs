using AutoMapper;
using Dapper;
using EcommerceAPI.Data.Dtos.CentroDistribuicao;
using EcommerceAPI.Exceptions;
using EcommerceAPI.Modelo;
using FluentResults;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace EcommerceAPI.Data.Dao
{
    public class CDRepository
    {
        private readonly EcommerceDbContext _context;
        private readonly IMapper _mapper;
        private readonly IDbConnection _dbConnection;

        public CDRepository(EcommerceDbContext context, IMapper mapper, IDbConnection dbConnection)
        {
            _context = context;
            _mapper = mapper;
            _dbConnection = dbConnection;
        }

        public void CadastrarCentro(CriarCentroDto centroDto, CentroDistribuicao endereco, string logradouro)
        {
            var centro = _mapper.Map<CentroDistribuicao>(centroDto);

            centro.Logradouro = logradouro;
            centro.Bairro = endereco.Bairro;
            centro.Localidade = endereco.Localidade;
            centro.UF = endereco.UF;

            _context.CentroDistribuicoes.Add(centro);
            _context.SaveChanges();
        }
        public List<CentroDistribuicao> RecuperarCentro(FiltroCentroDto filtroDto)
        {
            var sql = "SELECT * FROM centrodistribuicoes WHERE ";
            _dbConnection.Open();
            if (filtroDto.Nome != null)
            {
                sql += "Nome LIKE \"%" + filtroDto.Nome + "%\" and ";
            }

            if (filtroDto.Status != null)
            {
                sql += "Status = @status and ";
            }

            if (filtroDto.Bairro != null)
            {
                sql += "Bairro LIKE \"%" + filtroDto.Bairro + "%\" and ";
            }

            if (filtroDto.Logradouro != null)
            {
                sql += "Logradouro LIKE \"%" + filtroDto.Logradouro + "%\" and ";
            }

            if (filtroDto.UF != null)
            {
                sql += "UF = @UF and ";
            }

            if (filtroDto.Cep != null)
            {
                sql += "Cep LIKE \"%" + filtroDto.Cep + "%\" and ";
            }

            if (filtroDto.Numero != null)
            {
                sql += "Numero = @numero and ";
            }

            if (filtroDto.Complemento != null)
            {
                sql += "Complemento LIKE \"%" + filtroDto.Complemento + "%\" and ";
            }

            if (filtroDto.Localidade != null)
            {
                sql += "Localidade LIKE \"%" + filtroDto.Localidade + "%\" and ";
            }

            if (filtroDto.Nome == null && filtroDto.Status == null && filtroDto.Complemento == null && filtroDto.Logradouro == null && filtroDto.Numero == null && filtroDto.Bairro == null
                && filtroDto.Localidade == null && filtroDto.UF == null && filtroDto.Cep == null)
            {
                var removerWhere = sql.LastIndexOf("WHERE");
                sql = sql.Remove(removerWhere);
            }
            else
            {
                var removerAnd = sql.LastIndexOf("and");
                sql = sql.Remove(removerAnd);
            }

            if (filtroDto.Ordem == null)
            {
                sql += " ORDER BY Nome";
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

            var resultado = _dbConnection.Query<CentroDistribuicao>(sql, new
            {
                Nome = filtroDto.Nome,
                Status = filtroDto.Status,
                Logradouro = filtroDto.Logradouro,
                Numero = filtroDto.Numero,
                Complemento = filtroDto.Complemento,
                Bairro = filtroDto.Bairro,
                Localidade = filtroDto.Localidade,
                UF = filtroDto.UF,
                CEP = filtroDto.Cep
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
        public Result EditarCentro(CentroDistribuicao editarCentro, int id)
        {
            CentroDistribuicao centro = _context.CentroDistribuicoes.FirstOrDefault(centro => centro.Id == id);
            if (centro == null)
            {
                throw new NullException("Não foi possível encontrar um Centro de Distribuição com o Id informado!");
            }

            //_mapper.Map(editarCentro, centro);
            _context.CentroDistribuicoes.Update(editarCentro);
            _context.SaveChanges();
            return Result.Ok();
        }
        public Result DeletarCentro(int id)
        {
            var centro = _context.CentroDistribuicoes.FirstOrDefault(centro => centro.Id == id);
            if (centro == null)
            {
                throw new NullException("Não foi possível encontrar um Centro de Distribuição com o id informado!");
            }
            _context.Remove(centro);
            _context.SaveChanges();
            return Result.Ok();
        }
    }
}

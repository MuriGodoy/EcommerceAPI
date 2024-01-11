using EcommerceAPI.Data.Dao;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Exceptions;
using FluentResults;
using System.Collections.Generic;
using System.Net;
using System.Text.RegularExpressions;
using System.Web.Http;

namespace EcommerceAPI.Services
{
    public class CategoriaService
    {
        private readonly CategoriaRepository _categoriaRepository;

        public CategoriaService(CategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }


        public Result CadastrarCategoria(CriarCategoriaDto categoriaDto)
        {
            if (categoriaDto.Status == true)
            {
                Result cadastro = _categoriaRepository.CadastrarCategoria(categoriaDto);
                return Result.Ok();
            }
            throw new StatusException("Não é possível cadastrar uma categoria com status inativo!");
        }

        public List<LerCategoriaDto> RecuperaCategorias()
        {
            return _categoriaRepository.RecuperaCategorias();
        }

        public List<LerCategoriaDto> RecuperaCategoriaPorNome(string nome)
        {
            if (string.IsNullOrEmpty(nome) || nome.Length < 3 || nome.Length > 128 || Regex.IsMatch(nome, "[^a-zA-Z]"))
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
                // Colocar um Null exception aqui ou criar outra exceção?
                // throw new NullException();
            }
            else
            {
                var pesquisa = _categoriaRepository.RecuperaCategoriaPorNome(nome);
                return pesquisa;
            }
        }

        public List<LerCategoriaDto> RecuperaCategoriaPorStatus(bool? status)
        {
            if (status != null)
            {
                var pesquisa = _categoriaRepository.RecuperaCategoriaPorStatus(status);
                return pesquisa;
            }
            else
            {
                throw new NullException("Não foi possível recuperar uma categoria com status nulo!");
            }
        }

        public List<LerCategoriaDto> RecuperaCategoriaPorID(int? id)
        {
            if (id != null)
            {
                var pesquisa = _categoriaRepository.RecuperaCategoriaPorID(id);
                return pesquisa;
            }
            else
            {
                throw new NullException("Não foi possível recuperar uma categoria com o Id nulo!");
            }
        }

        public Result EditarCategoria(int id, EditarCategoriaDto categoriaDto)
        {
            if (string.IsNullOrEmpty(categoriaDto.Nome) || categoriaDto.Nome.Length < 3 || categoriaDto.Nome.Length > 128 || Regex.IsMatch(categoriaDto.Nome, "[^a-zA-Z]"))
            {
                throw new NullException("Não é possível editar o nome da categoria da forma informada!");
            }
            var editaCategoria = _categoriaRepository.EditarCategoria(id, categoriaDto);
            if (editaCategoria.IsFailed)
            {
                return Result.Fail("Não foi possível encontrar uma categoria com o id informado!");
            }
            return Result.Ok();
        }

        public Result EditarStatus(int id)
        {
            var editaStatus = _categoriaRepository.EditaStatus(id);
            if (editaStatus.IsFailed)
            {
                return Result.Fail(editaStatus.Errors);
            }
            return Result.Ok();
        }

        public Result DeletaCategoria(int id)
        {
            var deletaCategoria = _categoriaRepository.DeletaCategoria(id);
            if (deletaCategoria.IsFailed)
            {
                return Result.Fail(deletaCategoria.Errors);
            }
            return Result.Ok();
        }
    }
}

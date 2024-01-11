using EcommerceAPI.Data.Dtos.Subcategoria;
using EcommerceAPI.Data.Dtos.SubcategoriaDtos;
using EcommerceAPI.Data.Repository;
using EcommerceAPI.Exceptions;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EcommerceAPI.Services
{
    public class SubcategoriaService
    {
        private readonly SubcategoriaRepository _subcategoriaRepository;
        public SubcategoriaService(SubcategoriaRepository subcategoriaRepository)
        {
            _subcategoriaRepository = subcategoriaRepository;
        }

        public Result CadastrarSubcategoria(CriarSubcategoriaDto dto)
        {
            if (dto.Status != true)
            {
                throw new StatusException("Não é possível cadastrar uma subcategoria com status inativo!");
            }
            var cadastroSubcategoria = _subcategoriaRepository.CadastrarSubcategoria(dto);
            if (cadastroSubcategoria.IsFailed)
            {
                return Result.Fail(cadastroSubcategoria.Errors.FirstOrDefault());
            }
            return Result.Ok();
        }

        public List<LerSubcategoriaDto> RecuperarSubcategorias()
        {
            var pesquisa = _subcategoriaRepository.RecuperarSubcategorias();
            return pesquisa;
        }

        public List<LerSubcategoriaDto> RecuperarSubcategoriaPorFiltros(FiltroSubcategoriaDto filtroDto)
        {
            if (filtroDto.Ordem == null && filtroDto.Status == null)
            {
                if (string.IsNullOrEmpty(filtroDto.Nome) || filtroDto.Nome.Length < 3 || filtroDto.Nome.Length > 128 || Regex.IsMatch(filtroDto.Nome, "[^a-zA-Z]"))
                {
                    return null;
                }
                var pesquisaFiltros = _subcategoriaRepository.RecuperarSubcategoriaPorFiltros(filtroDto);
                return pesquisaFiltros;
            }
            if (filtroDto.Ordem == "crescente" && filtroDto.Status != null)
            {
                if (string.IsNullOrEmpty(filtroDto.Nome) || filtroDto.Nome.Length < 3 || filtroDto.Nome.Length > 128 || Regex.IsMatch(filtroDto.Nome, "[^a-zA-Z]"))
                {
                    return null;
                }
                var pesquisaFiltros = _subcategoriaRepository.RecuperarSubcategoriaPorFiltros(filtroDto);
                return pesquisaFiltros;
            }
            if (filtroDto.Ordem == "decrescente" && filtroDto.Status != null)
            {
                if (string.IsNullOrEmpty(filtroDto.Nome) || filtroDto.Nome.Length < 3 || filtroDto.Nome.Length > 128 || Regex.IsMatch(filtroDto.Nome, "[^a-zA-Z]"))
                {
                    return null;
                }
                var pesquisaFiltros = _subcategoriaRepository.RecuperarSubcategoriaPorFiltros(filtroDto);
                return pesquisaFiltros;

            }
            if (filtroDto.Status != null && filtroDto.Nome != null && filtroDto.Ordem == null)
            {
                if (string.IsNullOrEmpty(filtroDto.Nome) || filtroDto.Nome.Length < 3 || filtroDto.Nome.Length > 128 || Regex.IsMatch(filtroDto.Nome, "[^a-zA-Z]"))
                {
                    return null;
                }
                var pesquisaFiltros = _subcategoriaRepository.RecuperarSubcategoriaPorFiltros(filtroDto);
                return pesquisaFiltros;

            }
            if (filtroDto.Ordem == "crescente" && filtroDto.Nome != null && filtroDto.Status == null)
            {
                if (string.IsNullOrEmpty(filtroDto.Nome) || filtroDto.Nome.Length < 3 || filtroDto.Nome.Length > 128 || Regex.IsMatch(filtroDto.Nome, "[^a-zA-Z]"))
                {
                    return null;
                }
                var pesquisaFiltros = _subcategoriaRepository.RecuperarSubcategoriaPorFiltros(filtroDto);

                return pesquisaFiltros;

            }
            if (filtroDto.Ordem == "decrescente" && filtroDto.Nome != null && filtroDto.Status == null)
            {
                if (string.IsNullOrEmpty(filtroDto.Nome) || filtroDto.Nome.Length < 3 || filtroDto.Nome.Length > 128 || Regex.IsMatch(filtroDto.Nome, "[^a-zA-Z]"))
                {
                    return null;
                }
                var pesquisaFiltros = _subcategoriaRepository.RecuperarSubcategoriaPorFiltros(filtroDto);
                return pesquisaFiltros;
            }
            return null;
        }

        public Result DeletarSubcategoria(int id)
        {
            var deletaSubcategoria = _subcategoriaRepository.DeletarSubcategoria(id);
            if (deletaSubcategoria.IsFailed)
            {
                return Result.Fail(deletaSubcategoria.Errors);
            }
            return Result.Ok();
            
        }

        public Result EditarStatus(int id)
        {
            var editaStatus = _subcategoriaRepository.EditarStatus(id);
            if (editaStatus.IsFailed)
            {
                return Result.Fail(editaStatus.Errors);
            }
            return Result.Ok();
        }

        public Result EditarSubcategoria(int id, EditarSubcategoriaDto subcategoriaDto)
        {
            var editaSubcategoria = _subcategoriaRepository.EditarSubcategoria(id, subcategoriaDto);
            if (editaSubcategoria.IsFailed)
            {
                return Result.Fail(editaSubcategoria.Errors.FirstOrDefault());
            }
            return Result.Ok();
        }

        public List<LerSubcategoriaDto> RecuperarSubcategoriaPorStatus(bool? status)
        {
            var pesquisaStatus = _subcategoriaRepository.RecuperarSubcategoriaPorStatus(status);
            return pesquisaStatus;
        }

        public List<LerSubcategoriaDto> RecuperarSubcategoriaPorId(int id)
        {
            var pesquisaId = _subcategoriaRepository.RecuperarSubcategoriaPorId(id);
            if (pesquisaId != null)
            {
                return pesquisaId;
            }
            throw new NullException("Não foi possível encontrar uma subcategoria com o Id informado!");
        }
    }
}

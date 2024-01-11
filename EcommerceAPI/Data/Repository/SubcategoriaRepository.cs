using AutoMapper;
using EcommerceAPI.Data.Dtos.Subcategoria;
using EcommerceAPI.Data.Dtos.SubcategoriaDtos;
using EcommerceAPI.Exceptions;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Modelo;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EcommerceAPI.Data.Repository
{
    public class SubcategoriaRepository : ISubcategoriaRepository
    {
        private readonly EcommerceDbContext _context;
        private readonly IMapper _mapper;
        public SubcategoriaRepository(EcommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Result CadastrarSubcategoria(CriarSubcategoriaDto dto)
        {
            var categoria = _context.Categorias.FirstOrDefault(subcat => subcat.Id == dto.CategoriaId);
            if (categoria.Status == false)
            {
                return Result.Fail("Não é possível cadastrar uma subcategoria em uma categoria inativa!");
            }
            Subcategoria subcategoria = _mapper.Map<Subcategoria>(dto);
            _context.Subcategorias.Add(subcategoria);
            _context.SaveChanges();
            return Result.Ok();
        }
        public List<LerSubcategoriaDto> RecuperarSubcategorias()
        {
            var pesquisa = _context.Subcategorias.ToList();
            List<LerSubcategoriaDto> subcategoriaDto = _mapper.Map<List<LerSubcategoriaDto>>(pesquisa).ToList();
            return subcategoriaDto;
        }
        public List<LerSubcategoriaDto> RecuperarSubcategoriaPorFiltros(FiltroSubcategoriaDto filtroDto)
        {
            if (filtroDto.Ordem == null && filtroDto.Status == null)
            {
                var subcategoria = _context.Subcategorias.Where(subcategoria => subcategoria.Nome.ToLower().Contains(filtroDto.Nome.ToLower())).ToList();
                List<LerSubcategoriaDto> subcategoriaDto = _mapper.Map<List<LerSubcategoriaDto>>(subcategoria);
                return subcategoriaDto;
            }
            if (filtroDto.Ordem == "crescente" && filtroDto.Status != null)
            {
                var subcategoriaCrescente = _context.Subcategorias.Where(subcategoria => subcategoria.Nome.ToLower()
                    .Contains(filtroDto.Nome.ToLower()) && subcategoria.Status == filtroDto.Status)
                    .OrderBy(crescente => crescente.Nome)
                    .Skip((filtroDto.PaginaAtual - 1) * filtroDto.PorPagina)
                    .Take(filtroDto.PorPagina)
                    .ToList();
                List<LerSubcategoriaDto> subcategoriaDto = _mapper.Map<List<LerSubcategoriaDto>>(subcategoriaCrescente);
                return subcategoriaDto;
            }
            if (filtroDto.Ordem == "decrescente" && filtroDto.Status != null)
            {
                var subcategoriaDecrescente = _context.Subcategorias.Where(subcategoria => subcategoria.Nome.ToLower()
                    .Contains(filtroDto.Nome.ToLower()) && subcategoria.Status == filtroDto.Status)
                    .OrderByDescending(decrescente => decrescente.Nome)
                    .Skip((filtroDto.PaginaAtual - 1) * filtroDto.PorPagina)
                    .Take(filtroDto.PorPagina)
                    .ToList();
                List<LerSubcategoriaDto> subcategoriaDto = _mapper.Map<List<LerSubcategoriaDto>>(subcategoriaDecrescente);
                return subcategoriaDto;
            }
            if (filtroDto.Status != null && filtroDto.Nome != null && filtroDto.Ordem == null)
            {
                var subcategoria = _context.Subcategorias.Where(subcategoria => subcategoria.Nome.ToLower()
                    .Contains(filtroDto.Nome.ToLower()) && subcategoria.Status == filtroDto.Status)
                    .Skip((filtroDto.PaginaAtual - 1) * filtroDto.PorPagina)
                    .Take(filtroDto.PorPagina)
                    .ToList();
                List<LerSubcategoriaDto> subcategoriaDto = _mapper.Map<List<LerSubcategoriaDto>>(subcategoria);
                return subcategoriaDto;
            }
            if (filtroDto.Ordem == "crescente" && filtroDto.Nome != null && filtroDto.Status == null)
            {
                var subcategoriaCrescente = _context.Subcategorias.Where(subcategoria => subcategoria.Nome.ToLower()
                    .Contains(filtroDto.Nome.ToLower())).OrderBy(crescente => crescente.Nome)
                    .Skip((filtroDto.PaginaAtual - 1) * filtroDto.PorPagina)
                    .Take(filtroDto.PorPagina)
                    .ToList();
                List<LerSubcategoriaDto> subcategoriaDto = _mapper.Map<List<LerSubcategoriaDto>>(subcategoriaCrescente);
                return subcategoriaDto;
            }
            if (filtroDto.Ordem == "decrescente" && filtroDto.Nome != null && filtroDto.Status == null)
            {
                var subcategoriaDecrescente = _context.Subcategorias.Where(subcategoria => subcategoria.Nome.ToLower()
                    .Contains(filtroDto.Nome.ToLower())).OrderByDescending(decrescente => decrescente.Nome)
                    .Skip((filtroDto.PaginaAtual - 1) * filtroDto.PorPagina)
                    .Take(filtroDto.PorPagina)
                    .ToList();
                List<LerSubcategoriaDto> subcategoriaDto = _mapper.Map<List<LerSubcategoriaDto>>(subcategoriaDecrescente);

                return subcategoriaDto;
            }
            throw new NullException("Não foi possível encontrar uma subcategoria com os filtros informados!");
        }

        public Result DeletarSubcategoria(int id)
        {
            Subcategoria subcategoria = _context.Subcategorias.FirstOrDefault(subcategoria => subcategoria.Id == id);
            if (subcategoria == null)
            {
                return Result.Fail("Não foi possível encontrar uma subcategoria com o id informado!");
            }
            _context.Remove(subcategoria);
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result EditarStatus(int id)
        {
            Subcategoria subcategoria = _context.Subcategorias.FirstOrDefault(subcategoria => subcategoria.Id == id);
            List<Produto> produtos = _context.Produtos.Where(produto => produto.SubcategoriaId == id && produto.Status == true).ToList();
            if (produtos.Count > 0)
            {
                return Result.Fail("Não é possível inativar uma subcategoria com produtos ativos");
            }
            if (subcategoria.Status == false)
            {
                subcategoria.Status = true;
            }
            else
            {
                subcategoria.Status = false;
                foreach (Produto produto in produtos)
                {
                    if (subcategoria.Status == false)
                    {
                        produto.Status = false;
                    }
                }
            }
            _context.SaveChanges();
            return Result.Ok();
        }

        public Result EditarSubcategoria(int id, EditarSubcategoriaDto subcategoriaDto)
        {
            Subcategoria subcategoria = _context.Subcategorias.FirstOrDefault(subcategoria => subcategoria.Id == id);
            if (subcategoria == null)
            {
                return Result.Fail("Não foi possível identificar uma subcategoria com o id informado!");
            }
            _mapper.Map(subcategoriaDto, subcategoria);
            _context.SaveChanges();
            return Result.Ok();
        }

        public List<LerSubcategoriaDto> RecuperarSubcategoriaPorStatus(bool? status)
        {
            if (status != null)
            {
                List<Subcategoria> subcategoria = _context.Subcategorias.Where(sub => sub.Status == status).ToList();
                List<LerSubcategoriaDto> subcategoriaDto = _mapper.Map<List<LerSubcategoriaDto>>(subcategoria);
                return subcategoriaDto;
            }
            throw new NullException("Não foi possível encontrar uma subcategoria com o status informado!");
        }

        public List<LerSubcategoriaDto> RecuperarSubcategoriaPorId(int id)
        {
            List<Subcategoria> subcategoria = _context.Subcategorias.Where(sub => sub.Id == id).ToList();
            if (subcategoria != null)
            {
                List<LerSubcategoriaDto> subcategoriaDto = _mapper.Map<List<LerSubcategoriaDto>>(subcategoria);
                return subcategoriaDto;
            }
            throw new NullException("Não foi possível encontrar uma subcategoria com o Id informado!");
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

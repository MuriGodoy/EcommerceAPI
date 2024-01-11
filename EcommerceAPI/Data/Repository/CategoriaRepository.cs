using AutoMapper;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Exceptions;
using EcommerceAPI.Interfaces;
using EcommerceAPI.Modelo;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EcommerceAPI.Data.Dao
{
    public class CategoriaRepository : ICategoriaRepository
    {
        private readonly EcommerceDbContext _context;
        private readonly IMapper _mapper;

        public CategoriaRepository(EcommerceDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Result CadastrarCategoria(CriarCategoriaDto categoriaDto)
        {
            Categoria categoria = _mapper.Map<Categoria>(categoriaDto);
            _context.Categorias.Add(categoria);
            _context.SaveChanges();
            return Result.Ok();
        }
        public List<LerCategoriaDto> RecuperaCategorias()
        {
            var categoria = _context.Categorias.ToList();
            List<LerCategoriaDto> CategoriasDtos = _mapper.Map<List<LerCategoriaDto>>(categoria).ToList();
            return CategoriasDtos;
        }
        public List<LerCategoriaDto> RecuperaCategoriaPorNome(string nome)
        {
            var categoria = _context.Categorias.Where(categoria => categoria.Nome.ToLower().Contains(nome.ToLower())).ToList();
            List<LerCategoriaDto> categoriaDto = _mapper.Map<List<LerCategoriaDto>>(categoria);
            return categoriaDto.ToList();
        }
        public List<LerCategoriaDto> RecuperaCategoriaPorStatus(bool? status)
        {
            List<Categoria> categoria = _context.Categorias.Where(S => S.Status == status).ToList();
            if(categoria != null)
            {
                List<LerCategoriaDto> categoriaDto = _mapper.Map<List<LerCategoriaDto>>(categoria);
                return categoriaDto.ToList();
            }
            throw new NullException("Não foi possível encontrar uma categoria com status nulo!");
        }
        public List<LerCategoriaDto> RecuperaCategoriaPorID(int? id)
        {
            List<Categoria> categoria = _context.Categorias.Where(categoria => categoria.Id == id).ToList();
            if (categoria != null)
            {
                List<LerCategoriaDto> categoriaDto = _mapper.Map<List<LerCategoriaDto>>(categoria);
                return categoriaDto.ToList();
            }
            throw new NullException("Não foi possível encontrar uma categoria com o id nulo!");
        }
        public Result EditarCategoria(int id, EditarCategoriaDto categoriaDto)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            if (categoria == null)
            {
                return Result.Fail("Não foi possível encontrar uma categoria com o id informado!");
            }
            _mapper.Map(categoriaDto, categoria);
            _context.SaveChanges();
            return Result.Ok();
        }
        public Result EditaStatus(int id)
        {
            var categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            var subcategorias = _context.Subcategorias.Where(subcategorias => subcategorias.CategoriaId == id).ToList();
            if (categoria.Status == false)
            {
                categoria.Status = true;
            }
            else
            {
                categoria.Status = false;
                foreach (Subcategoria subcategoria in subcategorias)
                {
                    if (categoria.Status == false)
                    {
                        subcategoria.Status = false;
                    }
                }
            }
            _context.SaveChanges();
            return Result.Ok();
        }
        public Result DeletaCategoria(int id)
        {
            Categoria categoria = _context.Categorias.FirstOrDefault(categoria => categoria.Id == id);
            if (categoria == null)
            {
                return Result.Fail("Não foi possível encontrar uma categoria com o id informado1");
            }
            _context.Remove(categoria);
            _context.SaveChanges();
            return Result.Ok();
        }
        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}

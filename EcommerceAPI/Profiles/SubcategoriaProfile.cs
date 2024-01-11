using AutoMapper;
using EcommerceAPI.Data.Dtos.Subcategoria;
using EcommerceAPI.Modelo;
using System.Linq;

namespace EcommerceAPI.Profiles
{
    public class SubcategoriaProfile : Profile
    {
        public SubcategoriaProfile()
        {
            CreateMap<CriarSubcategoriaDto, Subcategoria>();
            CreateMap<Subcategoria, LerSubcategoriaDto>();
            CreateMap<EditarSubcategoriaDto, Subcategoria>();
        }
    }
}

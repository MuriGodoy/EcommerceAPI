using AutoMapper;
using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Modelo;
using System.Linq;

namespace EcommerceAPI.Profiles
{
    public class CategoriaProfile : Profile
    {
        public CategoriaProfile()
        {
            CreateMap<CriarCategoriaDto, Categoria>();
            CreateMap<Categoria, LerCategoriaDto>();
            CreateMap<EditarCategoriaDto, Categoria>();
            CreateMap<Categoria, LerCategoriaDto>()
                .ForMember(categoria => categoria.Subcategorias, opts => opts
                .MapFrom(categoria => categoria.Subcategorias.Select
                (s => new { s.Id, s.Nome, s.Criacao, s.Modificacao, s.Status})));
        }
    }
}

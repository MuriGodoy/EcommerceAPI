using EcommerceAPI.Modelo;
using AutoMapper;
using EcommerceAPI.Data.Dtos.ProdutoDto;
using System.Linq;

namespace EcommerceAPI.Profiles
{
    public class ProdutoProfile : Profile
    {
        public ProdutoProfile()
        {
            CreateMap<CriarProdutoDto, Produto>();
            CreateMap<EditarProdutoDto, Produto>();
            CreateMap<Produto, LerProdutoDto>();
        }
    }
}

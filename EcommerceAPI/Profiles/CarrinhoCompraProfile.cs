using AutoMapper;
using EcommerceAPI.Data.Dtos.CarrinhoDto;
using EcommerceAPI.Modelo;
using System.Linq;

namespace EcommerceAPI.Profiles
{
    public class CarrinhoCompraProfile : Profile
    {
        public CarrinhoCompraProfile()
        {
            CreateMap<CriarCarrinhoDto, CarrinhoDeCompra>();
            CreateMap<LerCarrinhoDto, CarrinhoDeCompra>();
            CreateMap<CarrinhoDeCompra, ProdutoCarrinho>();
            CreateMap<CarrinhoDeCompra, LerCarrinhoDto>();
        }
    }
}

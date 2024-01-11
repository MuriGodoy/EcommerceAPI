using AutoMapper;
using EcommerceAPI.Data.Dtos.CentroDistribuicao;
using EcommerceAPI.Modelo;

namespace EcommerceAPI.Profiles
{
    public class CentroDistribuicaoProfile : Profile
    {
        public CentroDistribuicaoProfile()
        {
            CreateMap<CriarCentroDto, CentroDistribuicao>();
            CreateMap<EditarCentroDto, CentroDistribuicao>();
            CreateMap<CentroDistribuicao, LerCentroDto>();
        }
    }
}

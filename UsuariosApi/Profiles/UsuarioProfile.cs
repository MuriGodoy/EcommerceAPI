using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.Dto;
using UsuariosApi.Models;

namespace UsuariosApi.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CreateUsuarioDto, Usuario>();
            CreateMap<Usuario, IdentityUser<int>>();
            CreateMap<Usuario, CustomIdentityUser>();
            CreateMap<EditarUsuarioDto, Usuario>();
            CreateMap<EditarUsuarioDto, CustomIdentityUser>();
            CreateMap<ReadUsuarioDto, Usuario>();
            CreateMap<ReadUsuarioDto, CustomIdentityUser>();
            CreateMap<CustomIdentityUser, ReadUsuarioDto>();
            CreateMap<CustomIdentityUser, IdentityUser<int>>();
            CreateMap<IdentityUser<int>, CustomIdentityUser>();
        }
    }
}

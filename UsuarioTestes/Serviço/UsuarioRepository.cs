using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApi.Data.Dto;
using UsuariosApi.Models;

namespace UsuarioTestes.Serviço
{
    public class UsuarioRepository
    {
        private readonly SignInManager<CustomIdentityUser> _signInManager;


        private List<CreateUsuarioDto> lerUsuario = new List<CreateUsuarioDto>()
        {
            new CreateUsuarioDto()
            {
                Nome = "teste1",
                UserName = "teste1user",
                Password = "Senha123*",
                Email = "muriteste@teste.com"
            }
        };

        public List<CreateUsuarioDto> LerUsuario { get { return lerUsuario; } }


    }
}


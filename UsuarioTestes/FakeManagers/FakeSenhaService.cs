using Microsoft.AspNetCore.Identity;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApi.Models;
using UsuariosApi.Services;

namespace UsuarioTestes.FakeManagers
{
    public class FakeSenhaService : TrocaSenhaService
    {
        public FakeSenhaService() : base(new Mock<FakeSignInManager>().Object)
        {

        }
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuariosApi.Models;

namespace UsuarioTestes.FakeManagers
{
    public class FakeUserManager : UserManager<CustomIdentityUser>
    {
        public FakeUserManager()
            : base(new Mock<IUserStore<CustomIdentityUser>>().Object,
                  new Mock<IOptions<IdentityOptions>>().Object,
                  new Mock<IPasswordHasher<CustomIdentityUser>>().Object,
                  new IUserValidator<CustomIdentityUser>[0],
                  new IPasswordValidator<CustomIdentityUser>[0],
                  new Mock<ILookupNormalizer>().Object,
                  new Mock<IdentityErrorDescriber>().Object,
                  new Mock<IServiceProvider>().Object,
                  new Mock<ILogger<UserManager<CustomIdentityUser>>>().Object)
        {
        }
    }
}

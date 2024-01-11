using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using UsuariosApi.Data;
using UsuariosApi.Data.Request;
using UsuariosApi.Models;
using UsuariosApi.Services;
using Xunit;

namespace UsuarioTestes.FakeManagers
{
    public class InjectFixture : IDisposable
    {
        public readonly UserManager<CustomIdentityUser> _userManager;
        public readonly SignInManager<CustomIdentityUser> _signInManager;
        public readonly UserDbContext _dbContext;
        public readonly IConfiguration _configuration;
        public readonly TrocaSenhaService _senhaService;
        public readonly TrocaSenhaRequest _senhaRequest;

        public InjectFixture()
        {
            var options = new DbContextOptionsBuilder<UserDbContext>()
                .UseInMemoryDatabase(databaseName: "fakeDb")
                .Options;

            _dbContext = new UserDbContext(options, _configuration);

            var users = new List<CustomIdentityUser>
            {
                new CustomIdentityUser
                {
                    UserName = "teste",
                    Email = "teste@teste.com"
                }
            }.AsQueryable();

            var fakeUserManager = new Mock<FakeUserManager>();

            fakeUserManager.Setup(x => x.Users)
                .Returns(users);

            fakeUserManager.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync(new CustomIdentityUser()
                {
                    UserName = "teste"
                });

            fakeUserManager.Setup(x => x.ChangePasswordAsync(It.IsAny<CustomIdentityUser>(), It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new IdentityResult());

            fakeUserManager.Setup(x => x.IsInRoleAsync(It.IsAny<CustomIdentityUser>(), "regular"))
                .ReturnsAsync(new bool());

            fakeUserManager.Setup(x => x.AddToRoleAsync(It.IsAny<CustomIdentityUser>(), It.IsAny<string>()))
                .ReturnsAsync(new IdentityResult());
            
            var senhaService = new Mock<FakeSenhaService>();

            var senhaRequest = new Mock<TrocaSenhaRequest>();
             
            _userManager = fakeUserManager.Object;
            _senhaService = senhaService.Object;
            _senhaRequest = senhaRequest.Object;
        }
        public void Dispose()
        {
            _userManager?.Dispose();
            _dbContext?.Dispose();
        }
    }
}

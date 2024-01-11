using UsuariosApi.Data.Request;
using UsuarioTestes.FakeManagers;
using Xunit;

namespace UsuarioTestes
{
    public class UsuarioTestes
    {
        private readonly InjectFixture _fixture;

        public UsuarioTestes()
        {
            _fixture = new InjectFixture();
        }

        [Fact]
        public void TestaTrocaSenha()
        {
            var trocaSenha = new TrocaSenhaRequest()
            {
                Email = "muriteste@teste.com",
                Password = "Senha1234*",
                NewPassword = "Senha123*",
                ReNewPassword = "Senha123*"
            };

            var usuario = _fixture._userManager.FindByEmailAsync(trocaSenha.Email);
            var senhaNova = _fixture._userManager.ChangePasswordAsync(usuario.Result, trocaSenha.Password, trocaSenha.NewPassword);
            Assert.NotNull(usuario);
            Assert.NotNull(senhaNova);
        }

        [Fact]
        public void TestaTrocaDePermissao()
        {
            var trocaPermissao = new TrocaRoleRequest()
            {
                Email = "muriteste@teste.com",
                Role = "admin"
            };

            var usuario = _fixture._userManager.FindByEmailAsync(trocaPermissao.Email);
            var usuarioRegular = _fixture._userManager.IsInRoleAsync(usuario.Result, "regular");
            var roleNova = _fixture._userManager.AddToRoleAsync(usuario.Result, trocaPermissao.Role);
            
            Assert.False(usuarioRegular.Result);
            Assert.NotNull(roleNova);
        }
    }
}

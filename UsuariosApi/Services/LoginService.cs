using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using UsuariosApi.Data.Request;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class LoginService
    {
        private readonly SignInManager<CustomIdentityUser> _signInManager;
        private readonly TokenService _tokenService;

        public LoginService(SignInManager<CustomIdentityUser> signInManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public Result LogaUsuario(LoginRequest request)
        {
            var usuario = _signInManager.UserManager.FindByEmailAsync(request.Email);
            var resultadoIdentity = _signInManager
                .PasswordSignInAsync(usuario.Result.UserName, request.Password, false, false);
            if (resultadoIdentity.Result.Succeeded)
            {
                var identityUser = _signInManager
                    .UserManager
                    .Users
                    .FirstOrDefault(user =>
                    user.NormalizedUserName == usuario.Result.UserName.ToUpper());
                Token token = _tokenService
                    .CreateToken(identityUser, _signInManager
                    .UserManager
                    .GetRolesAsync(identityUser)
                    .Result
                    .ToList());
                return Result.Ok().WithSuccess(token.Value);
            }
            return Result.Fail("Email e senha incorretos!");
        }
    }
}
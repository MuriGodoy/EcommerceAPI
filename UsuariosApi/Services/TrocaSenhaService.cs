using FluentResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using UsuariosApi.Data.Request;
using UsuariosApi.Interfaces;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class TrocaSenhaService
    {
        private readonly SignInManager<CustomIdentityUser> _signInManager;

        public TrocaSenhaService(SignInManager<CustomIdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public Result TrocaSenha(TrocaSenhaRequest request)
        {
            var identityUser = _signInManager.UserManager.FindByEmailAsync(request.Email).Result;
            var resultadoIdentity = _signInManager
                .UserManager
                .ChangePasswordAsync(identityUser, request.Password, request.NewPassword).Result;
            if (resultadoIdentity.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Não foi possível alterar a senha!");
        }
    }
}

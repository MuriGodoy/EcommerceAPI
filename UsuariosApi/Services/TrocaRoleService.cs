using FluentResults;
using Microsoft.AspNetCore.Identity;
using System;
using System.Threading.Tasks;
using UsuariosApi.Data.Request;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class TrocaRoleService
    {
        private readonly SignInManager<CustomIdentityUser> _signInManager;

        public TrocaRoleService(SignInManager<CustomIdentityUser> signInManager)
        {
            _signInManager = signInManager;
        }
        public Result TrocaRole(TrocaRoleRequest request)
        {
            var identityUser = _signInManager.UserManager.FindByEmailAsync(request.Email).Result;
            var usuarioRegular = _signInManager.UserManager.IsInRoleAsync(identityUser, "regular");
            if (usuarioRegular.Result == true)
            {
                return Result.Fail("Usuários com permissão de cliente não podem ter a permissão alterada!");
            }
            var identityResult = _signInManager.UserManager.AddToRoleAsync(identityUser, request.Role).Result;
            if (identityUser == null)
            {
                return Result.Fail("Não existe um usuário com o e-mail indicado!");
            }
            if (identityResult.Succeeded)
            {
                return Result.Ok();
            }
            return Result.Fail("Não foi possível trocar a role do usuário indicado!");
        }
    }
}

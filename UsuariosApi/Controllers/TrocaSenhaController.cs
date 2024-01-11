using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UsuariosApi.Data.Request;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrocaSenhaController : ControllerBase
    {
        private readonly TrocaSenhaService _senhaService;

        public TrocaSenhaController(TrocaSenhaService senhaService)
        {
            _senhaService = senhaService;
        }

        [HttpPost]
        public IActionResult TrocaSenha(TrocaSenhaRequest request)
        {
            Result resultado = _senhaService.TrocaSenha(request);
            if (resultado.IsFailed)
            {
                return Unauthorized(resultado.Errors.FirstOrDefault());
            }
            return Ok("Troca de senha realizada com sucesso!");
        }
    }
}

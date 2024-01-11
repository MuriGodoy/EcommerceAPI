using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using UsuariosApi.Data.Request;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class TrocaRoleController : ControllerBase
    {
        private readonly TrocaRoleService _service;

        public TrocaRoleController(TrocaRoleService service)
        {
            _service = service;
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public IActionResult TrocaRole(TrocaRoleRequest request)
        {
            Result resultado = _service.TrocaRole(request);
            if (resultado.IsFailed)
            {
                return Unauthorized(resultado.Reasons);
            }
            return Ok("Troca de permissão realizada com sucesso!");
        }

    }
}

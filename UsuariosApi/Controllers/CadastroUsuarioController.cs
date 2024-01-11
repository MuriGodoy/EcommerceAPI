using FluentResults;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosApi.Data;
using UsuariosApi.Data.Dto;
using UsuariosApi.Data.Request;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CadastroUsuarioController : ControllerBase
    {
        private readonly CadastroUsuarioService _cadastroService;

        public CadastroUsuarioController(CadastroUsuarioService cadastroService)
        {
            _cadastroService = cadastroService;
        }

        [HttpPost("/cadastra")]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDto createDto)
        {
            Result resultado = await _cadastroService.CadastraUsuario(createDto);
            if (resultado.IsFailed) return BadRequest(resultado.Errors.FirstOrDefault());
            return Ok(resultado.Successes.FirstOrDefault());
        }

        [HttpPost("/ativa")]
        public IActionResult AtivaContaUsuario([FromQuery] AtivaContaRequest request)
        {
            Result resultado = _cadastroService.AtivaContaUsuario(request);
            if (resultado.IsFailed)
            {
                return StatusCode(500);
            }
            return Ok(resultado.Successes);

        }

        [Authorize(Roles = "admin,regular")]
        [HttpPut("/{id}")]
        public async Task<Result> EditaUsuario(int id, [FromBody] EditarUsuarioDto editarDto)
        {
            Result lerDto = await _cadastroService.EditaUsuario(id, editarDto);
            return Result.Ok();
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public async Task<IActionResult> PesquisaUsuarios([FromQuery] FiltroDto filtroDto)
        {
            List<ReadUsuarioDto> resultado = await _cadastroService.PesquisaUsuarios(filtroDto);
            if (resultado == null) return NotFound();
            return Ok(resultado);
        }
    }
}
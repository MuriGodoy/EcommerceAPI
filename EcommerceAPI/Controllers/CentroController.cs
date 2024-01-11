using EcommerceAPI.Data.Dtos.CentroDistribuicao;
using EcommerceAPI.Modelo;
using EcommerceAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CentroController : ControllerBase
    {
        private readonly CentroService _centroService;

        public CentroController(CentroService centroService)
        {
            _centroService = centroService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarCD([FromBody] CriarCentroDto centroDto)
        {
            LerCentroDto lerDto = await _centroService.CadastrarCentro(centroDto);
            return CreatedAtAction(nameof(RecuperarCD), new { nome = centroDto.Nome }, centroDto);
        }

        [HttpGet("pesquisar")]
        public List<CentroDistribuicao> RecuperarCD([FromBody] FiltroCentroDto filtroDto)
        {
            return _centroService.RecuperarCentro(filtroDto);
        }

        [HttpPut("editar/{id}")]
        public Result EditarCD(int id, [FromBody] EditarCentroDto editarDto)
        {
            var lerDto = _centroService.EditarCentro(id, editarDto);
            if (lerDto.Result.IsFailed)
            {
                return Result.Fail(lerDto.Result.Errors);
            }
            return Result.Ok();
        }

        [HttpDelete("deletar/{id}")]
        public IActionResult DeletarCD(int id)
        {
            var deletarDto = _centroService.DeletarCentro(id);
            if (deletarDto.IsFailed)
            {
                return BadRequest("Não foi possível excluir o Centro de distribuição indicado!");
            }
            return NoContent();
        }
    }
}
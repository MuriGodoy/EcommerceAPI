using AutoMapper;
using EcommerceAPI.Data;
using EcommerceAPI.Data.Dtos.Subcategoria;
using EcommerceAPI.Data.Dtos.SubcategoriaDtos;
using EcommerceAPI.Modelo;
using EcommerceAPI.Services;
using FluentResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EcommerceAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SubcategoriaController : ControllerBase
    {
        private readonly SubcategoriaService _subcategoriaService;
        public SubcategoriaController(SubcategoriaService subcategoriaService)
        {
            _subcategoriaService = subcategoriaService;
        }

        [HttpPost]
        public IActionResult CadastrarSubcategoria([FromBody] CriarSubcategoriaDto dto)
        {
            var cadastroSubcategoria = _subcategoriaService.CadastrarSubcategoria(dto);
            return CreatedAtAction(nameof(RecuperarSubcategoriaPorFiltros), new { nome = dto.Nome }, dto);
        }

        [HttpGet]
        public List<LerSubcategoriaDto> RecuperarSubcategorias()
        {
            var pesquisa = _subcategoriaService.RecuperarSubcategorias();
            return pesquisa;
        }

        [HttpGet("pesquisar")]
        public IActionResult RecuperarSubcategoriaPorFiltros([FromQuery]FiltroSubcategoriaDto filtroDto)
        {
            var pesquisaFiltros = _subcategoriaService.RecuperarSubcategoriaPorFiltros(filtroDto);
            if(pesquisaFiltros == null || pesquisaFiltros.Count == 0)
            {
                return NotFound("Não foi possível encontrar a subcategoria desejada!");
            }
            return Ok(pesquisaFiltros);
        }

        [HttpGet("pesquisarid/{id}")]
        public IActionResult RecuperarSubcategoriaPorId(int id)
        {
            var pesquisaId = _subcategoriaService.RecuperarSubcategoriaPorId(id);
            if (pesquisaId == null || pesquisaId.Count == 0)
            {
                return NotFound("Não foi possível encontrar a subcategoria com o id informado!");
            }
            return Ok(pesquisaId);
        }

        [HttpGet("pesquisarstatus/{status}")]
        public IActionResult RecuperarSubcategoriaPorStatus(bool? status)
        {
            var pesquisaStatus = _subcategoriaService.RecuperarSubcategoriaPorStatus(status);
            if(pesquisaStatus == null || pesquisaStatus.Count == 0)
            {
                return NotFound("Não existem subcategorias com o status informado!");
            }
            return Ok(pesquisaStatus);
        }

        [HttpPut("{id}")]
        public IActionResult EditarSubcategoria(int id, [FromBody] EditarSubcategoriaDto subcategoriaDto)
        {
            var editaSubcategoria = _subcategoriaService.EditarSubcategoria(id, subcategoriaDto);
            if (editaSubcategoria.IsFailed)
            {
                return BadRequest(editaSubcategoria.Errors);
            }
            return NoContent();
        }

        [HttpPut("editarstatus/{id}")]
        public IActionResult EditarStatus(int id)
        {
            var editaStatus = _subcategoriaService.EditarStatus(id);
            if (editaStatus.IsFailed)
            {
                return BadRequest(editaStatus.Errors);
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletarSubcategoria(int id)
        {
            var deletaSubcategoria = _subcategoriaService.DeletarSubcategoria(id);
            if (deletaSubcategoria.IsFailed)
            {
                return BadRequest(deletaSubcategoria.Errors);
            }
            return NoContent();
        }
    }
}

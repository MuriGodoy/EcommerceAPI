using AutoMapper;
using EcommerceAPI.Data;
using EcommerceAPI.Data.Dtos;
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

    public class CategoriaController : ControllerBase
    {
        private readonly CategoriaService _service;

        public CategoriaController(CategoriaService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult CadastrarCategoria([FromBody] CriarCategoriaDto categoriaDto)
        {
            Result cadastro = _service.CadastrarCategoria(categoriaDto);
            return CreatedAtAction(nameof(RecuperaCategoriaPorNome), new { nome = categoriaDto.Nome }, categoriaDto);
        }

        [HttpGet]
        public IEnumerable<LerCategoriaDto> RecuperaCategorias()
        {
            List<LerCategoriaDto> pesquisaCategoria = _service.RecuperaCategorias();
            return (pesquisaCategoria);
        }

        [HttpGet("pesquisarnome/{nome}")]
        public IActionResult RecuperaCategoriaPorNome(string nome)
        {
            var pesquisaCategoria = _service.RecuperaCategoriaPorNome(nome);
            return Ok(pesquisaCategoria);
        }

        [HttpGet("pesquisarstatus/{status}")]
        public IActionResult RecuperaCategoriaPorStatus(bool? status)
        {
            var pesquisaCategoria = _service.RecuperaCategoriaPorStatus(status);
            return Ok(pesquisaCategoria);
        }

        [HttpGet("pesquisarid/{id}")]
        public List<LerCategoriaDto> RecuperaCategoriaPorID(int id)
        {
            var pesquisaCategoria = _service.RecuperaCategoriaPorID(id);
            return pesquisaCategoria;
        }

        [HttpPut("editar/{id}")]
        public IActionResult EditarCategoria(int id, [FromBody] EditarCategoriaDto categoriaDto)
        {
            var editaCategoria = _service.EditarCategoria(id, categoriaDto);
            if (editaCategoria.IsFailed)
            {
                return NotFound(editaCategoria.Errors.FirstOrDefault());
            }
            return NoContent();
        }

        [HttpPut("editarstatus/{id}")]
        public IActionResult EditarStatus(int id)
        {
            var editaStatus = _service.EditarStatus(id);
            if (editaStatus.IsFailed)
            {
                return NotFound(editaStatus.Errors.FirstOrDefault());
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeletaCategoria(int id)
        {
            var deletaCategoria = _service.DeletaCategoria(id);
            if (deletaCategoria.IsFailed)
            {
                return NotFound(deletaCategoria.Errors.FirstOrDefault());
            }
            return NoContent();
        }
    }
}

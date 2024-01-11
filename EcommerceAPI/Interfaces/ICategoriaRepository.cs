using EcommerceAPI.Data.Dtos;
using FluentResults;
using System;
using System.Collections.Generic;

namespace EcommerceAPI.Interfaces
{
    public interface ICategoriaRepository : IDisposable
    {
        public Result CadastrarCategoria(CriarCategoriaDto categoriaDto);
        public List<LerCategoriaDto> RecuperaCategorias();
        public List<LerCategoriaDto> RecuperaCategoriaPorNome(string nome);
        public List<LerCategoriaDto> RecuperaCategoriaPorStatus(bool? status);
        public List<LerCategoriaDto> RecuperaCategoriaPorID(int? id);
        public Result EditarCategoria(int id, EditarCategoriaDto categoriaDto);
        public Result EditaStatus(int id);
        public Result DeletaCategoria(int id);
    }
}

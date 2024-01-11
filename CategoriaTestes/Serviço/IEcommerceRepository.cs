using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Data.Dtos.Subcategoria;
using EcommerceAPI.Modelo;
using FluentResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CategoriaTestes.Serviço
{
    public interface IEcommerceRepository
    {

        public List<Categoria> RecuperaCategorias();
        public List<LerSubcategoriaDto> RecuperaSubcategorias();
    }
}

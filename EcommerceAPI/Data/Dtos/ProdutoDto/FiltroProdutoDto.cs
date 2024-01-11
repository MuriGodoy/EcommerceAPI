using EcommerceAPI.Modelo;
using Microsoft.AspNetCore.Mvc;
using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Data.Dtos.ProdutoDto
{
    public class FiltroProdutoDto
    {
        public string Nome { get; set; }
        public bool? Status { get; set; }
        public double? Peso { get; set; }
        public double? Altura { get; set; }
        public double? Largura { get; set; }
        public double? Comprimento { get; set; }
        public double? Valor { get; set; }
        public int? Estoque { get; set; }
        public string Ordem { get; set; }
        public int PorPagina { get; set; }
        public int PaginaAtual { get; set; }

    }
}
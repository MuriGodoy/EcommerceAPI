using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Data.Dtos.ProdutoDto
{
    public class EditarProdutoDto
    {
        [StringLength(128, ErrorMessage = "O tamanho do nome excede 128 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9'-'' '\s]{1,1000}$", ErrorMessage = "Números e caracteres especiais não são permitidos no nome.")]
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public DateTime HoraDeCriacao { get; set; }
        public DateTime HoraDeModificacao { get; set; } = DateTime.Now;
        public string Descricao { get; set; }
        public double Peso { get; set; }
        public double Altura { get; set; }
        public double Largura { get; set; }
        public double Comprimento { get; set; }
        public double Valor { get; set; }
        public int Estoque { get; set; }
        public int SubcategoriaId { get; set; }
        public object Subcategorias { get; set; }
        public int CentroId { get; set; }

    }
}

using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Data.Dtos.ProdutoDto
{
    public class CriarProdutoDto
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo de nome do produto é obrigatório!")]
        [StringLength(128, ErrorMessage = "O tamanho do nome excede 128 caracteres.")]
        [RegularExpression(@"^[a-zA-Z0-9'-'' '\s]{1,1000}$", ErrorMessage = "Números e caracteres especiais não são permitidos no nome.")]
        public string Nome { get; set; }
        [Required]
        public bool Status { get; set; } = true;
        [Required]
        public DateTime HoraDeCriacao { get; set; } = DateTime.Now;
        [Required]
        public string Descricao { get; set; }
        [Required]
        public double Peso { get; set; }
        [Required]
        public double Altura { get; set; }
        [Required]
        public double Largura { get; set; }
        [Required]
        public double Comprimento { get; set; }
        [Required]
        public double Valor { get; set; }
        [Required]
        public int Estoque { get; set; }
        public int SubcategoriaId { get; set; }
        public object Subcategoria { get; set; }
        public int CentroId { get; set; }
        public int CarrinhoId { get; set; }
    }
}

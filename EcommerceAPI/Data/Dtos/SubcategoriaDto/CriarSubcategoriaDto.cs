using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Data.Dtos.Subcategoria
{
    public class CriarSubcategoriaDto
    {
        [Required(ErrorMessage = "O campo de nome da categoria é obrigatório!")]
        [StringLength(128, ErrorMessage = "O tamanho do nome da categoria excede 128 caracteres.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,1000}$", ErrorMessage = "Números e caracteres especiais não são permitidos no nome.")]
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public DateTime Criacao { get; set; } = DateTime.Now;
        public DateTime Modificacao { get; set; }
        public int CategoriaId { get; set; }
    }
}

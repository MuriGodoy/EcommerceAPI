using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Data.Dtos
{
    public class EditarCategoriaDto
    {

        [StringLength(128, ErrorMessage = "O tamanho do nome da categoria excede 128 caracteres.")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,1000}$", ErrorMessage = "Números e caracteres especiais não são permitidos no nome.")]
        public string Nome { get; set; }

        public bool Status { get; set; } = true;

        public DateTime Modificacao { get; set; } = DateTime.Now;
        public object Subcategoria { get; set; }


    }
}

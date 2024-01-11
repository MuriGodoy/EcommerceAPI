using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Data.Dtos.CentroDistribuicao
{
    public class CriarCentroDto
    {
        [Required]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,130}$", ErrorMessage =
              "Números e caracteres especiais não são permitidos no nome.")]
        [StringLength(128, ErrorMessage = "Nome excede os 128 caracteres máximos.")]
        public string Nome { get; set; }
        [Required]
        public int Numero { get; set; }
        [Required]
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public DateTime HoraCriacao { get; set; } = DateTime.Now;
        public bool Status { get; set; } = true;



    }
}

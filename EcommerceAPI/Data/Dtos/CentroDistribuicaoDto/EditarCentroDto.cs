using System;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Data.Dtos.CentroDistribuicao
{
    public class EditarCentroDto
    {
        public string Nome { get; set; }
        [StringLength(256,
            ErrorMessage = "Não é possível editar um centro que o logradouro possua mais de 256 caracteres!")]
        [RegularExpression(@"^[a-zA-Z''-'\s]{1,1000}$", ErrorMessage = "Números e caracteres especiais não são permitidos no nome.")]

        public string Logradouro { get; set; }
        public int Numero { get; set; }
        [StringLength(128,
            ErrorMessage = "Não é possível editar um centro que o complemento possua mais de 128 caracteres!")]
        public string Complemento { get; set; }
        [StringLength(128,
            ErrorMessage = "Não é possível editar um centro que o bairro possua mais de 128 caracteres!")]
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public bool Status { get; set; }

        public DateTime HoraCriacao { get; set; }
        public DateTime HoraModificacao { get; set; } = DateTime.Now;
    }
}

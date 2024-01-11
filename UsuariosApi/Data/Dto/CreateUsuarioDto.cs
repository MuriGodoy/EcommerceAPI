using System;
using System.ComponentModel.DataAnnotations;
using UsuariosApi.Models;

namespace UsuariosApi.Data.Dto
{
    public class CreateUsuarioDto
    {
        [Required]
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$")]
        public string Email { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string Repassword { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public DateTime DataNascimento { get; set; }
        [Required]
        public string CPF { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public bool Status { get; set; } = true;
        public DateTime MomentoCriacao { get; set; } = DateTime.Now;
    }
}

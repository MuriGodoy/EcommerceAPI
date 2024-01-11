using System;
using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Dto
{
    public class EditarUsuarioDto
    {
        [StringLength(250, ErrorMessage = "Nome não pode ter mais de 250 caracteres!")]
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        [RegularExpression(@"^[A-Za-z0-9](([_\.\-]?[a-zA-Z0-9]+)*)@([A-Za-z0-9]+)(([\.\-]?[a-zA-Z0-9]+)*)\.([A-Za-z]{2,})$")]
        public string Email { get; set; }
        public string Senha { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Complemento { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public DateTime HorarioModificacao { get; set; } = DateTime.Now;

    }
}

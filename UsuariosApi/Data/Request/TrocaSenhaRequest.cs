using System.ComponentModel.DataAnnotations;

namespace UsuariosApi.Data.Request
{
    public class TrocaSenhaRequest
    {
        [Required]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare("NewPassword")]
        public string ReNewPassword { get; set; }
        [Required]
        public string Email { get; set; }
    }
}

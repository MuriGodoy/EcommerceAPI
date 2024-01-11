using System;

namespace UsuariosApi.Data.Dto
{
    public class FiltroDto
    {
        public string UserName { get; set; }
        public string CPF { get; set; }
        public string Email { get; set; }
        public bool? Status { get; set; }
    }
}

﻿using Microsoft.AspNetCore.Identity;
using System;

namespace UsuariosApi.Models
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; }
        public string Bairro { get; set; }
        public string Logradouro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string CEP { get; set; }
        public bool Status { get; set; } = true;
        public DateTime DataCadastro { get; set; } = DateTime.Now;
    }
}

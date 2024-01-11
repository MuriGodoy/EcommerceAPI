﻿using System;

namespace UsuariosApi.Data.Dto
{
    public class ReadUsuarioDto
    {
        public string Email { get; set; }
        public string UserName { get; set; }
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
        public bool Status { get; set; }
    }
}

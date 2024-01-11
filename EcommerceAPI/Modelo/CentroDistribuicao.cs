using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Modelo
{
    public class CentroDistribuicao
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get;  set; }
        public string UF { get; set; }
        public string CEP { get; set; }
        public bool Status { get; set; }
        public DateTime HoraCriacao { get; set; }
        public DateTime HoraModificacao { get; set; }
        [JsonIgnore]
        public virtual List<Produto> Produtos { get; set; }
    }
}

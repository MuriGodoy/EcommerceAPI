using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace EcommerceAPI.Modelo
{
    public class Categoria
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Nome { get; set; }
        public bool Status { get; set; } = true;
        public DateTime Criacao { get; set; } = DateTime.Now;
        public DateTime Modificacao { get; set; }
        [JsonIgnore]
        public virtual List<Subcategoria> Subcategorias { get; set; }
        [JsonIgnore]
        public virtual List<Produto> Produtos { get; set; }
    }
}

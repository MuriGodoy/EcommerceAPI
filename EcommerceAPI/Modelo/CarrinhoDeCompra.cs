using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Modelo
{
    public class CarrinhoDeCompra
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public double ValorTotal { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string Complemento { get; set; }
        public virtual ICollection<ProdutoCarrinho> ProdutosCarrinhos { get; set; }
        public int ProdutoId { get; set; }
        public int Quantidade { get; set; }
    }
}

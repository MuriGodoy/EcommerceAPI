using EcommerceAPI.Modelo;
using System.Collections.Generic;

namespace EcommerceAPI.Data.Dtos.CarrinhoDto
{
    public class LerCarrinhoDto
    {
        public int Id { get; set; }
        public int ValorTotal { get; set; }
        public string CEP { get; set; }
        public string Logradouro { get; set; }
        public int Numero { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string Complemento { get; set; }
        public int ProdutoId { get; set; }
        public virtual ICollection<Produto> Produto { get; set; }
        public int Quantidade { get; set; }
    }
}

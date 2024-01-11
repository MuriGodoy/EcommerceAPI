using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Modelo
{
    public class ProdutoCarrinho
    {
        public int ProdutoId { get; set; }
        public int CarrinhoId { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual CarrinhoDeCompra Carrinho { get; set; }
    }
}

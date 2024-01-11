using EcommerceAPI.Modelo;
using System.ComponentModel.DataAnnotations;

namespace EcommerceAPI.Data.Dtos.CarrinhoDto
{
    public class CriarCarrinhoDto
    {
        [Required]
        public string CEP { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public int Quantidade { get; set; }
        public int ProdutoId { get; set; }
        public object Produto { get; set; }
    }
}

namespace EcommerceAPI.Data.Dtos.CentroDistribuicao
{
    public class FiltroCentroDto
    {
        public string Nome { get; set; }
        public string Logradouro { get; set; }
        public int? Numero { get; set; }
        public string Complemento { get; set; }
        public string Bairro { get; set; }
        public string Localidade { get; set; }
        public string UF { get; set; }
        public string Cep { get; set; }
        public bool? Status { get; set; }
        public string Ordem { get; set; }
        public int PorPagina { get; set; }
        public int PaginaAtual { get; set; }

    }
}

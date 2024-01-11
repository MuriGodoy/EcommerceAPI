using AutoMapper;
using EcommerceAPI.Data.Dtos.CarrinhoDto;
using EcommerceAPI.Data.Repository;
using EcommerceAPI.Exceptions;
using EcommerceAPI.Modelo;
using FluentResults;
using Newtonsoft.Json;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EcommerceAPI.Services
{
    public class CarrinhoCompraService
    {
        private readonly CarrinhoCompraRepository _carrinhoRepository;
        private readonly IMapper _mapper;

        public CarrinhoCompraService(CarrinhoCompraRepository carrinhoRepository, IMapper mapper)
        {
            _carrinhoRepository = carrinhoRepository;
            _mapper = mapper;
        }

        public async Task<Result> CriarCarrinho(CriarCarrinhoDto carrinhoDto)
        {
            var carrinho = _mapper.Map<CarrinhoDeCompra>(carrinhoDto);
            var endereco = await BuscarEnderecoDeEntrega(carrinhoDto.CEP);
            var criar = _carrinhoRepository.CriarCarrinho(carrinho, endereco);
            if (endereco.ToResult().IsFailed)
            {
                return Result.Fail("Não foi possível encontrar o endereço com o CEP informado!");
            }
            if (criar.IsFailed)
            {
                return Result.Fail(criar.Errors);
            }
            return Result.Ok();
        }

        public Result AdicionarProduto(int carrinhoId, int produtoId)
        {
            var adiciona = _carrinhoRepository.AdicionarProduto(carrinhoId, produtoId);
            if (adiciona.IsFailed)
            {
                return Result.Fail(adiciona.Errors.FirstOrDefault());
            }
            return Result.Ok();
        }

        public LerCarrinhoDto PesquisarCarrinho(LerCarrinhoDto carrinhoDto)
        {
            var carrinho = _mapper.Map<CarrinhoDeCompra>(carrinhoDto);
            var pesquisar = _carrinhoRepository.PesquisarCarrinho(carrinho);
            return pesquisar;
        }

        public CarrinhoDeCompra AlterarQuantidade(int carrinhoId, int produtoId, int quantidade)
        {
            var alterar = _carrinhoRepository.AlterarQuantidade(carrinhoId, produtoId, quantidade);
            var carrinho = _mapper.Map<CarrinhoDeCompra>(alterar);
            return carrinho;
        }

        public Result DeletarCarrinho(int? id)
        {
            if (id == null)
            {
                throw new NullException("Não é possível encontrar um carrinho com id nulo!");
            }
            var deleta = _carrinhoRepository.DeletarCarrinho(id);
            if (deleta.IsFailed)
            {
                return Result.Fail(deleta.Errors);
            }
            return Result.Ok();
        }

        public async Task<CarrinhoDeCompra> BuscarEnderecoDeEntrega(string cep)
        {
            HttpClient client = new HttpClient();

            var resultado = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            var informacoes = await resultado.Content.ReadAsStringAsync();

            var endereco = JsonConvert.DeserializeObject<CarrinhoDeCompra>(informacoes);

            return endereco;
        }
    }
}

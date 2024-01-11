using AutoMapper;
using EcommerceAPI.Data;
using EcommerceAPI.Data.Dao;
using EcommerceAPI.Data.Dtos.CentroDistribuicao;
using EcommerceAPI.Exceptions;
using EcommerceAPI.Modelo;
using FluentResults;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EcommerceAPI.Services
{
    public class CentroService
    {
        private readonly EcommerceDbContext _context;
        private readonly CDRepository _cdRepository;
        private readonly IMapper _mapper;


        public CentroService(EcommerceDbContext context, CDRepository cdRepository, IMapper mapper)
        {
            _context = context;
            _cdRepository = cdRepository;
            _mapper = mapper;
        }

        public async Task<LerCentroDto> CadastrarCentro(CriarCentroDto centroDto)
        {
            if (centroDto.Status != true)
            {
                throw new StatusException("Não é possível cadastrar um Centro de Distribuição com status inativo!");
            }
            var endereco = await BuscarCentroPorCep(centroDto.CEP);
            bool enderecoUnico = EnderecoUnico(endereco.Logradouro, centroDto.Numero, centroDto.Complemento);
            if (enderecoUnico == false)
            {
                throw new EnderecoException("Não é possível cadastrar um Centro em que o endereço não seja único!");
            }
            else if (endereco.Logradouro == null)
            {
                throw new NullException();
            }
            else
            {
                _cdRepository.CadastrarCentro(centroDto, endereco, endereco.Logradouro);
            }
            return null;
        }

        public bool EnderecoUnico(string logradouro, int numero, string complemento)
        {
            string enderecoCompleto = logradouro;
            enderecoCompleto += numero;
            enderecoCompleto += complemento;

            FiltroCentroDto filtros = new FiltroCentroDto();
            List<CentroDistribuicao> filtroDto = _cdRepository.RecuperarCentro(filtros);

            foreach (var endereco in filtroDto)
            {
                string listaEndereco = endereco.Logradouro;
                listaEndereco += endereco.Numero;
                listaEndereco += endereco.Complemento;

                if (listaEndereco == enderecoCompleto)
                {
                    return false;
                }
            }
            return true;
        }

        public async Task<CentroDistribuicao> BuscarCentroPorCep(string cep)
        {
            HttpClient client = new HttpClient();

            var resultado = await client.GetAsync($"https://viacep.com.br/ws/{cep}/json/");
            var informacoes = await resultado.Content.ReadAsStringAsync();

            var endereco = JsonConvert.DeserializeObject<CentroDistribuicao>(informacoes);

            return endereco;

        }

        public List<CentroDistribuicao> RecuperarCentro(FiltroCentroDto filtroDto)
        {
            return _cdRepository.RecuperarCentro(filtroDto);
        }

        public async Task<Result> EditarCentro(int id, EditarCentroDto editarDto)
        {
            if (editarDto.CEP != null && editarDto.Logradouro != null || editarDto.Bairro != null || editarDto.Localidade != null || editarDto.UF != null)
            {
                throw new EnderecoException("Não é possível alterar as informações quando o cep é informado!");
            }

            var endereco = await BuscarCentroPorCep(editarDto.CEP);
            var enderecoUnico = EnderecoUnico(editarDto.Logradouro, editarDto.Numero, editarDto.Complemento);

            if (string.IsNullOrEmpty(endereco.Logradouro))
            {
                throw new NullException("Não é possível editar um Centro de Distribuição com logradouro em branco!");
            }
            if (enderecoUnico == false)
            {
                throw new EnderecoException();
            }
            else
            {
                var centro = _mapper.Map<CentroDistribuicao>(editarDto);
                var cd = _cdRepository.EditarCentro(centro, id);
                return Result.Ok();
            }
        }
        public Result DeletarCentro(int id)
        {
            var cd = _cdRepository.DeletarCentro(id);
            if (cd.IsFailed)
            {
                return Result.Fail("Não foi possível excluir o Centro de distribuição indicado!");
            }
            return Result.Ok();
        }
    }
}

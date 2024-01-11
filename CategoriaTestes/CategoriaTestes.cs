using CategoriaTestes.Serviço;
using EcommerceAPI.Data.Dtos;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;
using Xunit.Abstractions;

namespace CategoriaTestes
{
    public class CategoriaTestes
    {
        public ITestOutputHelper _saidaConsole;

        public CategoriaTestes(ITestOutputHelper saidaConsole)
        {
            _saidaConsole = saidaConsole;
        }

        [Fact]
        public void TestaAdicionarCategoria()
        {
            var categoria = new CriarCategoriaDto()
            {
                Nome = "Monitor"
            };

            var repository = new EcommerceRepository();

            var adicionado = repository.AdicionaCategoria(categoria);
            _saidaConsole.WriteLine("Data criação: " + categoria.Criacao);

            Assert.True(adicionado);
        }

        [Fact]
        public void TestaStatusCategoriaEhAtivo()
        {
            var categoria = new CriarCategoriaDto()
            {
                Nome = "Monitor",
            };

            var status = VerificaStatus(categoria.Status);

            Assert.True(status);
        }

        [Fact]
        public void TestaHorarioCriacaoDaCategoria()
        {
            var horarioCriacao = DateTime.Now;
            var categoria = new CriarCategoriaDto()
            {
                Nome = "Monitor",
                Criacao = horarioCriacao
            };

            var criacao = VerificaHorarioCriacao(categoria.Criacao, horarioCriacao);

            Assert.True(criacao);
        }

        [Theory]
        [InlineData("Monitor")]
        [InlineData("Mesa")]
        [InlineData("Mouse")]
        [InlineData("Bolos")]
        public void TestaValidacaoNomeDaCategoria(string nome)
        {
            var categoria = new CriarCategoriaDto()
            {
                Nome = nome
            };

            var verificado = VerificaNome(categoria.Nome);

            Assert.True(verificado);
        }

        [Fact]
        public void TestaTamanhoDoNomeDaCategoria()
        {
            var categoria = new CriarCategoriaDto()
            {
                Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };
            var categoriaDois = new CriarCategoriaDto()
            {
                Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa"
            };

            var tamanho = VerificaTamanho(categoria.Nome);
            var tamanhoMaior = VerificaTamanho(categoriaDois.Nome);

            Assert.True(tamanho);
            Assert.False(tamanhoMaior);
        }

        public bool VerificaStatus(bool status)
        {
            if (status == true)
            {
                return true;
            }
            return false;
        }

        public bool VerificaHorarioCriacao(DateTime criacao, DateTime horarioDefinido)
        {
            if (criacao == horarioDefinido)
            {
                return true;
            }
            return false;
        }

        public bool VerificaNome(string nome)
        {
            if (Regex.IsMatch(nome, "^[a-zA-Z''-']{1,1000}$"))
            {
                return true;
            }
            return false;
        }

        public bool VerificaTamanho(string nome)
        {
            if (nome.Count() <= 128)
            {
                return true;
            }
            return false;
        }
    }
}
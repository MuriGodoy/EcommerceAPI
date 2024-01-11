using EcommerceAPI.Data.Dtos.Subcategoria;
using EcommerceAPI.Modelo;
using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace CategoriaTestes
{
    public class SubcategoriaTestes
    {

        [Fact]
        public void TestaCadastroDeSubcategoria()
        {
            var categoriaFalse = new Categoria()
            {
                Nome = "cate",
                Id = 54,
                Status = false
            };

            var subcategoriaFalse = new CriarSubcategoriaDto()
            {
                Nome = "Monitor 60Hz",
                CategoriaId = 54
            };

            var categoriaTrue = new Categoria()
            {
                Nome = "teste",
                Id = 55,
                Status = true
            };
            var subcategoriaTrue = new CriarSubcategoriaDto()
            {
                Nome = "teste",
                CategoriaId = 55
            };

            var cadastroFalse = ValidacaoCategoria(categoriaFalse, subcategoriaFalse);
            var cadastroTrue = ValidacaoCategoria(categoriaTrue, subcategoriaTrue);

            Assert.False(cadastroFalse);
            Assert.True(cadastroTrue);
        }

        [Theory]
        [InlineData("SubcategoriaTeste",1)]
        [InlineData("Subcategoriatestedois", 2)]
        public void TestaValidacaoDoNomeDaSubcategoria(string nome, int categoriaId)
        {
            var subcategoria = new CriarSubcategoriaDto()
            {
                Nome = nome,
                CategoriaId = categoriaId
            };

            var teste = VerificaNome(subcategoria.Nome);

            Assert.True(teste);
        }

        [Fact]
        public void TestaTamanhoSubcategoria()
        {
            var subcategoriaLimite = new CriarSubcategoriaDto()
            {
                Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                CategoriaId = 1
            };
            var subcategoriaFora = new CriarSubcategoriaDto()
            {
                Nome = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                CategoriaId = 1
            };

            var verificadoLimite = VerificaTamanho(subcategoriaLimite.Nome);
            var verificadoFora = VerificaTamanho(subcategoriaFora.Nome);

            Assert.True(verificadoLimite);
            Assert.False(verificadoFora);
        }

        [Fact]
        public void TestaSeSubcategoriaIndicadaEstaAtiva()
        {
            var subcategoriaFalse = new CriarSubcategoriaDto()
            {
                Nome = "subcate teste",
                Status = false
            };

            var subcategoriaTrue = new CriarSubcategoriaDto()
            {
                Nome = "Teste"
            };

            var statusTrue = VerificaStatus(subcategoriaTrue.Status);
            var statusFalse = VerificaStatus(subcategoriaFalse.Status);

            Assert.True(statusTrue);
            Assert.False(statusFalse);
        }

        [Fact]
        public void TestaNomeTemSomenteCaracteres()
        {
            var nomeValido = new CriarSubcategoriaDto()
            {
                Nome = "subcatTeste"
            };
            var nomeInvalido = new CriarSubcategoriaDto()
            {
                Nome = "Teste5"
            };

            var caracter = VerificaNome(nomeValido.Nome);
            var numero = VerificaNome(nomeInvalido.Nome);

            Assert.True(caracter);
            Assert.False(numero);
        }

        [Fact]
        public void TestaHorarioCriacaoSubcategoria()
        {
            var horarioCriacao = DateTime.Now;
            var subcategoria = new CriarSubcategoriaDto()
            {
                Nome = "teste",
                Criacao = horarioCriacao
            };

            var horario = VerificaHorarioCriacao(subcategoria.Criacao, horarioCriacao);

            Assert.True(horario);
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
        public bool ValidacaoCategoria(Categoria categoria, CriarSubcategoriaDto subcategoria)
        {
            if (subcategoria.CategoriaId == categoria.Id)
            {
                if (categoria.Status == true)
                {
                    return true;
                }
                return false;
            }
            return false;
        }
    }
}

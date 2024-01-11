using EcommerceAPI.Data.Dtos;
using EcommerceAPI.Data.Dtos.Subcategoria;
using EcommerceAPI.Modelo;
using System;
using System.Collections.Generic;

namespace CategoriaTestes.Serviço
{
    public class EcommerceRepository : IEcommerceRepository
    {
        private List<Categoria> lerCategoria = new List<Categoria>()
        {
            new Categoria
            {
                Id = 1,
                Nome = "Monitor",
                Status = false
            },
            new Categoria
            {
                Id = 2,
                Nome = "Teclado"
            },
            new Categoria
            {
                Id = 3,
                Nome = "Gabinetes"
            },
            new Categoria
            {
                Nome = "Monitor teste"
            }
    };

        public List<Categoria> LerCategoria { get { return lerCategoria; } }

        private List<LerSubcategoriaDto> lerSubcategoria = new List<LerSubcategoriaDto>()
        {
            new LerSubcategoriaDto
            {
                Id = 1,
                Nome = "Monitor samsung",
                CategoriaId = 1
            },
            new LerSubcategoriaDto
            {
                Id = 2,
                Nome = "Monitor Lg",
                CategoriaId = 1
            },
            new LerSubcategoriaDto
            {
                Id = 3,
                Nome = "Monitor AOC",
                CategoriaId = 1
            },
            new LerSubcategoriaDto
            {
                Id = 1,
                Nome = "Teclado Logitech",
                CategoriaId = 2
            }
        };

        public List<LerSubcategoriaDto> LerSubcategoria { get { return lerSubcategoria; } }


        private List<CriarCategoriaDto> criarCategoria = new List<CriarCategoriaDto>()
        {
            new CriarCategoriaDto
            {
                Nome = "Mesas"
            },
            new CriarCategoriaDto
            {
                Nome = "LED"
            },
            new CriarCategoriaDto
            {
                Nome = "Mousepad"
            }
        };

        public List<CriarCategoriaDto> CriarCategoria { get { return criarCategoria; } }

        private List<CriarSubcategoriaDto> criarSubcategoria = new List<CriarSubcategoriaDto>()
        {
            new CriarSubcategoriaDto
            {
                Nome = "Teclado Razer",
                CategoriaId = 2
            },
            new CriarSubcategoriaDto
            {
                Nome = "Teclado Microsoft",
                CategoriaId = 2
            },
            new CriarSubcategoriaDto
            {
                Nome = "Teclado Redragon",
                CategoriaId = 2
            }
        };

        public List<CriarSubcategoriaDto> CriarSubcategoria { get { return criarSubcategoria; } }

        public List<Categoria> RecuperaCategorias()
        {
            return this.LerCategoria;
        }

        public List<LerSubcategoriaDto> RecuperaSubcategorias()
        {
            return this.LerSubcategoria;
        }

        public bool AdicionaCategoria(CriarCategoriaDto categoria)
        {
            try
            {
                this.CriarCategoria.Add(categoria);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AdicionaSubcategoria(CriarSubcategoriaDto subcategoria)
        {
            try
            {
                this.CriarSubcategoria.Add(subcategoria);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}

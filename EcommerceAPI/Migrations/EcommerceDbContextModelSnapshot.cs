﻿// <auto-generated />
using System;
using EcommerceAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EcommerceAPI.Migrations
{
    [DbContext(typeof(EcommerceDbContext))]
    partial class EcommerceDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 64)
                .HasAnnotation("ProductVersion", "5.0.5");

            modelBuilder.Entity("EcommerceAPI.Modelo.CarrinhoDeCompra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bairro")
                        .HasColumnType("text");

                    b.Property<string>("CEP")
                        .HasColumnType("text");

                    b.Property<string>("Complemento")
                        .HasColumnType("text");

                    b.Property<string>("Localidade")
                        .HasColumnType("text");

                    b.Property<string>("Logradouro")
                        .HasColumnType("text");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("Quantidade")
                        .HasColumnType("int");

                    b.Property<string>("UF")
                        .HasColumnType("text");

                    b.Property<double>("ValorTotal")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.ToTable("CarrinhoDeCompras");
                });

            modelBuilder.Entity("EcommerceAPI.Modelo.Categoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Criacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Modificacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Nome")
                        .HasColumnType("text");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("EcommerceAPI.Modelo.CentroDistribuicao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Bairro")
                        .HasColumnType("text");

                    b.Property<string>("CEP")
                        .HasColumnType("text");

                    b.Property<string>("Complemento")
                        .HasColumnType("text");

                    b.Property<DateTime>("HoraCriacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("HoraModificacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Localidade")
                        .HasColumnType("text");

                    b.Property<string>("Logradouro")
                        .HasColumnType("text");

                    b.Property<string>("Nome")
                        .HasColumnType("varchar(767)");

                    b.Property<int>("Numero")
                        .HasColumnType("int");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("UF")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Nome")
                        .IsUnique();

                    b.ToTable("CentroDistribuicoes");
                });

            modelBuilder.Entity("EcommerceAPI.Modelo.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Altura")
                        .HasColumnType("double");

                    b.Property<int>("CarrinhoId")
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<int>("CentroId")
                        .HasColumnType("int");

                    b.Property<double>("Comprimento")
                        .HasColumnType("double");

                    b.Property<string>("Descricao")
                        .HasColumnType("text");

                    b.Property<int>("Estoque")
                        .HasColumnType("int");

                    b.Property<DateTime>("HoraDeCriacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("HoraDeModificacao")
                        .HasColumnType("datetime");

                    b.Property<double>("Largura")
                        .HasColumnType("double");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<double>("Peso")
                        .HasColumnType("double");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("SubcategoriaId")
                        .HasColumnType("int");

                    b.Property<double>("Valor")
                        .HasColumnType("double");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("CentroId");

                    b.HasIndex("SubcategoriaId");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("EcommerceAPI.Modelo.ProdutoCarrinho", b =>
                {
                    b.Property<int>("ProdutoId")
                        .HasColumnType("int");

                    b.Property<int>("CarrinhoId")
                        .HasColumnType("int");

                    b.HasKey("ProdutoId", "CarrinhoId");

                    b.HasIndex("CarrinhoId");

                    b.ToTable("ProdutoCarrinhos");
                });

            modelBuilder.Entity("EcommerceAPI.Modelo.Subcategoria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Criacao")
                        .HasColumnType("datetime");

                    b.Property<DateTime>("Modificacao")
                        .HasColumnType("datetime");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<bool>("Status")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("Id");

                    b.HasIndex("CategoriaId");

                    b.ToTable("Subcategorias");
                });

            modelBuilder.Entity("EcommerceAPI.Modelo.Produto", b =>
                {
                    b.HasOne("EcommerceAPI.Modelo.Categoria", "Categorias")
                        .WithMany("Produtos")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerceAPI.Modelo.CentroDistribuicao", "CentroDistribuicao")
                        .WithMany("Produtos")
                        .HasForeignKey("CentroId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerceAPI.Modelo.Subcategoria", "Subcategoria")
                        .WithMany("Produtos")
                        .HasForeignKey("SubcategoriaId");

                    b.Navigation("Categorias");

                    b.Navigation("CentroDistribuicao");

                    b.Navigation("Subcategoria");
                });

            modelBuilder.Entity("EcommerceAPI.Modelo.ProdutoCarrinho", b =>
                {
                    b.HasOne("EcommerceAPI.Modelo.CarrinhoDeCompra", "Carrinho")
                        .WithMany("ProdutosCarrinhos")
                        .HasForeignKey("CarrinhoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EcommerceAPI.Modelo.Produto", "Produto")
                        .WithMany("ProdutosCarrinhos")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Carrinho");

                    b.Navigation("Produto");
                });

            modelBuilder.Entity("EcommerceAPI.Modelo.Subcategoria", b =>
                {
                    b.HasOne("EcommerceAPI.Modelo.Categoria", "Categorias")
                        .WithMany("Subcategorias")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categorias");
                });

            modelBuilder.Entity("EcommerceAPI.Modelo.CarrinhoDeCompra", b =>
                {
                    b.Navigation("ProdutosCarrinhos");
                });

            modelBuilder.Entity("EcommerceAPI.Modelo.Categoria", b =>
                {
                    b.Navigation("Produtos");

                    b.Navigation("Subcategorias");
                });

            modelBuilder.Entity("EcommerceAPI.Modelo.CentroDistribuicao", b =>
                {
                    b.Navigation("Produtos");
                });

            modelBuilder.Entity("EcommerceAPI.Modelo.Produto", b =>
                {
                    b.Navigation("ProdutosCarrinhos");
                });

            modelBuilder.Entity("EcommerceAPI.Modelo.Subcategoria", b =>
                {
                    b.Navigation("Produtos");
                });
#pragma warning restore 612, 618
        }
    }
}

using EcommerceAPI.Modelo;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Data
{
    public class EcommerceDbContext : DbContext
    {
        public EcommerceDbContext(DbContextOptions<EcommerceDbContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Subcategoria>()
                .HasOne(subcategoria => subcategoria.Categorias)
                .WithMany(categoria => categoria.Subcategorias)
                .HasForeignKey(subcategoria => subcategoria.CategoriaId);

            builder.Entity<Produto>()
                .HasOne(produto => produto.Subcategoria)
                .WithMany(subcategoria => subcategoria.Produtos)
                .HasForeignKey(produto => produto.SubcategoriaId);

            builder.Entity<Produto>()
                .HasOne(produto => produto.CentroDistribuicao)
                .WithMany(centro => centro.Produtos)
                .HasForeignKey(produto => produto.CentroId);

            builder.Entity<CentroDistribuicao>()
                .HasIndex(centro => centro.Nome)
                .IsUnique();

            //builder.Entity<ProdutoCarrinho>()
            //    .HasKey(x => new { x.ProdutoId, x.CarrinhoId });

            //builder.Entity<ProdutoCarrinho>()
            //    .HasOne(p => p.Produto)
            //    .WithMany(p => p.ProdutosCarrinhos)
            //    .HasForeignKey(p => p.ProdutoId);

            //builder.Entity<ProdutoCarrinho>()
            //    .HasOne(c => c.Carrinho)
            //    .WithMany(c => c.ProdutosCarrinhos)
            //    .HasForeignKey(c => c.CarrinhoId);

            builder.Entity<ProdutoCarrinho>()
                .HasKey(pc => new { pc.ProdutoId, pc.CarrinhoId });

            builder.Entity<ProdutoCarrinho>()
                .HasOne(pc => pc.Carrinho)
                .WithMany(carrinho => carrinho.ProdutosCarrinhos)
                .HasForeignKey(pc => pc.CarrinhoId);

            builder.Entity<ProdutoCarrinho>()
                .HasOne(pc => pc.Produto)
                .WithMany(produto => produto.ProdutosCarrinhos)
                .HasForeignKey(pc => pc.ProdutoId);
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Subcategoria> Subcategorias { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<CentroDistribuicao> CentroDistribuicoes { get; set; }
        public DbSet<CarrinhoDeCompra> CarrinhoDeCompras { get; set; }
        public DbSet<ProdutoCarrinho> ProdutoCarrinhos { get; set; }
    }
}

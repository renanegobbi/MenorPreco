using MenorPreco.Model;
using Microsoft.EntityFrameworkCore;

namespace MenorPreco.DAL.Estabelecimentos
{
    public class MenorPrecoContext : DbContext
    {
        public DbSet<Estabelecimento> Estabelecimentos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }
        public DbSet<VendaProduto> VendaProdutos { get; set; }

        public MenorPrecoContext(DbContextOptions<MenorPrecoContext> options)
        : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration<Estabelecimento>(new EstabelecimentoConfiguration());
            modelBuilder.ApplyConfiguration<Produto>(new ProdutoConfiguration());
            modelBuilder.ApplyConfiguration<Venda>(new VendaConfiguration());
            modelBuilder.ApplyConfiguration<VendaProduto>(new VendaProdutoConfiguration());
        }

    }
}

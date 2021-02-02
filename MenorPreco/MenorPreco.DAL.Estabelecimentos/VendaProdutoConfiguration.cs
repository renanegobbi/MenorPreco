using MenorPreco.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenorPreco.DAL.Estabelecimentos
{
    internal class VendaProdutoConfiguration : IEntityTypeConfiguration<VendaProduto>
    {
        public void Configure(EntityTypeBuilder<VendaProduto> builder)
        {
            builder.HasKey("ID_VENDA_PRODUTO");

            builder
                .HasOne(p => p.VENDA)
                .WithMany(b => b.VENDAPRODUTO)
                .HasForeignKey("ID_VENDA");

            builder
               .HasOne(vp => vp.PRODUTO)
               .WithOne(v => v.VENDAPRODUTO)
               .HasForeignKey<VendaProduto>("COD_PRODUTO");
        }
    }
}


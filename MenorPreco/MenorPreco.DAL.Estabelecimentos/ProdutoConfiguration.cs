using MenorPreco.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenorPreco.DAL.Estabelecimentos
{
    internal class ProdutoConfiguration : IEntityTypeConfiguration<Produto>
    {
        public void Configure(EntityTypeBuilder<Produto> builder)
        {
            builder.HasKey("COD_PRODUTO");

            builder
                .Property(p => p.COD_PRODUTO)
                .IsRequired();

            builder
                .Property(p => p.DSC_PRODUTO)
                .HasColumnType("varchar(100)")
                .IsRequired();

            builder
                .Property(p => p.COD_NCM)
                .IsRequired();

            builder
                .Property(p => p.COD_GTIN)
                .IsRequired();

            builder
                .Property(p => p.VLR_UNITARIO)
                //.HasColumnType("decimal(3,2)")
                .HasColumnType("decimal(7,2)")
                .IsRequired();

            builder
                .Property(p => p.COD_UNIDADE)
                .HasColumnType("char(3)")
                .IsRequired();

            // Add the shadow property to the model
            builder
                .Property<int>("ID_ESTABELECIMENTO");

            // Use the shadow property as a foreign key
            builder
                .HasOne(l => l.ESTABELECIMENTO)
                .WithMany(p => p.PRODUTOS)
                .HasForeignKey("ID_ESTABELECIMENTO");
        }
    }
}


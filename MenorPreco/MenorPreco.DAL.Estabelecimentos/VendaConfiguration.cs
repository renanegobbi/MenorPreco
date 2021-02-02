using MenorPreco.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenorPreco.DAL.Estabelecimentos
{
    internal class VendaConfiguration : IEntityTypeConfiguration<Venda>
    {
        public void Configure(EntityTypeBuilder<Venda> builder)
        {
            builder.ToTable("Venda");

            builder.HasKey("ID_VENDA");

            builder
                .Property(v => v.ID_VENDA)
                .IsRequired();

            builder
                .Property(v => v.DAT_EMISSAO)
                .HasColumnType("datetime")
                .HasDefaultValueSql("getdate()");

            builder
                .Property(v => v.COD_TIPO_PAGAMENTO)
                .IsRequired();

            builder
                .Property<int>("ID_ESTABELECIMENTO");

            builder
                .HasOne(l => l.ESTABELECIMENTO)
                .WithMany(p => p.VENDAS)
                .HasForeignKey("ID_ESTABELECIMENTO");
        }
    }
}


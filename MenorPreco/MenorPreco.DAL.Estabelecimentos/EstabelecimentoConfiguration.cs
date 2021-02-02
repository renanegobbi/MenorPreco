using MenorPreco.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MenorPreco.DAL.Estabelecimentos
{
    internal class EstabelecimentoConfiguration : IEntityTypeConfiguration<Estabelecimento>
    {
        public void Configure(EntityTypeBuilder<Estabelecimento> builder)
        {
            builder.ToTable("Estabelecimento");

            builder.HasKey("ID_ESTABELECIMENTO");

            builder
                .Property(l => l.ID_ESTABELECIMENTO)
                .IsRequired();

            builder
                .Property(l => l.NME_ESTABELECIMENTO)
                .HasColumnType("varchar(25)")
                .IsRequired();

            builder
                .Property(l => l.NME_LOGRADOURO)
                .HasColumnType("varchar(50)")
                .IsRequired();

            builder
                .Property(l => l.COD_NUMERO_LOGRADOURO)
                .IsRequired();

            builder
                .Property(l => l.NME_COMPLEMENTO)
                .HasColumnType("varchar(50)");

            builder
                .Property(l => l.NME_BAIRRO)
                .HasColumnType("varchar(25)")
                .IsRequired();

            builder
                .Property(l => l.COD_MUNICIPIO_IBGE)
                .IsRequired();

            builder
                .Property(l => l.NME_MUNICIPIO)
                .HasColumnType("varchar(25)")
                .IsRequired();

            builder
                .Property(l => l.NME_SIGLA_UF)
                .HasColumnType("char(2)")
                .IsRequired();

            builder
                .Property(l => l.COD_CEP)
                .HasColumnType("varchar(8)")
                .IsRequired();

            builder
                .Property(l => l.NUM_LATITUDE)
                .IsRequired();

            builder
                .Property(l => l.NUM_LONGITUDE)
                .IsRequired();
        }
    }
}

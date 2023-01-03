using Siscred.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Siscred.Infra.Data.EntityConfig
{
    public class EditalConfig : EntityTypeConfiguration<Edital>
    {
        public EditalConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Descricao).IsRequired().HasMaxLength(255);
            Property(x => x.DataPublicacao).IsRequired();
            Property(x => x.DataEncerramento).IsRequired();
            Property(x => x.AnexoEdital).IsRequired().HasMaxLength(500);
            Property(x => x.NomeOriginal).IsOptional().HasMaxLength(500);
            Property(x => x.Ativo).IsRequired();

            HasMany(x => x.Cidades).WithMany(x => x.Editais).Map(x =>
            {
                x.MapLeftKey("EditalId");
                x.MapRightKey("CidadeId");
                x.ToTable("EditalCidade");
            });

            HasMany(x => x.TiposArquivos).WithMany(x => x.Editais).Map(x =>
            {
                x.MapLeftKey("EditalId");
                x.MapRightKey("TipoArquivoId");
                x.ToTable("EditalTipoArquivo");
            });

            ToTable("Edital");
        }
    }
}
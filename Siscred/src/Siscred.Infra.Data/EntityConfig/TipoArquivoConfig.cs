using Siscred.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Siscred.Infra.Data.EntityConfig
{
    public class TipoArquivoConfig : EntityTypeConfiguration<TipoArquivo>
    {
        public TipoArquivoConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Descricao).IsRequired().HasMaxLength(500);
            Property(x => x.Observacao).IsOptional().HasMaxLength(500);
            Property(x => x.Modelo).IsOptional().HasMaxLength(500);
            Property(x => x.NomeOriginal).IsOptional().HasMaxLength(500);
            Property(x => x.Obrigatorio).IsRequired();
            Property(x => x.FinalidadeArquivo).IsRequired();
            Property(x => x.Ativo).IsRequired();         

            ToTable("TipoArquivo");
        }
    }
}
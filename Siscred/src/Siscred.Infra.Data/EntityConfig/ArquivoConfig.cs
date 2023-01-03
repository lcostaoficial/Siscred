using Siscred.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Siscred.Infra.Data.EntityConfig
{
    public class ArquivoConfig : EntityTypeConfiguration<Arquivo>
    {
        public ArquivoConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Nome).IsRequired().HasMaxLength(500);
            Property(x => x.Caminho).IsRequired().HasMaxLength(500);

            HasRequired(x => x.TipoArquivo).WithMany(x => x.Arquivos).HasForeignKey(x => x.TipoArquivoId).WillCascadeOnDelete(false);
            
            ToTable("Arquivo");
        }
    }
}
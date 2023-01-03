using Siscred.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Siscred.Infra.Data.EntityConfig
{
    public class ProfissionalIndicadoConfig : EntityTypeConfiguration<ProfissionalIndicado>
    {
        public ProfissionalIndicadoConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Nome).IsRequired().HasMaxLength(255);
            Property(x => x.Cpf).IsRequired().HasMaxLength(14);

            HasRequired(x => x.Inscricao).WithMany(x => x.ProfissionaisIndicados).HasForeignKey(x => x.InscricaoId).WillCascadeOnDelete(false);

            HasMany(x => x.Arquivos).WithMany(x => x.ProfissionaisIndicados).Map(x =>
            {
                x.MapLeftKey("ProfissionalIndicadoId");
                x.MapRightKey("ArquivoId");
                x.ToTable("ProfissionalIndicadoArquivo");
            });
        }
    }
}
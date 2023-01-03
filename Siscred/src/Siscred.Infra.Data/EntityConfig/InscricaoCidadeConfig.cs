using Siscred.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Siscred.Infra.Data.EntityConfig
{
    public class InscricaoCidadeConfig : EntityTypeConfiguration<InscricaoCidade>
    {
        public InscricaoCidadeConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);    
            HasRequired(x => x.Inscricao).WithMany(x => x.InscricoesCidades).HasForeignKey(x => x.InscricaoId).WillCascadeOnDelete(false);
            HasRequired(x => x.Cidade).WithMany(x => x.InscricoesCidades).HasForeignKey(x => x.CidadeId).WillCascadeOnDelete(false);

            ToTable("InscricaoCidade");
        }
    }
}
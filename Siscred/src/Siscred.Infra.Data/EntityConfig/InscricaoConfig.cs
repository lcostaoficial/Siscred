using Siscred.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Siscred.Infra.Data.EntityConfig
{
    public class InscricaoConfig : EntityTypeConfiguration<Inscricao>
    {
        public InscricaoConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Protocolo).IsRequired().HasMaxLength(6);
            Property(x => x.Data).IsRequired();
            Property(x => x.Experiencia).IsRequired().HasMaxLength(500);
            Property(x => x.RazaoSocial).IsRequired().HasMaxLength(255);
            Property(x => x.NomeFantasia).IsRequired().HasMaxLength(255);
            Property(x => x.ResponsavelLegal).IsRequired().HasMaxLength(255);
            Property(x => x.Cnpj).IsRequired().HasMaxLength(18);            
            Property(x => x.ObjetoSocial).IsRequired().HasMaxLength(500);
            Property(x => x.Telefone).IsRequired().HasMaxLength(14);
            Property(x => x.Celular).IsRequired().HasMaxLength(15);
            Property(x => x.EmailCorporativo).IsRequired().HasMaxLength(255);
            Property(x => x.Cep).IsRequired().HasMaxLength(9);
            Property(x => x.Rua).IsRequired().HasMaxLength(255);
            Property(x => x.Numero).IsRequired().HasMaxLength(5);
            Property(x => x.Bairro).IsRequired().HasMaxLength(255);
            Property(x => x.Cidade).IsRequired().HasMaxLength(255);
            Property(x => x.Estado).IsRequired();
            Property(x => x.SituacaoInscricao).IsRequired();
            Property(x => x.Justificativa).IsOptional().HasMaxLength(500);
       
            HasRequired(x => x.Edital).WithMany(x => x.Inscricoes).HasForeignKey(x => x.EditalId).WillCascadeOnDelete(false);

            HasMany(x => x.Arquivos).WithMany(x => x.Inscricoes).Map(x =>
            {
                x.MapLeftKey("InscricaoId");
                x.MapRightKey("ArquivoId");
                x.ToTable("InscricaoArquivo");
            });

            ToTable("Inscricao");
        }
    }
}
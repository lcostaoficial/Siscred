using Siscred.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Siscred.Infra.Data.EntityConfig
{
    public class TokenConfig : EntityTypeConfiguration<Token>
    {
        public TokenConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.DataEmissao).IsRequired();
            Property(x => x.Chave).IsRequired();
            Property(x => x.TipoToken).IsRequired();
            Property(x => x.Ativo).IsRequired();

            HasRequired(x => x.Usuario).WithMany(x => x.Tokens).HasForeignKey(x => x.UsuarioId).WillCascadeOnDelete(false);
         
            ToTable("Token");
        }
    }
}
using Siscred.Domain.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Siscred.Infra.Data.EntityConfig
{
    public class UsuarioConfig : EntityTypeConfiguration<Usuario>
    {
        public UsuarioConfig()
        {
            HasKey(x => x.Id);
            Property(x => x.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Nome).IsRequired().HasMaxLength(255);
            Property(x => x.Email).IsRequired().HasMaxLength(255);
            Property(x => x.Senha).IsRequired().HasMaxLength(100);
            Property(x => x.Ativo).IsRequired();
            Property(x => x.TipoUsuario).IsRequired();

            ToTable("Usuario");
        }
    }
}
using Siscred.Domain.Entities;
using Siscred.Infra.Data.EntityConfig;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Siscred.Infra.Data.Context
{
    public class MainContext : DbContext
    {
        public MainContext() : base("DBSISCRED") { }

        public DbSet<Arquivo> Arquivos { get; set; }
        public DbSet<Cidade> Cidades { get; set; }
        public DbSet<Edital> Editais { get; set; }
        public DbSet<Inscricao> Inscricoes { get; set; }  
        public DbSet<TipoArquivo> TiposArquivos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<InscricaoCidade> InscricoesCidades { get; set; }
        public DbSet<ProfissionalIndicado> ProfissionaisIndicados { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();

            modelBuilder.Properties<string>().Configure(x => x.HasColumnType("varchar"));
            modelBuilder.Properties<string>().Configure(x => x.HasMaxLength(100));

            modelBuilder.Configurations.Add(new ArquivoConfig());
            modelBuilder.Configurations.Add(new CidadeConfig());
            modelBuilder.Configurations.Add(new EditalConfig());
            modelBuilder.Configurations.Add(new InscricaoConfig());     
            modelBuilder.Configurations.Add(new TipoArquivoConfig());
            modelBuilder.Configurations.Add(new UsuarioConfig());
            modelBuilder.Configurations.Add(new TokenConfig());
            modelBuilder.Configurations.Add(new InscricaoCidadeConfig());
            modelBuilder.Configurations.Add(new ProfissionalIndicadoConfig());
        }
    }
}
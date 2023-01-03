namespace Siscred.Infra.Data.Migrations
{
    using Siscred.Domain.Entities;
    using Siscred.Infra.Data.Context;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<MainContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = false;
        }

        protected override void Seed(MainContext context)
        {
            var usuarios = new List<Usuario>
            {
                new Usuario
                {
                    Id = 1,
                    Nome = "Gestor",
                    Email = "admin@admin.com",
                    Senha = "6qMye1s9l3pQeL+vlxwHJHonz1HMJ7HMJ2gUKdRjH0QitJEPoW49W0/nWWOx1Yc9kDZsXarh/LVDEbCRiyGLU0H6/SfV",
                    TipoUsuario = TipoUsuario.Gestor,
                    Ativo = true
                },
                new Usuario
                {
                    Id = 1,
                    Nome = "Fulano",
                    Email = "fulano@fulano.com",
                    Senha = "bsgqnv64wKUOocAPNjEBb7Smr3ovgMrU/N3pZFoS4M3hSPLMzmxt6q5rUXVAT1L98WCee6UBR7V9MO+s4fJ3pB8pIiU=",
                    TipoUsuario = TipoUsuario.Empresa,
                    Ativo = true
                }
            };

            var tiposArquivos = new List<TipoArquivo>
            {
                new TipoArquivo
                {
                    Id = 1,
                    Descricao = "Ficha de apresentação de quadro técnico da pessoa jurídica",
                    Observacao = "Enviar digitalizado, assinado e carimbado.",
                    Ativo = true
                }                              
            };

            var cidades = new List<Cidade>
            {
                new Cidade { Id = 1, Nome = "Alta Floresta d'Oeste",  Estado = Estado.RO },
                new Cidade { Id = 2, Nome = "Alto Alegre do Parecis",  Estado = Estado.RO },
                new Cidade { Id = 3, Nome = "Alto Paraiso",  Estado = Estado.RO },
                new Cidade { Id = 4, Nome = "Alvorada d'Oeste",  Estado = Estado.RO },
                new Cidade { Id = 5, Nome = "Ariquemes",  Estado = Estado.RO },
                new Cidade { Id = 6, Nome = "Buritis",  Estado = Estado.RO },
                new Cidade { Id = 7, Nome = "Cabixi",  Estado = Estado.RO },
                new Cidade { Id = 8, Nome = "Cacaulandia",  Estado = Estado.RO },
                new Cidade { Id = 9, Nome = "Cacoal",  Estado = Estado.RO },
                new Cidade { Id = 10, Nome = "Campo Novo de Rondonia",  Estado = Estado.RO },
                new Cidade { Id = 11, Nome = "Candeias do Jamari",  Estado = Estado.RO },
                new Cidade { Id = 12, Nome = "Castanheiras",  Estado = Estado.RO },
                new Cidade { Id = 13, Nome = "Cerejeiras",  Estado = Estado.RO },
                new Cidade { Id = 14, Nome = "Chupinguaia",  Estado = Estado.RO },
                new Cidade { Id = 15, Nome = "Colorado do Oeste",  Estado = Estado.RO },
                new Cidade { Id = 16, Nome = "Corumbiara",  Estado = Estado.RO },
                new Cidade { Id = 17, Nome = "Costa Marques",  Estado = Estado.RO },
                new Cidade { Id = 18, Nome = "Cujubim",  Estado = Estado.RO },
                new Cidade { Id = 19, Nome = "Espigao d'Oeste",  Estado = Estado.RO },
                new Cidade { Id = 20, Nome = "Governador Jorge Teixeira",  Estado = Estado.RO },
                new Cidade { Id = 21, Nome = "Guajara-Mirim",  Estado = Estado.RO },
                new Cidade { Id = 22, Nome = "Jamari",  Estado = Estado.RO },
                new Cidade { Id = 23, Nome = "Jaru",  Estado = Estado.RO },
                new Cidade { Id = 24, Nome = "Ji-Parana",  Estado = Estado.RO },
                new Cidade { Id = 25, Nome = "Machadinho d'Oeste",  Estado = Estado.RO },
                new Cidade { Id = 26, Nome = "Ministro Andreazza",  Estado = Estado.RO },
                new Cidade { Id = 27, Nome = "Mirante da Serra",  Estado = Estado.RO },
                new Cidade { Id = 28, Nome = "Monte Negro",  Estado = Estado.RO },
                new Cidade { Id = 29, Nome = "Nova Brasilandia d'Oeste",  Estado = Estado.RO },
                new Cidade { Id = 30, Nome = "Nova Mamore",  Estado = Estado.RO },
                new Cidade { Id = 31, Nome = "Nova Uniao",  Estado = Estado.RO },
                new Cidade { Id = 32, Nome = "Novo Horizonte do Oeste",  Estado = Estado.RO },
                new Cidade { Id = 33, Nome = "Ouro Preto do Oeste",  Estado = Estado.RO },
                new Cidade { Id = 34, Nome = "Parecis",  Estado = Estado.RO },
                new Cidade { Id = 35, Nome = "Pimenta Bueno",  Estado = Estado.RO },
                new Cidade { Id = 36, Nome = "Pimenteiras do Oeste",  Estado = Estado.RO },
                new Cidade { Id = 37, Nome = "Porto Velho",  Estado = Estado.RO },
                new Cidade { Id = 38, Nome = "Presidente Medici",  Estado = Estado.RO },
                new Cidade { Id = 39, Nome = "Primavera de Rondonia",  Estado = Estado.RO },
                new Cidade { Id = 40, Nome = "Rio Crespo",  Estado = Estado.RO },
                new Cidade { Id = 41, Nome = "Rolim de Moura",  Estado = Estado.RO },
                new Cidade { Id = 42, Nome = "Santa Luzia d'Oeste",  Estado = Estado.RO },
                new Cidade { Id = 43, Nome = "Sao Felipe d'Oeste",  Estado = Estado.RO },
                new Cidade { Id = 44, Nome = "Sao Francisco do Guapore",  Estado = Estado.RO },
                new Cidade { Id = 45, Nome = "Sao Miguel do Guapore",  Estado = Estado.RO },
                new Cidade { Id = 46, Nome = "Seringueiras",  Estado = Estado.RO },
                new Cidade { Id = 47, Nome = "Teixeiropolis",  Estado = Estado.RO },
                new Cidade { Id = 48, Nome = "Theobroma",  Estado = Estado.RO },
                new Cidade { Id = 49, Nome = "Urupa",  Estado = Estado.RO },
                new Cidade { Id = 50, Nome = "Vale do Anari",  Estado = Estado.RO },
                new Cidade { Id = 51, Nome = "Vale do Paraiso",  Estado = Estado.RO },
                new Cidade { Id = 52, Nome = "Vilhena",  Estado = Estado.RO }               
            };

            if (!context.Usuarios.Any()) context.Usuarios.AddRange(usuarios);
            if (!context.TiposArquivos.Any()) context.TiposArquivos.AddRange(tiposArquivos);
            if (!context.Cidades.Any()) context.Cidades.AddRange(cidades);

            context.SaveChanges();
        }
    }
}
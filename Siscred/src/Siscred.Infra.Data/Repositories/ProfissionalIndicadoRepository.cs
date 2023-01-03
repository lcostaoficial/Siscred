using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace Siscred.Infra.Data.Repositories
{
    public class ProfissionalIndicadoRepository : BaseRepository<ProfissionalIndicado>, IProfissionalIndicadoRepository
    {
        public ProfissionalIndicadoRepository(MainContext mainContext) : base(mainContext) { }

        private void BalanciarListaArquivos(ProfissionalIndicado profissionalAntigo, ProfissionalIndicado profissionalNovo)
        {
            var arquivosIds = profissionalNovo.Arquivos.Select(x => x.Id).ToArray();
            profissionalNovo.Arquivos = new List<Arquivo>();
            profissionalAntigo.Arquivos = profissionalAntigo.Arquivos ?? new List<Arquivo>();
            var arquivos = Context.Arquivos.ToList();
            foreach (var arquivo in arquivos)
            {
                if (arquivosIds != null && arquivosIds.Any(x => x.Equals(arquivo.Id)))
                {
                    profissionalNovo.Arquivos.Add(Context.Arquivos.FirstOrDefault(x => x.Id == arquivo.Id));
                }
            }
            foreach (var arquivo in arquivos)
            {
                var arquivoAntigo = profissionalAntigo.Arquivos.FirstOrDefault(x => x.Id == arquivo.Id);
                if (arquivosIds != null && arquivosIds.Contains(arquivo.Id) && arquivoAntigo == null)
                {
                    profissionalAntigo.Arquivos.Add(Context.Arquivos.FirstOrDefault(x => x.Id == arquivo.Id));
                }
                else
                {
                    if (arquivoAntigo == null) continue;
                    if (arquivosIds != null && arquivosIds.Contains(arquivoAntigo.Id)) continue;
                    profissionalAntigo.Arquivos.Remove(arquivoAntigo);
                }
            }
        }

        public override ProfissionalIndicado Atualizar(ProfissionalIndicado profissionalNovo)
        {
            if (profissionalNovo.Arquivos.Any())
            {
                var profissionalAntigo = DbSet.Include(x => x.Arquivos).FirstOrDefault(x => x.Id == profissionalNovo.Id);
                BalanciarListaArquivos(profissionalAntigo, profissionalNovo);              
                Context.Entry(profissionalAntigo).CurrentValues.SetValues(profissionalNovo);
            }
            Context.SaveChanges();
            return profissionalNovo;
        }
    }
}
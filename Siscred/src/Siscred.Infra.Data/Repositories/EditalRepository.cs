using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Infra.Data.Context;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Siscred.Infra.Data.Repositories
{
    public class EditalRepository : BaseRepository<Edital>, IEditalRepository
    {
        public EditalRepository(MainContext mainContext) : base(mainContext) { }

        private void BalanciarListaCidades(Edital editalAntigo, Edital editalNovo)
        {
            var cidadesIds = editalNovo.Cidades.Select(x => x.Id).ToArray();
            editalNovo.Cidades = new List<Cidade>();
            editalAntigo.Cidades = editalAntigo.Cidades ?? new List<Cidade>();
            var cidades = Context.Cidades.ToList();
            foreach (var cidade in cidades)
            {
                if (cidadesIds != null && cidadesIds.Any(x => x.Equals(cidade.Id)))
                {
                    editalNovo.Cidades.Add(Context.Cidades.FirstOrDefault(x => x.Id == cidade.Id));
                }
            }
            foreach (var cidade in cidades)
            {
                var cidadeAntigo = editalAntigo.Cidades.FirstOrDefault(x => x.Id == cidade.Id);
                if (cidadesIds != null && cidadesIds.Contains(cidade.Id) && cidadeAntigo == null)
                {
                    editalAntigo.Cidades.Add(Context.Cidades.FirstOrDefault(x => x.Id == cidade.Id));
                }
                else
                {
                    if (cidadeAntigo == null) continue;
                    if (cidadesIds != null && cidadesIds.Contains(cidadeAntigo.Id)) continue;
                    editalAntigo.Cidades.Remove(cidadeAntigo);
                }
            }
        }

        private void BalanciarListaTiposArquivos(Edital editalAntigo, Edital editalNovo)
        {
            var tiposArquivosIds = editalNovo.TiposArquivos.Select(x => x.Id).ToArray();
            editalNovo.TiposArquivos = new List<TipoArquivo>();
            editalAntigo.TiposArquivos = editalAntigo.TiposArquivos ?? new List<TipoArquivo>();
            var tiposarquivos = Context.TiposArquivos.ToList();
            foreach (var tipoarquivo in tiposarquivos)
            {
                if (tiposArquivosIds != null && tiposArquivosIds.Any(x => x.Equals(tipoarquivo.Id)))
                {
                    editalNovo.TiposArquivos.Add(Context.TiposArquivos.FirstOrDefault(x => x.Id == tipoarquivo.Id));
                }
            }
            foreach (var tipoarquivo in tiposarquivos)
            {
                var tipoArquivoAntigo = editalAntigo.TiposArquivos.FirstOrDefault(x => x.Id == tipoarquivo.Id);
                if (tiposArquivosIds != null && tiposArquivosIds.Contains(tipoarquivo.Id) && tipoArquivoAntigo == null)
                {
                    editalAntigo.TiposArquivos.Add(Context.TiposArquivos.FirstOrDefault(x => x.Id == tipoarquivo.Id));
                }
                else
                {
                    if (tipoArquivoAntigo == null) continue;
                    if (tiposArquivosIds != null && tiposArquivosIds.Contains(tipoArquivoAntigo.Id)) continue;
                    editalAntigo.TiposArquivos.Remove(tipoArquivoAntigo);
                }
            }
        }

        public override Edital Atualizar(Edital editalNovo)
        {
            if (editalNovo.Cidades.Any() && editalNovo.TiposArquivos.Any())
            {
                var editalAntigo = DbSet.Include(x => x.Cidades).Include(x => x.TiposArquivos).FirstOrDefault(x => x.Id == editalNovo.Id);
                BalanciarListaCidades(editalAntigo, editalNovo);
                BalanciarListaTiposArquivos(editalAntigo, editalNovo);
                Context.Entry(editalAntigo).CurrentValues.SetValues(editalNovo);
                
            }
            Context.SaveChanges();
            return editalNovo;
        }
    }
}
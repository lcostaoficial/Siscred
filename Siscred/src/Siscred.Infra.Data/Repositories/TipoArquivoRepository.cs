using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Infra.Data.Context;

namespace Siscred.Infra.Data.Repositories
{
    public class TipoArquivoRepository : BaseRepository<TipoArquivo>, ITipoArquivoRepository
    {
        public TipoArquivoRepository(MainContext mainContext) : base(mainContext) { }
    }
}
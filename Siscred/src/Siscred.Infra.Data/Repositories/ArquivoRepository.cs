using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Infra.Data.Context;

namespace Siscred.Infra.Data.Repositories
{
    public class ArquivoRepository : BaseRepository<Arquivo>, IArquivoRepository
    {
        public ArquivoRepository(MainContext mainContext) : base(mainContext) { }
    }
}
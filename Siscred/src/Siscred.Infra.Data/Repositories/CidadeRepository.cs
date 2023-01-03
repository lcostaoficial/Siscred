using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Infra.Data.Context;

namespace Siscred.Infra.Data.Repositories
{
    public class CidadeRepository : BaseRepository<Cidade>, ICidadeRepository
    {
        public CidadeRepository(MainContext mainContext) : base(mainContext) { }
    }
}
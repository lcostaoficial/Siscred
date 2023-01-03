using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Infra.Data.Context;

namespace Siscred.Infra.Data.Repositories
{
    public class InscricaoRepository : BaseRepository<Inscricao>, IInscricaoRepository
    {
        public InscricaoRepository(MainContext mainContext) : base(mainContext) { }
    }
}
using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Infra.Data.Context;

namespace Siscred.Infra.Data.Repositories
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(MainContext mainContext) : base(mainContext) { }
    }
}
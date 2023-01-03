using Siscred.Domain.Entities;
using System.Collections.Generic;

namespace Siscred.Domain.Interfaces.Services
{
    public interface ICidadeService : IBaseService<Cidade>
    {
        IEnumerable<Cidade> ObterPorEditalId(int id);
    }
}
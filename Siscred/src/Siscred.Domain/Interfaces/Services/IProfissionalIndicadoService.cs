using Siscred.Domain.Entities;
using System.Collections.Generic;

namespace Siscred.Domain.Interfaces.Services
{
    public interface IProfissionalIndicadoService : IBaseService<ProfissionalIndicado>
    {
        IEnumerable<ProfissionalIndicado> ObterTodosPorInscricaoId(int inscricaoId);
        ProfissionalIndicado ObterPorIdComInscricao(int id);
        void Remover(int id);
    }
}
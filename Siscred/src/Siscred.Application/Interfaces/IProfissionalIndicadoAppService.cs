using Siscred.Application.ViewModel;
using System.Collections.Generic;

namespace Siscred.Application.Interfaces
{
    public interface IProfissionalIndicadoAppService : IAppServiceBase<ProfissionalIndicadoVm>
    {
        IEnumerable<ProfissionalIndicadoVm> ObterTodosPorInscricaoId(int inscricaoId);
        ProfissionalIndicadoVm ObterPorIdComInscricao(int id);
        void Remover(int id);
    }
}
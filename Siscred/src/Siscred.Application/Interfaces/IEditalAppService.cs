using Siscred.Application.ViewModel;
using System.Collections.Generic;

namespace Siscred.Application.Interfaces
{
    public interface IEditalAppService : IAppServiceBase<EditalVm>
    {
        void Desativar(EditalVm model);
        void Remover(EditalVm model);
        IEnumerable<EditalVm> ObterTodosComEdital(int usuarioId);
    }
}
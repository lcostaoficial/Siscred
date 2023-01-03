using Siscred.Application.ViewModel;
using System.Collections.Generic;

namespace Siscred.Application.Interfaces
{
    public interface ICidadeAppService : IAppServiceBase<CidadeVm>
    {
        IEnumerable<CidadeVm> ObterPorEditalId(int id);
    }
}
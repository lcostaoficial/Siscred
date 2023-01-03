using Siscred.Application.ViewModel;
using System.Collections.Generic;

namespace Siscred.Application.Interfaces
{
    public interface ITipoArquivoAppService : IAppServiceBase<TipoArquivoVm>
    {
        TipoArquivoVm Remover(TipoArquivoVm model);
        TipoArquivoVm Desativar(TipoArquivoVm model);
        IEnumerable<TipoArquivoVm> ObterPorEditalIdEmpresa(int id);
        IEnumerable<TipoArquivoVm> ObterPorEditalIdIndicado(int id);
    }
}
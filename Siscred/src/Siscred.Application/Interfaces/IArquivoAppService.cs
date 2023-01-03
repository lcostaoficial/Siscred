using Siscred.Application.ViewModel;
using System.Collections.Generic;

namespace Siscred.Application.Interfaces
{
    public interface IArquivoAppService : IAppServiceBase<ArquivoVm>
    {
        IEnumerable<ArquivoVm> ObterTodosArquivosInscricaoId(int inscricaoId);
    }
}
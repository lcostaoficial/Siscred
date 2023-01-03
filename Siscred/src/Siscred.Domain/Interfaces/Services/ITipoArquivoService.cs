using Siscred.Domain.Entities;
using System.Collections.Generic;

namespace Siscred.Domain.Interfaces.Services
{
    public interface ITipoArquivoService : IBaseService<TipoArquivo>
    {
        TipoArquivo Remover(TipoArquivo model);
        TipoArquivo Desativar(TipoArquivo model);
        IEnumerable<TipoArquivo> ObterPorEditalIdEmpresa(int id);
        IEnumerable<TipoArquivo> ObterPorEditalIdIndicado(int id);
    }
}
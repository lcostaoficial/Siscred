using Siscred.Domain.Entities;
using System.Collections.Generic;

namespace Siscred.Domain.Interfaces.Services
{
    public interface IArquivoService : IBaseService<Arquivo>
    {
        IEnumerable<Arquivo> ObterTodosArquivosInscricaoId(int inscricaoId);
    }
}
using Siscred.Application.ViewModel;
using System;
using System.Collections.Generic;

namespace Siscred.Application.Interfaces
{
    public interface ITokenAppService : IAppServiceBase<TokenVm>
    {
        LoginVm UtilizarToken(Guid token);
        TokenVm ObterPorChave(Guid token);
        TokenVm GerarToken(string email, TipoTokenVm tipoToken);
        TokenVm RedefinirSenha(Guid token, string novaSenha);
        TokenVm AlterarSenha(Guid token, string novaSenha);
        IEnumerable<TokenVm> ObterTokensAtivos(int usuarioId, TipoTokenVm tipoToken);
    }
}
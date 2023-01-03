using Siscred.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Siscred.Domain.Interfaces.Services
{
    public interface ITokenService : IBaseService<Token>
    {
        Token UtilizarToken(Guid token);
        Token ObterPorChave(Guid token);
        Token GerarToken(string email, TipoToken tipoToken);
        Token RedefinirSenha(Guid token, string novaSenha);
        Token AlterarSenha(Guid token, string novaSenha);
        IEnumerable<Token> ObterTokensAtivos(int usuarioId, TipoToken tipoToken);
    }
}
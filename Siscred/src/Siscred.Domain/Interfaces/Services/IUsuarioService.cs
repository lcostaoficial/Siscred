using Siscred.Domain.Entities;
using System.Collections.Generic;

namespace Siscred.Domain.Interfaces.Services
{
    public interface IUsuarioService : IBaseService<Usuario>
    {
        Usuario ValidarLogin(Usuario model);
        Usuario ObterPorEmail(string email);
        Usuario Remover(Usuario model);
        Usuario Desativar(Usuario model);
        IEnumerable<Usuario> ObterTodosGestor();
        int TotalUsuarios();
    }
}
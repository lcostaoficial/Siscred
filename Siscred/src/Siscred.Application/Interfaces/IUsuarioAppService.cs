using Siscred.Application.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Siscred.Application.Interfaces
{
    public interface IUsuarioAppService 
    {
        Task AdicionarEmpresa(UsuarioVm model);
        Task RecuperarSenha(RecuperarSenhaVm model);
        void AdicionarGestor(GestorVm model);
        void AtualizarEmpresa(UsuarioVm model);
        void AtualizarGestor(UsuarioVm model);
        UsuarioVm ObterPorId(object id);
        UsuarioVm ObterPorEmail(string email);
        IEnumerable<UsuarioVm> ObterTodosPaginado(Expression<Func<UsuarioVm, bool>> expressionWhere, int start, int length, out int recordsTotal);
        IEnumerable<UsuarioVm> ObterTodos();
        UsuarioVm ValidarLogin(LoginVm model);
        UsuarioVm Remover(UsuarioVm model);
        UsuarioVm Desativar(UsuarioVm model);
        IEnumerable<UsuarioVm> ObterTodosGestor();
        int TotalUsuarios();
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Siscred.Application.Interfaces
{
    public interface IAppServiceBase<TModel> : IDisposable where TModel : class
    {
        void Adicionar(TModel model);
        void Atualizar(TModel model);
        TModel ObterPorId(object id);
        IEnumerable<TModel> ObterTodosPaginado(Expression<Func<TModel, bool>> expressionWhere, int start, int length, out int recordsTotal);
        IEnumerable<TModel> ObterTodos();
    }
}
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Siscred.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<TEntity> : IDisposable where TEntity : class
    {
        TEntity Adicionar(TEntity entity);
        TEntity Atualizar(TEntity entity);
        TEntity Remover(TEntity entity);
        TEntity ObterUnico(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
        bool Existe(Expression<Func<TEntity, bool>> expression);
        int Contar(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> ObterTodos(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> ObterTodosFilter(Expression<Func<TEntity, bool>> expression, params Expression<Func<TEntity, object>>[] includes);
        IEnumerable<TEntity> ObterTodos();
        IEnumerable<TEntity> ObterTodosPaginado(Expression<Func<TEntity, bool>> expressionWhere, Expression<Func<TEntity, object>> expressionOrder, int start, int length, out int recordsTotal, params Expression<Func<TEntity, object>>[] includes);
    }
}
using Siscred.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Siscred.Domain.Interfaces.Services
{
    public interface IEditalService : IDisposable
    {
        void Adicionar(Edital model, int[] cidadesIds, int[] tiposArquivosIds);
        void Atualizar(Edital model, int[] cidadesIds, int[] tiposArquivosIds);
        Edital ObterPorId(object id);
        IEnumerable<Edital> ObterTodosPaginado(Expression<Func<Edital, bool>> expressionWhere, int start, int length, out int recordsTotal);
        IEnumerable<Edital> ObterTodos();
        Edital Remover(Edital model);
        Edital Desativar(Edital model);
        IEnumerable<Edital> ObterTodosComEdital(int usuarioId);
    }
}
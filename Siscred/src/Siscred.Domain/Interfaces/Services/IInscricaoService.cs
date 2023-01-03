using Siscred.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Siscred.Domain.Interfaces.Services
{
    public interface IInscricaoService : IDisposable
    {
        void Adicionar(Inscricao model);
        void Atualizar(Inscricao model);
        Inscricao ObterPorId(object id);
        Inscricao ObterPorCnpj(string cnpj);
        Inscricao ObterPorUsuarioId(int usuarioId);
        Inscricao ValidarDuplicidadeInscricaoUsuario(int usuarioId, int editalId);
        Inscricao ValidarDuplicidadeInscricaoProfissional(string cpf, int inscricaoId);
        IEnumerable<Inscricao> ObterTodosPaginado(Expression<Func<Inscricao, bool>> expressionWhere, int start, int length, out int recordsTotal);
        IEnumerable<Inscricao> ObterTodos();
        IEnumerable<Inscricao> ObterTodosPorUsuarioId(int usuarioId);
        IEnumerable<Inscricao> ObterTodosPorEditalId(int editalId);
        Inscricao ObterPorIdCustom(int id);
        int TotalInscricoes();
        Inscricao ObterPorIdParaComprovante(int id);
        Inscricao ObterPorIdParaFicha(int id);
    }
}
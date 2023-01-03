using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Domain.Interfaces.Services;

namespace Siscred.Domain.Services
{
    public class CidadeService : ICidadeService
    {
        private readonly ICidadeRepository _repository;

        public CidadeService(ICidadeRepository repository)
        {
            _repository = repository;
        }

        public void Adicionar(Cidade model)
        {
            _repository.Adicionar(model);
        }

        public void Atualizar(Cidade model)
        {
            _repository.Atualizar(model);
        }

        public Cidade ObterPorId(object id)
        {
            return _repository.ObterUnico(x => x.Id == (int)id);
        }

        public IEnumerable<Cidade> ObterPorEditalId(int id)
        {
            return _repository.ObterTodos(x => x.Editais.Any(y => y.Ativo && y.Id == id));
        }

        public IEnumerable<Cidade> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        public IEnumerable<Cidade> ObterTodosPaginado(Expression<Func<Cidade, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return _repository.ObterTodosPaginado(expressionWhere, x => x.Id, start, length, out recordsTotal, null);
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
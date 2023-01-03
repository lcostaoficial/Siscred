using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Siscred.Domain.Services
{
    public class ArquivoService : IArquivoService
    {
        private readonly IArquivoRepository _repository;

        public ArquivoService(IArquivoRepository repository)
        {
            _repository = repository;
        }

        public void Adicionar(Arquivo model)
        {
            _repository.Adicionar(model);
        }

        public void Atualizar(Arquivo model)
        {
            _repository.Atualizar(model);
        }

        public Arquivo ObterPorId(object id)
        {
            return _repository.ObterUnico(x => x.Id == (int)id);
        }

        public IEnumerable<Arquivo> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        public IEnumerable<Arquivo> ObterTodosArquivosInscricaoId(int inscricaoId)
        {
            return _repository.ObterTodos(x => x.Inscricoes.Any(y => y.Id == inscricaoId) || x.ProfissionaisIndicados.Any(y => y.InscricaoId == inscricaoId));
        }

        public IEnumerable<Arquivo> ObterTodosPaginado(Expression<Func<Arquivo, bool>> expressionWhere, int start, int length, out int recordsTotal)
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
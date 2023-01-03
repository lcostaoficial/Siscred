using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Siscred.Domain.Services
{
    public class TipoArquivoService : ITipoArquivoService
    {
        private readonly ITipoArquivoRepository _repository;

        public TipoArquivoService(ITipoArquivoRepository repository)
        {
            _repository = repository;
        }

        public void Adicionar(TipoArquivo model)
        {
            _repository.Adicionar(model);
        }

        public void Atualizar(TipoArquivo model)
        {
            var tipoArquivo = ObterPorId(model.Id);
            tipoArquivo.Atualizar(model);
            _repository.Atualizar(tipoArquivo);
        }

        public TipoArquivo ObterPorId(object id)
        {
            return _repository.ObterUnico(x => x.Id == (int)id);
        }

        public IEnumerable<TipoArquivo> ObterPorEditalIdEmpresa(int id)
        {
            return _repository.ObterTodos(x => (x.FinalidadeArquivo == FinalidadeArquivo.Empresa || x.FinalidadeArquivo == FinalidadeArquivo.Todos) && x.Ativo && x.Editais.Any(y => y.Ativo && y.Id == id));
        }

        public IEnumerable<TipoArquivo> ObterPorEditalIdIndicado(int id)
        {
            return _repository.ObterTodos(x => (x.FinalidadeArquivo == FinalidadeArquivo.Indicado || x.FinalidadeArquivo == FinalidadeArquivo.Todos) && x.Ativo && x.Editais.Any(y => y.Ativo && y.Id == id));
        }       

        public IEnumerable<TipoArquivo> ObterTodosPaginado(Expression<Func<TipoArquivo, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return _repository.ObterTodosPaginado(expressionWhere, x => x.Descricao, start, length, out recordsTotal, null);
        }

        public IEnumerable<TipoArquivo> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        public TipoArquivo Remover(TipoArquivo model)
        {
            return _repository.Remover(model);
        }

        public TipoArquivo Desativar(TipoArquivo model)
        {
            var unidade = ObterPorId(model.Id);
            unidade.InverterAtivo();
            return _repository.Atualizar(unidade);
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
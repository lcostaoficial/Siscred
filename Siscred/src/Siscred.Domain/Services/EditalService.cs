using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Domain.Interfaces.Services;
using Siscred.Infra.CrossCutting.Messages;

namespace Siscred.Domain.Services
{
    public class EditalService : IEditalService
    {
        private readonly IEditalRepository _repository;
        private readonly ICidadeService _cidadeService;
        private readonly ITipoArquivoService _tipoArquivoService;
        private readonly IInscricaoService _inscricaoService;

        public EditalService(IEditalRepository repository, ICidadeService cidadeService, ITipoArquivoService tipoArquivoService, IInscricaoService inscricaoService)
        {
            _repository = repository;
            _cidadeService = cidadeService;
            _tipoArquivoService = tipoArquivoService;
            _inscricaoService = inscricaoService;
        }

        public void Adicionar(Edital model, int[] cidadesIds, int[] tiposArquivosIds)
        {
            model = SetarLista(model, cidadesIds, tiposArquivosIds);
            _repository.Adicionar(model);
        }

        public void Atualizar(Edital model, int[] cidadesIds, int[] tiposArquivosIds)
        {
            model = SetarLista(model, cidadesIds, tiposArquivosIds);
            _repository.Atualizar(model);
        }

        public Edital Remover(Edital model)
        {
            return _repository.Remover(model);
        }

        public Edital Desativar(Edital model)
        {
            var edital = _repository.ObterUnico(x => x.Id == model.Id);
            edital.InverterAtivo();       
            return _repository.Atualizar(model);
        }

        public Edital ObterPorId(object id)
        {
            return _repository.ObterUnico(x => x.Id == (int)id, x => x.Cidades, x => x.TiposArquivos);
        }

        public IEnumerable<Edital> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        public IEnumerable<Edital> ObterTodosComEdital(int usuarioId)
        {         
            return _repository.ObterTodosFilter(null, x => x.Inscricoes.Where(y => y.UsuarioId == usuarioId));
        }

        public IEnumerable<Edital> ObterTodosPaginado(Expression<Func<Edital, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return _repository.ObterTodosPaginado(expressionWhere, x => x.Id, start, length, out recordsTotal, null);
        }

        private Edital SetarLista(Edital model, int[] cidadesIds, int[] tiposArquivosIds)
        {
            if (cidadesIds == null || !cidadesIds.Any()) throw new Exception(Error.CityRequired);
            if (tiposArquivosIds == null || !tiposArquivosIds.Any()) throw new Exception(Error.FileTypeRequired);
            model.Cidades = new List<Cidade>();
            model.TiposArquivos = new List<TipoArquivo>();
            foreach (var cidadeId in cidadesIds)
            {
                var cidade = _cidadeService.ObterPorId(cidadeId);
                model.Cidades.Add(cidade);
            }
            foreach (var tipoArquivoId in tiposArquivosIds)
            {
                var tipoArquivo = _tipoArquivoService.ObterPorId(tipoArquivoId);
                model.TiposArquivos.Add(tipoArquivo);
            }
            return model;
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
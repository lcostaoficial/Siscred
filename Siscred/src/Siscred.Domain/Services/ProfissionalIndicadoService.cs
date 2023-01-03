using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Domain.Interfaces.Services;

namespace Siscred.Domain.Services
{
    public class ProfissionalIndicadoService : IProfissionalIndicadoService
    {
        private readonly IProfissionalIndicadoRepository _repository;
        private readonly IArquivoRepository _arquivoRepository;
        private readonly ITipoArquivoService _tipoArquivoService;
        private readonly IInscricaoService _InscricaoService;

        public ProfissionalIndicadoService(IProfissionalIndicadoRepository repository, IArquivoRepository arquivoRepository, ITipoArquivoService tipoArquivoService, IInscricaoService inscricaoService)
        {
            _repository = repository;
            _arquivoRepository = arquivoRepository;
            _tipoArquivoService = tipoArquivoService;
            _InscricaoService = inscricaoService;
        }

        public void Adicionar(ProfissionalIndicado model)
        {
            var inscricao = _InscricaoService.ObterPorId(model.InscricaoId);
            var tiposArquivos = _tipoArquivoService.ObterPorEditalIdIndicado(inscricao.EditalId);
            model.ValidarArquivosExigidos(tiposArquivos.Where(x => x.Obrigatorio), model.Arquivos);
            _repository.Adicionar(model);
        }

        public void Atualizar(ProfissionalIndicado model)
        {
            _repository.Atualizar(model);
        }      

        public ProfissionalIndicado ObterPorId(object id)
        {
            return _repository.ObterUnico(x => x.Id == (int)id);
        }

        public ProfissionalIndicado ObterPorIdComInscricao(int id)
        {
            return _repository.ObterUnico(x => x.Id == id, x => x.Inscricao, x => x.Arquivos);
        }

        public IEnumerable<ProfissionalIndicado> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        public IEnumerable<ProfissionalIndicado> ObterTodosPorInscricaoId(int inscricaoId)
        {
            return _repository.ObterTodos(x => x.InscricaoId == inscricaoId);
        } 

        public IEnumerable<ProfissionalIndicado> ObterTodosPaginado(Expression<Func<ProfissionalIndicado, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return _repository.ObterTodosPaginado(expressionWhere, x => x.Id, start, length, out recordsTotal, null);
        }

        public void Remover(int id)
        {
            var model = _repository.ObterUnico(x => x.Id == id, x => x.Arquivos);
            var arquivos = _arquivoRepository.ObterTodos(x => x.ProfissionaisIndicados.Any(y => y.Id == id)).ToList();
            _repository.Remover(model);
            foreach (var item in arquivos)
            {              
                _arquivoRepository.Remover(item);
            }           
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
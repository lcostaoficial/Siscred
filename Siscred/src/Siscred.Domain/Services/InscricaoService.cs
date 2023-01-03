using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Domain.Interfaces.Services;
using Siscred.Infra.CrossCutting.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Siscred.Domain.Services
{
    public class InscricaoService : IInscricaoService
    {
        private readonly IInscricaoRepository _repository;
        private readonly ITipoArquivoService _tipoArquivoService;
        private readonly ICidadeService _cidadeService;

        public InscricaoService(IInscricaoRepository repository, ITipoArquivoService tipoArquivoService, ICidadeService cidadeService)
        {
            _repository = repository;
            _tipoArquivoService = tipoArquivoService;
            _cidadeService = cidadeService;
        }

        public void Adicionar(Inscricao model)
        {
            var tiposArquivos = _tipoArquivoService.ObterPorEditalIdEmpresa(model.EditalId);
            var cidades = _cidadeService.ObterPorEditalId(model.EditalId);
            foreach (var item in model.InscricoesCidades)
            {
                if (!cidades.Any(x => x.Id == item.CidadeId)) throw new Exception(Error.InvalidMicroregion);
            }
            model.ValidarArquivosExigidos(tiposArquivos.Where(x => x.Obrigatorio), model.Arquivos);
            model.SetarData();
            model.SetarProtocolo();
            model.SituacaoInscricao = SituacaoInscricao.Pendente;
            _repository.Adicionar(model);
        }

        public void Atualizar(Inscricao model)
        {
            var novo = model;
            model = ObterPorId(model.Id);
            model.SituacaoInscricao = novo.SituacaoInscricao;
            if (!string.IsNullOrEmpty(novo.Justificativa)) model.Justificativa = novo.Justificativa;
            if (novo.SituacaoInscricao == SituacaoInscricao.Inscrito) model.Data = DateTime.Now;
            _repository.Atualizar(model);
        }

        public Inscricao ObterPorId(object id)
        {
            return _repository.ObterUnico(x => x.Id == (int)id);
        }

        public Inscricao ObterPorIdCustom(int id)
        {
            return _repository.ObterUnico(
                x => x.Id == id,
                x => x.Arquivos.Select(z => z.TipoArquivo),
                x => x.ProfissionaisIndicados.Select(y => y.Arquivos.Select(z => z.TipoArquivo)), 
                x => x.InscricoesCidades.Select(y => y.Cidade)
            );
        }

        public Inscricao ObterPorIdParaComprovante(int id)
        {
            return _repository.ObterUnico(
                x => x.Id == id,              
                x => x.Arquivos.Select(z => z.TipoArquivo),
                x => x.ProfissionaisIndicados.Select(y => y.Arquivos.Select(z => z.TipoArquivo)),
                x => x.InscricoesCidades.Select(y => y.Cidade)
            );
        }

        public Inscricao ObterPorIdParaFicha(int id)
        {
            return _repository.ObterUnico(
               x => x.Id == id,             
               x => x.ProfissionaisIndicados,
               x => x.InscricoesCidades.Select(y => y.Cidade)
           );
        }

        public Inscricao ObterPorCnpj(string cnpj)
        {
            return _repository.ObterUnico(x => x.Cnpj == cnpj);
        }

        public Inscricao ObterPorUsuarioId(int usuarioId)
        {
            return _repository.ObterUnico(x => x.UsuarioId == usuarioId);
        }       

        public Inscricao ValidarDuplicidadeInscricaoUsuario(int usuarioId, int editalId)
        {
            return _repository.ObterUnico(x => x.UsuarioId == usuarioId && x.EditalId == editalId);
        }

        public Inscricao ValidarDuplicidadeInscricaoProfissional(string cpf, int inscricaoId)
        {
            return _repository.ObterUnico(x => x.Id == inscricaoId && x.ProfissionaisIndicados.Any(y => y.Cpf == cpf));
        }

        public IEnumerable<Inscricao> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        public IEnumerable<Inscricao> ObterTodosPorUsuarioId(int usuarioId)
        {
            return _repository.ObterTodos(x => x.UsuarioId == usuarioId);
        }

        public IEnumerable<Inscricao> ObterTodosPorEditalId(int editalId)
        {
            return _repository.ObterTodos(x => x.EditalId == editalId && x.Edital.Ativo);
        }

        public IEnumerable<Inscricao> ObterTodosPaginado(Expression<Func<Inscricao, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return _repository.ObterTodosPaginado(expressionWhere, x => x.Id, start, length, out recordsTotal, null);
        }

        public int TotalInscricoes()
        {
            return _repository.Contar(x => x.SituacaoInscricao == SituacaoInscricao.Inscrito);
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
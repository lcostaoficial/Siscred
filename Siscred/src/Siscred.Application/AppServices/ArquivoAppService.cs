using Siscred.Application.Interfaces;
using Siscred.Application.Mapper;
using Siscred.Application.ViewModel;
using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Siscred.Application.AppServices
{
    public class ArquivoAppService : IArquivoAppService
    {
        private readonly IArquivoService _service;

        public ArquivoAppService(IArquivoService service)
        {
            _service = service;
        }

        public void Adicionar(ArquivoVm model)
        {
            _service.Adicionar(MapperConfig.Mapper.Map<Arquivo>(model));
        }

        public void Atualizar(ArquivoVm model)
        {
            _service.Atualizar(MapperConfig.Mapper.Map<Arquivo>(model));
        }

        public ArquivoVm ObterPorId(object id)
        {
            return MapperConfig.Mapper.Map<ArquivoVm>(_service.ObterPorId(id));
        }    

        public IEnumerable<ArquivoVm> ObterTodos()
        {
            return MapperConfig.Mapper.Map<ICollection<ArquivoVm>>(_service.ObterTodos());
        }

        public IEnumerable<ArquivoVm> ObterTodosPaginado(Expression<Func<ArquivoVm, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return MapperConfig.Mapper.Map<ICollection<ArquivoVm>>(_service.ObterTodosPaginado(MapperConfig.Mapper.Map<Expression<Func<Arquivo, bool>>>(expressionWhere), start, length, out recordsTotal));
        }

        public IEnumerable<ArquivoVm> ObterTodosArquivosInscricaoId(int inscricaoId)
        {
            return MapperConfig.Mapper.Map<ICollection<ArquivoVm>>(_service.ObterTodosArquivosInscricaoId(inscricaoId));
        }

        public void Dispose()
        {
            _service.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
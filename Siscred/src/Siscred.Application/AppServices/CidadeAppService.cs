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
    public class CidadeAppService : ICidadeAppService
    {
        private readonly ICidadeService _service;

        public CidadeAppService(ICidadeService service)
        {
            _service = service;
        }

        public void Adicionar(CidadeVm model)
        {
            _service.Adicionar(MapperConfig.Mapper.Map<Cidade>(model));
        }

        public void Atualizar(CidadeVm model)
        {
            _service.Atualizar(MapperConfig.Mapper.Map<Cidade>(model));
        }

        public CidadeVm ObterPorId(object id)
        {
            return MapperConfig.Mapper.Map<CidadeVm>(_service.ObterPorId(id));
        }

        public IEnumerable<CidadeVm> ObterPorEditalId(int id)
        {
            return MapperConfig.Mapper.Map<ICollection<CidadeVm>>(_service.ObterPorEditalId(id));
        }

        public IEnumerable<CidadeVm> ObterTodos()
        {
            return MapperConfig.Mapper.Map<ICollection<CidadeVm>>(_service.ObterTodos());
        }

        public IEnumerable<CidadeVm> ObterTodosPaginado(Expression<Func<CidadeVm, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return MapperConfig.Mapper.Map<ICollection<CidadeVm>>(_service.ObterTodosPaginado(MapperConfig.Mapper.Map<Expression<Func<Cidade, bool>>>(expressionWhere), start, length, out recordsTotal));
        }

        public void Dispose()
        {
            _service.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
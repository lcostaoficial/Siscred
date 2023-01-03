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
    public class TokenAppService : ITokenAppService
    {
        private readonly ITokenService _service;

        public TokenAppService(ITokenService service)
        {
            _service = service;
        }

        public void Adicionar(TokenVm model)
        {
            _service.Adicionar(MapperConfig.Mapper.Map<Token>(model));
        }

        public void Atualizar(TokenVm model)
        {
            _service.Atualizar(MapperConfig.Mapper.Map<Token>(model));
        }

        public TokenVm GerarToken(string email, TipoTokenVm tipoToken)
        {
            return MapperConfig.Mapper.Map<TokenVm>(_service.GerarToken(email, MapperConfig.Mapper.Map<TipoToken>(tipoToken)));
        }

        public TokenVm RedefinirSenha(Guid token, string novaSenha)
        {
            return MapperConfig.Mapper.Map<TokenVm>(_service.RedefinirSenha(token, novaSenha));
        }

        public TokenVm AlterarSenha(Guid token, string novaSenha)
        {
            return MapperConfig.Mapper.Map<TokenVm>(_service.AlterarSenha(token, novaSenha));
        }

        public LoginVm UtilizarToken(Guid token)
        {
            return MapperConfig.Mapper.Map<LoginVm>(_service.UtilizarToken(token).Usuario);
        }

        public TokenVm ObterPorId(object id)
        {
            return MapperConfig.Mapper.Map<TokenVm>(_service.ObterPorId(id));
        }

        public TokenVm ObterPorChave(Guid token)
        {
            return MapperConfig.Mapper.Map<TokenVm>(_service.ObterPorChave(token));
        }

        public IEnumerable<TokenVm> ObterTodosPaginado(Expression<Func<TokenVm, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return MapperConfig.Mapper.Map<ICollection<TokenVm>>(_service.ObterTodosPaginado(MapperConfig.Mapper.Map<Expression<Func<Token, bool>>>(expressionWhere), start, length, out recordsTotal));
        }

        public IEnumerable<TokenVm> ObterTodos()
        {
            return MapperConfig.Mapper.Map<ICollection<TokenVm>>(_service.ObterTodos());
        }

        public IEnumerable<TokenVm> ObterTokensAtivos(int usuarioId, TipoTokenVm tipoToken)
        {
            return MapperConfig.Mapper.Map<ICollection<TokenVm>>(_service.ObterTokensAtivos(usuarioId, MapperConfig.Mapper.Map<TipoToken>(tipoToken)));
        }

        public void Dispose()
        {
            _service.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Domain.Interfaces.Services;
using Siscred.Infra.CrossCutting.Messages;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Siscred.Domain.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository _repository;
        private readonly IUsuarioService _usuarioService;

        public TokenService(ITokenRepository repository, IUsuarioService usuarioService)
        {
            _repository = repository;
            _usuarioService = usuarioService;
        }

        public void Adicionar(Token model)
        {
            _repository.Adicionar(model);
        }

        public void Atualizar(Token model)
        {
            _repository.Atualizar(model);
        }

        public IEnumerable<Token> ObterTokensAtivos(int usuarioId, TipoToken tipoToken)
        {
            return _repository.ObterTodos(x => x.UsuarioId == usuarioId && x.TipoToken == tipoToken && x.Ativo);
        }

        public Token GerarToken(string email, TipoToken tipoToken)
        {
            var usuario = _usuarioService.ObterPorEmail(email);
            if (usuario == null) throw new Exception(Error.AccountNotExist);
            var token = new Token
            {
                Chave = Guid.NewGuid(),
                DataEmissao = DateTime.Now,
                TipoToken = tipoToken,
                Usuario = usuario,
                Ativo = true
            };
            _repository.Adicionar(token);
            return token;
        }

        public Token UtilizarToken(Guid token)
        {
            var model = _repository.ObterUnico(x => x.Chave == token, x => x.Usuario);
            if (model == null) throw new Exception(Error.InvalidToken);
            if (!model.Ativo) throw new Exception(Error.InvalidToken);
            if (model.TipoToken != TipoToken.Ativacao) throw new Exception(Error.InvalidToken);
            model.Usuario.AtivarConta();
            model.DesativarToken();
            _repository.Atualizar(model);
            return model;
        }       

        public Token RedefinirSenha(Guid token, string novaSenha)
        {
            var model = ObterPorChave(token);
            if (model == null) throw new Exception(Error.InvalidToken);
            if (!model.Ativo) throw new Exception(Error.InvalidToken);
            if (model.TipoToken != TipoToken.Recuperacao) throw new Exception(Error.InvalidToken);
            model.Usuario.EncriptarSenha(novaSenha);
            model.Usuario.AtivarConta();
            model.DesativarToken();
            _repository.Atualizar(model);
            return model;
        }

        public Token AlterarSenha(Guid token, string novaSenha)
        {
            var model = ObterPorChave(token);
            if (model == null) throw new Exception(Error.InvalidToken);
            if (!model.Ativo) throw new Exception(Error.InvalidToken);
            if (model.TipoToken != TipoToken.Alteracao) throw new Exception(Error.InvalidToken);
            model.Usuario.EncriptarSenha(novaSenha);       
            model.DesativarToken();
            _repository.Atualizar(model);
            return model;
        }

        public Token ObterPorChave(Guid token)
        {
            return _repository.ObterUnico(x => x.Chave == token && x.Ativo, x => x.Usuario);
        }

        public Token ObterPorId(object id)
        {
            return _repository.ObterUnico(x => x.Id == (int)id);
        }

        public IEnumerable<Token> ObterTodosPaginado(Expression<Func<Token, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return _repository.ObterTodosPaginado(expressionWhere, x => x.Chave, start, length, out recordsTotal, null);
        }

        public IEnumerable<Token> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
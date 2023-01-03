using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Repositories;
using Siscred.Domain.Interfaces.Services;
using Siscred.Infra.CrossCutting.Messages;

namespace Siscred.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioService(IUsuarioRepository repository)
        {
            _repository = repository;
        }

        public Usuario ValidarLogin(Usuario model)
        {
            var senha = model.Senha;
            model = _repository.ObterUnico(x => x.Email == model.Email);
            if (model == null) throw new Exception(Error.AccountNotExist);
            if (!model.ValidarSenha(senha)) throw new Exception(Error.InvalidPassword);
            if (!model.Ativo) throw new Exception(Error.AccountDisabled);  
            return model;
        }

        public void Adicionar(Usuario model)
        {
            if (VerificarExistenciaEmail(model.Email)) throw new Exception(Error.AccountExists);
            if (model.TipoUsuario == TipoUsuario.Empresa) model.EncriptarSenha();           
            _repository.Adicionar(model);
        }

        public Usuario ObterPorEmail(string email)
        {
            return _repository.ObterUnico(x => x.Email == email);
        }

        private bool VerificarExistenciaEmail(string email)
        {
            return _repository.Existe(x => x.Email == email);
        }

        public void Atualizar(Usuario model)
        {
            _repository.Atualizar(model);
        }

        public Usuario ObterPorId(object id)
        {
            return _repository.ObterUnico(x => x.Id == (int)id);
        }      

        public IEnumerable<Usuario> ObterTodosPaginado(Expression<Func<Usuario, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return _repository.ObterTodosPaginado(expressionWhere, x => x.Nome, start, length, out recordsTotal, null);
        }

        public IEnumerable<Usuario> ObterTodos()
        {
            return _repository.ObterTodos();
        }

        public IEnumerable<Usuario> ObterTodosGestor()
        {
            return _repository.ObterTodos(x => x.TipoUsuario == TipoUsuario.Gestor);
        }

        public Usuario Remover(Usuario model)
        {
            return _repository.Remover(model);
        }

        public Usuario Desativar(Usuario model)
        {
            var unidade = ObterPorId(model.Id);
            unidade.InverterAtivo();
            return _repository.Atualizar(unidade);
        }

        public int TotalUsuarios()
        {
            return _repository.Contar(null);
        }

        public void Dispose()
        {
            _repository.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
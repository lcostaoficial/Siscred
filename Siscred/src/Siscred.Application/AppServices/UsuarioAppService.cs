using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Siscred.Application.Interfaces;
using Siscred.Application.Mapper;
using Siscred.Application.ViewModel;
using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Services;
using Siscred.Infra.CrossCutting.Helpers;

namespace Siscred.Application.AppServices
{
    public class UsuarioAppService : IUsuarioAppService
    {
        private readonly IUsuarioService _service;
        private readonly ITokenAppService _tokenApp;

        public UsuarioAppService(IUsuarioService service, ITokenAppService tokenApp)
        {
            _service = service;
            _tokenApp = tokenApp;
        }

        public async Task AdicionarEmpresa(UsuarioVm model)
        {
            var chave = model.AcoplarToken();
            var applicationUrl = HttpContext.Current.Request.Url.Authority;
            var emailManager = new EmailManager(new ArrayList { model.Email });           
            emailManager.AtivarConta(model.Nome, model.Email, $"{applicationUrl}/Conta/Ativar?token={chave}");
            await emailManager.Enviar();
            model.SetPrimeiroCadastroEmpresa();
            model.SetUsuarioEmpresa();
            _service.Adicionar(MapperConfig.Mapper.Map<Usuario>(model));
        }

        public void AtualizarEmpresa(UsuarioVm model)
        {
            throw new NotImplementedException();
        }
        
        public void AdicionarGestor(GestorVm model)
        {
            model.TipoUsuario = TipoUsuarioVm.Gestor;
            model.Senha = PasswordEncryption.ComputeHash(model.Email, "SHA512", null);
            _service.Adicionar(MapperConfig.Mapper.Map<Usuario>(model));
        }

        public void AtualizarGestor(UsuarioVm model)
        {
            throw new NotImplementedException();
        }
                
        public async Task RecuperarSenha(RecuperarSenhaVm model)
        {
            var token = _tokenApp.GerarToken(model.Email, TipoTokenVm.Recuperacao);
            var applicationUrl = HttpContext.Current.Request.Url.Authority;
            var emailManager = new EmailManager(new ArrayList { model.Email });
            emailManager.RecuperarConta(token.Usuario.Nome, model.Email, $"{applicationUrl}/Conta/RedefinirSenha?token={token.Chave}");
            await emailManager.Enviar();
        }

        public UsuarioVm ValidarLogin(LoginVm model)
        {
            return MapperConfig.Mapper.Map<UsuarioVm>(_service.ValidarLogin(MapperConfig.Mapper.Map<Usuario>(model)));
        }

        public UsuarioVm ObterPorId(object id)
        {
            return MapperConfig.Mapper.Map<UsuarioVm>(_service.ObterPorId(id));
        }

        public UsuarioVm ObterPorEmail(string email)
        {
            return MapperConfig.Mapper.Map<UsuarioVm>(_service.ObterPorEmail(email));
        }

        public IEnumerable<UsuarioVm> ObterTodosPaginado(Expression<Func<UsuarioVm, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return MapperConfig.Mapper.Map<ICollection<UsuarioVm>>(_service.ObterTodosPaginado(MapperConfig.Mapper.Map<Expression<Func<Usuario, bool>>>(expressionWhere), start, length, out recordsTotal));
        }

        public IEnumerable<UsuarioVm> ObterTodos()
        {
            return MapperConfig.Mapper.Map<ICollection<UsuarioVm>>(_service.ObterTodos());
        }

        public IEnumerable<UsuarioVm> ObterTodosGestor()
        {
            return MapperConfig.Mapper.Map<ICollection<UsuarioVm>>(_service.ObterTodosGestor());
        }

        public UsuarioVm Remover(UsuarioVm model)
        {
            return MapperConfig.Mapper.Map<UsuarioVm>(_service.Remover(MapperConfig.Mapper.Map<Usuario>(model)));
        }

        public UsuarioVm Desativar(UsuarioVm model)
        {
            return MapperConfig.Mapper.Map<UsuarioVm>(_service.Desativar(MapperConfig.Mapper.Map<Usuario>(model)));
        }

        public int TotalUsuarios()
        {
            return _service.TotalUsuarios();
        }
        
        public void Dispose()
        {
            _service.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Web;
using Siscred.Application.Interfaces;
using Siscred.Application.Mapper;
using Siscred.Application.ViewModel;
using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Services;
using Siscred.Infra.CrossCutting.Messages;
using Siscred.Infra.CrossCutting.Uploads;

namespace Siscred.Application.AppServices
{
    public class InscricaoAppService : IInscricaoAppService
    {
        private readonly IInscricaoService _service;

        public InscricaoAppService(IInscricaoService service)
        {
            _service = service;
        }

        public void Adicionar(InscricaoVm model)
        {
            try
            {
                if (model.Arquivos != null) model.Arquivos = Anexar(model);
                _service.Adicionar(MapperConfig.Mapper.Map<Inscricao>(model));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }            
        }

        public void Atualizar(InscricaoVm model)
        {
            _service.Atualizar(MapperConfig.Mapper.Map<Inscricao>(model));
        }

        public InscricaoVm ObterPorCnpj(string cnpj)
        {
            return MapperConfig.Mapper.Map<InscricaoVm>(_service.ObterPorCnpj(cnpj));
        }

        public InscricaoVm ObterPorId(object id)
        {
            return MapperConfig.Mapper.Map<InscricaoVm>(_service.ObterPorId(id));
        }

        public InscricaoVm ObterPorIdCustom(int id)
        {
            return MapperConfig.Mapper.Map<InscricaoVm>(_service.ObterPorIdCustom(id));
        }

        public InscricaoVm ObterPorIdParaComprovante(int id)
        {
            return MapperConfig.Mapper.Map<InscricaoVm>(_service.ObterPorIdParaComprovante(id));
        }

        public InscricaoVm ObterPorIdParaFicha(int id)
        {
            return MapperConfig.Mapper.Map<InscricaoVm>(_service.ObterPorIdParaFicha(id));
        }

        public InscricaoVm ObterPorUsuarioId(int usuarioId)
        {
            return MapperConfig.Mapper.Map<InscricaoVm>(_service.ObterPorUsuarioId(usuarioId));
        }

        public InscricaoVm ValidarDuplicidadeInscricaoUsuario(int usuarioId, int editalId)
        {
            return MapperConfig.Mapper.Map<InscricaoVm>(_service.ValidarDuplicidadeInscricaoUsuario(usuarioId, editalId));    
        }

        public InscricaoVm ValidarDuplicidadeInscricaoProfissional(string cpf, int inscricaoId)
        {
            return MapperConfig.Mapper.Map<InscricaoVm>(_service.ValidarDuplicidadeInscricaoProfissional(cpf, inscricaoId));
        }

        public IEnumerable<InscricaoVm> ObterTodos()
        {
            return MapperConfig.Mapper.Map<ICollection<InscricaoVm>>(_service.ObterTodos());
        }

        public IEnumerable<InscricaoVm> ObterTodosPorEditalId(int editalId)
        {
            return MapperConfig.Mapper.Map<ICollection<InscricaoVm>>(_service.ObterTodosPorEditalId(editalId));
        }

        public IEnumerable<InscricaoVm> ObterTodosPaginado(Expression<Func<InscricaoVm, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return MapperConfig.Mapper.Map<ICollection<InscricaoVm>>(_service.ObterTodosPaginado(MapperConfig.Mapper.Map<Expression<Func<Inscricao, bool>>>(expressionWhere), start, length, out recordsTotal));
        }

        public int TotalInscricoes()
        {
            return _service.TotalInscricoes();
        }

        private ICollection<ArquivoVm> Anexar(InscricaoVm model)
        {
            var virtualPath = InscricaoConfigUpload.DefaultPath;
            var allowedExtensions = InscricaoConfigUpload.ValidExtensions;
            var files = new List<ArquivoVm>();
            foreach (var item in model.Arquivos)
            {
                if (item.ArquivoBinario != null && item.ArquivoBinario.ContentLength > 0)
                {
                    if (!EditalConfigUpload.ValidateSize(item.ArquivoBinario.ContentLength)) throw new Exception(Error.MaximumFileSize);
                    string extension = Path.GetExtension(item.ArquivoBinario.FileName);
                    if (!allowedExtensions.Contains(extension.ToLower())) throw new Exception(Error.InvalidExtension);
                    var physicalPath = HttpContext.Current.Server.MapPath(virtualPath);
                    if (!Directory.Exists(physicalPath)) throw new Exception(Error.NonexistentDirectory);
                    string fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{Guid.NewGuid()}.pdf";
                    string filePath = Path.Combine(physicalPath, fileName);
                    item.ArquivoBinario.SaveAs(filePath);
                    item.Caminho = $"{virtualPath}/{fileName}";
                    item.Nome = item.ArquivoBinario.FileName;
                    files.Add(item);
                }               
            }
            return files;
        }      

        public void Dispose()
        {
            _service.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
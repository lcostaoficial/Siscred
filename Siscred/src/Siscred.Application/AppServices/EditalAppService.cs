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
    public class EditalAppService : IEditalAppService
    {
        private readonly IEditalService _service;  

        public EditalAppService(IEditalService service)
        {
            _service = service;           
        }

        public void Adicionar(EditalVm model)
        {            
            try
            {
                model = Anexar(model);
                _service.Adicionar(MapperConfig.Mapper.Map<Edital>(model), model.CidadesIds, model.TiposArquivosIds);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }            
        }

        public void Atualizar(EditalVm model)
        {            
            try
            {
                model = Anexar(model);
                _service.Atualizar(MapperConfig.Mapper.Map<Edital>(model), model.CidadesIds, model.TiposArquivosIds);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }            
        }

        public void Desativar(EditalVm model)
        {
            _service.Desativar(MapperConfig.Mapper.Map<Edital>(model));
        }

        public void Remover(EditalVm model)
        {
            _service.Remover(MapperConfig.Mapper.Map<Edital>(model));
        }

        public EditalVm ObterPorId(object id)
        {
            return MapperConfig.Mapper.Map<EditalVm>(_service.ObterPorId(id));
        }

        public IEnumerable<EditalVm> ObterTodos()
        {
            return MapperConfig.Mapper.Map<ICollection<EditalVm>>(_service.ObterTodos());
        }

        public IEnumerable<EditalVm> ObterTodosComEdital(int usuarioId)
        {
            return MapperConfig.Mapper.Map<ICollection<EditalVm>>(_service.ObterTodosComEdital(usuarioId));
        }

        public IEnumerable<EditalVm> ObterTodosPaginado(Expression<Func<EditalVm, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return MapperConfig.Mapper.Map<ICollection<EditalVm>>(_service.ObterTodosPaginado(MapperConfig.Mapper.Map<Expression<Func<Edital, bool>>>(expressionWhere), start, length, out recordsTotal));
        }

        private EditalVm Anexar(EditalVm model)
        {
            var virtualPath = EditalConfigUpload.DefaultPath;
            var allowedExtensions = EditalConfigUpload.ValidExtensions;
            var edital = new Edital();
            if (model.Id != 0) edital = _service.ObterPorId(model.Id);
            if (model.ArquivoEdital != null && model.ArquivoEdital.ContentLength > 0)
            {
                if (!EditalConfigUpload.ValidateSize(model.ArquivoEdital.ContentLength)) throw new Exception(Error.MaximumFileSize);
                string extension = Path.GetExtension(model.ArquivoEdital.FileName);
                if (!allowedExtensions.Contains(extension.ToLower())) throw new Exception(Error.InvalidExtension);
                var physicalPath = HttpContext.Current.Server.MapPath(virtualPath);
                if (!Directory.Exists(physicalPath)) throw new Exception(Error.NonexistentDirectory);
                string fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
                string filePath = Path.Combine(physicalPath, fileName);
                if (model.Id != 0)
                {
                    var fileOld = HttpContext.Current.Server.MapPath(model.AnexoEdital);
                    if (!string.IsNullOrEmpty(model.AnexoEdital) && Directory.Exists(fileOld)) File.Delete(fileOld);
                }    
                model.ArquivoEdital.SaveAs(filePath);
                model.AnexoEdital = $"{virtualPath}/{fileName}";
                model.NomeOriginal = model.ArquivoEdital.FileName;
            }
            else if (model.Id != 0)
            {                
                model.AnexoEdital = edital.AnexoEdital;
                model.NomeOriginal = edital.NomeOriginal;
            }
            return model;
        }

        public void Dispose()
        {
            _service.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
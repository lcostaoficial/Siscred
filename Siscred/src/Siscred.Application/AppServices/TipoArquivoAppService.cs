using Siscred.Application.Interfaces;
using Siscred.Application.Mapper;
using Siscred.Application.ViewModel;
using Siscred.Domain.Entities;
using Siscred.Domain.Interfaces.Services;
using Siscred.Infra.CrossCutting.Messages;
using Siscred.Infra.CrossCutting.Uploads;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Web;

namespace Siscred.Application.AppServices
{
    public class TipoArquivoAppService : ITipoArquivoAppService
    {
        private readonly ITipoArquivoService _service;

        public TipoArquivoAppService(ITipoArquivoService service)
        {
            _service = service;
        }

        public void Adicionar(TipoArquivoVm model)
        {            
            try
            {
                model = Anexar(model);
                _service.Adicionar(MapperConfig.Mapper.Map<TipoArquivo>(model));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }            
        }

        public void Atualizar(TipoArquivoVm model)
        {            
            try
            {
                model = Anexar(model);
                _service.Atualizar(MapperConfig.Mapper.Map<TipoArquivo>(model));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }            
        }

        public TipoArquivoVm Remover(TipoArquivoVm model)
        {
            return MapperConfig.Mapper.Map<TipoArquivoVm>(_service.Remover(MapperConfig.Mapper.Map<TipoArquivo>(model)));
        }

        public TipoArquivoVm Desativar(TipoArquivoVm model)
        {
            return MapperConfig.Mapper.Map<TipoArquivoVm>(_service.Desativar(MapperConfig.Mapper.Map<TipoArquivo>(model)));
        }

        public TipoArquivoVm ObterPorId(object id)
        {
            return MapperConfig.Mapper.Map<TipoArquivoVm>(_service.ObterPorId(id));
        }

        public IEnumerable<TipoArquivoVm> ObterPorEditalIdEmpresa(int id)
        {
            return MapperConfig.Mapper.Map<IEnumerable<TipoArquivoVm>>(_service.ObterPorEditalIdEmpresa(id));
        }

        public IEnumerable<TipoArquivoVm> ObterPorEditalIdIndicado(int id)
        {
            return MapperConfig.Mapper.Map<IEnumerable<TipoArquivoVm>>(_service.ObterPorEditalIdIndicado(id));
        }        

        public IEnumerable<TipoArquivoVm> ObterTodosPaginado(Expression<Func<TipoArquivoVm, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return MapperConfig.Mapper.Map<ICollection<TipoArquivoVm>>(_service.ObterTodosPaginado(MapperConfig.Mapper.Map<Expression<Func<TipoArquivo, bool>>>(expressionWhere), start, length, out recordsTotal));
        }

        public IEnumerable<TipoArquivoVm> ObterTodos()
        {
            return MapperConfig.Mapper.Map<ICollection<TipoArquivoVm>>(_service.ObterTodos());
        }  
        
        private TipoArquivoVm Anexar(TipoArquivoVm model)
        {
            var virtualPath = TipoArquivoConfigUpload.DefaultPath;
            var allowedExtensions = TipoArquivoConfigUpload.ValidExtensions;
            var tipoArquivo = new TipoArquivo();
            if (model.Id != 0) tipoArquivo = _service.ObterPorId(model.Id);
            if (model.ModeloArquivo != null && model.ModeloArquivo.ContentLength > 0)
            {
                if (!TipoArquivoConfigUpload.ValidateSize(model.ModeloArquivo.ContentLength)) throw new Exception(Error.MaximumFileSize);
                string extension = Path.GetExtension(model.ModeloArquivo.FileName);
                if (!allowedExtensions.Contains(extension.ToLower())) throw new Exception(Error.InvalidExtension);
                var physicalPath = HttpContext.Current.Server.MapPath(virtualPath);
                if (!Directory.Exists(physicalPath)) throw new Exception(Error.NonexistentDirectory);
                string fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmss")}.pdf";
                string filePath = Path.Combine(physicalPath, fileName);
                if (model.Id != 0)
                {
                    var fileOld = HttpContext.Current.Server.MapPath(model.Modelo);
                    if (!string.IsNullOrEmpty(model.Modelo)) File.Delete(fileOld);
                }
                model.ModeloArquivo.SaveAs(filePath);
                model.Modelo = $"{virtualPath}/{fileName}";
                model.NomeOriginal = model.ModeloArquivo.FileName;
            }
            else if (model.Id != 0)
            {
                model.Modelo = tipoArquivo.Modelo;
                model.NomeOriginal = tipoArquivo.NomeOriginal;
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
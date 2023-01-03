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
    public class ProfissionalIndicadoAppService : IProfissionalIndicadoAppService
    {
        private readonly IProfissionalIndicadoService _service;
        private readonly IArquivoAppService _arquivoApp;

        public ProfissionalIndicadoAppService(IProfissionalIndicadoService service, IArquivoAppService arquivoApp)
        {
            _service = service;
            _arquivoApp = arquivoApp;
        }

        public void Adicionar(ProfissionalIndicadoVm model)
        {
            try
            {
                if (model.Arquivos == null) throw new Exception("Nenhum arquivo selecionado!");
                model.Arquivos = Anexar(model);                
                _service.Adicionar(MapperConfig.Mapper.Map<ProfissionalIndicado>(model));
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }            
        }

        public void Atualizar(ProfissionalIndicadoVm model)
        {
            _service.Atualizar(MapperConfig.Mapper.Map<ProfissionalIndicado>(model));
        }

        public void Remover(int id)
        {
            _service.Remover(id);
        }

        public ProfissionalIndicadoVm ObterPorId(object id)
        {
            return MapperConfig.Mapper.Map<ProfissionalIndicadoVm>(_service.ObterPorId(id));
        }

        public ProfissionalIndicadoVm ObterPorIdComInscricao(int id)
        {
            return MapperConfig.Mapper.Map<ProfissionalIndicadoVm>(_service.ObterPorIdComInscricao(id));
        }

        public IEnumerable<ProfissionalIndicadoVm> ObterTodos()
        {
            return MapperConfig.Mapper.Map<ICollection<ProfissionalIndicadoVm>>(_service.ObterTodos());
        }

        public IEnumerable<ProfissionalIndicadoVm> ObterTodosPorInscricaoId(int inscricaoId)
        {
            return MapperConfig.Mapper.Map<ICollection<ProfissionalIndicadoVm>>(_service.ObterTodosPorInscricaoId(inscricaoId));
        }

        public IEnumerable<ProfissionalIndicadoVm> ObterTodosPaginado(Expression<Func<ProfissionalIndicadoVm, bool>> expressionWhere, int start, int length, out int recordsTotal)
        {
            return MapperConfig.Mapper.Map<ICollection<ProfissionalIndicadoVm>>(_service.ObterTodosPaginado(MapperConfig.Mapper.Map<Expression<Func<ProfissionalIndicado, bool>>>(expressionWhere), start, length, out recordsTotal));
        }

        private ICollection<ArquivoVm> Anexar(ProfissionalIndicadoVm model)
        {
            var virtualPath = IndicadoConfigUpload.DefaultPath;
            var allowedExtensions = IndicadoConfigUpload.ValidExtensions;
            var files = new List<ArquivoVm>();
            foreach (var item in model.Arquivos)
            {
                var arquivo = new ArquivoVm();
                if (model.Id != 0) arquivo = _arquivoApp.ObterPorId(model.Id);
                if (item.ArquivoBinario != null && item.ArquivoBinario.ContentLength > 0)
                {
                    if (!EditalConfigUpload.ValidateSize(item.ArquivoBinario.ContentLength)) throw new Exception(Error.MaximumFileSize);
                    string extension = Path.GetExtension(item.ArquivoBinario.FileName);
                    if (!allowedExtensions.Contains(extension.ToLower())) throw new Exception(Error.InvalidExtension);
                    var physicalPath = HttpContext.Current.Server.MapPath(virtualPath);
                    if (!Directory.Exists(physicalPath)) throw new Exception(Error.NonexistentDirectory);
                    string fileName = $"{DateTime.Now.ToString("yyyyMMddHHmmssfff")}{Guid.NewGuid()}.pdf";
                    string filePath = Path.Combine(physicalPath, fileName);
                    if (item.Id != 0)
                    {
                        var fileOld = HttpContext.Current.Server.MapPath(item.Caminho);
                        if (!string.IsNullOrEmpty(item.Caminho)) File.Delete(fileOld);
                    }
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
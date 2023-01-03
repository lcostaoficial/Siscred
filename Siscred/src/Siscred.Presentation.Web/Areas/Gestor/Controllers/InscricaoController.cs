using Siscred.Application.Interfaces;
using Siscred.Application.ViewModel;
using Siscred.Infra.CrossCutting.Helpers;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Siscred.Presentation.Web.Areas.Gestor.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class InscricaoController : Controller
    {
        private readonly IInscricaoAppService _inscricaoApp;
        private readonly IEditalAppService _editalApp;
        private readonly IArquivoAppService _arquivoApp;

        public InscricaoController(IInscricaoAppService inscricaoApp, IEditalAppService editalApp, IArquivoAppService arquivoApp)
        {
            _inscricaoApp = inscricaoApp;
            _editalApp = editalApp;
            _arquivoApp = arquivoApp;
        }

        public ActionResult Index()
        {
            ViewBag.Editais = _editalApp.ObterTodos();
            return View(_inscricaoApp.ObterTodos());
        }
        
        public ActionResult Inscricoes(int editalId)
        {
            var list = _inscricaoApp.ObterTodosPorEditalId(editalId);           
            return PartialView("_Table", list);
        }

        public ActionResult Situacao(SituacaoInscricaoVm situacao)
        {          
            return PartialView("_Situacao", situacao);
        }

        public ActionResult ObterInscricao(int inscricaoId)
        {
            var model = _inscricaoApp.ObterPorIdCustom(inscricaoId);
            return PartialView("_Inscricao", model);
        }

        public JsonResult Aprovar(int inscricaoId, string justificativa, SituacaoInscricaoVm situacao)
        {
            try
            {
                var inscricao = _inscricaoApp.ObterPorId(inscricaoId);
                if (string.IsNullOrEmpty(justificativa)) throw new Exception("Justificativa não informada!");
                if (inscricao.SituacaoInscricao != SituacaoInscricaoVm.Inscrito) throw new Exception("Somente inscrições finalizadas podem serem homologadas!");
                _inscricaoApp.Atualizar(new InscricaoVm
                {
                    Id = inscricao.Id, SituacaoInscricao = situacao, Justificativa = justificativa
                });
                return Json(new { success = "Situação da inscrição alterada com sucesso!" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ImprimirDocumentos(int inscricaoId)
        {
            try
            {
                var inscricao = _inscricaoApp.ObterPorIdParaFicha(inscricaoId);
                var ficha = PdfFicha(inscricao);
                var documentos = _arquivoApp.ObterTodosArquivosInscricaoId(inscricaoId);
                List<byte[]> listaDocumentos = new List<byte[]>();
                listaDocumentos.Add(ficha);
                foreach (var item in documentos)
                {
                    byte[] bytes = System.IO.File.ReadAllBytes(Server.MapPath(item.Caminho));
                    listaDocumentos.Add(bytes);
                }
                var arquivosUnidos = PdfExtension.MergeFilesByte(listaDocumentos);
                PdfExtension.ReturnPDF(arquivosUnidos, "Documentos.pdf");
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Inscricao").Error(e.Message);
            }            
        }

        private byte[] PdfFicha(InscricaoVm model)
        {
            try
            {
                var html = ControllerContext.RenderPartialToString("_Ficha", model);
                return PdfExtension.Render(html);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
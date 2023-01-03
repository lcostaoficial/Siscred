using Siscred.Application.Interfaces;
using Siscred.Application.ViewModel;
using Siscred.Infra.CrossCutting.Helpers;
using Siscred.Infra.CrossCutting.Messages;
using System;
using System.Web.Mvc;

namespace Siscred.Presentation.Web.Areas.Gestor.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class EditalController : Controller
    {
        private readonly IEditalAppService _editalApp;
        private readonly ITipoArquivoAppService _tipoArquivoApp;
        private readonly ICidadeAppService _cidadeApp;

        public EditalController(IEditalAppService editalApp, ITipoArquivoAppService tipoArquivoApp, ICidadeAppService cidadeApp)
        {
            _editalApp = editalApp;
            _tipoArquivoApp = tipoArquivoApp;
            _cidadeApp = cidadeApp;
        }

        public ActionResult Index()
        {
            return View(_editalApp.ObterTodos());
        }

        public ActionResult Adicionar()
        {
            SetViewBag();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(EditalVm model)
        {
            try
            {
                if (model.ArquivoEdital == null) throw new Exception(Error.NoticeRequired);
                if (!ModelState.IsValid) throw new Exception(Error.InvalidFields);
                _editalApp.Adicionar(model);
                return RedirectToAction("Index").Success();
            }
            catch (Exception e)
            {
                SetViewBag();
                return View(model).Error(e.Message);
            }
        }

        public ActionResult Editar(int id)
        {
            var model = _editalApp.ObterPorId(id);
            model.SetCidadesIds();
            model.SetTiposArquivosIds();
            SetViewBag();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(EditalVm model)
        {
            try
            {
                if (model.ArquivoEdital != null && model.ArquivoEdital.ContentLength < 0) throw new Exception(Error.NoticeRequired);
                if (!ModelState.IsValid) throw new Exception(Error.InvalidFields);
                _editalApp.Atualizar(model);
                return RedirectToAction("Index").Success();
            }
            catch (Exception e)
            {
                SetViewBag();
                return View(model).Error(e.Message);
            }
        }       

        public ActionResult Desativar(int id)
        {
            try
            {
                _editalApp.Desativar(new EditalVm { Id = id });
                return RedirectToAction("Index").Success();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Error(e.Message);
            }
        }

        public void SetViewBag()
        {
            ViewBag.TiposArquivos = _tipoArquivoApp.ObterTodos();
            ViewBag.Cidades = _cidadeApp.ObterTodos();
        }
    }
}
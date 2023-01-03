using Siscred.Application.Interfaces;
using Siscred.Application.ViewModel;
using Siscred.Infra.CrossCutting.Helpers;
using Siscred.Infra.CrossCutting.Messages;
using System;
using System.Web.Mvc;

namespace Siscred.Presentation.Web.Areas.Gestor.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class TipoArquivoController : Controller
    {
        private readonly ITipoArquivoAppService _tipoArquivoApp;

        public TipoArquivoController(ITipoArquivoAppService tipoArquivoApp)
        {
            _tipoArquivoApp = tipoArquivoApp;
        }

        public ActionResult Index()
        {
            return View(_tipoArquivoApp.ObterTodos());
        }

        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(TipoArquivoVm model)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception(Error.InvalidFields);
                _tipoArquivoApp.Adicionar(model);
                return RedirectToAction("Index").Success();
            }
            catch (Exception e)
            {
                return View(model).Error(e.Message);
            }
        }

        public ActionResult Editar(int id)
        {
            var model = _tipoArquivoApp.ObterPorId(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(TipoArquivoVm model)
        {
            try
            {
                if (!ModelState.IsValid) throw new Exception(Error.InvalidFields);
                _tipoArquivoApp.Atualizar(model);
                return RedirectToAction("Index").Success();
            }
            catch (Exception e)
            {
                return View(model).Error(e.Message);
            }
        }

        public ActionResult Remover(int id)
        {

            try
            {
                _tipoArquivoApp.Remover(new TipoArquivoVm { Id = id });
                return RedirectToAction("Index").Success();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Error(e.Message);
            }
        }

        public ActionResult Desativar(int id)
        {
            try
            {
                _tipoArquivoApp.Desativar(new TipoArquivoVm { Id = id });
                return RedirectToAction("Index").Success();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Error(e.Message);
            }
        }
    }
}
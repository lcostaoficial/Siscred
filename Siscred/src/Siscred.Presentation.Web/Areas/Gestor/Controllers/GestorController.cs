using Siscred.Application.Interfaces;
using Siscred.Application.ViewModel;
using Siscred.Infra.CrossCutting.Helpers;
using Siscred.Infra.CrossCutting.Messages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Siscred.Presentation.Web.Areas.Gestor.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class GestorController : Controller
    {
        private readonly IUsuarioAppService _UsuarioApp;

        public GestorController(IUsuarioAppService UsuarioApp)
        {
            _UsuarioApp = UsuarioApp;
        }

        public ActionResult Index()
        {
            return View(_UsuarioApp.ObterTodosGestor());
        }

        public ActionResult Adicionar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(GestorVm model)
        {
            try
            {
                if (!new EmailAddressAttribute().IsValid(model.Email)) throw new Exception(Error.InvalidEmail);
                if (!ModelState.IsValid) throw new Exception(Error.InvalidFields);
                _UsuarioApp.AdicionarGestor(model);
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
                _UsuarioApp.Remover(new UsuarioVm { Id = id });
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
                _UsuarioApp.Desativar(new UsuarioVm { Id = id });
                return RedirectToAction("Index").Success();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index").Error(e.Message);
            }
        }
    }
}
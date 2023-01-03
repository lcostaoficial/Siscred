using Siscred.Application.Interfaces;
using System.Web.Mvc;

namespace Siscred.Presentation.Web.Areas.Gestor.Controllers
{
    [Authorize(Roles = "Gestor")]
    public class HomeController : Controller
    {
        private readonly IInscricaoAppService _inscricaoApp;
        private readonly IUsuarioAppService _usuarioApp;

        public HomeController(IInscricaoAppService inscricaoApp, IUsuarioAppService usuarioApp)
        {
            _inscricaoApp = inscricaoApp;
            _usuarioApp = usuarioApp;
        }

        public ActionResult Index()
        {
            ViewBag.QtdeInscricao = _inscricaoApp.TotalInscricoes(); ;
            ViewBag.QtdeUsuario = _usuarioApp.TotalUsuarios();
            return View();
        }
    }
}
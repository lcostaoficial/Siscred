using Siscred.Application.Interfaces;
using Siscred.Infra.CrossCutting.Security;
using System.Web.Mvc;

namespace Siscred.Presentation.Web.Areas.Empresa.Controllers
{
    [Authorize(Roles = "Empresa")]
    public class HomeController : Controller
    {
        private readonly IEditalAppService _editalApp;

        public HomeController(IEditalAppService editalApp)
        {          
            _editalApp = editalApp;            
        }

        public ActionResult Index()
        {
            var editais = _editalApp.ObterTodosComEdital(Account.UsuarioId);
            return View(editais);
        }
    }
}
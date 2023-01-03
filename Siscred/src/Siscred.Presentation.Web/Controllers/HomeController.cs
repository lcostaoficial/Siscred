using System.Web.Mvc;

namespace Siscred.Presentation.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {           
            return RedirectToAction("Autenticar", "Conta");
        }   
    }
}
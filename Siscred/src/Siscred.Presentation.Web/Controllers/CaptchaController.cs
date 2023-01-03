using Siscred.Infra.CrossCutting.Helpers;
using System.Web.Mvc;

namespace Siscred.Presentation.Web.Controllers
{
    public class CaptchaController : Controller
    {
        public CaptchaResult GetCaptcha()
        {
            string captchaText = Captcha.GenerateRandomCode();
            HttpContext.Session.Add("captcha", captchaText);
            return new CaptchaResult(captchaText);
        }
    }
}
using Siscred.Application.Interfaces;
using Siscred.Application.ViewModel;
using Siscred.Infra.CrossCutting.Helpers;
using Siscred.Infra.CrossCutting.Messages;
using Siscred.Infra.CrossCutting.Security;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using static Siscred.Infra.CrossCutting.Helpers.NotificationMessage;

namespace Siscred.Presentation.Web.Controllers
{
    [Authorize]
    public class ContaController : Controller
    {
        private readonly IUsuarioAppService _usuarioAppService;
        private readonly ITokenAppService _tokenAppService;

        public ContaController(IUsuarioAppService usuarioAppService, ITokenAppService tokenAppService)
        {
            _usuarioAppService = usuarioAppService;
            _tokenAppService = tokenAppService;
        }

        [AllowAnonymous]
        public ActionResult Autenticar(string returnUrl)
        {
            return Account.IsAuthenticated ? RedirectTo(returnUrl, 0) : View(new LoginVm { ReturnUrl = returnUrl });
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Autenticar(LoginVm model)
        {
            try
            {
                if (!ModelState.IsValid) return View().Error(Error.InvalidFields);
                var usuario = _usuarioAppService.ValidarLogin(model);
                Authentication.Signin(usuario);
                if (PasswordEncryption.VerifyHash(model.Email, "SHA512", usuario.Senha) && usuario.TipoUsuario == TipoUsuarioVm.Gestor)
                {
                    var listaTokens = _tokenAppService.ObterTokensAtivos(usuario.Id, TipoTokenVm.Recuperacao);
                    if (listaTokens != null && listaTokens.Any())
                    {
                        var tokenAtual = listaTokens.FirstOrDefault();
                        return RedirectToAction("RedefinirSenha", "Conta", new { token = tokenAtual.Chave });
                    }
                    else
                    {
                        var tokenNovo = _tokenAppService.GerarToken(model.Email, TipoTokenVm.Recuperacao);
                        return RedirectToAction("RedefinirSenha", "Conta", new { token = tokenNovo.Chave });
                    }                    
                }
                return RedirectTo(model.ReturnUrl, usuario.TipoUsuario);
            }
            catch (Exception e)
            {
                return View("Autenticar", model).Error(e.Message);
            }
        }

        [AllowAnonymous]
        public ActionResult Cadastrar()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Cadastrar(UsuarioVm model)
        {
            try
            {          
                if (!new EmailAddressAttribute().IsValid(model.Email)) throw new Exception(Error.InvalidEmail);
                if (model.Captcha != (string)HttpContext.Session["captcha"]) throw new Exception(Error.InvalidCaptcha);
                if (!ModelState.IsValid) return View().Error(Error.InvalidFields);
                if (model.Senha != model.SenhaConfirmada) throw new Exception(Error.DifferentPasswords);
                if (model.SenhaConfirmada == model.Email) throw new Exception(Error.SamePasswordEmail);
                await _usuarioAppService.AdicionarEmpresa(model);
                return View("Autenticar").Success(Success.AccountRegistration);
            }
            catch (Exception e)
            {
                return View("Cadastrar", model).Error(e.Message);
            }
        }

        [AllowAnonymous]
        public ActionResult RecuperarSenha()
        {
            return PartialView("_FormRecuperarSenha");
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> RecuperarSenha(RecuperarSenhaVm model)
        {
            try
            {
                if (model.Captcha != (string)HttpContext.Session["captcha"]) throw new Exception(Error.InvalidCaptcha);
                if (!ModelState.IsValid) throw new Exception(Error.InvalidFields);
                await _usuarioAppService.RecuperarSenha(model);
                var messages = new List<Alert>();
                messages.Add(new Alert { Message = Success.AccountRecovery, AlertStyle = MessageType.success.ToString() });
                TempData["Message"] = messages;
                return Json(new { Url = Url.Action("Autenticar"), Success = Success.AccountRecovery }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Error = e.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [AllowAnonymous]
        public ActionResult RedefinirSenha(Guid token)
        {
            try
            {
                var model = _tokenAppService.ObterPorChave(token);
                if (model == null) return RedirectToAction("Autenticar").Error(Error.InvalidToken);
                if (model.TipoToken != TipoTokenVm.Recuperacao) return RedirectToAction("Autenticar").Error(Error.InvalidToken);
                return View(new RedefinirSenhaVm
                {
                    Email = model.Usuario.Email,
                    Chave = token
                });
            }
            catch (Exception e)
            {
                return RedirectToAction("Autenticar").Error(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RedefinirSenha(RedefinirSenhaVm model)
        {
            try
            {
                if (model.Senha != model.SenhaConfirmada) throw new Exception(Error.DifferentPasswords);
                if (model.SenhaConfirmada == model.Email) throw new Exception(Error.SamePasswordEmail);
                if (!ModelState.IsValid) return View(model).Error(Error.InvalidFields);  
                _tokenAppService.RedefinirSenha(model.Chave, model.SenhaConfirmada);
                return RedirectToAction("Autenticar").Success(Success.AccountRedefined);
            }
            catch (Exception e)
            {
                return View(model).Error(e.Message);
            }
        }

        public ActionResult Sair()
        {
            Authentication.Signout("ApplicationCookie");
            return RedirectToAction("Autenticar", "Conta", new { area = "" });
        }


        public ActionResult AlterarSenha()
        {
            return PartialView("_AlterarSenha");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AlterarSenha(AlterarSenhaVm model)
        {
            try
            {
                var usuarioLogado = _usuarioAppService.ObterPorId(Account.UsuarioId);
                if (model.SenhaNova == usuarioLogado.Email) throw new Exception(Error.SamePasswordEmail);
                if (!ModelState.IsValid) throw new Exception(Error.InvalidFields);
                var usuario = _usuarioAppService.ValidarLogin(new LoginVm { Email = usuarioLogado.Email, Senha = model.SenhaAntiga });
                if (usuario == null) throw new Exception("A senha antiga é inválida!");
                var token = _tokenAppService.GerarToken(usuarioLogado.Email, TipoTokenVm.Alteracao);
                _tokenAppService.AlterarSenha(token.Chave, model.SenhaNova);                
                return Json(new { Success = Success.Default }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new { Error = e.Message }, JsonRequestBehavior.AllowGet);
            }                      
        }

        [AllowAnonymous]
        public ActionResult Ativar(Guid token)
        {
            try
            {
                var model = _tokenAppService.UtilizarToken(token);
                return View("Autenticar", model).Success(Success.AccountActivation);
            }
            catch (Exception e)
            {
                return View("Autenticar").Error(e.Message);
            }
        }

        private ActionResult RedirectTo(string returnUrl, TipoUsuarioVm tipoUsuario)
        {
            if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl)) return Redirect(returnUrl);
            if (tipoUsuario == TipoUsuarioVm.Gestor) return RedirectToAction("Index", "Home", new { area = "Gestor" });
            if (tipoUsuario == TipoUsuarioVm.Empresa) return RedirectToAction("Index", "Home", new { area = "Empresa" });
            return View();
        }
    }
}
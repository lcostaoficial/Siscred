using Siscred.Application.Interfaces;
using Siscred.Application.ViewModel;
using Siscred.Infra.CrossCutting.Helpers;
using Siscred.Infra.CrossCutting.Messages;
using Siscred.Infra.CrossCutting.Security;
using System;
using System.Web.Mvc;

namespace Siscred.Presentation.Web.Areas.Empresa.Controllers
{
    [Authorize(Roles = "Empresa")]
    public class InscricaoController : Controller
    {
        private readonly IInscricaoAppService _inscricaoApp;
        private readonly IEditalAppService _editalApp;
        private readonly ICidadeAppService _cidadeApp;
        private readonly ITipoArquivoAppService _tipoArquivoApp;

        public InscricaoController(IInscricaoAppService inscricaoApp, IEditalAppService editalApp, ICidadeAppService cidadeApp, ITipoArquivoAppService tipoArquivoApp)
        {
            _inscricaoApp = inscricaoApp;
            _editalApp = editalApp;
            _cidadeApp = cidadeApp;
            _tipoArquivoApp = tipoArquivoApp;
        }

        private void ValidarPeriodoEdital(EditalVm edital)
        {
            var dataAtual = DateTime.Now;
            if (dataAtual.Date > edital.DataEncerramento.Date)
            {
                throw new Exception($"Prazo para inscrição expirou em {edital.DataEncerramento.ToShortDateString()}!");
            }
            if (dataAtual.Date < edital.DataPublicacao.Date)
            {
                throw new Exception($"Inscrições abertas somente a partir de {edital.DataPublicacao.ToShortDateString()}!");
            }
        }

        public ActionResult Index(int editalId)
        {
            try
            {
                var edital = _editalApp.ObterPorId(editalId);
                var inscricao = _inscricaoApp.ValidarDuplicidadeInscricaoUsuario(Account.UsuarioId, editalId);
                if (inscricao != null)
                {
                    if (inscricao.SituacaoInscricao == SituacaoInscricaoVm.Inscrito)
                    {
                        return RedirectToAction("Comprovante", "Indicado", new { inscricaoId = inscricao.Id }).Warning(Error.EnrollmentExists);
                    }
                    else if (inscricao.SituacaoInscricao == SituacaoInscricaoVm.Pendente)
                    {
                        return RedirectToAction("Index", "Indicado", new { editalId, area = "Empresa" });
                    }
                    else if (inscricao.SituacaoInscricao == SituacaoInscricaoVm.Homologado)
                    {
                        return RedirectToAction("Comprovante", "Indicado", new { inscricaoId = inscricao.Id }).Warning(Error.EnrollmentExists);
                    }
                    else if (inscricao.SituacaoInscricao == SituacaoInscricaoVm.Reprovado)
                    {
                        return RedirectToAction("Comprovante", "Indicado", new { inscricaoId = inscricao.Id }).Warning(Error.EnrollmentExists);
                    }
                }              
                ValidarPeriodoEdital(edital);
                SetViewBag(editalId);
                return View();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home").Error(e.Message);
            }           
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(InscricaoVm model)
        {
            try
            {
                if (!ValidationExtension.IsCnpj(model.Cnpj)) throw new Exception(Error.InvalidCnpj);
                var edital = _editalApp.ObterPorId(model.EditalId);
                ValidarPeriodoEdital(edital);
                model.SetInscricoesCidades(edital.HabilitarMicrorregiao);
                if (edital.HabilitarMicrorregiao) model.ValidarInscricoesCidades(Error.Default);                
                var inscricao = _inscricaoApp.ObterPorCnpj(model.Cnpj);
                if (inscricao != null) throw new Exception(Error.CnpjExists);
                if (!edital.HabilitarMicrorregiao) RemoverValidacaoMicrorregiao();
                if (!ModelState.IsValid) throw new Exception(Error.InvalidFields);
                model.UsuarioId = Account.UsuarioId;
                _inscricaoApp.Adicionar(model);
                return RedirectToAction("Index", "Indicado", new { editalId = model.EditalId });
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home").Error(e.Message);
            }
        }

        public JsonResult ValidarCnpj(string cnpj, int editalId)
        {
            var validateCnpj = ValidationExtension.IsCnpj(cnpj);
            if (validateCnpj)
            {
                var inscricao = _inscricaoApp.ObterPorCnpj(cnpj);
                if (inscricao == null)
                {
                    var result = new { msg = "", valid = true };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { msg = Error.CnpjExists, valid = false };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var result = new { msg = Error.InvalidCnpj, valid = false };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public void SetViewBag(int editalId)
        {
            ViewBag.ArquivosExigidos = _tipoArquivoApp.ObterPorEditalIdEmpresa(editalId);
            ViewBag.MicroRegiao = _cidadeApp.ObterPorEditalId(editalId);
            ViewBag.Edital = _editalApp.ObterPorId(editalId);
        }

        public void RemoverValidacaoMicrorregiao()
        {
            ModelState.Remove("MicroRegiao1Id");
            ModelState.Remove("MicroRegiao2Id");
            ModelState.Remove("MicroRegiao3Id");
        }
    }
}
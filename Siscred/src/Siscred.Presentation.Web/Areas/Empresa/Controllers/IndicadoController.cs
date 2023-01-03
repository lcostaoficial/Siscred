using Siscred.Application.Interfaces;
using Siscred.Application.ViewModel;
using Siscred.Infra.CrossCutting.Helpers;
using Siscred.Infra.CrossCutting.Messages;
using Siscred.Infra.CrossCutting.Security;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Siscred.Presentation.Web.Areas.Empresa.Controllers
{
    [Authorize(Roles = "Empresa")]
    public class IndicadoController : Controller
    {
        private readonly IProfissionalIndicadoAppService _profissionalIndicadoApp;
        private readonly ITipoArquivoAppService _tipoArquivoApp;
        private readonly IEditalAppService _editalApp;
        private readonly IInscricaoAppService _inscricaoApp;
        private readonly IArquivoAppService _arquivoApp;

        public IndicadoController(IProfissionalIndicadoAppService profissionalIndicadoApp, ITipoArquivoAppService tipoArquivoApp, IEditalAppService editalApp, IInscricaoAppService inscricaoApp, IArquivoAppService arquivoApp)
        {
            _profissionalIndicadoApp = profissionalIndicadoApp;
            _tipoArquivoApp = tipoArquivoApp;
            _editalApp = editalApp;
            _inscricaoApp = inscricaoApp;
            _arquivoApp = arquivoApp;
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

        public void ValidarInscricao(InscricaoVm inscricao)
        {
            if (inscricao.SituacaoInscricao != SituacaoInscricaoVm.Pendente) throw new Exception("Falha ao tentar localizar uma inscrição pendente!");
        }

        public void ValidarQuantidadeIndicado(int editalId, bool finalizaInscricao)
        {
            var inscricao = _inscricaoApp.ValidarDuplicidadeInscricaoUsuario(Account.UsuarioId, editalId);
            var listaIndicado = _profissionalIndicadoApp.ObterTodosPorInscricaoId(inscricao.Id);
            if (listaIndicado != null)
            {
                if (listaIndicado.Count() <= 0 && finalizaInscricao) throw new Exception($"Você precisa informar ao menos 1 profissional indicado!");
                if (listaIndicado.Count() >= 16) throw new Exception($"Você pode informar no máximo 16 indicados!");
            }
        }

        public bool ValidarDuplicidadeProfissional(string cpf, int editalId)
        {
            var inscricao = _inscricaoApp.ValidarDuplicidadeInscricaoUsuario(Account.UsuarioId, editalId);
            var inscricaoExiste = _inscricaoApp.ValidarDuplicidadeInscricaoProfissional(cpf, inscricao.Id);
            if (inscricaoExiste == null)
                return true;
            else
                return false;
        }

        public void ValidarGlobal(int editalId, out InscricaoVm inscricao, out EditalVm edital)
        {
            inscricao = _inscricaoApp.ValidarDuplicidadeInscricaoUsuario(Account.UsuarioId, editalId);
            ValidarInscricao(inscricao);
            edital = _editalApp.ObterPorId(inscricao.EditalId);
            ValidarPeriodoEdital(edital);
        }

        public void ValidarGlobal(int editalId)
        {
            var inscricao = _inscricaoApp.ValidarDuplicidadeInscricaoUsuario(Account.UsuarioId, editalId);
            ValidarInscricao(inscricao);
            var edital = _editalApp.ObterPorId(inscricao.EditalId);
            ValidarPeriodoEdital(edital);
        }

        public ActionResult Index(int editalId)
        {
            try
            {
                var inscricao = new InscricaoVm();
                var edital = new EditalVm();
                ValidarGlobal(editalId, out inscricao, out edital);
                ViewBag.Edital = edital;
                var list = _profissionalIndicadoApp.ObterTodosPorInscricaoId(inscricao.Id);
                return View(list);
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Home").Error(e.Message);
            }
        }

        public ActionResult Adicionar(int editalId)
        {
            try
            {
                ValidarQuantidadeIndicado(editalId, false);
                var inscricao = new InscricaoVm();
                var edital = new EditalVm();
                ValidarGlobal(editalId, out inscricao, out edital);
                ViewBag.Inscricao = inscricao;
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
        public ActionResult Adicionar(ProfissionalIndicadoVm model)
        {
            try
            {
                if (!ValidarDuplicidadeProfissional(model.Cpf, model.EditalId)) throw new Exception($"O CPF {model.Cpf} já foi cadastrado para esta inscrição!");
                ValidarQuantidadeIndicado(model.EditalId, false);
                ValidarGlobal(model.EditalId);
                if (!ValidationExtension.IsCpf(model.Cpf)) throw new Exception(Error.InvalidCpf);
                if (!ModelState.IsValid) throw new Exception(Error.InvalidFields);
                _profissionalIndicadoApp.Adicionar(model);
                return RedirectToAction("Index", "Indicado", new { editalId = model.EditalId }).Success();
            }           
            catch (Exception e)
            {
                return RedirectToAction("Adicionar", "Indicado", new { editalId = model.EditalId }).Error(e.Message);
            }
        }

        public ActionResult Excluir(int editalId, int profissionalIndicadoId)
        {
            try
            {
                var indicado = _profissionalIndicadoApp.ObterPorIdComInscricao(profissionalIndicadoId);
                if (indicado.Inscricao.UsuarioId != Account.UsuarioId) throw new Exception("Você não pode excluir um profissional indicado que não pertença a você!");
                ValidarGlobal(editalId);
                _profissionalIndicadoApp.Remover(profissionalIndicadoId);
                RemoverArquivos(indicado);
                return RedirectToAction("Index", "Indicado", new { editalId }).Success();
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Indicado", new { editalId }).Error(e.Message);
            }
        }

        private void RemoverArquivos(ProfissionalIndicadoVm indicado)
        {
            foreach (var item in indicado.Arquivos)
            {
                var fileOld = HttpContext.Server.MapPath(item.Caminho);
                if (!string.IsNullOrEmpty(item.Caminho))
                {
                    if (System.IO.File.Exists(fileOld)) System.IO.File.Delete(fileOld);
                }
            }
        }

        public ActionResult Comprovante(int inscricaoId)
        {
            var model = _inscricaoApp.ObterPorIdParaComprovante(inscricaoId);
            return View(model);
        }

        public ActionResult Finalizar(int editalId)
        {
            try
            {
                var inscricao = _inscricaoApp.ValidarDuplicidadeInscricaoUsuario(Account.UsuarioId, editalId);
                if (inscricao.SituacaoInscricao != SituacaoInscricaoVm.Pendente) throw new Exception("Somente inscrições pendentes podem serem finalizadas!");
                ValidarQuantidadeIndicado(editalId, true);              
                ValidarGlobal(editalId);
                _inscricaoApp.Atualizar(new InscricaoVm { Id = inscricao.Id, SituacaoInscricao = SituacaoInscricaoVm.Inscrito });
                return RedirectToAction("Comprovante", "Indicado", new { inscricaoId = inscricao.Id }).Success("Inscrição finalizada com sucesso!");
            }
            catch (Exception e)
            {
                return RedirectToAction("Index", "Indicado", new { editalId }).Error(e.Message);
            }
        }

        public JsonResult ValidarCpf(string cpf, int editalId)
        {
            var validateCnpj = ValidationExtension.IsCpf(cpf);
            if (validateCnpj)
            {
                var inscricao = ValidarDuplicidadeProfissional(cpf, editalId);
                if (inscricao)
                {
                    var result = new { msg = "", valid = true };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    var result = new { msg = $"O CPF {cpf} já foi cadastrado para esta inscrição!", valid = false };
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                var result = new { msg = Error.InvalidCpf, valid = false };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        public void SetViewBag(int editalId)
        {
            ViewBag.Edital = _editalApp.ObterPorId(editalId);
            ViewBag.ArquivosExigidos = _tipoArquivoApp.ObterPorEditalIdIndicado(editalId);
        }
    }
}
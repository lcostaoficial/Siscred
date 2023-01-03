using System.Web.Optimization;

namespace Siscred.Presentation.Web
{
    public class BundleConfig
    {      
        public static void RegisterBundles(BundleCollection bundles)
        {

            #region Básico

            bundles.Add(new StyleBundle("~/Content/basic-css")
                .Include(
                "~/css/bootstrap.min.css",
                "~/css/metisMenu.min.css",               
                "~/css/font-awesome.min.css",
                "~/css/validation.css"));

            bundles.Add(new ScriptBundle("~/bundles/basic-js").Include(
                "~/js/jquery.min.js",
                "~/js/bootstrap.min.js",
                "~/js/metisMenu.min.js",
                "~/js/site.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                "~/plugins/jquery-validate/jquery.validate.js",
                "~/plugins/jquery-validate/jquery.validate.unobtrusive.js",
                "~/plugins/jquery-validate/jquery.validate.extension.js"));

            #endregion

            #region Tema

            bundles.Add(new StyleBundle("~/Content/login-css").Include("~/css/login.css"));

            bundles.Add(new StyleBundle("~/Content/empresa-css").Include("~/css/empresa.css"));

            bundles.Add(new StyleBundle("~/Content/gestor-css").Include("~/css/gestor.css"));

            bundles.Add(new StyleBundle("~/Content/indicado-css").Include("~/css/indicado.css"));

            #endregion

            #region Estilos

            bundles.Add(new StyleBundle("~/Content/wizard-css").Include("~/css/wizard.css"));

            #endregion

            #region Plugins

            bundles.Add(new StyleBundle("~/Content/sweetalert2-css").Include("~/plugins/sweetalert2/sweetalert.css"));

            bundles.Add(new ScriptBundle("~/bundles/sweetalert2-js").Include(
                "~/plugins/sweetalert2/sweetalert.js",
                "~/plugins/sweetalert2/polyfill.js"));

            bundles.Add(new StyleBundle("~/Content/datapicker-css").Include(
                "~/plugins/datepicker/bootstrap-datepicker.min.css",
                "~/plugins/datepicker/bootstrap-datepicker.standalone.min.css",
                "~/plugins/datepicker/bootstrap-datepicker3.min.css",
                "~/plugins/datepicker/bootstrap-datepicker3.standalone.min.cs"));

            bundles.Add(new ScriptBundle("~/bundles/datapicker-js").Include(
                "~/plugins/datepicker/bootstrap-datepicker.min.js",
                "~/plugins/datepicker/locales/bootstrap-datepicker.pt-BR.min.js",
                "~/plugins/datepicker/datepicker.config.js"));

            bundles.Add(new StyleBundle("~/Content/datatables-css").Include(
                "~/plugins/datatables/css/jquery.datatables.css",
                "~/plugins/datatables/css/jquery.datatables_themeroller.css"));

            bundles.Add(new ScriptBundle("~/bundles/datatables-js").Include(
                "~/plugins/datatables/js/jquery.datatables.js"));

            bundles.Add(new StyleBundle("~/Content/bootstrap-select-css").Include(
                "~/plugins/bootstrap-select/css/bootstrap-select.css"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap-select-js").Include(
                "~/plugins/bootstrap-select/js/bootstrap-select.js",
                "~/plugins/bootstrap-select/js/i18n/defaults-pt_BR.js"));

            bundles.Add(new ScriptBundle("~/bundles/chosen-order-css").Include("~/plugins/chosen-order/css/chosen.css"));

            bundles.Add(new ScriptBundle("~/bundles/chosen-order-js").Include(
                "~/plugins/chosen-order/js/chosen.jquery.min.js",
                "~/plugins/chosen-order/js/chosen.order.jquery.js",
                "~/plugins/chosen-order/js/chosen.config.js"));

            bundles.Add(new StyleBundle("~/bundles/multi-select-css").Include("~/plugins/multi-select/css/multi-select.css"));

            bundles.Add(new ScriptBundle("~/bundles/multi-select-js").Include("~/plugins/multi-select/js/jquery.multi-select.js"));

            bundles.Add(new ScriptBundle("~/bundles/maskedinput-js").Include("~/plugins/jquery-maskedinput/jquery.maskedinput.js"));

            bundles.Add(new ScriptBundle("~/bundles/maskmoney-js").Include("~/plugins/jquery-maskmoney/jquery.maskmoney.js"));

            #endregion

            #region Páginas

            bundles.Add(new ScriptBundle("~/bundles/table-data-js").Include("~/js/pages/table-data.js"));

            bundles.Add(new ScriptBundle("~/bundles/recuperacao-senha-js").Include("~/js/pages/recuperacao-senha.js"));

            bundles.Add(new ScriptBundle("~/bundles/alterar-senha-js").Include("~/js/pages/alterar-senha.js"));

            bundles.Add(new ScriptBundle("~/bundles/cadastrar-empresa-js").Include("~/js/pages/cadastrar-empresa.js"));

            bundles.Add(new ScriptBundle("~/bundles/inscricao-js").Include("~/js/pages/inscricao.js"));

            bundles.Add(new ScriptBundle("~/bundles/inscricao-visualizacao-js").Include("~/js/pages/inscricao-visualizacao.js"));

            bundles.Add(new ScriptBundle("~/bundles/indicado-js").Include("~/js/pages/indicado.js"));

            bundles.Add(new ScriptBundle("~/bundles/globalmask-js").Include("~/js/globalmask.js"));

            #endregion

            BundleTable.EnableOptimizations = false;

            bundles.IgnoreList.Clear();
        }
    }
}
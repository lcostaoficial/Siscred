using System;
using System.IO;
using System.Text;
using System.Web.Mvc;
using System.Web.UI;

namespace Siscred.Infra.CrossCutting.Helpers
{
    public static class RenderPartialToStringExtension
    {
        public static string RenderPartialToString(this ControllerContext context, string partialViewName, object model)
        {
            return RenderPartialToStringMethod(context, partialViewName, model);
        }

        public static string RenderPartialToString(ControllerContext context, string partialViewName, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            return RenderPartialToStringMethod(context, partialViewName, viewData, tempData);
        }

        public static string RenderPartialToStringMethod(ControllerContext context, string partialViewName, ViewDataDictionary viewData, TempDataDictionary tempData)
        {
            ViewEngineResult result = ViewEngines.Engines.FindPartialView(context, partialViewName);
            if (result.View != null)
            {
                StringBuilder sb = new StringBuilder();
                using (StringWriter sw = new StringWriter(sb))
                {
                    using (HtmlTextWriter output = new HtmlTextWriter(sw))
                    {
                        ViewContext viewContext = new ViewContext(context, result.View, viewData, tempData, output);
                        result.View.Render(viewContext, output);
                    }
                }
                return sb.ToString();
            }
            return String.Empty;
        }

        public static string RenderPartialToStringMethod(ControllerContext context, string partialViewName, object model)
        {
            ViewDataDictionary viewData = new ViewDataDictionary(model);
            TempDataDictionary tempData = new TempDataDictionary();
            return RenderPartialToStringMethod(context, partialViewName, viewData, tempData);
        }
    }
}
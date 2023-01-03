using System;
using System.Web;

namespace Siscred.Infra.CrossCutting.Helpers
{
    public static class UrlExtension
    {
        public static string GetFullUrl(this HttpRequest request, string relativeUrl)
        {
            return String.Format("{0}://{1}{2}", request.Url.Scheme, request.Url.Authority, VirtualPathUtility.ToAbsolute(relativeUrl));
        }
    }
}
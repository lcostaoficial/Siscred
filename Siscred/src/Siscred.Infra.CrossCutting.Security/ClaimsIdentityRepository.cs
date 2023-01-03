using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Microsoft.Owin.Security;

namespace Siscred.Infra.CrossCutting.Security
{
    public class ClaimsIdentityRepository
    {
        public static void Add(string type, string value)
        {
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
            if (identity == null) return;
            Remove(type);
            identity.AddClaim(new Claim(type, value));
            var authenticationManager = HttpContext.Current.GetOwinContext().Authentication;
            authenticationManager.AuthenticationResponseGrant = new AuthenticationResponseGrant(new ClaimsPrincipal(identity), new AuthenticationProperties { IsPersistent = true });
        }

        public static void Remove(string type)
        {
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
            if (identity == null) return;
            var existingClaim = identity.FindFirst(type);
            if (existingClaim != null) identity.RemoveClaim(existingClaim);
        }

        public static string GetByType(string type)
        {
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
            var byType = identity.Claims.FirstOrDefault(c => c.Type == type);
            if (byType != null) return identity == null ? string.Empty : byType.Value;
            return string.Empty;
        }

        public static bool IsInRole(string role)
        {
            return HttpContext.Current.User.IsInRole(role);
        }

        public static List<Claim> GetAll()
        {
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
            return identity == null ? null : identity.Claims.ToList();
        }

        public static bool IsAuthenticated()
        {
            var identity = HttpContext.Current.User.Identity as ClaimsIdentity;
            return identity == null ? false : identity.IsAuthenticated;
        }
    }
}
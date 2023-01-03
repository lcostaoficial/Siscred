using Microsoft.Owin.Security;
using Siscred.Application.ViewModel;
using System.Collections.Generic;
using System.Security.Claims;
using System.Web;

namespace Siscred.Infra.CrossCutting.Security
{
    public class Authentication
    {
        private static IAuthenticationManager Auth => HttpContext.Current.Request.GetOwinContext().Authentication;

        public static void Signin(UsuarioVm usuario)
        {
            var claims = new List<Claim>
            {
                new Claim("http://schemas.microsoft.com/accesscontrolservice/2010/07/claims/identityprovider","ASP.NET Identity"),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
                new Claim(ClaimTypes.GivenName, usuario.Nome)
            };
            claims.Add(new Claim(ClaimTypes.Role, usuario.TipoUsuario.ToString()));
            Auth.SignIn(new ClaimsIdentity(claims, "ApplicationCookie"));
        }

        public static void Signout(params string[] authenticationTypes)
        {
            Auth.SignOut(authenticationTypes);
        }
    }
}
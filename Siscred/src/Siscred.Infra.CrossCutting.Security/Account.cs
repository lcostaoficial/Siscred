using System.Security.Claims;

namespace Siscred.Infra.CrossCutting.Security
{
    public class Account
    {
        public static bool IsAuthenticated => ClaimsIdentityRepository.IsAuthenticated();
        public static int UsuarioId => int.Parse(ClaimsIdentityRepository.GetByType(ClaimTypes.NameIdentifier));
        public static string Nome => ClaimsIdentityRepository.GetByType(ClaimTypes.GivenName);
        public static bool IsInRole(string role) => ClaimsIdentityRepository.IsInRole(role);
    }
}
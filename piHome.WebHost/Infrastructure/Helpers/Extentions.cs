using System.Security.Principal;

namespace piHome.WebHost.Infrastructure.Helpers
{
    public static class Extentions
    {
        public static string GetLoggedUserId(this IPrincipal principal)
        {
            return principal.Identity.Name;
        }
    }
}

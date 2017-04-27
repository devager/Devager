using System.Security.Principal;

namespace Devager
{
    public class Helpers
    {
        public static bool IsAdministrator()
        {
            return (new WindowsPrincipal(WindowsIdentity.GetCurrent())).IsInRole(WindowsBuiltInRole.Administrator);
        }
    }
}

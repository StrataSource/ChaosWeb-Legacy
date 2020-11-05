using System.Collections.Generic;

namespace ChaosInitiative.Web.ControlPanel
{
    public static class NavbarControlPanel
    {
        public static Dictionary<string, string> GetNavbar()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                {"/Panel/Dashboard", "Dashboard"},
                {"/Panel/Games", "Games"},
                {"/Panel/Releases", "Releases"},
                {"/Panel/Features", "Features"},
                {"/Panel/Tools", "Tools"}
            };

            return dictionary;
        }
    }
}
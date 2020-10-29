using System.Collections.Generic;

namespace ChaosInitiative.Web.P2CE
{
    public class NavbarP2CE
    {
        public static Dictionary<string, string> GetNavbar()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                {"/", "Home"},
                {"/features", "Features"},
                {"/team", "Who we are"},
                {"/mods", "Mods"},
                //{"/wiki", "Wiki"},
                {"/faq", "FAQ"},
            };

            return dictionary;
        }
    }
}
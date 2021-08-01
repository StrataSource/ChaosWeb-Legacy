using System.Collections.Generic;

namespace ChaosInitiative.Web.P2CE
{
    public class NavbarP2CE
    {
        public static Dictionary<string, string> GetNavbar()
        {
            var dictionary = new Dictionary<string, string>
            {
                {"/", "Home"},
                {"/features", "Features"},
                {"/team", "Team Members"},
                {"/mods", "Mods"},
                {"https://chaosinitiative.github.io/Wiki/", "Wiki"},
                {"/faq", "FAQ"},
            };
            return dictionary;
        }
    }
}
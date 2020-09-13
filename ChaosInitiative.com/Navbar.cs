using System.Collections.Generic;

namespace ChaosInitiative.com
{
    public class Navbar
    {
        public Dictionary<string, string> Pages { get; set; }
        public string ActivePage { get; set; }

        public Navbar(Dictionary<string, string> dictionary = null)
        {
            Pages = dictionary == null ? new Dictionary<string, string>() : null;
        }

        public static Navbar GetDefaultNavbar(string activePage = null)
        {
            Navbar navbar = new Navbar();
            navbar.Pages.Add("/", "Home");
            navbar.Pages.Add("/engine", "Chaos Source");
            navbar.Pages.Add("/games", "Games");

            navbar.ActivePage = activePage;
            
            return navbar;
        }
    }

}
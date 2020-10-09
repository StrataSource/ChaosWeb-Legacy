using System.Collections.Generic;

namespace ChaosInitiative.Web.Shared
{
    public class Navbar
    {
        public Dictionary<string, string> Pages { get; set; }
        public string ActivePage { get; set; }

        public Navbar(Dictionary<string, string> dictionary = null)
        {
            Pages = dictionary == null ? new Dictionary<string, string>() : null;
        }

        public static Navbar GetDefaultNavbarChaos(string activePage = null)
        {
            Navbar navbar = new Navbar();
            navbar.Pages.Add("/", "Home");
            navbar.Pages.Add("/engine", "Chaos Source");
            navbar.Pages.Add("/games", "Games");

            navbar.ActivePage = activePage;
            
            return navbar;
        }
        
        public static Navbar GetDefaultNavbarP2CE(string activePage = null)
        {
            Navbar navbar = new Navbar();
            navbar.Pages.Add("/", "Home");
            navbar.Pages.Add("/features", "Features");
            navbar.Pages.Add("/team", "Who we are");
            navbar.Pages.Add("/mods", "Mods");
            navbar.Pages.Add("/wiki", "Wiki");
            navbar.Pages.Add("/faq", "FAQ");

            navbar.ActivePage = activePage;
            
            return navbar;
        }

        public static Navbar GetNavbarControlPanel()
        {
            Navbar navbar = new Navbar();
            navbar.Pages.Add("/Panel/Dashboard", "Dashboard");
            navbar.Pages.Add("/Panel/Games", "Games");
            navbar.Pages.Add("/Panel/Releases", "Releases");
            navbar.Pages.Add("/Panel/Features", "Features");
            navbar.Pages.Add("/Panel/Tools", "Tools");
            
            navbar.ActivePage = null;

            return navbar;
        }
    }
}
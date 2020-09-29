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
            navbar.Pages.Add("/wiki", "Wiki");
            navbar.Pages.Add("/faq", "FAQ");

            navbar.ActivePage = activePage;
            
            return navbar;
        }

        public static Navbar GetNavbarControlPanel(bool authenticated = false)
        {
            Navbar navbar = new Navbar();
            if (authenticated)
            {
                navbar.Pages.Add("/Panel/Dashboard", "Dashboard");
                navbar.Pages.Add("/Panel/Releases", "Releases");
                navbar.Pages.Add("/Panel/Features", "Features");
                navbar.Pages.Add("/Panel/Tools", "Tools");
            }
            else
            {
                navbar.Pages.Add("/Panel/Login", "Login");
                navbar.Pages.Add("https://www.chaosinitiative.com", "Homepage");
            }

            navbar.ActivePage = null;

            return navbar;
        }
    }
}
using System.Collections.Generic;

namespace Portal2CommunityEdition.com
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
            navbar.Pages.Add("/features", "Features");
            navbar.Pages.Add("/team", "Who we are");
            navbar.Pages.Add("/wiki", "Wiki");

            navbar.ActivePage = activePage;
            
            return navbar;
        }
    }
}
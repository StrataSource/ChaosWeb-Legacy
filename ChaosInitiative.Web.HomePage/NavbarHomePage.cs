using System.Collections.Generic;

namespace ChaosInitiative.Web.HomePage
{
    public class NavbarHomePage
    {
        public static Dictionary<string, string> GetNavbar()
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                {"/", "Home"}, 
                {"/engine", "Chaos Source"}, 
                {"/games", "Games"},
                {"/contact", "Contact"},
                //{"/wiki", "Wiki"}
            };

            return dictionary;
        }
    }
}
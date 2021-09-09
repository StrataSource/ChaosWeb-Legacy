using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChaosInitiative.Web.P2CE.Pages
{
    public class ModsModel : PageModel
    {
        public List<Mod> Mods { get; set; }
        
        public void OnGet()
        {
            // Filling the list manually for now. This can later be hooked up to control panel if needed
            Mods = new List<Mod>
            {
                new Mod()
                {
                    Name = "abyss",
                    DisplayName = "Portal: Abyss",
                    Description = "In an alternate universe, set during the Seven Hour War, you find yourself plunged" +
                                  " into the depths of an abandoned Aperture Science facility." +
                                  "Abyss explores the failed ambitions of a once-flourishing company," + 
                                  " and the legacy it left behind. " +
                                  " Your choices have consequences, and your actions dictate your survival -- or lack thereof..",
                    Developers = new List<ModDeveloper>
                    {
                        new ModDeveloper
                        {
                            Name = "Distanced",
                            Link = ""
                        },
                        new ModDeveloper
                        {
                            Name = "Jeremy (peefTube / spiro9)"
                        }
                    },
                    Links = new Dictionary<string, string>
                    {
                        {
                            "Discord",
                            "https://discord.gg/zcrqaFS"
                        },
                        {
                            "ModDB",
                            "http://www.moddb.com/mods/portal-2-abyss"
                        },
                        {
                            "Twitter",
                            "https://twitter.com/AbyssPortal"
                        }
                    }
                },
            };
        }
    }
}
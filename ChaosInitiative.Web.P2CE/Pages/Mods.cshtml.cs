using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChaosInitiative.Web.P2CE.Pages
{
    public class ModsModel : PageModel
    {
        public List<Mod> Mods { get; set; }
        
        public void OnGet()
        {
            // Yes this is bad
            // No we're not changing it just yet
            Mods = new List<Mod>
            {
                new Mod()
                {
                    Name = "psc",
                    DisplayName = "Portal: Singularity Collapse",
                    Description = "Long time ago Aperture Science tried to make minature holes for Portal Guns. It turns out that they needed a giant one to make them." +
                                  "Because GLaDOS killed all of the employees of the facilty, the black hole has become VERY unstable and can suck Earth at any time." +
                                  "The question is: Will you save Earth and Aperture Science, or use the black hole to your advantage?",
                    Developers = new List<ModDeveloper>()
                    {
                        new ModDeveloper()
                        {
                            Name = "Enderek0"
                        }
                    },
                    Links = new Dictionary<string, string>
                    {
                        {
                            "ModDB",
                            "https://www.moddb.com/mods/portal-singularity-collapse"
                        },
                        {
                            "Discord",
                            "https://discord.gg/Azv6bfsXS8"
                        }
                    }
                }
            };
        }
    }
}
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
                    Name = "revolution",
                    DisplayName = "Portal: Revolution",
                    Description = "Portal: Revolution is a mod developed alongside P2CE by Tewan." +
                                  "The features that the mod could benefit from the most when jumping to Portal 2: Community Edition's engine are" +
                                  "several graphical enhancements, panorama ui, fixed and improved tools and other small features needed for development.",
                    Developers = new List<ModDeveloper>
                    {
                        new ModDeveloper
                        {
                            Name = "Tewan",
                            Link = "https://twitter.com/spyce_tewan"
                        },
                        new ModDeveloper
                        {
                            Name = "MysticalAce"
                        },
                        new ModDeveloper
                        {
                            Name = "ZRezlon"
                        }
                    },
                    Links = new Dictionary<string, string>
                    {
                        {
                            "Steam", 
                            "https://store.steampowered.com/app/601360/Portal_Revolution/"
                        },
                        {
                            "Discord",
                            "https://discord.gg/zrSVsM7"
                        },
                        {
                            "ModDB",
                            "https://www.moddb.com/mods/portal-revolution-spyce-software"
                        }
                    }
                    
                }
            };
        }
    }
}
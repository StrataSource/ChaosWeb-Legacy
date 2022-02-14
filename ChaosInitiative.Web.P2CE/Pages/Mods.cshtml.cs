using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting.Internal;

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
                    Developers = new List<ModDeveloper>
                    {
                        new ModDeveloper
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
                },
                new Mod()
                {
                    Name = "abyss",
                    DisplayName = "Abyss",
                    Description = "In an alternate universe, set during the Seven Hour War, you find yourself plunged" +
                                  " into the depths of an abandoned Aperture Science facility." +
                                  " Abyss explores the failed ambitions of a once-flourishing company," + 
                                  " and the legacy it left behind. " +
                                  " Your choices have consequences, and your actions dictate your survival -- or lack thereof..",
                    Developers = new List<ModDeveloper>
                    {
                        new ModDeveloper
                        {
                            Name = "Team Abyss",
                            Link = "https://abyssmod.com/credits"
                        },
                    },
                    Links = new Dictionary<string, string>
                    {
                        {
                            "ModDB",
                            "http://www.moddb.com/mods/portal-2-abyss"
                        },
                        {
                            "Discord",
                            "https://discord.gg/zcrqaFS"
                        },
                        {
                            "Website",
                            "https://abyssmod.com/"
                        },
                        {
                            "Twitter",
                            "https://twitter.com/AbyssPortal"
                        }
                    }
                },
                new Mod()
                {
                    Name = "gis",
                    DisplayName = "Growth: Infinite Stories",
                    Description = "Playing as a former scientist, you follow the lead of the Core And Subject Special Initiative Engineer, or CASSIE, as they guide you through the Multiverse Exploration Initiative." +
                                  " Your goal is to explore as many universes as possible, discovering new and exciting universes and technology." +
                                  " But as you explore, you start to question the intentions of this initiative... why does Aperture need to explore the Multiverse for new technologies? Is there something more to this?",
                    Developers = new List<ModDeveloper>
                    {
                        new ModDeveloper
                        {
                            Name = "Erin-Rose"
                        },
                    }, 
                    Links = new Dictionary<string, string>
                    {
                        {
                            "Discord",
                            "https://discord.gg/DHrCFPJ"
                        },
                    }
                }
            };
        }

        // HACK: asp.net sucks and this is the only way I can do this :)
        public static bool DoesFileExist(string filename)
        {
            return System.IO.File.Exists($"{Startup.Environment.ContentRootPath}/wwwroot/{filename}");
        }
    }
}

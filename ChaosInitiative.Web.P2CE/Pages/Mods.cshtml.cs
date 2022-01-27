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
                    Name = "sep",
                    DisplayName = "Scorched Earth Policy",
                    Description = "The Portal franchise has stated before that there are an infinite amount of universes with an " +
                                  "infinite amount of Earths and an infinite amount of Apertures... But sometimes in those universes things change.<br/><br/>" +
                                  "One crazy artist late for work.<br/>" +
                                  "One test subject never deployed.<br/>"+
                                  "One AI never killed.<br/>" +
                                  "One alliance made.<br/><br/>" +
                                  "In this universe The Combine and Aperture Labs are business partners and the finest at it in all the universe too! " +
                                  "Come visit their Citadel's fine Galleria on Earth! So many amazing products waiting for you! You can also take home your very own human slave straight from the cloning lab! "+
                                  "More the type to be entertained ? Watch humans scramble trying to solve horrifyingly deadly puzzles in our theatre! We'll be waiting...",

                    Developers = new List<ModDeveloper>
                    {
                        new ModDeveloper
                        {
                            Name = "War The Nuclear Shill"
                        }
                    },
                    Links = new Dictionary<string, string>
                    {
                        {
                            "ModDB",
                            "https://www.moddb.com/mods/scorched-earth-policy/"
                        },
                        {
                            "Discord",
                            "https://discord.gg/FrzmaybCnY"
                        }
                    }
                }
            };
        }
    }
}

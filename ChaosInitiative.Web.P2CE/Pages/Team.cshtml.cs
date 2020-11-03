using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChaosInitiative.Web.P2CE.Pages
{
    public class TeamModel : PageModel
    {

        public List<TeamMember> TeamMembers { get; set; }

        public Dictionary<string, string> Contributors { get; set; }

        public TeamModel()
        {
            TeamMembers = new List<TeamMember>
            {
                new TeamMember
                {
                    UserName = "CitadelCore",
                    Roles = new []{ "Project Founder", "Engine Programming", "Project Manager" },
                    SocialMediaServices = new Dictionary<SocialMediaService, string>
                    {
                        {
                            SocialMediaService.GitHub, "https://github.com/citadelcore"
                        }
                    }
                },
                new TeamMember
                {
                    UserName = "Tewan",
                    Roles = new [] { 
                        "UI/UX & Gameplay Programming", 
                        "Web & Tool Development", 
                        "Community Manager", 
                        "Campaign Lead", 
                        "<a href=\"https://store.steampowered.com/app/601360/Portal_Revolution\">Portal: Revolution Developer</a>" 
                    },
                    SocialMediaServices = new Dictionary<SocialMediaService, string>
                    {
                        {
                            SocialMediaService.GitHub, "https://github.com/spycetewan"
                        },
                        {
                            SocialMediaService.Twitter, "https://twitter.com/spyce_tewan"
                        }
                    }
                },
                new TeamMember
                {
                    UserName = "EchoesForeAndAft",
                    Roles = new []
                    {
                        "Several Programming",
                        "<a href=\"https://www.moddb.com/mods/vance\">VANCE Developer</a>"
                    },
                    SocialMediaServices = new Dictionary<SocialMediaService, string>
                    {
                        {
                            SocialMediaService.GitHub, "https://github.com/blightedlight"
                        }
                    }
                },
                new TeamMember
                {
                    UserName = "JJI77",
                    Roles = new []
                    {
                        "Linux Port",
                        "Backend Programming"
                    },
                    SocialMediaServices = new Dictionary<SocialMediaService, string>
                    {
                        {
                            SocialMediaService.GitHub, "https://github.com/jjl772"
                        }
                    }
                },/*
                new TeamMember
                {
                    UserName = "BenVlodgi",
                    Roles = new []
                    {
                        "<a href=\"https://portal2backstock.com/bee/\">BEEMod Developer</a>"
                    },
                    SocialMediaServices = new Dictionary<SocialMediaService, string>
                    {
                        {
                            SocialMediaService.GitHub, "https://github.com/benvlodgi"
                        }
                    }
                }*/
            };

            Contributors = new Dictionary<string, string>
            {
                {
                    "Wii2", "Graphics asset creation"
                },
                {
                    "Josepezdj", "3D Modelling"
                },
                {
                    "Sonop", "3D Modelling"
                }
            };
        }
        
        public void OnGet()
        {
            
        }
    }

    public class TeamMember
    {
        public string UserName { get; set; }
        public string[] Roles { get; set; }
        public Dictionary<SocialMediaService, string> SocialMediaServices { get; set; }
    }

    public enum SocialMediaService
    {
        GitHub,
        Twitter,
        Steam,
        Reddit,
        Facebook
    }
}
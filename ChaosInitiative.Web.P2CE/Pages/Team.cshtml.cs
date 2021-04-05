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
            
            // TODO: Integrate team member list into backend
            TeamMembers = new List<TeamMember>
            {
                new TeamMember
                {
                    UserName = "CitadelCore",
                    Roles = new []
                    {
                        "Project Founder", 
                        "Engine Programming", 
                        "Project Manager"
                    },
                    SocialMediaServices = new Dictionary<SocialMediaService, string>
                    {
                        {
                            SocialMediaService.GitHub, "https://github.com/citadelcore"
                        },
                        {
                            SocialMediaService.Twitter, "https://twitter.com/fabric_operator"
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
                        @"<a href=""https://store.steampowered.com/app/601360/Portal_Revolution"">Portal: Revolution Developer</a>" 
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
                },
                new TeamMember
                {
                    UserName = "Blenderiste09",
                    Roles = new []
                    {
                        "Speedrun Support",
                        "Quality of Life"
                    },
                    SocialMediaServices = new Dictionary<SocialMediaService, string>
                    {
                        {
                            SocialMediaService.GitHub, "https://github.com/Blenderiste09"
                        },
                        {
                            SocialMediaService.YouTube, "https://www.youtube.com/channel/UCLs0a4Fi1d6AXDxcCaLZpAw"
                        },
                        {
                            SocialMediaService.Steam, "https://steamcommunity.com/profiles/76561198251755710"
                        }
                    }
                },
                new TeamMember
                {
                    UserName = "mlugg",
                    Roles = new []
                    {
                        "Linux Port",
                        "Engine Programming",
                        "Speedrun Support"
                    },
                    SocialMediaServices = new Dictionary<SocialMediaService, string>
                    {
                        {
                            SocialMediaService.GitHub, "https://github.com/mlugg"
                        },
                        {
                            SocialMediaService.Twitter, "https://twitter.com/matthew_lugg"
                        }
                    }
                }
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
        Facebook,
        YouTube
    }
}
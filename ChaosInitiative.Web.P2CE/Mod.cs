using System.Collections.Generic;

namespace ChaosInitiative.Web.P2CE
{
    // =======================
    // Example
    // -----------------------
    // Name:                revolution
    // DisplayName:         Portal: Revolution
    // Description:         This mod is developed alongside...
    // Developers:
    // #0
    // - Name:              Tewan
    // - Link:              https://www.steamcommunity.com/id/tewanxd
    // #1
    // - Name:              MysticalAce
    // - Link:              null (No anchor created when links are null)
    // Links:
    //   #Twitter           https://www.twitter.com/spyce_tewan
    //   #Steam             https://store.steampowered.com/app/601360/Portal_Revolution/
    //   #Discord           https://discord.gg/zrSVsM7
    // =======================

    
    public class Mod
    {
        public string Name { get; set; } 
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public List<ModDeveloper> Developers { get; set; }
        public Dictionary<string, string> Links { get; set; }
        }

    public class ModDeveloper
    {
        public string Name { get; set; }
        public string Link { get; set; }
    }
    
}
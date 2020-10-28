using System.Collections.Generic;
using System.Linq;
using ChaosInitiative.Web.ControlPanel.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ChaosInitiative.Web.ControlPanel.Pages.Panel
{
    public class GamesModel : PageModel
    {

        public List<Game> Games;
        public List<SelectListItem> AvailableRepoOwners;

        private readonly ApplicationContext _context;

        public GamesModel(ApplicationContext context)
        {
            _context = context;
        }
        
        public void OnGet()
        {
            Games = _context.Games.ToList();
            AvailableRepoOwners = new List<SelectListItem>
            {
                new SelectListItem("Chaos Initiative", "ChaosInitiative")
            };
        }
    }
}
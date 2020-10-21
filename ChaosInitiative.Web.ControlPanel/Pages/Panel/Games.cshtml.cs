using System.Collections.Generic;
using System.Linq;
using ChaosInitiative.Web.ControlPanel.Model;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChaosInitiative.Web.ControlPanel.Pages.Panel
{
    public class GamesModel : PageModel
    {

        public List<Game> Games;

        private readonly ApplicationContext _context;

        public GamesModel(ApplicationContext context)
        {
            _context = context;
        }
        
        public void OnGet()
        {
            Games = _context.Games.ToList();
        }
    }
}
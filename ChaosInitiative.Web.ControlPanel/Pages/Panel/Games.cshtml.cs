using System.Collections.Generic;
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

            Games = new List<Game>();
            
            /*
            Games = new List<Game>
            {
                new Game
                {
                    Id = 0,
                    Name = "Portal 2: Community Edition",
                    RepositoryOwner = "ChaosInitiative",
                    RepositoryName = "Portal-2-Community-Edition"
                }
            };*/
        }
    }
}
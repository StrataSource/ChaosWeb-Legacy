using System.Collections.Generic;
using System.Linq;
using ChaosInitiative.Web.ControlPanel.Model;

namespace ChaosInitiative.Web.ControlPanel.Services.Repositories
{
    public class GameRepository : RepositoryBase
    {

        public GameRepository(ApplicationContext context) : base(context)
        {
        }

        public IEnumerable<Game> GetAll()
        {
            return Context.Games.ToList();
        }
        
    }
}
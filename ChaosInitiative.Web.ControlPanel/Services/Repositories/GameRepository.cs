using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using ChaosInitiative.Web.ControlPanel.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChaosInitiative.Web.ControlPanel.Services.Repositories
{
    public class GameRepository : RepositoryBase
    {

        private readonly IGitHubService _gitHubService;

        public GameRepository(ApplicationContext context, IGitHubService gitHubService) : base(context)
        {
            _gitHubService = gitHubService;
        }

        public IEnumerable<Game> GetAll()
        {
            return Context.Games.ToList();
        }

        public async Task<bool> Add(Game game)
        {
            if (string.IsNullOrWhiteSpace(game.Name))
                throw new RepositoryInsertException("Game name is empty");
            
            if (Context.Games.Any(g => g.Name == game.Name))
                throw new RepositoryInsertException($"Game with name '{game.Name}' already exists");
            
            if (!Regex.IsMatch(game.HexColor, @"([0-9]|[a-f]){6}"))
                throw new RepositoryInsertException("Game color is invalid");

            if (!Game.LegalRepositoryOwners.Contains(game.RepositoryOwner))
                throw new RepositoryInsertException($"Chaos Initiative cannot access repository owner '{game.RepositoryOwner}'");

            if (!await _gitHubService.IsValidGitHubRepository(game.RepositoryOwner, game.RepositoryName))
                throw new RepositoryInsertException("Repository is invalid");

            EntityEntry<Game> result = await Context.Games.AddAsync(game);
            await Context.SaveChangesAsync();
            return result.State == EntityState.Added;
        }
        
    }
}
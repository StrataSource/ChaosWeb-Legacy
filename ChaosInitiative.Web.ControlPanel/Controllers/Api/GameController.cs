using System.Linq;
using System.Threading.Tasks;
using ChaosInitiative.Web.ControlPanel.Model;
using Microsoft.AspNetCore.Mvc;
using Octokit;

namespace ChaosInitiative.Web.ControlPanel.Controllers.Api
{
    [Route("api/game")]
    public class GameController : Controller
    {

        private readonly ApplicationContext _context;

        public GameController(ApplicationContext context)
        {
            _context = context;
        }
        
        [HttpPost]
        public async Task<StatusCodeResult> OnPost([FromForm] Game game)
        {
            // Id is assigned via autoincrement
            
            string givenRepoOwner = game.RepositoryOwner;
            string givenRepoName = game.RepositoryName;
            
            // Check github repo validity
            GitHubClient client = GitHubUtil.CreateClient();
            Repository repo = await client.Repository.Get(givenRepoOwner, givenRepoName);

            if (!(repo.Name == givenRepoName && repo.Owner.Login == givenRepoOwner))
            {
                return BadRequest();
            }

            await _context.Games.AddAsync(game);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<StatusCodeResult> OnPut([FromForm] Game game)
        {
            
            return Ok();
        }
    }
}
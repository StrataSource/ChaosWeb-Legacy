using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaosInitiative.Web.ControlPanel.Model;
using Octokit;
using Issue = ChaosInitiative.Web.ControlPanel.Model.Issue;

namespace ChaosInitiative.Web.ControlPanel.Services.Repositories
{
    public class IssueRepository : RepositoryBase
    {
        
        private readonly IGitHubService _gitHubService;
        
        public IssueRepository(ApplicationContext context, IGitHubService gitHubService) : base(context)
        {
            _gitHubService = gitHubService;
        }

        public IEnumerable<Issue> GetAll()
        {
            return Context.Issues.ToList();
        }

        public async Task<IEnumerable<Issue>> GetAllUnassigned(Game game)
        {
            var availableIssues = await GetAllAvailable(game);

            return GetAll().Except(availableIssues);
        }

        public async Task<IEnumerable<Issue>> GetAllAvailable(Game game)
        {
            GitHubClient gitHubClient = _gitHubService.GetClient();
            var availableIssues = await gitHubClient.Issue.GetAllForRepository(game.RepositoryOwner, game.RepositoryName);

            IList<Issue> issues = availableIssues.Select(i => new Issue
            {
                IssueId = (uint) i.Id,
                GameId = game.Id,
                Game = game
            }).ToList();

            return issues;
        }
    }
}
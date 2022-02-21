using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChaosInitiative.Web.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Octokit;

namespace ChaosInitiative.Web.P2CE.Pages 
{ 
    public class FeaturesModel : PageModel
    {
        
        public List<IssuesCache> Caches => IssuesCache.ClosedIssuesCaches;
        
        public void OnGet()
        {
            
        }
    }

    public class IssuesCache
    {
        
        public static readonly List<IssuesCache> ClosedIssuesCaches = new()
        {
            new IssuesCache("Type: bug", "Bugs"),
            new IssuesCache("Type: enhancement", "Features")
        };
        
        public string LabelName { get; set; }
        public string DisplayName { get; set; }
        public int Count { get; set; }

        public IssuesCache(string labelName, string displayName)
        {
            LabelName = labelName;
            DisplayName = displayName;
        }
        
        public async Task RefreshCount()
        {
            var client = new GitHubClient(new ProductHeaderValue("Portal2CommunityEdition.com"));
            var issueProperties = new RepositoryIssueRequest
            {
                State = ItemStateFilter.Closed,
                Labels = { LabelName }
            };
            var issues = await client.Issue.GetAllForRepository(Constants.REPO_OWNER, Constants.REPO_NAME_P2CE, issueProperties);
            Count = issues.Count;
        }
    }

    public class IssueCacheRefreshService : BackgroundService
    {

        private readonly ILogger<IssueCacheRefreshService> _logger;

        public IssueCacheRefreshService(ILogger<IssueCacheRefreshService> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Refreshing issue cache");
            while (!stoppingToken.IsCancellationRequested)
            {
                IssuesCache.ClosedIssuesCaches.ForEach(async cache =>
                {
                    try
                    {
                        await cache.RefreshCount();
                    }
                    catch (RateLimitExceededException exception)
                    {
                        _logger.LogCritical($"GitHub API rate limit exceeded when trying to refresh issue cache. Limit: {exception.Limit}");
                    }
                });

                await Task.Delay(TimeSpan.FromMinutes(10), stoppingToken);
            }
        }
    }
}
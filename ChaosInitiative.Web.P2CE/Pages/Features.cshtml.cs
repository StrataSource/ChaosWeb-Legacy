using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ChaosInitiative.Web.Shared;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Hosting;
using Octokit;

namespace ChaosInitiative.Web.P2CE.Pages 
{ 
    public class FeaturesModel : PageModel
    {

        public readonly List<ClosedIssuesDisplay> ClosedIssuesDisplays = new List<ClosedIssuesDisplay>()
        {
            new ClosedIssuesDisplay("type/bug", "Bugs fixed"),
            new ClosedIssuesDisplay("type/enhancement", "Features added"),
        };
        
        public void OnGet()
        {
            
        }
    }

    public class ClosedIssuesDisplay
    {
        public string LabelName { get; set; }
        public string DisplayName { get; set; }

        public ClosedIssuesDisplay(string labelName, string displayName)
        {
            LabelName = labelName;
            DisplayName = displayName;
        }
        
        public int GetCount()
        {
            GitHubClient client = new GitHubClient(new ProductHeaderValue("Portal2CommunityEdition.com"));
            var bugIssueProperties = new RepositoryIssueRequest()
            {
                State = ItemStateFilter.Closed,
                Labels = { LabelName }
            };
            IReadOnlyList<Issue> issues = client.Issue.GetAllForRepository(Constants.REPO_OWNER, Constants.REPO_NAME_P2CE, bugIssueProperties).Result;
            return issues.Count;
        }
    }

    public abstract class IssueDisplayCacheService : IHostedService, IDisposable
    {
        public Task StartAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}

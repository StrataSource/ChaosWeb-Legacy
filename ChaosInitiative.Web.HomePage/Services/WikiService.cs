using System;
using System.IO;
using System.Linq;
using LibGit2Sharp;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChaosInitiative.Web.HomePage.Services
{
    public class WikiService
    {

        private readonly ILogger<WikiService> _logger;
        private readonly IHostEnvironment _host;
        
        public WikiService(ILogger<WikiService> logger, IHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _host = webHostEnvironment;
        }

        private readonly string _repoDirectoryName = "wiki_markdown";
        private readonly string _repoUrl = "https://github.com/ChaosInitiative/Wiki.git";
        private readonly Signature _signature = new Signature("p2ce website", "contact@chaosinitiative.com", DateTimeOffset.Now);
        
        public void InitWikiGitRepository()
        {
            _logger.LogInformation($"Initializing wiki git repository to '{GetWikiRepositoryPath()}'");
            if (Repository.IsValid(GetWikiRepositoryPath())) // Is this really how we check if the repo already exists?
            {
                _logger.LogInformation("Trying to init wiki repository, but it is already initialized. Refreshing.");
                RefreshWikiGitRepository();
                return;
            }
            
            _logger.LogInformation("Repository doesn't exist. Cloning...");
            Repository.Clone(_repoUrl, _repoDirectoryName);
        }
        
        public void RefreshWikiGitRepository()
        {
            Repository repo = GetWikiRepository();
            var originRemote = repo.Network.Remotes["origin"];

            string logMessage = "";
            Commands.Fetch(repo, 
                           originRemote.Name, 
                           originRemote.FetchRefSpecs.Select(x => x.Specification), 
                           null, 
                           logMessage);
            
            var mergeResult = Commands.Pull(repo, _signature, null);

            if (mergeResult.Status == MergeStatus.Conflicts)
            {
                _logger.LogCritical("MERGE CONFLICT WHEN PULLING WIKI. Don't ever ever ever commit to the local submodule directly");
            }
        }

        public void BuildWiki()
        {
            
            
        }

        private Repository GetWikiRepository()
        {
            return new Repository(GetWikiRepositoryPath());
        }

        private string GetWikiRepositoryPath()
        {
            return Path.Join(_host.ContentRootPath, _repoDirectoryName);
        }
    }

    public abstract class WikiException : Exception
    {
        
    }

    public class WikiRefreshException : WikiException
    {
        
    }
}
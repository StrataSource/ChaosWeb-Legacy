using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LibGit2Sharp;
using Markdig;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ChaosInitiative.Web.HomePage.Services
{
    public class WikiService
    {
        
        private static string WikiLayout { get; set; }

        public WikiService(ILogger<WikiService> logger, IHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _host = webHostEnvironment;
        }

        private readonly ILogger<WikiService> _logger;
        private readonly IHostEnvironment _host;
        
        private readonly string _repoDirectoryName = "wiki_markdown";
        private readonly string _outputDirectoryName = "wiki_pages";
        private readonly string _repoUrl = "https://github.com/ChaosInitiative/Wiki.git";
        private readonly Signature _signature = new Signature("chaos initiative", "contact@chaosinitiative.com", DateTimeOffset.Now);
        
        public void InitWikiGitRepository()
        {
            _logger.LogInformation($"Initializing wiki git repository to '{GetWikiRepositoryPath()}'");
            if (Repository.IsValid(GetWikiRepositoryPath())) // Is this really how we check if the repo already exists?
            {
                _logger.LogInformation("Trying to init wiki repository, but it is already initialized. Refreshing.");
                RefreshWikiGitRepository();
            }
            else
            {
                _logger.LogInformation("Repository doesn't exist. Cloning...");
                Repository.Clone(_repoUrl, _repoDirectoryName);
                FinishInitWikiRepository();
            }

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
            FinishInitWikiRepository();
        }

        public void FinishInitWikiRepository()
        {
            var path = GetWikiLayoutFilePath();
            WikiLayout = File.ReadAllText(path);
        }

        public void BuildWiki()
        {
            BuildWikiFolder(GetWikiRepositoryPath());
        }
        
        // This is a recursive function that crawls down the repository path and builds every .md file
        private void BuildWikiFolder(string folderPath)
        {
            // First go down the path
            foreach (string dir in Directory.GetDirectories(folderPath))
            {
                var splits = dir.Split(Path.DirectorySeparatorChar);
                if (splits.Last().StartsWith(".") || splits.Last().StartsWith("_")) continue;
                
                BuildWikiFolder(dir);
            }
            
            // Then, once we're at the end, start building!
            foreach (string file in Directory.GetFiles(folderPath))
            {
                if(!file.EndsWith(".md")) continue;
                    
                BuildWikiFile(file);
            }
        }

        private void BuildWikiFile(string inputFilePath)
        {
            string outputFilePath = inputFilePath.Replace(GetWikiRepositoryPath(), GetWikiOutputPath())
                                                 .Replace(".md", ".html");
            Directory.CreateDirectory(Path.GetDirectoryName(outputFilePath));
            
            string inputMarkdown = File.ReadAllText(inputFilePath);
            string outputHtmlText = Markdown.ToHtml(inputMarkdown);
            
            outputHtmlText = PopulateWikiLayout(inputMarkdown, outputHtmlText);
            
            File.WriteAllText(outputFilePath, outputHtmlText);
        }

        private string PopulateWikiLayout(string inputMarkdown, string outputHtml)
        {
            Dictionary<string, string> kvTable = new Dictionary<string, string>();
            
            kvTable["#Content"] = outputHtml;
            kvTable["#Title"] = GetMarkdownTitle(inputMarkdown);

            string layout = WikiLayout;
            foreach (string key in kvTable.Keys)
            {
                layout = layout.Replace(key, kvTable[key]);
            }

            return layout;
        }

        // This is a janky hack mate, but it works
        private string GetMarkdownTitle(string markdownText)
        {
            if (markdownText == null || !markdownText.Contains("# ")) return "[No Title]";
            
            int h1Index = markdownText.IndexOf("# ", StringComparison.Ordinal);
            int h1EndIndex = markdownText.IndexOf("\n", Math.Min(h1Index, markdownText.Length), StringComparison.Ordinal);
            
            //                                    Skip '# '                     
            string output = markdownText.Substring(h1Index + 2, h1EndIndex - h1Index)
                                        .Trim()
                                        .Trim('\r', '\n'); // Does Trim() already do this? I didn't test it, docs just say it removes "whitespace" but doesn't specify if that includes line breaks
            return output;
        }

        private Repository GetWikiRepository()
        {
            return new Repository(GetWikiRepositoryPath());
        }

        private string GetWikiRepositoryPath()
        {
            return Path.Join(_host.ContentRootPath, _repoDirectoryName);
        }

        public string GetWikiOutputPath()
        {
            return Path.Join(_host.ContentRootPath, _outputDirectoryName);
        }

        public string GetWikiLayoutFilePath()
        {
            return Path.Join(GetWikiRepositoryPath(), ".shared", "layout.html");
        }
    }

    public abstract class WikiException : Exception
    {
        
    }

    public class WikiRefreshException : WikiException
    {
        
    }
}
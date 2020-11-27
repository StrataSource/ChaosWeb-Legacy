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
        
        public static string WikiLayout { get; set; }
        public static List<WikiPage> WikiPages { get; set; }

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
                Refresh();
            }
            else
            {
                _logger.LogInformation("Repository doesn't exist. Cloning...");
                Repository.Clone(_repoUrl, _repoDirectoryName);
                FinishInitWikiRepository();
            }

        }
        

        public void FinishInitWikiRepository()
        {
            WikiLayout = File.ReadAllText(GetWikiLayoutFilePath());

            string[] pagePaths = Directory.GetFiles(GetWikiRepositoryPath(), "*.md", SearchOption.AllDirectories);
            WikiPages = new List<WikiPage>();
            foreach (string pagePath in pagePaths)
            {
                WikiPage page = new WikiPage(pagePath, this);
                page.ReadMarkdown();
                
                WikiPages.Add(page);
            }
            
            // Sorting the pages alphabetically, just in case
            WikiPages.Sort((left, right) => String.Compare(left.GetPageCategories().FirstOrDefault(), 
                                                           right.GetPageCategories().FirstOrDefault(), StringComparison.Ordinal));
            
            BuildWiki();
        }
        
        public void Refresh()
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

        public void BuildWiki()
        {
            Directory.Delete(GetWikiOutputPath(), true);
            foreach (WikiPage page in WikiPages)
            {
                page.Build();
            }
        }
        
        public Repository GetWikiRepository()
        {
            return new Repository(GetWikiRepositoryPath());
        }

        public string GetWikiRepositoryPath()
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

    public class WikiPage
    {
        private readonly WikiService _wikiService;
        public readonly string RawPath;
        public string MarkdownText { get; set; }
        public string HtmlText { get; set; }

        public WikiPage(string rawPath, WikiService wikiService)
        {
            _wikiService = wikiService;
            RawPath = rawPath;
        }

        public void ReadMarkdown()
        {
            MarkdownText = File.ReadAllText(RawPath);
        }

        public void Build()
        {
            Directory.CreateDirectory(Path.GetDirectoryName(GetCompiledPath()));
            PopulateHtml();
            
            File.WriteAllText(GetCompiledPath(), HtmlText);
        }

        public void PopulateHtml()
        {
            HtmlText = Markdown.ToHtml(MarkdownText);
            Dictionary<string, string> kvTable = new Dictionary<string, string>();
            
            kvTable["#Content"] = HtmlText;
            kvTable["#Title"] = GetTitle();
            kvTable["#Navigation"] = GetSidebar();
            kvTable["#Date"] = DateTime.Today.ToUniversalTime().ToShortDateString();

            string layout = WikiService.WikiLayout;
            foreach (string key in kvTable.Keys)
            {
                layout = layout.Replace(key, kvTable[key]);
            }

            HtmlText = layout;

        }

        public string[] GetPageCategories()
        {
            var splits = GetPageName().Split(Path.DirectorySeparatorChar).ToList();
            splits.RemoveAt(0);
            splits.RemoveAt(splits.Count - 1);
            return splits.ToArray();
        }

        public string GetPageName()
        {
            return RawPath.Replace(_wikiService.GetWikiRepositoryPath(), "").Replace(".md", "");
        }

        public string GetTitle()
        {
            if (MarkdownText == null || !MarkdownText.Contains("# ")) return "[No Title]";
            
            int h1Index = MarkdownText.IndexOf("# ", StringComparison.Ordinal);
            int h1EndIndex = MarkdownText.Contains("\n") ? 
                                 MarkdownText.IndexOf("\n", Math.Min(h1Index, MarkdownText.Length), StringComparison.Ordinal)
                                 : MarkdownText.Length;

            int startIndex = h1Index + 2;
            int length = Math.Clamp(h1EndIndex - h1Index, 0, MarkdownText.Length - startIndex);
            //                                    Skip '# '                     
            string output = MarkdownText.Substring(startIndex, length)
                                        .Trim()
                                        .Trim('\r', '\n'); // Does Trim() already do this? I didn't test it, docs just say it removes "whitespace" but doesn't specify if that includes line breaks
            return output;
        }
        
        public string GetCompiledPath()
        {
            return RawPath.Replace(_wikiService.GetWikiRepositoryPath(), _wikiService.GetWikiOutputPath())
                          .Replace(".md", ".html");
        }
        
        public string GetSidebar()
        {
            // Yes it is really cursed to write it this way. So?

            string output = "<ul class=\"nav flex-column nav-pills\">";
            string[] currentCategory = { };
            foreach (WikiPage page in WikiService.WikiPages)
            {
                var pageCategories = page.GetPageCategories();

                if (!currentCategory.SequenceEqual(pageCategories)) // When the category changes
                {
                    
                    output += "<ul class=\"nav flex-column nav-pills\">";
                    foreach (string category in pageCategories)
                    {
                        output += $"<p class=\"nav-category\">{category}</p>";
                    }

                    output += "</ul>";
                }
                output += $"<li class=\"nav-item\"><a class=\"nav-link {(page.GetPageName() == GetPageName() ? "active" : "")}\" href=\"/wiki{page.GetPageName()}.html\"><i class=\"fas fa-file-alt\"></i>{page.GetTitle()}</a></li>";

                currentCategory = pageCategories;
            }
            
            // After writing this, I really appreciate how clean asp.net's workflow otherwise is 
            
            return output;

        }
    }

}

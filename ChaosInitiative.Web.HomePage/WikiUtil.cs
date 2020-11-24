using System;
using System.IO;
using System.Linq;
using LibGit2Sharp;

namespace ChaosInitiative.Web.HomePage
{
    public class WikiUtil
    {

        private static readonly string REPO_DIRECTORY_NAME = "wiki_markdown";
        private static readonly Signature Signature = new Signature("p2ce website", "contact@chaosinitiative.com", DateTimeOffset.Now);
        
        public static void RefreshWikiGitRepository()
        {
            Repository repo = new Repository(GetRepositoryPath());
            var originRemote = repo.Network.Remotes["origin"];

            string logMessage = "";
            Commands.Fetch(repo, 
                           originRemote.Name, 
                           originRemote.FetchRefSpecs.Select(x => x.Specification), 
                           null, 
                           logMessage);
            
            var mergeResult = Commands.Pull(repo, Signature, null);

            if (mergeResult.Status == MergeStatus.Conflicts)
            {
                Console.WriteLine("MERGE CONFLICT WHEN PULLING WIKI. Don't ever ever ever commit to the local submodule directly");
            }
        }

        public static void BuildWiki()
        {
            
            
        }

        private static string GetRepositoryPath()
        {
            string dir = "./";
            for (int i = 0; i < 10; i++)
            {
                string wikiPath = $"{dir}/{REPO_DIRECTORY_NAME}";
                if (!Directory.Exists(wikiPath))
                {
                    dir += "../";
                    continue;
                }

                return wikiPath;
            }

            return null;
        }
    }

    public abstract class WikiException : Exception
    {
        
    }

    public class WikiRefreshException : WikiException
    {
        
    }
}
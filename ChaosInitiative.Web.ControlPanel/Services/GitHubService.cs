using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaosInitiative.Web.Shared;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Octokit;

namespace ChaosInitiative.Web.ControlPanel.Services
{
    public interface IGitHubService
    {
        string ClientId { get; }
        string ClientSecret { get; }
        string GitHubOrganisationName { get; }
        
        Task<bool> IsUserOrganizationMember(string gitHubUserName, string gitHubOrgName, string token);
        Task<bool> IsUserChaosOrganizationMember(string gitHubUserName, string token);
        Task<bool> IsValidGitHubRepository(string owner, string name);
        IEnumerable<string> GetOrgRepositories(string owner);
        GitHubClient GetClient();
        string GenerateState(int length);
    }

    public class GitHubService : IGitHubService
    {
        public readonly ProductHeaderValue ProductHeader = new ProductHeaderValue("ChaosInitiativeControlPanel");
        
        private ILogger<GitHubService> _logger;
        private GitHubServiceOptions _options;
        private GitHubClient _gitHubClient;

        public string ClientId => _options.ClientId;
        public string ClientSecret => _options.ClientSecret;
        public string GitHubOrganisationName => "ChaosInitiative";


        public GitHubService(ILogger<GitHubService> logger, IOptions<GitHubServiceOptions> options)
        {
            _logger = logger;
            _options = options.Value;
        }

        
        public async Task<bool> IsUserOrganizationMember(string gitHubUserName, string gitHubOrgName, string token)
        {
            // TODO: This currently only works on members who show their membership publicly, because github's api 
            //       for authenticated access on that endpoint is broken or whatever
            GitHubClient client = new GitHubClient(ProductHeader);
            client.Credentials = new Credentials(token);
            
            return await client.Organization.Member.CheckMember(gitHubOrgName, gitHubUserName);
        }

        public async Task<bool> IsUserChaosOrganizationMember(string gitHubUserName, string token)
        {
            return await IsUserOrganizationMember(gitHubUserName, GitHubOrganisationName, token);
        }

        public async Task<bool> IsValidGitHubRepository(string owner, string name)
        {
            GitHubClient gitHubClient = GetClient();
            try
            {
                Repository repo = await gitHubClient.Repository.Get(owner, name);
                return repo != null;
            }
            catch (ApiException)
            {
                return false;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }

        public IEnumerable<string> GetOrgRepositories(string owner)
        {
            GitHubClient gitHubClient = GetClient();
            var repos = gitHubClient.Repository.GetAllForOrg(owner).Result;
            return repos.Select(r => r.Name);
        }


        public GitHubClient GetClient()
        {
            if (_gitHubClient is null)
            {
                _logger.LogDebug("Creating new GitHubClient");
                _gitHubClient = new GitHubClient(ProductHeader);
            }
            else
            {
                _logger.LogTrace("Using existing GitHubClient");
            }

            return _gitHubClient;
        }
        
        // My cool janky af random string generation method because dotnet doesn't actually come with one..
        public string GenerateState(int length)
        {
            List<char> chars = new List<char>();

            for (int i = 65; i < 90; i++) // A-Z
            {
                chars.Add((char) i);
            }
            
            for (int i = 97; i < 122; i++) // a-z
            {
                chars.Add((char) i);
            }
            
            for (int i = 48; i < 57; i++) // A-Z
            {
                chars.Add((char) i);
            }
            
            chars.Add('+');

            string output = "";
            Random rng = new Random();
            for (int i = 0; i < length; i++)
            {
                output += chars[rng.Next(0, chars.Count)];
            }

            return output;
        }
    }
}

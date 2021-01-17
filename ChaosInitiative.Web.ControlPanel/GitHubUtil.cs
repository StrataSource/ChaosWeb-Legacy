using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChaosInitiative.Web.Shared;
using Octokit;

namespace ChaosInitiative.Web.ControlPanel
{
    public class GitHubUtil
    {

        private static DeploymentType _deploymentType;
        
        public static void Init(DeploymentType deploymentType)
        {
            _deploymentType = deploymentType;
            
            Console.WriteLine($"Starting deployment type {deploymentType.ToString()}");
            
            ApplicationClientId = Secrets.Get("oauthClientId", _deploymentType);
            ApplicationClientSecret = Secrets.Get("oauthClientSecret", _deploymentType);
            
            Console.WriteLine("GitHub Application Credentials");
            Console.WriteLine($"Client ID: {ApplicationClientId}");
            Console.WriteLine($"Client Secret: {ApplicationClientSecret}");
        }

        public static readonly ProductHeaderValue ProductHeader = new ProductHeaderValue("ChaosInitiativeControlPanel");
        
        public static async Task<bool> IsUserOrganizationMember(string gitHubUserName, string gitHubOrgName, string token)
        {
            // TODO: This currently only works on members who show their membership publicly, because github's api 
            //       for authenticated access on that endpoint is broken or whatever
            GitHubClient client = new GitHubClient(ProductHeader);
            client.Credentials = new Credentials(token);
            
            return await client.Organization.Member.CheckMember(gitHubOrgName, gitHubUserName);
        }

        public static async Task<bool> IsUserChaosOrganizationMember(string gitHubUserName, string token)
        {
            return await IsUserOrganizationMember(gitHubUserName, GITHUB_ORG_NAME, token);
        }

        public static async Task<bool> IsValidGitHubRepository(string owner, string name)
        {
            GitHubClient gitHubClient = CreateClient();
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

        public static IEnumerable<string> GetOrgRepositories(string owner)
        {
            GitHubClient gitHubClient = CreateClient();
            var repos = gitHubClient.Repository.GetAllForOrg(owner).Result;
            return repos.Select(r => r.Name);
        }

        private static string _applicationClientId;
        public static string ApplicationClientId
        {
            get => _applicationClientId;
            set
            {
                if (_applicationClientId == null) _applicationClientId = value;
            }
        }
        
        private static string _applicationClientSecret;
        public static string ApplicationClientSecret
        {
            get => _applicationClientSecret;
            set
            {
                if (_applicationClientSecret == null) _applicationClientSecret = value;
            }
        }

        public static GitHubClient CreateClient()
        {
            return new GitHubClient(ProductHeader);
        }

        public static readonly string GITHUB_ORG_NAME = "ChaosInitiative";

        // My cool janky af random string generation method because dotnet doesn't actually come with one..
        public static string GenerateState(int length)
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

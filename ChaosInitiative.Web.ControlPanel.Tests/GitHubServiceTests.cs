using System.Collections.Generic;
using System.Threading.Tasks;
using ChaosInitiative.Web.ControlPanel.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Microsoft.Extensions.Options;
using NUnit.Framework;

namespace ChaosInitiative.Web.ControlPanel.Tests
{
    public class GitHubServiceTests
    {
        private GitHubService _gitHubService;
        
        [OneTimeSetUp]
        public void Setup()
        {
            // Logging 
            var loggerFactory = LoggerFactory.Create(builder => builder.AddSimpleConsole(formatterOptions =>
            {
                formatterOptions.ColorBehavior = LoggerColorBehavior.Enabled;
                formatterOptions.SingleLine = true;
            }));

            ILogger<GitHubService> logger = loggerFactory.CreateLogger<GitHubService>();
            
            // User Secrets

            var config = new ConfigurationBuilder().AddUserSecrets<Program>().Build();

            // GitHub

            ConfigureOptions<GitHubServiceOptions> configureOptions = new ConfigureOptions<GitHubServiceOptions>(gitHubOptions =>
            {
                gitHubOptions.ClientId = config["GitHub:ClientId"];
                gitHubOptions.ClientSecret = config["GitHub:ClientSecret"];
            });
            
            OptionsFactory<GitHubServiceOptions> factory = new OptionsFactory<GitHubServiceOptions>(new []{ configureOptions }, new List<IPostConfigureOptions<GitHubServiceOptions>>(), new List<IValidateOptions<GitHubServiceOptions>>());
            
            IOptions<GitHubServiceOptions> options = new OptionsManager<GitHubServiceOptions>(factory);

            _gitHubService = new GitHubService(logger, options);
        }

        [Test]
        public void EnsureStateStringGeneratorOutput()
        {
            string state = _gitHubService.GenerateState(0);
            
            Assert.That(state, Is.Not.Null);
            Assert.That(state, Is.EqualTo(""));

            state = _gitHubService.GenerateState(30);
            
            Assert.That(state, Is.Not.Null);
            Assert.That(state, Has.Length.EqualTo(30));
        }
        
        [Test]
        public async Task TestIsValidGitHubRepository()
        {
            Assert.That(await _gitHubService.IsValidGitHubRepository("aaaaaaa", ""), Is.False);
            Assert.That(await _gitHubService.IsValidGitHubRepository("ChaosInitiative", ""), Is.False);
            Assert.That(await _gitHubService.IsValidGitHubRepository("ChaosInitiative", "Portal-2-Community-Edition"), Is.True);
            Assert.That(await _gitHubService.IsValidGitHubRepository("SpyceTewan", "portal2-workshop-vmf"), Is.True);
            Assert.That(await _gitHubService.IsValidGitHubRepository("SpyceTewan", "Portal-Revolution"), Is.False); // This repo is private and archived, so it should return false
        }

        [Test]
        public void TestGetOrgRepositories()
        {
            var repos = _gitHubService.GetOrgRepositories("ChaosInitiative");
            Assert.That(repos, Is.Not.Empty);
        }
    }
}
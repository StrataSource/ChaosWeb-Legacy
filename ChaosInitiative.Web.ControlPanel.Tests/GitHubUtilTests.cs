using System.Threading.Tasks;
using NUnit.Framework;

namespace ChaosInitiative.Web.ControlPanel.Tests
{
    public class GitHubUtilTests
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void EnsureStateStringGeneratorOutput()
        {
            string state = GitHubUtil.GenerateState(0);
            
            Assert.That(state, Is.Not.Null);
            Assert.That(state, Is.EqualTo(""));

            state = GitHubUtil.GenerateState(30);
            
            Assert.That(state, Is.Not.Null);
            Assert.That(state, Has.Length.EqualTo(30));
        }
        
        [Test]
        public async Task TestIsValidGitHubRepository()
        {
            Assert.That(await GitHubUtil.IsValidGitHubRepository("aaaaaaa", ""), Is.False);
            Assert.That(await GitHubUtil.IsValidGitHubRepository("ChaosInitiative", ""), Is.False);
            Assert.That(await GitHubUtil.IsValidGitHubRepository("ChaosInitiative", "Portal-2-Community-Edition"), Is.True);
            Assert.That(await GitHubUtil.IsValidGitHubRepository("SpyceTewan", "portal2-workshop-vmf"), Is.True);
            Assert.That(await GitHubUtil.IsValidGitHubRepository("SpyceTewan", "Portal-Revolution"), Is.False); // This repo is private and archived, so it should return false
        }
    }
}
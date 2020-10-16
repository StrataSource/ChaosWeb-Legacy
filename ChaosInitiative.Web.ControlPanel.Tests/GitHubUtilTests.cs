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
    }
}
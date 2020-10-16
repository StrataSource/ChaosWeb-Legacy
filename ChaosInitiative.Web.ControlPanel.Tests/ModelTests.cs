using ChaosInitiative.Web.ControlPanel.Model;
using NUnit.Framework;

namespace ChaosInitiative.Web.ControlPanel.Tests
{
    [TestFixture]
    public class ModelTests
    {

        [Test]
        public void TestIssuePathConcatenatesCorrectly()
        {
            Game game = new Game
            {
                Name = "Portal 2: Community Edition",
                Repository = "https://github.com/ChaosInitiative/Portal-2-Community-Edition/" // <== This trailing / should be removed by the setter
            };

            Issue issue = new Issue
            {
                Game = game, 
                IssueId = 40
            };

            string expectedPath = "https://github.com/ChaosInitiative/Portal-2-Community-Edition/issues/40";
            string actualPath = issue.GetFullPath();

            Assert.That(actualPath, Is.EqualTo(expectedPath));
        }
        
    }
}
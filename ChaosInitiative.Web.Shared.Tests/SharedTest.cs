using NUnit.Framework;

namespace ChaosInitiative.Web.Shared.Tests
{
    public class SecretsTests
    {
        [Test]
        public void TestIfSecretsConfigCanBeLoaded()
        {
            Assert.DoesNotThrow(() =>
            {
                string result = Secrets.Get("", DeploymentType.Development);
                
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Length.EqualTo(0));
            });
        }
    }
}
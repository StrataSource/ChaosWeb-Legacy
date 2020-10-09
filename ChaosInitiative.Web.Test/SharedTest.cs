using System;
using ChaosInitiative.Web.Shared;
using NUnit.Framework;

namespace ChaosInitiative.Web.Test
{
    public class SharedTests
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
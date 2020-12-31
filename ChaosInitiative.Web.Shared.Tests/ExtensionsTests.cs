using System.Collections.Generic;
using NUnit.Framework;

namespace ChaosInitiative.Web.Shared.Tests
{
    [TestFixture]
    public class ExtensionsTests
    {
        
        [Test]
        public void TestIEnumerableIsEmpty()
        {
            IEnumerable<string> emptyCollection = new List<string>();
            IEnumerable<string> filledCollection = new[] { "I am full" };
            
            Assert.That(emptyCollection.IsEmpty(), Is.True);
            Assert.That(filledCollection.IsEmpty(), Is.False);
            
        }
    }
}
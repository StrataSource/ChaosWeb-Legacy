using System.Collections.Generic;
using NUnit.Framework;

namespace ChaosInitiative.Web.Shared.Tests
{
    [TestFixture]
    public class ExtensionsTests
    {
        [Test]
        public void TestIEnumerableOverlapping()
        {
            IEnumerable<string> col1Strings = new[] {"hi", "bye", "ciao", "thanks"};
            IEnumerable<string> col2Strings = new[] {"cya", "heyo", "ciao", "welcome", "hi"};
            IEnumerable<int> col3Ints = new[] {1, 2, 3, 4, 5, 6};
            IEnumerable<int> col4Ints = new[] {9, 8, 7, 6, 5, 4};

            IEnumerable<string> resultStrings = col1Strings.GetOverlapping(col2Strings);
            IEnumerable<string> resultStringsInverse = col2Strings.GetOverlapping(col1Strings);
            IEnumerable<int> resultInts = col3Ints.GetOverlapping(col4Ints);
            IEnumerable<int> resultIntsInverse = col4Ints.GetOverlapping(col3Ints);
            
            Assert.That(resultStrings, Contains.Item("ciao").
                                                And.Contains("hi").
                                                And.Not.Contains("cya").
                                                And.Not.Contains("thanks").
                                                And.Not.Contains("heyo").
                                                And.Not.Contains("bye"));
            
            Assert.That(resultStrings, Is.EquivalentTo(resultStringsInverse));
            
            Assert.That(resultInts, Contains.Item(4).
                                             And.Contains(5).
                                             And.Contains(6));
            
            Assert.That(resultInts, Is.EquivalentTo(resultIntsInverse));
            
        }

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
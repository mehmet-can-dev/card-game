using CardGame.Core.Sort.Forward;
using CardGame.Core.Sort.Recursive;
using CardGame.Core.Test.Testables;
using NUnit.Framework;
using UnityEngine;

namespace CardGame.Core.Test
{
    public class ManualSortTest
    {
        [Test]
        public void ExampleTest()
        {
            var test = new ExampleDocCards();
            Debug.Log("Smart Test");

            AutomaticSortTest.SmartTest(test);

            Debug.Log("Colored Test");

            AutomaticSortTest.ColoredTest(test);

            Debug.Log("Numeric Test");
            AutomaticSortTest.NumericTest(test);
        }
    }
}
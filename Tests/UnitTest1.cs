using Microsoft.VisualStudio.TestTools.UnitTesting;
using NJ05_T1_TimeslotEnumerator;
using System.Collections.Generic;

namespace Tests
{
    [TestClass]
    public class TimeSlotCollectionTest
    {
        [TestMethod]
        public void SingleItemTest()
        {
            var enumeratorData = new string[] { "item1", "item2" };

            var results = GetElementsFromEnumerator(1, 1, enumeratorData);

            Assert.AreEqual(1, results.Count);
            Assert.AreEqual("item2", results[0]);
        }

        [TestMethod]
        public void LeftBoundariesTest()
        {
            var enumeratorData = new string[] { "item1", "item2", "item3" };
            var expectedResults = new string[] { "item1", "item2", "item3" };

            var results = GetElementsFromEnumerator(3, 0, enumeratorData);

            Assert.AreEqual(3, results.Count);

            for (int i = 0; i < enumeratorData.Length; i++)
            {
                Assert.AreEqual(expectedResults[i], enumeratorData[i]);
            }
        }

        private List<string> GetElementsFromEnumerator(int howMany, int startIndex, string[] enumeratorList)
        {
            var enumerator = new TimeslotEnumerator(enumeratorList, startIndex);
            var list = new List<string>();

            int elementsRendered = 0;
            while (enumerator.MoveNext())
            {
                list.Add((string)enumerator.Current);
                elementsRendered++;
                if (elementsRendered == howMany) break;
            }

            return list;
        }

        [TestMethod]
        public void TestExample1()
        {
            var expectedResult = new string[] { "12:00", "11:45", "12:15", "11:30", "12:30" };

            var enumeratorData = new string[] { "11:00", "11:15", "11:30", "11:45", "12:00", "12:15", "12:30", "12:45", "13:00", "13:15", "13:30", "13:45" };

            var results = GetElementsFromEnumerator(5, 4, enumeratorData);

            Assert.AreEqual(expectedResult.Length, results.Count);

            int i = 0;
            foreach (var item in results)
            {
                Assert.AreEqual(expectedResult[i], item);
                i++;
            }
        }

        [TestMethod]
        public void TestExample2()
        {
            var expectedResult = new string[] { "11:30", "11:15", "11:45", "11:00", "10:45", "10:30" };

            var enumeratorData = new string[] { "09:00", "09:15", "09:30", "09:45", "10:00", "10:15", "10:30", "10:45", "11:00", "11:15", "11:30", "11:45" };

            var results = GetElementsFromEnumerator(6, 10, enumeratorData);

            Assert.AreEqual(expectedResult.Length, results.Count);

            int i = 0;
            foreach (var item in results)
            {
                Assert.AreEqual(expectedResult[i], item);
                i++;
            }
        }

        [TestMethod]
        public void TestExample3()
        {
            var expectedResult = new string[] { "17:00", "17:15", "17:30", "17:45" };

            var enumeratorData = new string[] { "17:00", "17:15", "17:30", "17:45", "18:00", "18:15", "18:30", "18:45", "19:00", "19:15", "19:30", "19:45" };

            var results = GetElementsFromEnumerator(4, 0, enumeratorData);

            Assert.AreEqual(expectedResult.Length, results.Count);

            int i = 0;
            foreach (var item in results)
            {
                Assert.AreEqual(expectedResult[i], item);
                i++;
            }
        }
    }
}

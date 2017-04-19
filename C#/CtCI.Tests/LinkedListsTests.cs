using Microsoft.VisualStudio.TestTools.UnitTesting;

using static CtCI.LinkedLists;

namespace CtCI.Tests
{
    [TestClass]
    public class LinkedListsTests
    {
        [TestMethod]
        public void RemoveDupsTests()
        {
            var n = new Node { Label = "F" };
            n.Append(new Node { Label = "F" });
            n.Append(new Node { Label = "C" });
            n.Append(new Node { Label = "A" });
            n.Append(new Node { Label = "C" });

            RemoveDups(n);

            Assert.AreEqual("{ F, C, A, }", n.ToString());
        }

        [TestMethod]
        public void RemoveDupsNoAuxiliaryTests()
        {
            var n = new Node { Label = "F" };
            n.Append(new Node { Label = "F" });
            n.Append(new Node { Label = "C" });
            n.Append(new Node { Label = "A" });
            n.Append(new Node { Label = "C" });

            RemoveDupsNoAuxiliaryStructure(n);

            Assert.AreEqual("{ F, C, A, }", n.ToString());
        }

        [TestMethod]
        public void KthToLastTests()
        {
            var n = new Node { Label = "1" };
            n.Append(new Node { Label = "2" });
            n.Append(new Node { Label = "3" });
            n.Append(new Node { Label = "4" });
            n.Append(new Node { Label = "5" });

            Assert.AreEqual("4", KthToLast(n, 2));
        }

        [TestMethod]
        public void PartitionTests()
        {
            var n = new Node { Label = "5", Value = 5 };
            n.Append(new Node { Label = "6", Value = 6 });
            n.Append(new Node { Label = "7", Value = 7 });
            n.Append(new Node { Label = "1", Value = 1 });
            n.Append(new Node { Label = "2", Value = 2 });

            Assert.AreEqual("{ 1, 2, 5, 6, 7, }", Partition(n, 3).ToString());
        }

        [TestMethod]
        public void GetIntesectingNodeTests()
        {
            var n = new Node { Label = "F" };
            n.Append(new Node { Label = "F" });
            n.Append(new Node { Label = "C" });
            n.Append(new Node { Label = "A" });
            n.Append(new Node { Label = "C" });

            var l1 = new Node { Label = "Z" };
            l1.Append(n);

            var l2 = new Node { Label = "Y" };
            l2.Append(new Node() { Label = "X" });
            l2.Append(new Node() { Label = "K" });
            l2.Append(n);

            Assert.AreEqual(n, GetIntersectingNode(l1, l2));
        }

        [TestMethod]
        public void SumTests()
        {
            var l1 = new Node { Value = 1 };
            l1.Append(new Node { Value = 2 });
            l1.Append(new Node { Value = 3 });
            l1.Append(new Node { Value = 0 });
            l1.Append(new Node {Value =  4 });

            var l2 = new Node { Value = 7 };
            l2.Append(new Node { Value = 8 });
            l2.Append(new Node { Value = 9 });

            Assert.AreEqual(987 + 40321, Sum(l1, l2));
        }

        [TestMethod]
        public void SumBigEndianTests()
        {
            var l1 = new Node { Value = 1 };
            l1.Append(new Node { Value = 2 });
            l1.Append(new Node { Value = 3 });
            l1.Append(new Node { Value = 0 });
            l1.Append(new Node { Value = 4 });

            var l2 = new Node { Value = 7 };
            l2.Append(new Node { Value = 8 });
            l2.Append(new Node { Value = 9 });

            Assert.AreEqual(12304 + 789, SumBigEndian(l1, l2));
        }

        [TestMethod]
        public void IsPalindromeTests()
        {
            var l1 = new Node { Value = 1 };
            l1.Append(new Node { Value = 2 });
            l1.Append(new Node { Value = 3 });
            l1.Append(new Node { Value = 2 });
            l1.Append(new Node { Value = 1 });

            Assert.IsTrue(IsPalindrome(l1));

            var l2 = new Node { Value = 1 };
            l2.Append(new Node { Value = 2 });
            l2.Append(new Node { Value = 2 });
            l2.Append(new Node { Value = 1 });

            Assert.IsTrue(IsPalindrome(l2));

            var l3 = new Node { Value = 1 };
            l3.Append(new Node { Value = 2 });
            l3.Append(new Node { Value = 2 });

            Assert.IsFalse(IsPalindrome(l3));
        }

        [TestMethod]
        public void GetEntryPointOfCorruptedListTest()
        {
            var n = new Node();
            n.Append(new Node());
            n.Append(new Node());
            n.Append(new Node());
            n.Append(n);

            var list = new Node();
            list.Append(new Node());
            list.Append(n);

            Assert.AreEqual(n, GetEntryPointOfCorruptedList(list));
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;

using static CtCI.ArraysAndStrings;

namespace CtCI.Tests
{
    [TestClass]
    public class ArraysAndStringsTests
    {
        [TestMethod]
        public void IsUniqueTests()
        {
            Assert.IsTrue(IsUnique("abcdef"));
            Assert.IsFalse(IsUnique("abcdefaghijk"));
        }

        [TestMethod]
        public void IsUniqueNoStructuresTests()
        {
            Assert.IsTrue(IsUniqueNoAdditionalDataStructures("abcdef"));
            Assert.IsFalse(IsUniqueNoAdditionalDataStructures("abcdefaghijk"));
        }

        [TestMethod]
        public void IsPermutationTests()
        {
            Assert.IsTrue(IsPermutation("abcde", "ebdca"));
            Assert.IsFalse(IsPermutation("abcde", "fbcaa"));
        }
    }
}

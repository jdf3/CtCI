using Microsoft.VisualStudio.TestTools.UnitTesting;

using static CtCI.BitManipulation;

namespace CtCI.Tests
{
    [TestClass]
    public class BitManipulationTests
    {
        [TestMethod]
        public void FitIntoTests()
        {
            Assert.AreEqual((uint)0b10001001100, FitInto(0b10000000000, 0b10011, 2, 6));
        }

        [TestMethod]
        public void GetBinaryRepresentationTests()
        {
            Assert.AreEqual("ERROR", GetBinaryRepresentation(0.3));
            Assert.AreEqual("0.1", GetBinaryRepresentation(0.5));
            Assert.AreEqual("0.0101", GetBinaryRepresentation(0.25 + 0.0625));
        }
    }
}

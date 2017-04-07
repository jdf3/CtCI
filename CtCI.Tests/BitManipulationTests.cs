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
            Assert.AreEqual("0.0101", GetBinaryRepresentation(0.3125));
        }

        [TestMethod]
        public void GetLongestSequenceAfterFlipTests()
        {
            Assert.AreEqual(32, GetLongestSequenceAfterFlip(0b1111_1111_1111_1111_1111_1111_1111_1111));
            Assert.AreEqual(32, GetLongestSequenceAfterFlip(0b1111_1111_1111_1110_1111_1111_1111_1111));
            Assert.AreEqual(32, GetLongestSequenceAfterFlip(0b0111_1111_1111_1111_1111_1111_1111_1111));
            Assert.AreEqual(32, GetLongestSequenceAfterFlip(0b1111_1111_1111_1111_1111_1111_1111_1110));
            Assert.AreEqual(31, GetLongestSequenceAfterFlip(0b0111_1111_1111_1111_1111_1111_1111_1110));
            Assert.AreEqual(25, GetLongestSequenceAfterFlip(0b0000_1111_1111_1111_1111_1111_1111_0000));
            Assert.AreEqual(24, GetLongestSequenceAfterFlip(0b0000_1111_1111_1110_1111_1111_1111_0000));
        }

        [TestMethod]
        public void GetNextWithSameBitsTests()
        {
            Assert.AreEqual((uint)0b101000, GetNextWithSameBits((uint)0b100100), "case 1");
            Assert.AreEqual((uint)0b110001, GetNextWithSameBits((uint)0b101100), "case 2");
            Assert.AreEqual((uint)0b1111_1111_1111_1111_1011_1111_1111_1111, GetNextWithSameBits((uint)0b1111_1111_1111_1111_0111_1111_1111_1111), "case 3");
        }

        [TestMethod]
        public void CountBitsTests()
        {
            Assert.AreEqual(24, CountBits(0b1111_1111_0000_1111_0000_1111_1111_1111));
        }

        [TestMethod]
        public void BitFlipsUntilEqualityTests()
        {
            Assert.AreEqual(2, BitFlipsUntilEquality((uint)29, (uint)15));
        }
    }
}

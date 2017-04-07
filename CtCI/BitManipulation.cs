using System;
using System.Runtime.InteropServices;
using System.Text;

namespace CtCI
{
    public class BitManipulation
    {
        #region 5.1
        /* You are given two 32-bit numbers, N and M, and two bit positions, i and j.
         * Write a method to insert M into N such that M starts at bit j and ends at bit i.
         * You can assume that the bits j through i have enough space to fit all of M.
         * That is, if M = 10011, you can assume that there are at least 5 bits between j and i.
         * You would not, for example, have j = 3 and i = 2, because M could not fully fit between bit 3 and bit 2. */
        public static uint FitInto(uint n, uint m, int i, int j)
        {
            uint mask = (uint)(1 << j) - 1;
            m &= mask;
            m <<= i;
            n |= m;
            return n;
        }
        #endregion

        #region 5.2
        /* Given a real number between 0 and 1 (e.g., 0.72) that is passed in as a double, print the binary representation.
         * If the number cannot be represented accurately in binary with at most 32 characters, print "ERROR". */
        // TODO: look at solutions/hints - i didn't exactly use any bit manipulation here...
        public static string GetBinaryRepresentation(double number)
        {
            if (number <= 0 || number >= 1) return "ERROR";

            StringBuilder stringRep = new StringBuilder("0.");

            while (number != 0 && stringRep.Length < 32)
            {
                if (number >= 0.5)
                {
                    number -= 0.5;
                    stringRep.Append("1");
                }
                else
                {
                    stringRep.Append("0");
                }
                number *= 2;
            }
            if (stringRep.Length == 32 && number != 0) return "ERROR";
            return stringRep.ToString();
        }
        #endregion

        #region 5.3
        /* You have an integer and you can flip exactly one bit from a 0 to a 1.
         * Write code to find the length of the longest sequence of 1s you could create. */
        public static int GetLongestSequenceAfterFlip(uint number)
        {
            int best = 1;

            int startPos = 31;

            while (startPos >= 0)
            {
                while (((number >> startPos) & 1) != 1 && startPos >= 0)
                {
                    startPos--;
                }
                if (startPos <= 0) return best;

                int firstZeroPos = startPos - 1;

                while (((number >> firstZeroPos) & 1) != 0 && firstZeroPos >= 0)
                {
                    firstZeroPos--;
                }
                if (firstZeroPos < 0)
                {
                    return startPos == 31 ? Math.Max(best, startPos + 1) : Math.Max(best, startPos + 2);
                }

                int endPos = firstZeroPos - 1;

                while (((number >> endPos) & 1) != 0 && endPos >= 0)
                {
                    endPos--;
                }
                best = Math.Max(best, startPos - endPos);
                startPos = endPos;
            }

            return best;
        }
        #endregion

        #region 5.4
        /* Given a positive integer, print the next smallest and the next largest number
         * that have the same number of 1 bits in their binary representation */
        // I'll just do the next, since the previous is similar
        public static uint GetNextWithSameBits(uint number)
        {
            int seqStart = 0;
            while (((number >> seqStart) & 1) == 0)
            {
                seqStart++;
            }

            int pos = seqStart;
            while (!(((number >> pos) & 1) == 1 && ((number >> (pos + 1)) & 1) == 0))
            {
                pos++;
            }

            number += (uint)(1 << (pos + 1)) - (uint)(1 << pos);

            uint bitsToShift = ((uint)(1 << pos) - 1) & number;

            number -= bitsToShift;
            uint shiftedBits = bitsToShift >> seqStart;

            uint mask = ~(uint)0 - (uint)(1 << pos) - 1;
            number &= mask;
            number += shiftedBits;

            return number;
        }

        public static int CountBits(uint number)
        {
            int bits = 0;

            while (number != 0)
            {
                bits += (int)(number & 1);
                number >>= 1;
            }

            return bits;
        }
        #endregion

        #region 5.6
        /* Write a function to determine the number of bits you would need to flip to convert integer A to integer B */
        public static int BitFlipsUntilEquality(uint a, uint b)
        {
            int flips = 0;

            uint xor = a ^ b;
            while (xor != 0)
            {
                flips++;
                xor &= xor - 1;
            }
            return flips;
        }
        #endregion
    }
}

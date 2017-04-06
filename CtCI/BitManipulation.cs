using System;
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
            uint mask = (uint) (1 << j) - 1;
            m &= mask;
            m <<= i;
            n |= m;
            return n;
        }
        #endregion

        #region 5.2
        /* Given a real number between 0 and 1 (e.g., 0.72) that is passed in as a double, print the binary representation.
         * If the number cannot be represented accurately in binary with at most 32 characters, print "ERROR". */
        // TODO: look at solutions/hints - i didn't exactly use any bit manipulation here..
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
            }
            if (stringRep.Length == 32 && number != 0) return "ERROR";
            return stringRep.ToString();
        }
        #endregion
    }
}

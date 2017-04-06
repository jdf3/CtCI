using System.Collections.Generic;

namespace CtCI
{
    public static class ArraysAndStrings
    {
        #region 1.1
        // Implement an algorithm to determine if a string has all unique characters.
        // O(N) space, O(N) time
        public static bool IsUnique(string input)
        {
            var characters = new HashSet<char>();
            foreach (char c in input)
            {
                if (characters.Contains(c)) return false;
                else characters.Add(c);
            }
            return true;
        }

        // What if you can't use any additional data structures?
        // This becomes O(N^2)
        public static bool IsUniqueNoAdditionalDataStructures(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                char head = input[i];
                string tail = input.Substring(i + 1);
                if (StringContains(tail, head))
                {
                    return false;
                }
            }
            return true;

            bool StringContains(string haystack, char needle)
            {
                foreach (char c in haystack)
                {
                    if (needle == c) return true;
                }
                return false;
            }
        }
        #endregion

        #region 1.2
        // Given two strings, write a method to decide if one is a permutation of the other
        public static bool IsPermutation(string left, string right)
        {
            Dictionary<char, int> leftChars = BuildFrequencyMap(left);
            Dictionary<char, int> rightChars = BuildFrequencyMap(right);

            var keys = new List<char>();
            keys.AddRange(leftChars.Keys);
            keys.AddRange(rightChars.Keys);

            foreach (char key in keys)
            {
                if (leftChars.TryGetValue(key, out int leftCount)
                    && rightChars.TryGetValue(key, out int rightCount)
                    && leftCount == rightCount)
                {
                    continue;
                }
                return false;
            }
            return true;

            Dictionary<char, int> BuildFrequencyMap(string str)
            {
                var frequencyMap = new Dictionary<char, int>();
                foreach (char c in str)
                {
                    if (frequencyMap.ContainsKey(c))
                    {
                        frequencyMap[c] += 1;
                    }
                    else
                    {
                        frequencyMap[c] = 1;
                    }
                }
                return frequencyMap;
            }
        }
        #endregion

        #region 1.3
        // Keep an array of ints that represent how far the character should be "shifted".
        // Then, iterate from the right of the array backing the string, and "pull" characters as needed to the right.
        // If you're "pulling" a space, add '%20' instead. This creates an O(N) space and O(N) time algorithm.
        #endregion

        #region 1.4
        // Create hashmaps/dictionaries that map a character's occurrence to the frequency for which it occurs.
        // A palindrome can be created if and only if only one frequency is odd, so go through the key-value pairs at the end and make sure that's the case.
        // This again creates an O(N) time and space algorithm.
        // O(N) is the information-theoretic lower-bound for run-time, the question remains if we can perform better in space.
        #endregion

        #region 1.5
        // Naive solution is to just try everything - try to put each character in each position, or replace each character with any other.
        // Getting smarter, there's a lot of symmetry to this problem that boils it down to an O(N) time and O(1) space problem (assuming we are free to mutate the strings provided as input):
        // -- if the lengths are the same, then the strings must either be the same, or we must replace a character.
        //    -- start from the left side of the string, and go through until we find that the characters differ.
        //       replace that character so that the characters now match. now it all boils down to whether the strings are equal.
        // -- if the lenghts are different, then one string must be exactly one character larger than the other, or it can't be true.
        //    in this case, we must remove exactly one character from the larger - we can check that quickly!
        //    -- start with the larger string, and go through until you find a character that doesn't "line up" with the smaller string.
        //       remove that character - it'll all boil down to whether the strings are equal at that point.
        #endregion

        #region 1.6
        // A bit too straightforward to merit spending time on, but could be a worthy whiteboard session
        #endregion

        #region 1.7
        // Making a copy is simple, doing in-place is possible by the implementation of a "swap4" method performed on only the top-left quadrant of the image, using only a single temp variable for each pixel.
        // Maintaining the data structures and making sure that the odd N case is handled would be the challenge here.
        // N = n - 1
        // (x, y)         -> y, N - x
        // (y, N - x)     -> N - x, N - y
        // (N - x, N - y) -> N - y, x
        // (N - y, x)     -> x, y
        #endregion

        #region 1.8

        #endregion

        #region 1.9

        #endregion
    }
}

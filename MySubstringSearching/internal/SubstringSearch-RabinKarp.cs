using System.Collections.Generic;

namespace MySubstringSearching
{
    internal class SubstringSearch_RabinKarp : ISubstringSearch
    {
        private SubstringSearch_RabinKarp() { }
        private static SubstringSearch_RabinKarp ThisClass;
        public static SubstringSearch_RabinKarp Creating()
        {
            if (ThisClass == null) ThisClass = new SubstringSearch_RabinKarp();
            return ThisClass;
        }

        private uint CalculateHash(string s)
        {
            uint hash = 0;
            foreach (char c in s)
            {
                hash = ((hash) << 8) + (uint)c;
            }
            return hash;
        }
        private uint RecalculateHash(uint oldHash, char oldChar, char newChar, uint step)
        {
            uint newHash = (((oldHash - (uint)oldChar * step) << 8) + (uint)newChar);
            return newHash;
        }
        public List<int> SearchIndexesSubstring(string StringForSearching, string pattern)
        {
            List<int> indexes = new List<int>();

            uint patternHash = CalculateHash(pattern);
            uint textHash = CalculateHash(StringForSearching.Substring(0, pattern.Length));

            uint step = 1;
            for (int i = 0; i < pattern.Length - 1; i++)
            {
                step = step << 8;
            }

            if (patternHash == textHash && StringForSearching.Substring(0, pattern.Length) == pattern)
            {                
                indexes.Add(0);
            }
            for (int i = 1; i <= StringForSearching.Length - pattern.Length; i++)
            {                
                if (i <= StringForSearching.Length - pattern.Length)
                {
                    textHash = RecalculateHash(textHash, StringForSearching[i - 1], StringForSearching[i + pattern.Length - 1], step);
                }                
                if (patternHash == textHash && StringForSearching.Substring(i, pattern.Length) == pattern)
                {
                    indexes.Add(i);
                }
            }
            return indexes;
        }

        public bool SearchAvailabSubstring(string StringForSearching, string pattern)
        {
            uint patternHash = CalculateHash(pattern);
            uint textHash = CalculateHash(StringForSearching.Substring(0, pattern.Length));

            uint step = 1;
            for (int i = 0; i < pattern.Length - 1; i++)
            {
                step = step << 8;
            }

            if (patternHash == textHash && StringForSearching.Substring(0, pattern.Length) == pattern)
            {
                return true;
            }
            for (int i = 1; i <= StringForSearching.Length - pattern.Length; i++)
            {
                if (i <= StringForSearching.Length - pattern.Length)
                {
                    textHash = RecalculateHash(textHash, StringForSearching[i - 1], StringForSearching[i + pattern.Length - 1], step);
                }
                if (patternHash == textHash && StringForSearching.Substring(i, pattern.Length) == pattern)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

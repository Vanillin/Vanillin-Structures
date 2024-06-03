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
        private uint step;
        public List<int> SearchSubstring(string text, string pattern)
        {
            List<int> indexes = new List<int>();

            uint patternHash = CalculateHash(pattern);
            uint textHash = CalculateHash(text.Substring(0, pattern.Length));

            step = 1;
            for (int i = 0; i < pattern.Length - 1; i++)
            {
                step = step << 8;
            }

            if (patternHash == textHash && text.Substring(0, pattern.Length) == pattern)
            {                
                indexes.Add(0);
            }
            for (int i = 1; i <= text.Length - pattern.Length; i++)
            {                
                if (i <= text.Length - pattern.Length)
                {
                    textHash = RecalculateHash(textHash, text[i - 1], text[i + pattern.Length - 1], pattern.Length);
                }                
                if (patternHash == textHash && text.Substring(i, pattern.Length) == pattern)
                {
                    indexes.Add(i);
                }
            }
            return indexes;
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
        private uint RecalculateHash(uint oldHash, char oldChar, char newChar, int patternLength)
        {
            uint newHash = (((oldHash - (uint)oldChar * step) << 8) + (uint)newChar);

            return newHash;
        }
    }
}

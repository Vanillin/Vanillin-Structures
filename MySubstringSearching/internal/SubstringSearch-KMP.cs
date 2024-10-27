using System.Collections.Generic;

namespace MySubstringSearching
{
    internal class SubstringSearch_KMP : ISubstringSearch
    {
        private SubstringSearch_KMP() { }
        private static SubstringSearch_KMP ThisClass;
        public static SubstringSearch_KMP Creating()
        {
            if (ThisClass == null) ThisClass = new SubstringSearch_KMP();
            return ThisClass;
        }

        private int[] CalculatePrefixFunction(string pattern)
        {
            int[] prefixFunction = new int[pattern.Length];
            int j = 0;
            for (int i = 1; i < pattern.Length; i++)
            {
                while (j > 0 && pattern[i] != pattern[j])
                {
                    j = prefixFunction[j - 1];
                }
                if (pattern[i] == pattern[j])
                {
                    j++;
                }
                prefixFunction[i] = j;
            }
            return prefixFunction;
        }
        public List<int> SearchIndexesSubstring(string StringForSearching, string pattern)
        {
            List<int> indexes = new List<int>();
            int[] prefixFunction = CalculatePrefixFunction(pattern);

            int j = 0;
            for (int i = 0; i < StringForSearching.Length; i++)
            {
                while (j > 0 && StringForSearching[i] != pattern[j])
                {
                    j = prefixFunction[j - 1];
                }
                if (StringForSearching[i] == pattern[j])
                {
                    j++;
                }
                if (j == pattern.Length)
                {
                    indexes.Add(i - j + 1);
                    j = prefixFunction[j - 1];
                }
            }

            return indexes;
        }

        public bool SearchAvailabSubstring(string StringForSearching, string pattern)
        {
            int[] prefixFunction = CalculatePrefixFunction(pattern);

            int j = 0;
            for (int i = 0; i < StringForSearching.Length; i++)
            {
                while (j > 0 && StringForSearching[i] != pattern[j])
                {
                    j = prefixFunction[j - 1];
                }
                if (StringForSearching[i] == pattern[j])
                {
                    j++;
                }
                if (j == pattern.Length)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

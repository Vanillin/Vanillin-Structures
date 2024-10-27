using System.Collections.Generic;

namespace MySubstringSearching
{
    internal class SubstringSearch_BruteForce : ISubstringSearch
    {
        private SubstringSearch_BruteForce() { }
        private static SubstringSearch_BruteForce ThisClass;
        public static SubstringSearch_BruteForce Creating()
        {
            if (ThisClass == null) ThisClass = new SubstringSearch_BruteForce();
            return ThisClass;
        }
        public List<int> SearchIndexesSubstring(string StringForSearching, string pattern)
        {
            List<int> retur = new List<int>();
            for (int i = 0; i < StringForSearching.Length - pattern.Length + 1; i++)
            {
                bool IsTrue = true;
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (StringForSearching[i + j] != pattern[j])
                    {
                        IsTrue = false;
                        break;
                    }
                }
                if (IsTrue)
                {
                    retur.Add(i);
                }
            }
            return retur;
        }

        public bool SearchAvailabSubstring(string StringForSearching, string pattern)
        {
            for (int i = 0; i < StringForSearching.Length - pattern.Length + 1; i++)
            {
                bool IsTrue = true;
                for (int j = 0; j < pattern.Length; j++)
                {
                    if (StringForSearching[i + j] != pattern[j])
                    {
                        IsTrue = false;
                        break;
                    }
                }
                if (IsTrue)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

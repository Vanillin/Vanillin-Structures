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
        public List<int> SearchSubstring(string StringForSearching, string SearchingString)
        {
            List<int> retur = new List<int>();
            for (int i = 0; i < StringForSearching.Length - SearchingString.Length + 1; i++)
            {
                bool IsTrue = true;
                for (int j = 0; j < SearchingString.Length; j++)
                {
                    if (StringForSearching[i + j] != SearchingString[j])
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

    }
}

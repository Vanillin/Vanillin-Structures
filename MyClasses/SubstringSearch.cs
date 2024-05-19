using System;
using System.Collections.Generic;

namespace MyClasses
{
    internal interface ISubstringSearch
    {
        List<int> SearchSubstring(string StringForSearching, string SearchingString);
    }
    public enum SubstringSearching
    {
        BruteForce,
        BoilerMur,
        KMP,
        RabinKarp,
    }
    /// <summary>
    /// Реализует паттерн одиночки. Для создания используйте статический метод Creating()
    /// </summary>
    public class SubstringSearch
    {
        private List<ISubstringSearch> substringSearches = new List<ISubstringSearch>
        {
            SubstringSearch_BoilerMur.Creating(),
            SubstringSearch_BruteForce.Creating(),
            SubstringSearch_KMP.Creating(),
            SubstringSearch_RabinKarp.Creating(),
        };
        private SubstringSearch() { }
        private static SubstringSearch ThisClass;
        public static SubstringSearch Creating()
        {
            if (ThisClass == null) ThisClass = new SubstringSearch();
            return ThisClass;
        }
        public List<int> SearchSubstring(SubstringSearching search, string stringForSearching, string pattern)
        {
            switch (search)
            {
                case SubstringSearching.BoilerMur: return substringSearches[0].SearchSubstring(stringForSearching, pattern);
                case SubstringSearching.BruteForce: return substringSearches[1].SearchSubstring(stringForSearching, pattern);
                case SubstringSearching.KMP: return substringSearches[2].SearchSubstring(stringForSearching, pattern);
                case SubstringSearching.RabinKarp: return substringSearches[3].SearchSubstring(stringForSearching, pattern);
                default: throw new ArgumentException("No search");
            }
        }
    }
}

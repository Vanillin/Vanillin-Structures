using System;
using System.Collections.Generic;

namespace MySubstringSearching
{
    internal interface ISubstringSearch
    {
        List<int> SearchIndexesSubstring(string StringForSearching, string pattern);
        bool SearchAvailabSubstring(string StringForSearching, string pattern);
    }
    public enum SubstringSearching
    {
        BoilerMur,
        BruteForce,
        KMP,
        RabinKarp,
    }
    /// <summary>
    /// Класс для быстрого поиска подстрок. Реализует паттерн одиночки. (Для создания используйте статический метод Creating())
    /// </summary>
    public class SubstringSearch
    {
        private ISubstringSearch[] substringSearches = new ISubstringSearch[]
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
        /// <summary>
        /// Метод поиска подстрок
        /// </summary>
        public List<int> SearchIndexesSubstring(string stringForSearching, string pattern)
        {
            return substringSearches[0].SearchIndexesSubstring(stringForSearching, pattern);
        }
        /// <summary>
        /// Метод проверки наличия подстроки
        /// </summary>
        public bool SearchAvailabSubstring(string stringForSearching, string pattern)
        {
            return substringSearches[0].SearchAvailabSubstring(stringForSearching, pattern);
        }
        /// <summary>
        /// Метод поиска подстрок (для сравнивания по времени)
        /// </summary>
        public List<int> SearchIndexesSubstring(SubstringSearching search, string stringForSearching, string pattern)
        {
            switch (search)
            {
                case SubstringSearching.BoilerMur: return substringSearches[0].SearchIndexesSubstring(stringForSearching, pattern);
                case SubstringSearching.BruteForce: return substringSearches[1].SearchIndexesSubstring(stringForSearching, pattern);
                case SubstringSearching.KMP: return substringSearches[2].SearchIndexesSubstring(stringForSearching, pattern);
                case SubstringSearching.RabinKarp: return substringSearches[3].SearchIndexesSubstring(stringForSearching, pattern);
                default: throw new ArgumentException("No search");
            }
        }
        /// <summary>
        /// Метод проверки наличия подстроки (для сравнивания по времени)
        /// </summary>
        public bool SearchAvailabSubstring(SubstringSearching search, string stringForSearching, string pattern)
        {
            switch (search)
            {
                case SubstringSearching.BoilerMur: return substringSearches[0].SearchAvailabSubstring(stringForSearching, pattern);
                case SubstringSearching.BruteForce: return substringSearches[1].SearchAvailabSubstring(stringForSearching, pattern);
                case SubstringSearching.KMP: return substringSearches[2].SearchAvailabSubstring(stringForSearching, pattern);
                case SubstringSearching.RabinKarp: return substringSearches[3].SearchAvailabSubstring(stringForSearching, pattern);
                default: throw new ArgumentException("No search");
            }
        }
    }
}

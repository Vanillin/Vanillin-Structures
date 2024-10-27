﻿using System.Collections.Generic;

namespace MySubstringSearching
{
    internal class SubstringSearch_BoilerMur : ISubstringSearch
    {
        private SubstringSearch_BoilerMur() { }
        private static SubstringSearch_BoilerMur ThisClass;
        public static SubstringSearch_BoilerMur Creating()
        {
            if (ThisClass == null) ThisClass = new SubstringSearch_BoilerMur();
            return ThisClass;
        }

        private int MaxIndexSymbol(string str)
        {
            int maxSymbols = 0;
            foreach (char c in str)
                if ((int)c > maxSymbols) maxSymbols = c;
            return maxSymbols;
        }
        private void CreateStopSymbols(string SearchingString, out int[] StopSymbols)
        {
            StopSymbols = new int[   MaxIndexSymbol(SearchingString)   ];
            for (int i = SearchingString.Length - 1; i >= 0; i--)
            {
                int index = SearchingString[i] - 1;
                if (StopSymbols[index] == 0) StopSymbols[index] = SearchingString.Length - i - 1;
            }
            int lastIndex = SearchingString[SearchingString.Length - 1] - 1;
            if (StopSymbols[lastIndex] == 0) StopSymbols[lastIndex] = SearchingString.Length;
        }
        private void CreateSuffics(string SearchingString, out int[] Suffics)
        {
            Suffics = new int[  SearchingString.Length  ];
            int lastIndex = 0;

            for (int shift = 1; shift < SearchingString.Length; shift++)
            {
                int len = 0;

                while (SearchingString.Length - 1 - len - shift < 0 ||
                    SearchingString[SearchingString.Length - 1 - len] == SearchingString[SearchingString.Length - 1 - len - shift])
                {
                    len++;
                    if (len - 1 == SearchingString.Length) break;
                    if (Suffics[len - 1] == 0) Suffics[len - 1] = shift;
                    lastIndex = len;
                }
            }
            for (int i = lastIndex; i < SearchingString.Length; i++)
            {
                Suffics[i] = SearchingString.Length;
            }
        }
        public List<int> SearchIndexesSubstring(string StringForSearching, string pattern)
        {
            List<int> retur = new List<int>();

            CreateStopSymbols(pattern, out int[] StopSimbols);
            CreateSuffics(pattern, out int[] Suffics);

            for (int i = 0; i < StringForSearching.Length - pattern.Length + 1;)
            {
                bool IsTrue = true;
                int equalsCount = 0;
                for (int j = pattern.Length - 1; j >= 0;)
                {
                    if (StringForSearching[i + j] != pattern[j])
                    {
                        char c = StringForSearching[i + j];
                        int indexChar = c - 1;

                        int shift = pattern.Length - equalsCount;
                        if (indexChar < StopSimbols.Length && StopSimbols[indexChar] != 0)
                            shift = StopSimbols[indexChar] - equalsCount;

                        if (equalsCount != 0 && shift < Suffics[equalsCount - 1])
                            shift = Suffics[equalsCount];

                        i += shift;
                        IsTrue = false;
                        break;
                    }
                    else
                    {
                        equalsCount++;
                        j--;
                    }
                }
                if (IsTrue)
                {
                    retur.Add(i);
                    i += Suffics[Suffics.Length - 1];
                }
            }
            return retur;
        }

        public bool SearchAvailabSubstring(string StringForSearching, string pattern)
        {
            CreateStopSymbols(pattern, out int[] StopSimbols);
            CreateSuffics(pattern, out int[] Suffics);

            for (int i = 0; i < StringForSearching.Length - pattern.Length + 1;)
            {
                bool IsTrue = true;
                int equalsCount = 0;
                for (int j = pattern.Length - 1; j >= 0;)
                {
                    if (StringForSearching[i + j] != pattern[j])
                    {
                        char c = StringForSearching[i + j];
                        int indexChar = c - 1;

                        int shift = pattern.Length - equalsCount;
                        if (indexChar < StopSimbols.Length && StopSimbols[indexChar] != 0)
                            shift = StopSimbols[indexChar] - equalsCount;

                        if (equalsCount != 0 && shift < Suffics[equalsCount - 1])
                            shift = Suffics[equalsCount];

                        i += shift;
                        IsTrue = false;
                        break;
                    }
                    else
                    {
                        equalsCount++;
                        j--;
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace DecodeShifrVizhener
{
    public class Decoder
    {
        private List<char>[] memoryPartText;
        private int[] Shifts;

        public char[] alfavit { get; }
        public List<char>[] MemoryPartText => memoryPartText;

        public Decoder(string text, int n)
        {
            alfavit = new char[] { 'а', 'б', 'в', 'г', 'д', 'е', 'ё', 'ж', 'з', 'и', 'й', 
                'к', 'л', 'м', 'н', 'о', 'п', 'р', 'с', 'т', 'у', 'ф', 'х', 'ц', 'ч', 'ш', 
                'щ', 'ъ', 'ы', 'ь', 'э', 'ю', 'я', ' ', ',', '.'};

            var memoryText = text.ToCharArray().ToList();
            Shifts = new int[n];
            memoryPartText = new List<char>[n];
            for (int i = 0; i < n; i++) { memoryPartText[i] = new List<char>(); }
            int number = 0;
            foreach (char elem in memoryText)
            {
                memoryPartText[number].Add(elem);
                number = (number + 1) % n;
            }
        }
        private Dictionary<char, double> CreateDictionary(char[] chars)
        {
            Dictionary<char, double> dict = new Dictionary<char, double>();
            foreach (var v in chars)
                dict.Add(v, 0);
            return dict;
        }
        private int FindIndexInAlfavit(char c)
        {
            for (int i = 0; i < alfavit.Length; i++)
            {
                if (alfavit[i] == c) return i;
            }
            throw new Exception();
        }
        public List<List<(char, double)>> AnalyzeVerSymbols()
        {
            var retur = new List<List<(char, double)>>();

            foreach (var chars in memoryPartText)
            {
                var currentList = new List<(char, double)>();
                var dict = CreateDictionary(alfavit);
                foreach (char elem in chars) { dict[elem]++; }
                foreach (var cha in alfavit) { dict[cha] = dict[cha] / chars.Count; }
                List<double> doubles = new List<double>();
                foreach (var v in dict) { doubles.Add(v.Value); }
                for (int i = 0; i < doubles.Count; i++)
                {
                    double print = Math.Round(doubles[i], 2);
                    currentList.Add((alfavit[i],print));
                }
                retur.Add(currentList);
            }
            return retur;
        }
        public void ChangeShiftOnMas(int K, int N)
        {
            var chars = memoryPartText[K];
            for (int i = 0; i < chars.Count; i++)
            {
                int index = FindIndexInAlfavit(chars[i]);
                int shiftIndex = (index + N) % alfavit.Length;
                if (shiftIndex < 0) shiftIndex += alfavit.Length;
                chars[i] = alfavit[shiftIndex];
            }
            Shifts[K] += N;
        }
        public void ChangeAllShiftOnMas()
        {   
            for ( int i = 0; i < Shifts.Length; i++)
            {
                ChangeShiftOnMas(i, -1);
            }
        }
        public void AutomaticalyShift()
        {
            for (int i = 0; i < Shifts.Length; i++)
            {
                var dict = CreateDictionary(alfavit);
                List<char> chars = memoryPartText[i];
                foreach (char elem in chars) { dict[elem]++; }
                foreach (var cha in alfavit) { dict[cha] = dict[cha] / chars.Count; }
                List<double> doubles = new List<double>();
                foreach (var v in dict) { doubles.Add(v.Value); }

                double maxDouble = 0;
                int maxDoubleIndex = -1;
                for (int j = 0; j < doubles.Count; j++)
                {
                    if (maxDouble < doubles[j])
                    {
                        maxDouble = doubles[j];
                        maxDoubleIndex = j;
                    }
                }
                ChangeShiftOnMas(i, -maxDoubleIndex);
                Shifts[i] -= maxDoubleIndex;
            }
        }
        
    }
}

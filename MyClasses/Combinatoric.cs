using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasses
{
    public static class Combinatorics<T>
    {
        /// <summary>
        /// Получить все возможные последовательности из элементов input
        /// </summary>
        public static IEnumerable<List<T>> SubSetGeneration(T[] input)
        {
            var resList = new List<List<T>>();
            int n = input.Length;
            int[] a = new int[n + 1];
            while (a[n] != 1)
            {
                var list = new List<T>();
                for (var i = 0; i < n; i++)
                {
                    if (a[i] == 1) list.Add(input[i]);
                }
                resList.Add(list);
                int j = 0;
                while (a[j] == 1)
                {
                    a[j] = 0;
                    j++;
                }
                a[j] = 1;
            }
            return resList;
        }
        /// <summary>
        /// Получить все последовательности длины k из элементов input
        /// </summary>
        public static IEnumerable<List<T>> RepetitionPlacing(T[] input, int k)
        {
            var resList = new List<List<T>>();
            int n = input.Length;
            int[] b = new int[k + 1];

            while (b[k] != 1)
            {
                var list = new List<T>();
                for (var i = k - 1; i >= 0; i--)
                {
                    list.Add(input[b[i]]);
                }
                resList.Add(list);
                int j = 0;
                while (b[j] == n - 1)
                {
                    b[j] = 0;
                    j++;
                }
                b[j] = b[j] + 1;
            }
            return resList;
        }
        /// <summary>
        /// Получить все последовательности длины k из элементов input (без повторов)
        /// </summary>
        public static IEnumerable<List<T>> Combinations(T[] input, int k)
        {
            var resList = new List<List<T>>();
            int n = input.Length;
            int[] b = new int[k + 1];
            for (var i = 0; i < k + 1; i++)
            {
                b[i] = i;
            }
            b[0] = -1;
            int j = 1;
            while (j != 0)
            {
                var list = new List<T>();
                for (var i = 1; i < k + 1; i++)
                {
                    list.Add(input[b[i] - 1]);
                }
                resList.Add(list);
                j = k;
                while (b[j] == n - k + j)
                {
                    j--;
                }
                b[j] = b[j] + 1;
                for (int i = j + 1; i < k + 1; i++)
                {
                    b[i] = b[i - 1] + 1;
                }
            }
            return resList;
        }
        /// <summary>
        /// Получить все перестановки из элементов input
        /// </summary>
        public static IEnumerable<List<T>> GenerationPermutations(T[] input)
        {
            var resList = new List<List<T>>();
            int n = input.Length + 1;
            int[] c = new int[n];
            for (var i = 0; i < n; i++)
            {
                c[i] = i - 1;
            }
            c[0] = n;
            int j = n - 2;
            while (j != 0)
            {
                var list = new List<T>();
                for (var i = 1; i < n; i++)
                {
                    list.Add(input[c[i]]);
                }
                resList.Add(list);
                while (c[j] >= c[j + 1])
                {
                    j--;
                    if (j == 0) break;
                }
                if (j == 0) break;
                int min = n;
                for (int i = n - 1; i > j; i--)
                {
                    if (c[j] < c[i])
                    {
                        if (min == n)
                        {
                            min = i;
                        }
                        else if (c[i] < c[min])
                        {
                            min = i;
                        }
                    }
                }
                (c[min], c[j]) = (c[j], c[min]);
                bool key = true;
                while (key)
                {
                    key = false;
                    for (int i = j + 1; i < n - 1; i++)
                    {
                        if (c[i] > c[i + 1])
                        {
                            (c[i], c[i+1]) = (c[i+1], c[i]);
                            key = true;
                        }
                    }
                }
                j = n - 2;
            }
            if (resList.Count == 0)
            {
                return new List<List<T>>() { new List<T> { input[0] } };
            }
            return resList;
        }
        /// <summary>
        /// Получить перестановку из элементов input по номеру count
        /// </summary>
        public static List<T> GetPermutationByCount(T[] input, int count)
        {
            count--;
            var resList = new List<T>();
            int n = input.Length;
            int number = count / Factorial(n - resList.Count - 1);
            count = count % Factorial(n - resList.Count - 1);
            resList.Add(input[number]);
            while (resList.Count != input.Count())
            {
                number = count / Factorial(n - resList.Count - 1);
                count = count % Factorial(n - resList.Count - 1);
                int j = -1;
                for (int i = 0; i < n; i++)
                {
                    if (!resList.Contains(input[i]))
                    {
                        j++;
                    }
                    if (j == number)
                    {
                        resList.Add(input[i]);
                        break;
                    }
                }
            }
            return resList;
        }
        private static int Factorial(int n)
        {
            int count = 1;
            for (int i = 2; i <= n; i++)
            {
                count *= i;
            }
            return count;
        }
    }
}

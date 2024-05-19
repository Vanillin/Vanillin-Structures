﻿using System;

namespace MyClasses
{
    internal class PrimeNumbers
    {
        private int[] primes = {
             11, 23, 37, 59, 89, 131, 197, 239, 293, 353, 431, 521, 631, 761, 919,
            1103, 1327, 1597, 1931, 2333, 2801, 3371, 4049, 4861, 5839, 7013, 8419, 10103, 12143, 14591,
            17519, 21023, 25229, 30293, 36353, 43627, 52361, 62851, 75431, 90523, 108631, 130363, 156437,
            187751, 225307, 270371, 324449, 389357, 467237, 560689, 672827, 807403, 968897, 1162687, 1395263,
            1674319, 2009191, 2411033, 2893249, 3471899, 4166287, 4999559, 5999471, 7199369};
        private int select;

        public PrimeNumbers()
        {
            select = -1;
        }
        public int Next()
        {
            select++;
            if (select >= primes.Length)
                throw new ArgumentOutOfRangeException();
            return primes[select];
        }
        public int GetNext()
        {
            if (select + 1 >= primes.Length)
                throw new ArgumentOutOfRangeException();
            return primes[select + 1];
        }
    }
}

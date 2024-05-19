using System;

namespace MyClasses
{
    internal interface IHashFunc<TKey>
    {
        int GetHashResult(TKey key);
    }
    internal class HashFunction<TKey> : IHashFunc<TKey>
    {
        private int a;
        private int b;
        public HashFunction(int capacity, Random rand)
        {
            a = rand.Next(1, capacity - 1);
            b = rand.Next(0, capacity - 1);
        }
        public int GetHashResult(TKey key)
        {
            return a * key.GetHashCode() + b;
        }
    }
}

using System;
using System.Collections;
using System.Collections.Generic;

namespace MyClasses
{
    public class HashTableCuckoo<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        private const double fillFactor = 0.6; //0.7;  //0.85;
        private const int NumberTriesReCreateHashFunc = 3;
        private NodeHashTable<TKey, TValue>[] hashTable1;
        private NodeHashTable<TKey, TValue>[] hashTable2;
        private IHashFunc<TKey> hashFunc1;
        private IHashFunc<TKey> hashFunc2;
        private int count;
        private int capacity;
        private PrimeNumbers primes;
        private Random rand;

        public int Count => count;
        public int Capacity => capacity;
        public TValue this[TKey key]
        {
            get
            {
                var current = Find(key);
                if (current != null)
                    return current.Value;
                else
                    throw new KeyNotFoundException();
            }
            set
            {
                var current = Find(key);
                if (current != null)
                    current.Value = value;
                else
                    throw new KeyNotFoundException();
            }
        }
        public HashTableCuckoo()
        {
            rand = new Random(DateTime.Now.Millisecond);

            primes = new PrimeNumbers();
            capacity = primes.Next();
            hashTable1 = new NodeHashTable<TKey, TValue>[capacity];
            hashTable2 = new NodeHashTable<TKey, TValue>[capacity];
            ChangeThis(Changing.OnlyHashFunction);

            count = 0;
        }
        private NodeHashTable<TKey, TValue> Find(TKey key)
        {
            int hash1 = TakeHashIndex(hashFunc1, key);
            int hash2 = TakeHashIndex(hashFunc2, key);
            if (hashTable1[hash1] != null && !hashTable1[hash1].IsDelete && hashTable1[hash1].Key.Equals(key))
                return hashTable1[hash1];
            if (hashTable2[hash2] != null && !hashTable2[hash2].IsDelete && hashTable2[hash2].Key.Equals(key))
                return hashTable2[hash2];
            return null;
        }
        public bool ContainsKey(TKey key)
        {
            if (Find(key) != null) 
                return true;
            return false;
        }

        #region ---------------ChangeThis---------------
        private enum Changing
        {
            OnlyHashFunction,
            HashFunctionWithTable,
            Resize,
        }
        private void ChangeThis(Changing ch)
        {
            if (ch == Changing.OnlyHashFunction)
            {
                CreateHashFunctions(out hashFunc1, out hashFunc2);
            }
            else if (ch == Changing.HashFunctionWithTable)
            {
                CreateHashFunctions(out hashFunc1, out hashFunc2);
                ReCreateHashTable();
            }
            else if (ch == Changing.Resize)
            {
                capacity = primes.Next();
                ReCreateHashTable();
            }
        }

        private void CreateHashFunctions(out IHashFunc<TKey> hashFunc1, out IHashFunc<TKey> hashFunc2)
        {
            //Исследования в релизе!

            //count = 50.000 capaciry = 60.000 time = 70-1200 70-1000 70-750 70-1000 70-800
            //NumberTriesReCreateHashFunc = 3;
            hashFunc1 = new HashFunction<TKey>(capacity, rand);
            hashFunc2 = new HashFunction<TKey>(capacity, rand);

            ////time = 100\1200 200\1300 260\1400 70\1200
            ////NumberTriesReCreateHashFunc = 4;
            //hashFunc1 = new HashFunction<TKey>(capacity, rand, true);
            //hashFunc2 = new HashFunction<TKey>(capacity, rand, false);

            ////time = 200\1200 130\1300 170\1400 70\1100
            ////NumberTriesReCreateHashFunc = 4;
            //hashFunc1 = new HashFunction<TKey>(capacity, rand);
            //hashFunc2 = new HashFunction<TKey>(capacity, rand);

            ////time = 100\2000 200\3000 400\3000 70\2000
            ////NumberTriesReCreateHashFunc = 10;
            //hashFunc1 = new HashFunction<TKey>(capacity, rand, true);
            //hashFunc2 = new HashFunction<TKey>(capacity, rand, false);

            ////time = 190\2600 400\3000 200\2500 70\2100
            ////NumberTriesReCreateHashFunc = 10;
            //hashFunc1 = new HashFunction<TKey>(capacity, rand);
            //hashFunc2 = new HashFunction<TKey>(capacity, rand);

            ////time = 200\400 200\500 150\450 70\400 !!!!!Count = 50.000 Capacity = 1.000.000
            ////NumberTriesReCreateHashFunc = 1;
            //hashFunc1 = new HashFunction<TKey>(capacity, rand, 2);
            //hashFunc2 = new HashFunction<TKey>(capacity, rand, 5);
        }
        private void ReCreateHashTable()
        {
            for (int i = 0; i < NumberTriesReCreateHashFunc; i++)
            {
                NodeHashTable<TKey, TValue>[] NewHashTable1 = new NodeHashTable<TKey, TValue>[capacity];
                NodeHashTable<TKey, TValue>[] NewHashTable2 = new NodeHashTable<TKey, TValue>[capacity];

                int memory = 0;
                AddMassivKeyValue(hashTable1, out bool IsGood, ref memory, NewHashTable1, NewHashTable2);
                if (!IsGood)
                {
                    ChangeThis(Changing.OnlyHashFunction);
                    continue;
                }
                AddMassivKeyValue(hashTable2, out IsGood, ref memory, NewHashTable1, NewHashTable2);
                if (!IsGood)
                {
                    ChangeThis(Changing.OnlyHashFunction);
                    continue;
                }

                hashTable1 = NewHashTable1;
                hashTable2 = NewHashTable2;
                return;
            }
            ChangeThis(Changing.Resize);


        }
        private void AddMassivKeyValue(NodeHashTable<TKey, TValue>[] pairs, out bool IsGood, ref int memory, 
            NodeHashTable<TKey, TValue>[] hashT1, NodeHashTable<TKey, TValue>[] hashT2)
        {
            IsGood = true;
            foreach (var v in pairs)
            {
                if (v != null && v.IsDelete == false)
                {
                    AddKeyValue(v.Key, v.Value, out IsGood, hashT1, hashT2);
                    if (!IsGood)
                        return;
                    memory++;
                }
            }
        }
        #endregion

        private int TakeHashIndex(IHashFunc<TKey> hashFunc, TKey key)
        {
            int index = (hashFunc.GetHashResult(key) % primes.GetNext()) % capacity;
            if (index < 0) index += capacity;
            return index;
        }
        public void Remove(TKey key)
        {
            int hash1 = TakeHashIndex(hashFunc1, key);
            if (hashTable1[hash1] != null && hashTable1[hash1].Key.Equals(key))
            {
                hashTable1[hash1].IsDelete = true;
                count--;
                return;
            }

            int hash2 = TakeHashIndex(hashFunc2, key);
            if (hashTable2[hash2] != null && hashTable2[hash2].Key.Equals(key))
            {
                hashTable2[hash2].IsDelete = true;
                count--;
                return;
            }
        }
        public void Add(TKey key, TValue value)
        {
            if (ContainsKey(key))
                throw new ArgumentException();
            
            AddKeyValue(key, value, out bool IsGood, hashTable1, hashTable2);

            int counter = 0;
            while (!IsGood)
            {
                counter++;
                if (counter > NumberTriesReCreateHashFunc)
                {
                    ChangeThis(Changing.Resize);
                    counter = 0;
                }
                else
                    ChangeThis(Changing.HashFunctionWithTable);
                AddKeyValue(key, value, out IsGood, hashTable1, hashTable2);
            }
            count++;

            if ((double)count / (capacity * 2) > fillFactor)
                ChangeThis(Changing.Resize);
        }
        private void AddKeyValue(TKey key, TValue value, out bool IsGood, 
            NodeHashTable<TKey, TValue>[] hashT1, NodeHashTable<TKey, TValue>[] hashT2)
        {
            IsGood = false;
            int hash1;
            int hash2;
            TKey memoryKey = key;
            TValue memoryValue = value;

            do
            {
                hash1 = TakeHashIndex(hashFunc1, memoryKey);

                if (hashT1[hash1] == null || hashT1[hash1].IsDelete == true)
                {
                    hashT1[hash1] = new NodeHashTable<TKey, TValue>(memoryKey, memoryValue);
                    IsGood = true;
                    return;
                }
                else
                {
                    NodeHashTable<TKey, TValue> memory = hashT1[hash1];
                    hashT1[hash1] = new NodeHashTable<TKey, TValue>(memoryKey, memoryValue);
                    memoryKey = memory.Key;
                    memoryValue = memory.Value;
                }

                hash2 = TakeHashIndex(hashFunc2, memoryKey);

                if (hashT2[hash2] == null || hashT2[hash2].IsDelete == true)
                {
                    hashT2[hash2] = new NodeHashTable<TKey, TValue>(memoryKey, memoryValue);
                    IsGood = true;
                    return;
                }
                else
                {
                    NodeHashTable<TKey, TValue> memory = hashT2[hash2];
                    hashT2[hash2] = new NodeHashTable<TKey, TValue>(memoryKey, memoryValue);
                    memoryKey = memory.Key;
                    memoryValue = memory.Value;
                }
            }
            while (!memoryKey.Equals(key));
            return;
        }
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            foreach (NodeHashTable<TKey, TValue> v in hashTable1)
            {
                if (v != null && v.IsDelete == false)
                    yield return new KeyValuePair<TKey, TValue>(v.Key, v.Value);
            }
            foreach (NodeHashTable<TKey, TValue> v in hashTable2)
            {
                if (v != null && v.IsDelete == false)
                    yield return new KeyValuePair<TKey, TValue>(v.Key, v.Value);
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            foreach (NodeHashTable<TKey, TValue> v in hashTable1)
            {
                if (v != null && v.IsDelete == false)
                    yield return new KeyValuePair<TKey, TValue>(v.Key, v.Value);
            }
            foreach (NodeHashTable<TKey, TValue> v in hashTable2)
            {
                if (v != null && v.IsDelete == false)
                    yield return new KeyValuePair<TKey, TValue>(v.Key, v.Value);
            }
        }
    }
}

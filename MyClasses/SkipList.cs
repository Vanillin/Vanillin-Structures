using System;
using System.Collections;
using System.Collections.Generic;

namespace MyClasses
{
    public class SkipList<TKey, TValue> : IEnumerable<TValue> where TKey : IComparable<TKey>
    {
        private const int NumberLevelConst = 10;
        private const double ProbabilityConst = 0.6;

        private double[] ProbabilityLevel;
        private int NumberLevel;
        private Random random;
        private NodeSkipList<TKey, TValue>[] HeadsLevel;
        private int count;

        public int Count => count;
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
        private void CreatingSkipList(int numberLevel, double probablity)
        {
            if (numberLevel <= 0)
                throw new ArgumentException();
            NumberLevel = numberLevel;

            HeadsLevel = new NodeSkipList<TKey, TValue>[NumberLevel];
            ProbabilityLevel = new double[NumberLevel];
            for (int i = 0; i < NumberLevel; i++)
            {
                //ProbabilityLevel[i] = Math.Pow(2, i) / Math.Pow(2, NumberLevel - 1);
                ProbabilityLevel[i] = probablity;
            }

            string NowTicks = DateTime.Now.Ticks.ToString();
            random = new Random(int.Parse($"{NowTicks[1]}{NowTicks[3]}{NowTicks[NowTicks.Length - 2]}{NowTicks[NowTicks.Length - 4]}"));
        }
        public SkipList(int numberLevel, double probablity)
        {
            CreatingSkipList(numberLevel, probablity);
        }
        public SkipList()
        {
            CreatingSkipList(NumberLevelConst, ProbabilityConst);
        }
        private NodeSkipList<TKey, TValue> Find(TKey key)
        {
            if (count == 0) return null;

            NodeSkipList<TKey, TValue> current = HeadsLevel[0];
            for (int i = 0; i < NumberLevel; i++)
            {
                while (current.Right != null && current.Right.Key.CompareTo(key) <= 0)
                {
                    current = current.Right;
                }

                if (i != NumberLevel - 1)
                    current = current.Down;
            }
            if (current.Key.CompareTo(key) == 0)
                return current;
            return null;
        }
        public bool ContainsKey(TKey key)
        {
            if (Find(key) == null) return false;
            return true;
        }
        private NodeSkipList<TKey, TValue>[] CreateNewElements(TKey key, TValue value, int count)
        {
            NodeSkipList<TKey, TValue>[] newElements = new NodeSkipList<TKey, TValue>[count];
            for (int i = 0; i < count; i++)
            {
                newElements[i] = new NodeSkipList<TKey, TValue>(key, value);
                if (i != 0)
                {
                    newElements[i].Up = newElements[i - 1];
                    newElements[i - 1].Down = newElements[i];
                }
            }
            return newElements;
        }
        public void Add(TKey key, TValue value)
        {
            if (count == 0)
            {
                var v = CreateNewElements(key, value, NumberLevel);
                for ( int i = 0; i < NumberLevel; i++)
                    HeadsLevel[i] = v[i];
                count++;
                return;
            }
            if (HeadsLevel[0].Key.CompareTo(key) == 0)
                throw new ArgumentException();

            NodeSkipList<TKey, TValue> current = HeadsLevel[0];
            NodeSkipList<TKey, TValue>[] LeftElements = new NodeSkipList<TKey, TValue>[NumberLevel];

            for (int i = 0; i < NumberLevel; i++)
            {
                while (current.Right != null && current.Right.Key.CompareTo(key) <= 0)
                {
                    current = current.Right;
                }
                LeftElements[i] = current;

                if (i != NumberLevel - 1)
                    current = current.Down;
            }
            if (current.Key.CompareTo(key) == 0)
                throw new ArgumentException();

            if (HeadsLevel[0].Key.CompareTo(key) > 0)
            {
                TKey keyHead = HeadsLevel[0].Key;
                TValue valueHead = HeadsLevel[0].Value;
                for (int i = 0; i < NumberLevel; i++)
                {
                    HeadsLevel[i].Key = key;
                    HeadsLevel[i].Value = value;
                }
                key = keyHead;
                value = valueHead;
            }

            var newElements = CreateNewElements(key, value, LevelDetected());
            for (int i = 0; i < NumberLevel; i++)
            {
                if (i >= newElements.Length)
                    continue;
                var Right = LeftElements[NumberLevel -1 -i].Right;
                LeftElements[NumberLevel - 1 - i].Right = newElements[newElements.Length - 1 -i];
                newElements[newElements.Length - 1 - i].Right = Right;
            }
            count++;
            return;
        }
        public void Remove(TKey key)
        {
            if (count == 0)
                return;
            if (HeadsLevel[0].Key.CompareTo(key) == 0)
            {
                if (count == 1)
                {
                    for (int i = 0; i < NumberLevel; i++)
                    {
                        HeadsLevel[i] = null;
                    }
                }
                else
                {
                    TKey rightKey = HeadsLevel[NumberLevel - 1].Right.Key;
                    TValue rightValue = HeadsLevel[NumberLevel - 1].Right.Value;
                    for (int i = 0; i < NumberLevel; i++)
                    {
                        HeadsLevel[i].Key = rightKey;
                        HeadsLevel[i].Value = rightValue;
                        if (HeadsLevel[i].Right != null)
                        {
                            HeadsLevel[i].Right = HeadsLevel[i].Right.Right;
                        }
                    }
                }
                count--;
                return;
            }

            NodeSkipList<TKey, TValue> current = HeadsLevel[0];

            NodeSkipList<TKey, TValue>[] LeftElements = new NodeSkipList<TKey, TValue>[NumberLevel];
            for (int i = 0; i < NumberLevel; i++)
            {
                while (current.Right != null && current.Right.Key.CompareTo(key) < 0)
                {
                    current = current.Right;
                }
                LeftElements[i] = current;

                if (i != NumberLevel - 1)
                    current = current.Down;
            }
            if (current.Right.Key.CompareTo(key) == 0)
            {
                for (int i = 0; i < NumberLevel; i++)
                {
                    if (LeftElements[i].Right != null)
                    {
                        LeftElements[i].Right = LeftElements[i].Right.Right;
                    }
                }
            }

            count--;
            return;
        }
        private int LevelDetected()
        {
            int selectLevel = NumberLevel -1 ;
            int count = 0;
            bool Eagle = true;
            while (Eagle && selectLevel >= 0)
            {
                if (random.NextDouble() <= ProbabilityLevel[selectLevel])
                {
                    selectLevel--;
                    count++;
                }
                else
                    Eagle = false;
            }
            if (count == 0) count = 1;
            return count;
        }

        public IEnumerator<TValue> GetEnumerator()
        {
            if (count != 0)
            {
                var current = HeadsLevel[NumberLevel - 1];
                yield return current.Value;
                while (current.Right != null)
                {
                    current = current.Right;
                    yield return current.Value;
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            if (count != 0)
            {
                var current = HeadsLevel[NumberLevel - 1];
                yield return current.Value;
                while (current.Right != null)
                {
                    current = current.Right;
                    yield return current.Value;
                }
            }
        }
    }
}

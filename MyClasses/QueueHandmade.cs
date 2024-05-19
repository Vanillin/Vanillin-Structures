using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MyClasses
{
    public class QueueHandmade<T> : IEnumerable<T>
    {
        int count;
        int capacity;
        int head;
        int tail;
        T[] Memory;

        public int Capacity => capacity;
        public int Count => count;
        public QueueHandmade()
        {
            capacity = 4;
            count = 0;
            Memory = new T[capacity];
            tail = -1;
            head = 0;
        }
        public QueueHandmade(int length)
        {
            capacity = length;
            count = 0;
            Memory = new T[capacity];
            tail = -1;
            head = 0;
        }
        public QueueHandmade(IEnumerable<T> elements)
        {
            capacity = 4;
            while (capacity < elements.Count())
                capacity *= 2;
            count = 0;
            tail = -1;
            Memory = new T[capacity];
            foreach (var v in elements)
                Enqueue(v);
            head = 0;
        }
        private void Resize()
        {
            T[] memoryLast = Memory;
            Memory = new T[capacity * 2];
            int index = 0;
            while (head != tail)
            {
                Memory[index] = memoryLast[head];
                head++;
                head = head % capacity;
                index++;
            }
            Memory[index] = memoryLast[tail];
            head = 0;
            tail = index;
            capacity *= 2;
        }
        public T Peek()
        {
            if (count == 0)
            {
                throw new InvalidOperationException();
            }
            return Memory[head];
        }
        public T Dequeue()
        {
            if (count == 0)
            {
                throw new InvalidOperationException();
            }
            count--;
            T retur = Memory[head];
            head++;
            head = head % capacity;
            return retur;
        }
        public void Enqueue(T element)
        {
            count++;
            if (count > capacity)
            {
                Resize();
            }
            tail++;
            tail = tail % capacity;
            Memory[tail] = element;
        }
        public bool Contains(T element)
        {
            if (count != 0)
            {
                int index = head;
                while (index != tail)
                {
                    if (Memory[index].Equals(element)) return true;
                    index++;
                    index = index % capacity;
                }
                if (Memory[tail].Equals(element)) return true;
            }
            return false;
        }
        public IEnumerator<T> GetEnumerator()
        {
            int index = head;
            if (count != 0)
            {
                while (index != tail)
                {
                    yield return Memory[index];
                    index++;
                    index = index % capacity;
                }
                yield return Memory[tail];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            int index = head;
            if (count != 0)
            {
                while (index != tail)
                {
                    yield return Memory[index];
                    index++;
                    index = index % capacity;
                }
                yield return Memory[tail];
            }
        }
    }
}

using System;
using System.Linq;

namespace MyClasses
{
    public class BinaryHeap<T> where T : IComparable<T>
    {
        private T[] array;
        private int capacity;
        private int length;
        public int Length => length;
        public BinaryHeap()
        {
            this.array = new T[4];
            capacity = 4;
            length = 0;
        }
        public BinaryHeap(int length)
        {
            capacity = length;
            this.array = new T[capacity];
            this.length = 0;
        }
        public BinaryHeap(T[] array)
        {
            if (array == null) throw new ArgumentNullException();
            capacity = array.Length;
            length = array.Length;
            this.array = new T[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                this.array[i] = array[i];
            }

            HalfFullHeapify();
        }
        public T FindMaxElement()
        {
            if (length == 0)
                throw new InvalidOperationException();

            return array[0];
        }
        private void Resize()
        {
            T[] memoryLast = array;
            array = new T[capacity * 2];
            for (int i = 0; i < length; i++)
            {
                array[i] = memoryLast[i];
            }
            capacity *= 2;
        }
        public void RemoveMaxElement()
        {
            if (length == 0)
                return;

            (array[length - 1], array[0]) = (array[0], array[length - 1]);
            length--;
            Heapify(0, length);
        }
        public void Add(T item)
        {
            if (length == capacity)
            {
                Resize();
            }
            array[length] = item;
            length++;

            ReverseHeapify(length - 1, length);
        }

        private void HalfFullHeapify()
        {
            for (int i = array.Length / 2; i >= 0; i--)
            {
                Heapify(i, array.Length);
            }
        }
        private void Heapify(int index, int length)
        {
            int leftChild = 2 * index + 1;
            int rightChild = 2 * index + 2;

            if (leftChild >= length) return;
            T left = array[leftChild];

            int indexMax;
            if (rightChild < length)
            {
                T rigth = array[rightChild];
                if (left.CompareTo(rigth) > 0)
                    indexMax = leftChild;
                else
                    indexMax = rightChild;
            }
            else
            {
                indexMax = leftChild;
            }

            if (array[index].CompareTo(array[indexMax]) < 0)
            {
                (array[indexMax], array[index]) = (array[index], array[indexMax]);
                Heapify(indexMax, length);
            }
        }
        private void ReverseHeapify(int index, int length)
        {
            int indexParent = (index - 1) / 2;
            if (array[index].CompareTo(array[indexParent]) > 0)
            {
                (array[indexParent], array[index]) = (array[index], array[indexParent]);
                if (indexParent != 0)
                    ReverseHeapify(indexParent, length);
            }
        }
        public void PiramidalSort()
        {
            for (int l = array.Length - 1; l >= 0; l--)
            {
                (array[l], array[0]) = (array[0], array[l]);
                Heapify(0, l - 1);
            }
            array.Reverse();
        }
    }
}

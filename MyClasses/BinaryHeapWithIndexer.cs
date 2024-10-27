using System;
using System.Linq;

namespace MyClasses
{
    public class BinaryHeapWithIndexer<T> where T : IComparable<T>
    {
        private int?[] indexes;
        private NodeBinaryHeapWithIndexer<T>[] array;
        private int capacity;
        private int length;
        public int Length => length;
        public BinaryHeapWithIndexer()
        {
            this.array = new NodeBinaryHeapWithIndexer<T>[4];
            indexes = new int?[4];
            capacity = 4;
            length = 0;
        }
        public BinaryHeapWithIndexer(int length)
        {
            capacity = length;
            this.array = new NodeBinaryHeapWithIndexer<T>[capacity];
            indexes = new int?[capacity];
            this.length = 0;
        }
        public BinaryHeapWithIndexer(T[] array, int[] indexes)
        {
            if (array == null) throw new ArgumentNullException();
            capacity = array.Length;
            length = array.Length;
            this.array = new NodeBinaryHeapWithIndexer<T>[array.Length];
            this.indexes = new int?[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                this.array[i] = new NodeBinaryHeapWithIndexer<T>(array[i], indexes[i]);
                if (this.indexes[indexes[i]] != null)
                    throw new ArgumentException();
                this.indexes[indexes[i]] = i;
            }

            HalfFullHeapify();
        }
        public T FindMaxElement()
        {
            if (length == 0)
                throw new InvalidOperationException();

            return array[0].Element;
        }
        public int FindMaxElementIndex()
        {
            if (length == 0)
                throw new InvalidOperationException();

            return array[0].Index;
        }
        private void Resize()
        {
            NodeBinaryHeapWithIndexer<T>[] memoryElement = array;
            array = new NodeBinaryHeapWithIndexer<T>[capacity * 2];
            int?[] memoryIndex = indexes;
            indexes = new int?[capacity * 2];

            for (int i = 0; i < length; i++)
            {
                array[i] = memoryElement[i];
                indexes[i] = memoryIndex[i];
            }
            capacity *= 2;
        }
        public void RemoveMaxElement()
        {
            if (length == 0)
                return;

            array[0] = array[length - 1];
            (indexes[array[length - 1].Index], indexes[array[0].Index]) = (null, indexes[array[length - 1].Index]);
            length--;
            Heapify(0, length);
        }
        public void Add(T item, int index)
        {
            if (length == capacity)
            {
                Resize();
            }
            if (indexes[index] != null)
                throw new ArgumentException();
            array[length] = new NodeBinaryHeapWithIndexer<T>(item, index);
            indexes[index] = length;
            length++;

            ReverseHeapify(length - 1);
        }
        public void ChangeElement(int index, T newElement)
        {
            if (indexes[index] == null)
                throw new ArgumentException();
            int indexElement = (int)indexes[index];

            T memory = array[indexElement].Element;
            if (memory.CompareTo(newElement) == 0) return;

            if (memory.CompareTo(newElement) < 0)
            {
                Heapify(indexElement, length);
            }
            else
            {
                ReverseHeapify(indexElement);
            }
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

            int indexMax = leftChild;
            if (rightChild < length && array[leftChild].CompareTo(array[rightChild]) <= 0)
            {
                indexMax = rightChild;
            }

            if (array[index].CompareTo(array[indexMax]) < 0)
            {
                (array[indexMax], array[index]) = (array[index], array[indexMax]);
                (indexes[array[indexMax].Index], indexes[array[index].Index]) = (indexes[array[index].Index], indexes[array[indexMax].Index]);
                Heapify(indexMax, length);
            }
        }
        private void ReverseHeapify(int index)
        {
            int indexParent = (index - 1) / 2;
            if (array[index].CompareTo(array[indexParent]) > 0)
            {
                (array[indexParent], array[index]) = (array[index], array[indexParent]);
                (indexes[array[indexParent].Index], indexes[array[index].Index]) = (indexes[array[index].Index], indexes[array[indexParent].Index]);
                if (indexParent != 0)
                    ReverseHeapify(indexParent);
            }
        }
        public void PiramidalSort()
        {
            for (int l = array.Length - 1; l >= 0; l--)
            {
                (array[l], array[0]) = (array[0], array[l]);
                (indexes[array[l].Index], indexes[array[0].Index]) = (indexes[array[0].Index], indexes[array[l].Index]);
                Heapify(0, l - 1);
            }
            array.Reverse();
        }
    }
}

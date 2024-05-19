using System;

namespace MyClasses
{
    internal class NodeBinaryHeapWithIndexer<T> : IComparable<NodeBinaryHeapWithIndexer<T>> where T : IComparable<T>
    {
        public T Element { get; set; }
        public int Index { get; set; }
        public NodeBinaryHeapWithIndexer(T element, int index)
        {
            Index = index;
            Element = element;
        }
        public int CompareTo(NodeBinaryHeapWithIndexer<T> other)
        {
            return Element.CompareTo(other.Element);
        }
    }
}

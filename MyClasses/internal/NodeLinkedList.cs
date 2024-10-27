namespace MyClasses
{
    internal class NodeLinkedList<T>
    {
        public T Value { get; set; }
        public NodeLinkedList<T> Next { get; set; }
        public NodeLinkedList(T value)
        {
            Value = value;
        }
    }
}

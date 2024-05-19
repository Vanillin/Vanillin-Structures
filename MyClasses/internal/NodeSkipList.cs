namespace MyClasses
{
    internal class NodeSkipList<Tkey, TValue>
    {
        public Tkey Key { get; set; }
        public TValue Value { get; set; }
        public NodeSkipList<Tkey, TValue> Up { get; set; }
        public NodeSkipList<Tkey, TValue> Down { get; set; }
        public NodeSkipList<Tkey, TValue> Right { get; set; }
        public NodeSkipList(Tkey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
            Up = null;
            Down = null;
            Right = null;
        }
    }
}

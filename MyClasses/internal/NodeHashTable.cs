namespace MyClasses
{
    internal class NodeHashTable<TKey, TValue>
    {
        private TKey key;
        public TKey Key => key;
        public TValue Value { get; set; }
        public bool IsDelete { get; set; }


        public NodeHashTable(TKey key, TValue value)
        {
            this.key = key;
            Value = value;
            IsDelete = false;
        }
    }
}

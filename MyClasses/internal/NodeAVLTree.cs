namespace MyClasses
{
    internal class NodeAVLTree<Tkey, TValue>
    {
        public Tkey Key { get; set; }
        public TValue Value { get; set; }
        public NodeAVLTree<Tkey, TValue> Parent { get; set; }
        public NodeAVLTree<Tkey, TValue> LeftChildren { get; set; }
        public NodeAVLTree<Tkey, TValue> RightChildren { get; set; }
        public int Weigth { get; set; }
        public NodeAVLTree(Tkey key, TValue value)
        {
            Key = key;
            Value = value;
            Parent = null;
            LeftChildren = null;
            RightChildren = null;
            Weigth = 0;
        }
    }
}

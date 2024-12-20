using System.Collections.Generic;
using System.Linq;

namespace MyClasses
{
    internal class NodeDict<Tkey, TValue>
    {
        public Tkey Key { get; set; }
        public TValue Value { get; set; }
        public NodeDict<Tkey, TValue> Parent { get; set; }
        public NodeDict<Tkey, TValue> LeftChildren { get; set; }
        public NodeDict<Tkey, TValue> RightChildren { get; set; }
        public NodeDict(Tkey key, TValue value)
        {
            Key = key;
            Value = value;
            Parent = null;
            LeftChildren = null;
            RightChildren = null;
        }
        public IEnumerable<KeyValuePair<Tkey, TValue>> GetEnumerable()
        {
            if (LeftChildren == null && RightChildren == null)
                return Enumerable.Repeat(new KeyValuePair<Tkey, TValue>(Key, Value), 1);
            else if (LeftChildren == null && RightChildren != null)
                return Enumerable.Repeat(new KeyValuePair<Tkey, TValue>(Key, Value), 1).Union(RightChildren.GetEnumerable());
            else if (LeftChildren != null && RightChildren == null)
                return LeftChildren.GetEnumerable().Append(new KeyValuePair<Tkey, TValue>(Key, Value));
            else
                return LeftChildren.GetEnumerable().Append(new KeyValuePair<Tkey, TValue>(Key, Value)).Union(RightChildren.GetEnumerable());
        }
    }
}

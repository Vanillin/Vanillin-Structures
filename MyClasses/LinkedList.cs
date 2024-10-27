using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyClasses
{
    public class LinkedList<T> : IEnumerable<T>
    {
        NodeLinkedList<T> head;
        NodeLinkedList<T> tail;
        int count;
        public int Count => count;

        public void AddLast(T value)
        {
            NodeLinkedList<T> node = new NodeLinkedList<T>(value);

            if (head == null)
                head = node;
            else
                tail.Next = node;

            tail = node;
            count++;
        }
        public void AddFirst(T data)
        {
            NodeLinkedList<T> node = new NodeLinkedList<T>(data);
            node.Next = head;
            head = node;
            if (count == 0)
                tail = head;
            count++;
        }
        public void Remove(T value)
        {
            if (count == 0)
            {
                return;
            }

            NodeLinkedList<T> current = head;
            NodeLinkedList<T> previous = null;

            while (current != null && current.Value != null)
            {
                if (current.Value.Equals(value))
                {
                    if (previous != null)
                    {
                        previous.Next = current.Next;

                        if (current.Next == null)
                            tail = previous;
                    }
                    else
                    {
                        head = head.Next;

                        if (head == null)
                            tail = null;
                    }
                }
                previous = current;
                current = current.Next;
            }
            count--;
        }
        public void Clear()
        {
            head = null;
            tail = null;
            count = 0;
        }
        public bool Contains(T value)
        {
            NodeLinkedList<T> current = head;
            while (current != null && current.Value != null)
            {
                if (current.Value.Equals(value)) return true;
                current = current.Next;
            }
            return false;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            NodeLinkedList<T> current = head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            NodeLinkedList<T> current = head;
            while (current != null)
            {
                yield return current.Value;
                current = current.Next;
            }
        }
    }
}

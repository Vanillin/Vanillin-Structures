using System;
using System.Collections.Generic;

namespace MyClasses
{
    public class AVLTree<TKey, TValue> where TKey : IComparable<TKey>
    {
        private enum Where
        {
            Left, Rigth
        }
        NodeAVLTree<TKey, TValue> root;
        int count;

        public TValue this[TKey key]
        {
            get
            {
                var current = Find(key);
                if (current != null)
                    return current.Value;
                else
                    throw new KeyNotFoundException();
            }
            set
            {
                var current = Find(key);
                if (current != null)
                    current.Value = value;
                else
                    throw new KeyNotFoundException();
            }
        }
        public int Count => count;
        public AVLTree()
        {
            root = null;
            count = 0;
        }
        private NodeAVLTree<TKey, TValue> Find(TKey key)
        {
            if (root == null) return null;

            var current = root;
            while (current != null)
            {
                if (current.Key.CompareTo(key) > 0)
                    current = current.LeftChildren;
                else if (current.Key.CompareTo(key) < 0)
                    current = current.RightChildren;
                else return current;
            }
            return null;
        }
        public bool ContainsKey(TKey key)
        {
            var current = Find(key);
            if (current != null)
                return true;
            return false;
        }
        public void Add(TKey key, TValue value)
        {
            var newNode = new NodeAVLTree<TKey, TValue>(key, value);
            if (root == null)
            {
                root = newNode;
                count++;
                return;
            }
            var current = root;
            while (current != null)
            {
                if (current.Key.CompareTo(key) > 0)
                {
                    if (current.LeftChildren == null)
                    {
                        current.LeftChildren = newNode;
                        newNode.Parent = current;
                        count++;
                        NodeAVLTree<TKey, TValue> fromBalance;
                        ChangeWeigthFromNodeToRoot(current, out fromBalance);
                        if (fromBalance != null)
                            BalanceTreeAroundNode(fromBalance);
                        return;
                    }
                    current = current.LeftChildren;
                }
                else if (current.Key.CompareTo(key) < 0)
                {
                    if (current.RightChildren == null)
                    {
                        current.RightChildren = newNode;
                        newNode.Parent = current;
                        count++;
                        NodeAVLTree<TKey, TValue> fromBalance;
                        ChangeWeigthFromNodeToRoot(current, out fromBalance);
                        if (fromBalance != null)
                            BalanceTreeAroundNode(fromBalance);
                        return;
                    }
                    current = current.RightChildren;
                }
                else throw new ArgumentException();
            }
        }
        public void Remove(TKey key)
        {
            var current = Find(key);
            if (current == null)
                throw new KeyNotFoundException();

            count--;
            if (current.LeftChildren == null && current.RightChildren == null) //нет потомков
            {
                NodeAVLTree<TKey, TValue> parent = current.Parent;
                if (parent == null)
                    root = null;
                else
                    if (parent.LeftChildren == current)
                    parent.LeftChildren = null;
                else
                    parent.RightChildren = null;

                ChangeWeigthFromNodeToRoot(parent, out NodeAVLTree<TKey, TValue> fromBalance);
                if (fromBalance != null)
                    BalanceTreeAroundNode(fromBalance);
            }
            else if (current.RightChildren != null && current.LeftChildren != null) //оба потомка
            {
                NodeAVLTree<TKey, TValue> VecB = current.RightChildren;
                while (VecB.LeftChildren != null)
                    VecB = VecB.LeftChildren;

                TKey memoryKey = VecB.Key;
                TValue memoryValue = VecB.Value;

                Remove(memoryKey);

                current.Key = memoryKey;
                current.Value = memoryValue;
            }
            else if (current.LeftChildren != null) //левый потомок
            {
                NodeAVLTree<TKey, TValue> left = current.LeftChildren;
                NodeAVLTree<TKey, TValue> parent = current.Parent;
                if (parent == null)
                    root = left;
                else
                    if (parent.LeftChildren == current)
                    parent.LeftChildren = left;
                else
                    parent.RightChildren = left;
                left.Parent = parent;

                ChangeWeigthFromNodeToRoot(parent, out NodeAVLTree<TKey, TValue> fromBalance);
                if (fromBalance != null)
                    BalanceTreeAroundNode(fromBalance);
            }
            else //правый потомок
            {
                NodeAVLTree<TKey, TValue> right = current.RightChildren;
                NodeAVLTree<TKey, TValue> parent = current.Parent;
                if (parent == null)
                    root = right;
                else
                    if (parent.LeftChildren == current)
                    parent.LeftChildren = right;
                else
                    parent.RightChildren = right;
                right.Parent = parent;

                ChangeWeigthFromNodeToRoot(parent, out NodeAVLTree<TKey, TValue> fromBalance);
                if (fromBalance != null)
                    BalanceTreeAroundNode(fromBalance);
            }
        }
        private void ChangeWeigthFromNodeToRoot(NodeAVLTree<TKey, TValue> current, out NodeAVLTree<TKey, TValue> fromBalance)
        {
            fromBalance = null;
            while (current != null)
            {
                int left = -1;
                int right = -1;
                if (current.LeftChildren != null) left = current.LeftChildren.Weigth;
                if (current.RightChildren != null) right = current.RightChildren.Weigth;

                current.Weigth = Math.Max(left, right) + 1;
                if (fromBalance == null && (left - right >= 2 || right - left >= 2))
                    fromBalance = current;
                current = current.Parent;
            }
        }

        //                   Parent
        //                    VecA
        //      VecB           /      VecAR
        // VecBL   /    VecC
        //        VecD / VecE
        private void BalanceTreeAroundNode(NodeAVLTree<TKey, TValue> VecA)
        {
            Where? whoChildren = null;
            bool parentNull = false;
            NodeAVLTree<TKey, TValue> parent = VecA.Parent;
            if (parent == null)
                parentNull = true;
            else
                if (parent.LeftChildren == VecA)
                whoChildren = Where.Left;
            else
                whoChildren = Where.Rigth;

            int leftWeigth = -1;
            int rightWeigth = -1;
            if (VecA.LeftChildren != null) leftWeigth = VecA.LeftChildren.Weigth;
            if (VecA.RightChildren != null) rightWeigth = VecA.RightChildren.Weigth;

            if (leftWeigth - rightWeigth >= 2) //first Left
            {
                NodeAVLTree<TKey, TValue> VecB = VecA.LeftChildren;
                NodeAVLTree<TKey, TValue> VecBLeft = VecB.LeftChildren;
                NodeAVLTree<TKey, TValue> VecC = VecB.RightChildren;

                int leftWeigthSecond = -1;
                int rightWeigthSecond = -1;
                if (VecBLeft != null) leftWeigthSecond = VecBLeft.Weigth;
                if (VecC != null) rightWeigthSecond = VecC.Weigth;

                if (leftWeigthSecond >= rightWeigthSecond) //second left
                {
                    BalanceTreeAroundNodeVersionLL(parentNull, whoChildren, parent, VecA, VecB, VecC);
                }
                else //second right
                {
                    BalanceTreeAroundNodeVersionLR(parentNull, whoChildren, parent, VecA, VecB, VecC);
                }
            }
            else if (rightWeigth - leftWeigth >= 2) //first Right
            {
                NodeAVLTree<TKey, TValue> VecB = VecA.RightChildren;
                NodeAVLTree<TKey, TValue> VecBRight = VecB.RightChildren;
                NodeAVLTree<TKey, TValue> VecC = VecB.LeftChildren;

                int leftWeigthSecond = -1;
                int rightWeigthSecond = -1;
                if (VecBRight != null) rightWeigthSecond = VecBRight.Weigth;
                if (VecC != null) leftWeigthSecond = VecC.Weigth;

                if (leftWeigthSecond >= rightWeigthSecond) //second left
                {
                    BalanceTreeAroundNodeVersionRL(parentNull, whoChildren, parent, VecA, VecB, VecC);
                }
                else  //second right
                {
                    BalanceTreeAroundNodeVersionRR(parentNull, whoChildren, parent, VecA, VecB, VecC);
                }
            }
        }
        private void BalanceTreeAroundNodeVersionLL(bool parentNull, Where? whoChildren,
            NodeAVLTree<TKey, TValue> parent, NodeAVLTree<TKey, TValue> VecA, NodeAVLTree<TKey, TValue> VecB, NodeAVLTree<TKey, TValue> VecC)
        {
            if (parentNull)
                root = VecB;
            else
                if (whoChildren == Where.Left)
                parent.LeftChildren = VecB;
            else
                parent.RightChildren = VecB;
            VecB.Parent = parent;

            VecB.RightChildren = VecA;
            VecA.Parent = VecB;

            VecA.LeftChildren = VecC;
            if (VecC != null)
                VecC.Parent = VecA;

            //проверка на правильность
            ChangeWeigthFromNodeToRoot(VecA, out NodeAVLTree<TKey, TValue> fromBalance);
            if (fromBalance != null)
                BalanceTreeAroundNode(fromBalance);
        }
        private void BalanceTreeAroundNodeVersionLR(bool parentNull, Where? whoChildren,
            NodeAVLTree<TKey, TValue> parent, NodeAVLTree<TKey, TValue> VecA, NodeAVLTree<TKey, TValue> VecB, NodeAVLTree<TKey, TValue> VecC)
        {
            NodeAVLTree<TKey, TValue> VecD = VecC.LeftChildren;
            NodeAVLTree<TKey, TValue> VecE = VecC.RightChildren;

            if (parentNull)
                root = VecC;
            else
                if (whoChildren == Where.Left)
                parent.LeftChildren = VecC;
            else
                parent.RightChildren = VecC;
            VecC.Parent = parent;

            VecC.LeftChildren = VecB;
            VecB.Parent = VecC;
            VecC.RightChildren = VecA;
            VecA.Parent = VecC;

            VecB.RightChildren = VecD;
            if (VecD != null)
                VecD.Parent = VecB;

            VecA.LeftChildren = VecE;
            if (VecE != null)
                VecE.Parent = VecA;

            //проверка на правильность
            int left1 = -1;
            int right1 = -1;
            if (VecA.LeftChildren != null) left1 = VecA.LeftChildren.Weigth;
            if (VecA.RightChildren != null) right1 = VecA.RightChildren.Weigth;

            VecA.Weigth = Math.Max(left1, right1) + 1;

            int left2 = -1;
            int right2 = -1;
            if (VecB.LeftChildren != null) left2 = VecB.LeftChildren.Weigth;
            if (VecB.RightChildren != null) right2 = VecB.RightChildren.Weigth;

            VecB.Weigth = Math.Max(left2, right2) + 1;

            BalanceTreeAroundNode(VecA);
            BalanceTreeAroundNode(VecB);

            ChangeWeigthFromNodeToRoot(VecC, out NodeAVLTree<TKey, TValue> fromBalance);
            if (fromBalance != null)
                BalanceTreeAroundNode(fromBalance);

        }
        private void BalanceTreeAroundNodeVersionRR(bool parentNull, Where? whoChildren,
            NodeAVLTree<TKey, TValue> parent, NodeAVLTree<TKey, TValue> VecA, NodeAVLTree<TKey, TValue> VecB, NodeAVLTree<TKey, TValue> VecC)
        {
            if (parentNull)
                root = VecB;
            else
                if (whoChildren == Where.Left)
                parent.LeftChildren = VecB;
            else
                parent.RightChildren = VecB;
            VecB.Parent = parent;

            VecB.LeftChildren = VecA;
            VecA.Parent = VecB;

            VecA.RightChildren = VecC;
            if (VecC != null)
                VecC.Parent = VecA;

            //проверка на правильность
            ChangeWeigthFromNodeToRoot(VecA, out NodeAVLTree<TKey, TValue> fromBalance);
            if (fromBalance != null)
                BalanceTreeAroundNode(fromBalance);
        }
        private void BalanceTreeAroundNodeVersionRL(bool parentNull, Where? whoChildren,
            NodeAVLTree<TKey, TValue> parent, NodeAVLTree<TKey, TValue> VecA, NodeAVLTree<TKey, TValue> VecB, NodeAVLTree<TKey, TValue> VecC)
        {
            NodeAVLTree<TKey, TValue> VecD = VecC.RightChildren;
            NodeAVLTree<TKey, TValue> VecE = VecC.LeftChildren;

            if (parentNull)
                root = VecC;
            else
                if (whoChildren == Where.Left)
                parent.LeftChildren = VecC;
            else
                parent.RightChildren = VecC;
            VecC.Parent = parent;

            VecC.RightChildren = VecB;
            VecB.Parent = VecC;
            VecC.LeftChildren = VecA;
            VecA.Parent = VecC;

            VecB.LeftChildren = VecD;
            if (VecD != null)
                VecD.Parent = VecB;

            VecA.RightChildren = VecE;
            if (VecE != null)
                VecE.Parent = VecA;

            //проверка на правильность
            int left1 = -1;
            int right1 = -1;
            if (VecA.LeftChildren != null) left1 = VecA.LeftChildren.Weigth;
            if (VecA.RightChildren != null) right1 = VecA.RightChildren.Weigth;

            VecA.Weigth = Math.Max(left1, right1) + 1;

            int left2 = -1;
            int right2 = -1;
            if (VecB.LeftChildren != null) left2 = VecB.LeftChildren.Weigth;
            if (VecB.RightChildren != null) right2 = VecB.RightChildren.Weigth;

            VecB.Weigth = Math.Max(left2, right2) + 1;

            BalanceTreeAroundNode(VecA);
            BalanceTreeAroundNode(VecB);

            ChangeWeigthFromNodeToRoot(VecC, out NodeAVLTree<TKey, TValue> fromBalance);
            if (fromBalance != null)
                BalanceTreeAroundNode(fromBalance);

        }
    }
}

using System;
using System.Collections.Generic;

namespace BTree
{
    public class Node<T> where T : IComparable<T>
    {
        public List<T> Values { get; } = new List<T>(3);


        public bool isLeafNode => Children.Count == 0;
        public List<Node<T>> Children { get; } = new List<Node<T>>(3);

        public Node(T value)
        {
            Values.Add(value);
        }

        public void SwapValues()
        {
            if (Values[0].CompareTo(Values[1]) <= 0) return;

            (Values[0], Values[1]) = (Values[1], Values[0]);
        }

        public override string ToString()
        {
            string vals = "";
            foreach (var item in Values)
            {
                vals += $"{item}, ";
            }
            return $"keys: {vals}";
        }

    }


    public class BTree<T> where T : IComparable<T>
    {
        public Node<T> rootNode;
        public int Count { get; private set; }

        const int maxDegree = 3;
        public BTree()
        {
            Count = 0;
            rootNode = null;
        }

        public Node<T> Find(T value, bool returnLastCheckedNodeOrNull)
        {
            var curr = rootNode;

            do
            {
                bool shouldContinue = false;
                for (int i = 0; i < curr.Values.Count; i++)
                {
                    if (curr.Values[i].CompareTo(value) == 0) return curr;

                    if (curr.Values[i].CompareTo(value) > 0)
                    {
                        if (curr.isLeafNode)
                        {
                            if (!returnLastCheckedNodeOrNull)
                            {
                                return null;
                            }
                            else
                            {
                                return curr;
                            }
                        }

                        curr = curr.Children[i];
                        shouldContinue = true;
                        break;
                    }
                }
                if (shouldContinue) { continue; }
                if (curr.isLeafNode)
                {
                    if (!returnLastCheckedNodeOrNull)
                    {
                        return null;
                    }
                    else
                    {
                        return curr;
                    }
                }
                curr = curr.Children[curr.Children.Count - 1];
            } while (true);
        }

        public Node<T> FindParent(T value, bool returnLastCheckedNodeOrNull)
        {
            var curr = rootNode;
            var prev = rootNode;
            do
            {
                bool shouldContinue = false;
                for (int i = 0; i < curr.Values.Count; i++)
                {
                    if (curr.Values[i].CompareTo(value) == 0) return prev;

                    if (curr.Values[i].CompareTo(value) > 0)
                    {
                        if (curr.isLeafNode)
                        {
                            if (!returnLastCheckedNodeOrNull)
                            {
                                return null;
                            }
                            else
                            {
                                return prev;
                            }
                        }
                        curr = curr.Children[i];
                        shouldContinue = true;
                        break;
                    }
                }
                if (shouldContinue) { continue; }
                if (curr.isLeafNode)
                {
                    if (!returnLastCheckedNodeOrNull)
                    {
                        return null;
                    }
                    else
                    {
                        return prev;
                    }
                }
                prev = curr;
                curr = curr.Children[curr.Children.Count - 1];
            } while (true);
        }

        public Node<T> AddNode(T value)
        {
            var NodeToAddChildTo = FindParent(value, true);
            var AddToNode = Find(value, true);

            for (int i = 0; i < NodeToAddChildTo.Children.Count; i++)
            {
                if (NodeToAddChildTo.Children[i].Values.Equals(AddToNode.Values))
                {
                    NodeToAddChildTo.Children[i].Values.Add(value);
                    return AddToNode;
                }
            }
            NodeToAddChildTo.Children.Add(new Node<T>(value));
            return Find(value, true);
        }

        public void Split(Node<T> parent, Node<T> child, int childIndex)
        {
            if (child.Values.Count >= maxDegree)
            {
                parent.Values.Insert(childIndex, child.Values[1]);
                child.Values.RemoveAt(1);
                var ChildrenofChildren = child.Children;
                var newChild1 = new Node<T>(child.Values[0]);
                var newChild2 = new Node<T>(child.Values[1]);
                if (ChildrenofChildren.Count > 0)
                {
                    newChild1.Children.Add(ChildrenofChildren[0]);
                    newChild1.Children.Add(ChildrenofChildren[1]);
                    newChild2.Children.Add(ChildrenofChildren[2]);
                    newChild2.Children.Add(ChildrenofChildren[3]);
                }
                parent.Children.RemoveAt(childIndex);
                parent.Children.Insert(childIndex, newChild1);
                parent.Children.Insert(childIndex + 1, newChild2);
            }
        }


        public void SplitRoot()
        {
            if (rootNode.Values.Count >= maxDegree)
            {
                var temp = rootNode.Values[1];
                var newRoot = new Node<T>(temp);
                var ChildrenofChildren = rootNode.Children;
                var newChild1 = new Node<T>(rootNode.Values[0]);
                var newChild2 = new Node<T>(rootNode.Values[2]);
                newRoot.Children.Add(newChild1);
                newRoot.Children.Add(newChild2);
                if (ChildrenofChildren.Count > 0)
                {
                    newChild1.Children.Add(ChildrenofChildren[0]);
                    newChild1.Children.Add(ChildrenofChildren[1]);
                    newChild2.Children.Add(ChildrenofChildren[2]);
                    newChild2.Children.Add(ChildrenofChildren[3]);
                }
                rootNode.Values.Remove(temp);
                rootNode = newRoot;
                Count++;
            }
        }

        public void Add(T value)
        {
            if (rootNode == null)
            {
                rootNode = new Node<T>(value);
                Count++;
                return;
            }
            Add(value, rootNode);
            SplitRoot();
        }

        private void Add(T value, Node<T> curr)
        {
            void Insert(int i)
            {
                if (curr.Children.Count == 0)
                {
                    curr.Values.Insert(i, value);
                    Count++;
                    return;
                }
                Add(value, curr.Children[i]);
                Split(curr, curr.Children[i], i);
            }

            for (int i = 0; i < curr.Values.Count; i++)
            {
                if (curr.Values[i].CompareTo(value) < 0)
                {
                    Insert(i);
                    return;
                }
            }
            Insert(curr.Values.Count);
            return;

        }
    }
}

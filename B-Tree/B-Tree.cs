using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private void SwapValuesIfNeeded()
        {
            if (Values[0].CompareTo(Values[1]) <= 0) return;

            (Values[0], Values[1]) = (Values[1], Values[0]);
        }


        //public Node(Node<T> twoNodeToUpgrade, T valueToAdd)
        //{
        //    if (twoNodeToUpgrade.Values.Count != 1) throw new InvalidOperationException("bug"); 

        //    Values = new List<T> { twoNodeToUpgrade.Values[0], valueToAdd };
        //    SwapValuesIfNeeded();
        //}
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

      


        public void Add(T value)
        {
            if (rootNode == null)
            {
                rootNode = new Node<T>(value);
                Count++;
                return;
            }

            var insertionNode = Find(value, true);
            insertionNode.Values.Add(value);
            insertionNode.Values.Sort();

            if(insertionNode.Values.Count == maxDegree)
            {
                //return tuple from function & need to find parent from find function
                // or need parent links instead
            }


            // The node might now have maxDegree values
            // time to split it!
            // Middle value is given up to parent



        }

    }
}

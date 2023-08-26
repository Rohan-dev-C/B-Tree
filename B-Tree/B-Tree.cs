


using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

        public void SwapValues()
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

        public Node<T> AddNode(T value, ref BTree<T> tree) // check balance 
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
        public Node<T> Add(T value)
        {
            if (rootNode == null)
            {
                rootNode = new Node<T>(value);
                Count++;
                return rootNode;
            }

            var FoundNode = this.Find(value, true);

            var insertedNode= Add(value);
            for (int i = 0; i < insertedNode.Children.Count; i++)
            {
                if (insertedNode.Children.Count >= maxDegree - 1)
                {
                    var temp = insertedNode.Children[i].Values[1];
                    insertedNode.Children[i].Values.Remove(temp); 
                    insertedNode.Values.Add(temp);
                }
                
            }
            return insertedNode; 
        }

             
            //if(insertionNode.Values.Count == maxDegree)
            //{  

            //     //return tuple from function & need to find parent from find function
            //    // or need parent links instead
            //}



            // The node might now have maxDegree values
            // then split
            // Middle value is given up to parent
             



    }
}

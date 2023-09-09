


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

        public Node<T> AddNode(T value) // check balance 
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

        public void Split(Node<T> node, Node<T> child)
        {
            var parent = node; 
            
            if (child.Values.Count >= maxDegree)
            {
                parent.Values.Add(child.Values[1]);
                child.Values.RemoveAt(1);
                var ChildrenofChildren = child.Children; 
                var newChild1 = new Node<T>(child.Values[0]);
                var newChild2 = new Node<T>(child.Values[1]); 
                parent.Children.Remove(child); 
                parent.Children.Add(newChild1); 
                parent.Children.Add(newChild2);
                if (ChildrenofChildren.Count > 0)
                {
                    newChild1.Children.Add(ChildrenofChildren[0]);
                    newChild2.Children.Add(ChildrenofChildren[1]);
                }
            }
        }
        
        public void SplitRoot()
        {
            if (rootNode.Values.Count >= maxDegree)
            {
                var temp = rootNode.Values[1];
                var newRoot = new Node<T>(rootNode.Values[1]);
                Split(newRoot, rootNode);
                rootNode = newRoot;
                rootNode.Values.Remove(temp);
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
            for (int i = 0; i < curr.Values.Count; i++)
            {
                if (curr.Values[i].CompareTo(value) < 0) 
                {
                    if (curr.Children.Count == 0)
                    {
                        curr.Values.Insert(i, value);
                        Count++; 
                        return;
                    }
                    Add(value, curr.Children[i]);
                    Split(curr, curr.Children[i]); 
                }
            }
        }

        //public Node<T> Add(T value)
        //{
        //    if (rootNode == null)
        //    {
        //        rootNode = new Node<T>(value);
        //        Count++;
        //        return rootNode;
        //    }

        //    var FoundNode = this.Find(value, true);

        //    var insertedNode = AddNode(FoundNode); 
        //    for (int i = 0; i < insertedNode.Children.Count; i++)
        //    {
        //        if (insertedNode.Children.Count >= maxDegree - 1)
        //        {
        //            var temp = insertedNode.Children[i].Values[1];
        //            insertedNode.Children[i].Values.Remove(temp); 
        //            insertedNode.Values.Add(temp);
        //        }

        //    }
        //    return insertedNode; 

        //}


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

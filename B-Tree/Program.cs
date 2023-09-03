using System;

namespace BTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BTree<int> tree = new BTree<int>();

            //tree.Add(1);
            //tree.Add(2);
            //tree.Add(3);
            //tree.Add(4);
            //tree.Add(5);
            //tree.Add(6);
            //tree.Add(7);
            //tree.Add(8);
            //tree.Add(9);

            tree.AddNode(1);
            tree.AddNode(2); 
            tree.AddNode(3); 
            tree.AddNode(4); 

            //tree.rootNode = new Node<int>(4);
            //tree.rootNode.Values.Add(8);
            //tree.rootNode.Children.Add(new Node<int>(2));
            //tree.rootNode.Children.Add(new Node<int>(6));
            //tree.rootNode.Children.Add(new Node<int>(10));
            //tree.rootNode.Children[0].Children.Add(new Node<int>(1));
            //tree.rootNode.Children[0].Children.Add(new Node<int> (3));
            //tree.rootNode.Children[1].Children.Add(new Node<int> (5));
            //tree.rootNode.Children[1].Children.Add(new Node<int> (7));
            //tree.rootNode.Children[2].Children.Add(new Node<int> (9));
            //tree.rootNode.Children[2].Children.Add(new Node<int> (11));





            //tree.Add(5);
            //tree.Add(6);
            ;

            var isPresent = tree.Find(11, true);
            var findParent = tree.FindParent(11, true);
           var addedNotes = tree.AddNode(12, ref tree);
            ;
        }
    }
}

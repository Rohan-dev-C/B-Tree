using System;

namespace BTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BTree<int> tree = new BTree<int>();

            Random random = new Random(45);
            var amount = random.Next(6, 21);
            ;
            for (int i = 0; i < amount; i++)
            {
                tree.Add(random.Next(0, 100)); 
            }
          

            //tree.Add(5);
            //tree.Add(6);
            ;

            var isPresent = tree.Find(11, true);
            var findParent = tree.FindParent(11, true);
           var addedNotes = tree.AddNode(12);
            ;
        }
    }
}

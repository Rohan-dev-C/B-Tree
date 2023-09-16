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
          
            ;
        }
    }
}

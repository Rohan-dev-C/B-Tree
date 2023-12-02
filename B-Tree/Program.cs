using System;

namespace BTree
{
    class Program
    {
        static void Main(string[] args)
        {
            BTree<int> tree = new BTree<int>();

            Random random = new Random(55);
            var amount = random.Next(6, 21);
            var values = new int[amount];

            for (int i = 0; i < amount; i++)
            {
                values[i] = random.Next(0, 100);
            }
            for (int i = 0; i < amount; i++)
            {
                tree.Add(values[i]); 
            }
            // 6 breaks it
            ;
        }
    }
}

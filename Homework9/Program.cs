using Laba18;

namespace Homework9
{
    internal class Program
    {
        static void Main()
        {
            TreeUtils.PrintTreePostfix(TreeUtils.GetSampleIntTree2());
            Console.WriteLine(BinarySearchTree.Min(TreeUtils.GetSampleIntTree1()));
            Console.WriteLine(BinarySearchTree.Max(TreeUtils.GetSampleIntTree1()));
            Console.WriteLine(BinarySearchTree.SumNMinTreeNum(TreeUtils.GetSampleIntTree1(), 2));
            foreach (var x in BinarySearchTree.ToSortedArray(TreeUtils.GetSampleIntTree1()))
                Console.Write(x + " ");
        }
    }
}
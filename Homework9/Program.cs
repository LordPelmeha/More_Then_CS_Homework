using Laba18;

namespace Homework9
{
    internal class Program
    {
        static void Main()
        {
            TreeUtils.PrintTreePostfix(TreeUtils.GetSampleIntTree2());
            Console.WriteLine(BinarySearchTree.FindMinAndMax(TreeUtils.GetSampleIntTree1()));
        }
    }
}
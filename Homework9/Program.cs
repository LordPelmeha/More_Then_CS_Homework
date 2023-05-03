using Laba18;

namespace Homework9
{
    internal class Program
    {
        static void Main()
        {
            var b = TreeUtils.GetSampleIntTree1();
            TreeUtils.PrintTreePostfix(b);
            var a = b.Copy();
            TreeUtils.PrintTreePostfix(a);
            b = TreeUtils.GetSampleIntTree2();
            TreeUtils.PrintTreePostfix(b);
            TreeUtils.PrintTreePostfix(a);
            Console.WriteLine(TreeUtils.LeafSum(a));
            Console.WriteLine(TreeUtils.LevelWidth(a, 1));
            Console.WriteLine(TreeUtils.IsTreeSum(
                new TreeNode<int>(26,
                new TreeNode<int>(10,
                    new TreeNode<int>(4),
                    new TreeNode<int>(6)
                ),
                new TreeNode<int>(3,
                    right: new TreeNode<int>(3)
                )
            )));
            TreeUtils.PrintTreePostfix(TreeUtils.GetSampleIntTree2());
            Console.WriteLine(BinarySearchTree.Min(TreeUtils.GetSampleIntTree1()));
            Console.WriteLine(BinarySearchTree.Max(TreeUtils.GetSampleIntTree1()));
            Console.WriteLine(BinarySearchTree.SumNMinTreeNum(TreeUtils.GetSampleIntTree1(), 2));
            foreach (var x in BinarySearchTree.ToSortedArray(TreeUtils.GetSampleIntTree1()))
                Console.Write(x + " ");
        }
    }
}
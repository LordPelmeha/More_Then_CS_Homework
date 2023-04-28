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
        }
    }
}
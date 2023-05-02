using Homework9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laba18
{
    public class BinarySearchTree
    {
        public TreeNode<int> root;

        public BinarySearchTree(params int[] arr)
        {
            foreach (var x in arr)
            {
                AddToBST(ref root, x);
            }
        }
        public void PrintTreeInfix()
        {
            TreeUtils.PrintTreeInfix(root);
        }

        static void AddToBST(ref TreeNode<int> r, int x)
        {
            if (r == null)
                r = new TreeNode<int>(x, null, null);
            else if (x < r.Data)
                AddToBST(ref r.Left, x);
            else if (x > r.Data)
                AddToBST(ref r.Right, x);
        }

        public bool Contains(int n)
        {
            var currentNode = root;
            while (currentNode != null)
            {
                if (n == currentNode.Data)
                    return true;
                else if (n < currentNode.Data)
                    currentNode = currentNode.Left;
                else
                    currentNode = currentNode.Right;
            }
            return false;
        }
        public static (int, int) FindMinAndMax(TreeNode<int> root)
        {
            int min = int.MaxValue;
            int max = int.MinValue;
            void Find(TreeNode<int> root, ref int min, ref int max)
            {
                if (root == null)
                    return;
                if (root.Data < min)
                    min = root.Data;
                if (root.Data > max)
                    max = root.Data;
                Find(root.Left, ref min, ref max);
                Find(root.Right, ref max, ref min);
            }
            Find(root, ref min, ref max);
            return (min, max);
        }
    }
}

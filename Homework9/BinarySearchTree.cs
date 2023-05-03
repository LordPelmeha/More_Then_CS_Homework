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
        /// <summary>
        /// Находит минимальный И максимальный элемент в дереве. 
        /// По ошибке сделал так, что находит и то, и то
        /// </summary>
        private static (int, int) FindMinAndMax(TreeNode<int> root)
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

                Find(root.Right, ref min, ref max);
            }
            Find(root, ref min, ref max);
            return (min, max);
        }
        /// <summary>
        ///  Находит минимальный в дереве
        /// </summary>
        public static int Min(TreeNode<int> root) => FindMinAndMax(root).Item1;
        /// <summary>
        ///  Находит максимальный элемент в дереве
        /// </summary>
        public static int Max(TreeNode<int> root) => FindMinAndMax(root).Item2;
        /// <summary>
        /// Добавляет элемент в дерево
        /// </summary>
        private static void AddToTree(TreeNode<int> root, ref TreeNode<int> bst)
        {
            if (root == null)
                return;
            AddToBST(ref bst, root.Data);
            AddToTree(root.Left, ref bst);
            AddToTree(root.Right, ref bst);
        }
        /// <summary>
        /// Добавляет эелементы дерева в список
        /// </summary>
        private static void AddToList(TreeNode<int> root, ref List<int> lst)
        {
            if (root == null)
                return;
            AddToList(root.Left, ref lst);
            lst.Add(root.Data);
            AddToList(root.Right, ref lst);

        }
        /// <summary>
        /// Находит сумму N минимальных элементов дерева
        /// </summary>
        public static int SumNMinTreeNum(TreeNode<int> root, int n)
        {
            var bst = new BinarySearchTree();
            var lst = new List<int>();
            AddToTree(root, ref bst.root);
            AddToList(bst.root, ref lst);
            return lst.Take(n).Sum();
        }
        /// <summary>
        /// Возвращает массив, состоящий из всех значений дерева по возрастсанию
        /// </summary>
        public static int[] ToSortedArray(TreeNode<int> root)
        {
            var lst = new List<int>();
            var bst = new BinarySearchTree();
            AddToTree(root, ref bst.root);
            AddToList(bst.root, ref lst);
            return lst.ToArray();
        }
    }
}

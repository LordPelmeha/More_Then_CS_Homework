﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Homework9
{
    public static class TreeUtils
    {
        private static Random _random = new();

        /// <summary>
        /// Печатает дерево инфиксным обходом. Если дерево пустое, выводится &lt;empty tree&gt;
        /// </summary>
        /// <param name="root"></param>
        public static void PrintTreeInfix<T>(TreeNode<T>? root)
        {
            if (root == null)
                Console.WriteLine("<empty tree>");
            PrintNodeInfix(root);
            Console.WriteLine();

            void PrintNodeInfix(TreeNode<T>? node)
            {
                if (node == null)
                    return;
                PrintNodeInfix(node.Left);
                Console.Write($"{node.Data} ");
                PrintNodeInfix(node.Right);
            }
        }
        /// <summary>
        /// Печатает дерево постфиксным обходом. Если дерево пустое, выводится &lt;empty tree&gt;
        /// </summary>
        public static void PrintTreePostfix<T>(TreeNode<T>? root)
        {
            if (root == null)
                Console.WriteLine("<empty tree>");
            PrintNodeInfix(root);
            Console.WriteLine();

            void PrintNodeInfix(TreeNode<T>? node)
            {
                if (node == null)
                    return;

                PrintNodeInfix(node.Left);
                PrintNodeInfix(node.Right);
                Console.Write($"{node.Data} ");
            }
        }


        /// <summary>
        /// Создаёт рандомное бинарное дерево.
        /// </summary>
        /// <param name="count">Количество узлов в дереве</param>
        /// <param name="minVal">Левая граница значений узлов</param>
        /// <param name="maxVal">Правая граница значений узлов (не включая)</param>
        public static TreeNode<int>? GetRandomTree(int count, int minVal = -10, int maxVal = 10)
        {
            if (count < 0)
                throw new ArgumentException("Count must be >= 0.");
            if (minVal > maxVal)
                throw new ArgumentException("Min value must be greater than or equal to Max value.");
            if (count == 0)
                return null;
            var leftNodesCount = _random.Next(0, count - 1);
            return new TreeNode<int>(_random.Next(minVal, maxVal),
                GetRandomTree(leftNodesCount, minVal, maxVal),
                GetRandomTree(count - leftNodesCount - 1, minVal, maxVal));
        }

        /// <summary>
        /// Создаёт бинарное дерево из 6 целых чисел
        /// </summary>
        ///      7
        ///    /   \
        ///   -6    32
        ///  /  \     \
        /// 0   11     -5
        public static TreeNode<int> GetSampleIntTree1()
        {
            return new TreeNode<int>(7,
                new TreeNode<int>(-6,
                    new TreeNode<int>(0),
                    new TreeNode<int>(11)
                ),
                new TreeNode<int>(32,
                    right: new TreeNode<int>(-5)
                )
            );
        }

        /// <summary>
        /// Создаёт бинарное дерево из 5 первых натуральных чисел
        /// </summary>
        ///      1
        ///    /   \
        ///   2     3
        ///  	   / \
        ///       4   5
        public static TreeNode<int> GetSampleIntTree2()
        {
            return new TreeNode<int>(1,
                new TreeNode<int>(2),
                new TreeNode<int>(3,
                    new TreeNode<int>(4),
                    new TreeNode<int>(5)
                )
            );
        }

        /// <summary>
        /// Создаёт бинарное дерево из 4 целых чисел,
        /// расположенных в левой крайней ветви
        /// </summary>
        ///       -1001
        ///       /   
        ///     999   
        ///     /  
        ///    0
        ///   / 
        /// -57 
        public static TreeNode<int> GetSampleIntTree3()
        {
            return new TreeNode<int>(-1001,
                new TreeNode<int>(999,
                    new TreeNode<int>(0,
                        new TreeNode<int>(-57)
                    )
                )
            );
        }

        /// <summary>
        /// Создаёт бинарное дерево из 3 целых чисел,
        /// расположенных в правой крайней ветви
        /// </summary>
        ///      3
        ///    	  \
        ///  	   5
        ///  	    \ 
        ///      	 8 
        public static TreeNode<int> GetSampleIntTree4()
        {
            return new TreeNode<int>(3,
                right: new TreeNode<int>(5,
                    right: new TreeNode<int>(8)
                )
            );
        }


        /// <summary>
        /// Создаёт бинарное дерево из 6 целых чисел,
        /// расположенных в крайних ветвях
        /// </summary>
        ///      18
        ///    	/  \
        ///   -20  35
        ///   /	     \ 
        /// -75       66
        ///	           \
        ///				94
        public static TreeNode<int> GetSampleIntTree5()
        {
            return new TreeNode<int>(18,
                new TreeNode<int>(-20,
                    new TreeNode<int>(-75)
                ),
                new TreeNode<int>(35,
                    right: new TreeNode<int>(66,
                        right: new TreeNode<int>(94)
                    )
                )
            );
        }
        /// <summary>
        /// Находит сумму значений в листьях
        /// </summary>
        public static int LeafSum(TreeNode<int> root)
        {
            int s = 0;
            sum(root, ref s);
            void sum(TreeNode<int> r, ref int s)
            {
                if (r == null)
                    return;
                if (r.Left == null && r.Right == null)
                    s += r.Data;
                sum(r.Right, ref s);
                sum(r.Left, ref s);
            }
            return s;
        }
        /// <summary>
        /// Находит ширину уровня с заданным номером (номер не может быть отрицательным). Если дерево не содержит такого уровня, возвращает 0
        /// </summary>
        public static int LevelWidth(TreeNode<int> root, int level)
        {
            if (level < 0)
                throw new ArgumentException("Уровень не может быть отрицательным!");
            int count = 0;
            CountNodeOnLevel(root, level, ref count);
            void CountNodeOnLevel(TreeNode<int> r, int lvl, ref int count)
            {
                if (lvl == 0 && r != null)
                    ++count;
                --lvl;
                if (lvl < 0 || r == null)
                    return;
                CountNodeOnLevel(r.Left, lvl, ref count);
                CountNodeOnLevel(r.Right, lvl, ref count);
            }
            return count;
        }
        /// <summary>
        /// Назовём деревом сумм бинарное дерево, для которого выполняется правило: листья могут принимать 
        /// любые значения, в то время как значением других узлов является сумма значений всех узлов в обоих поддеревьях.
        /// Функция, принимает на вход бинарное дерево (то есть его корень) и определяет, является ли оно деревом сумм
        /// </summary>
        public static bool IsTreeSum(TreeNode<int> root)
        {
            void sum(TreeNode<int> root, ref int s)
            {
                if (root == null)
                    return;
                s += root.Data;
                sum(root.Left, ref s);
                sum(root.Right, ref s);
            }
            bool ChekNode(TreeNode<int> root)
            {
                if (root == null)
                    return true;
                int su = 0;
                sum(root, ref su);
                if (root.Data != su - root.Data && root.Left != null && root.Right != null)
                    return false;
                return ChekNode(root.Left) && ChekNode(root.Right);
            }
            return ChekNode(root);
        }
    }
}

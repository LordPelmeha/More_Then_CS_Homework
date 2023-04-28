using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Laba18
{
    /// <summary>
    /// Узел бинарного дерева
    /// </summary>
    public class TreeNode<T>
    {
        /// <summary>
        /// Поле данных
        /// </summary>
        public int Data;

        /// <summary>
        /// Левое поддерево
        /// </summary>
        public TreeNode<T>? Left;

        /// <summary>k
        /// Правое поддерево
        /// </summary>
        public TreeNode<T>? Right;

        /// <summary>
        /// Инициализирует узел бинарного дерева значением data поля данных
        /// и поддеревьями left, right
        /// </summary>
        /// <param name="data">Значение поля данных узла</param>
        /// <param name="left">Левое поддерево</param>
        /// <param name="right">Правое поддерево</param>
        public TreeNode(int data, TreeNode<T>? left = null, TreeNode<T>? right = null)
        {
            Data = data;
            Left = left;
            Right = right;
        }
        public TreeNode<T> Copy() => new TreeNode<T>(Data, Left, Right);
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework8
{

    public class AgileLinkedList<T>
    {
        public Node<T> First { get; private set; }
        public Node<T> Last { get; private set; }
        public int Count { get; private set; }

        public class Node<T>
        {
            public T Data { get; set; }
            public Node<T> Previous { get; set; }
            public Node<T> Next { get; set; }
            public Node(T Data, Node<T> Previous, Node<T> Next)
            {
                this.Data = Data;
                this.Previous = Previous;
                this.Next = Next;
            }
        }
        public AgileLinkedList(params T[] s)
        {
            foreach (var x in s)
                AddLast(x);
        }
        public override string ToString()
        {
            var s = new StringBuilder();
            var a = First;
            while (a != null)
            {
                s.Append($"{a.Data} ");
                a = a.Next;
            }
            return s.ToString();
        }
        public void AddFirst(T x)
        {
            var p = new Node<T>(x, null, First);
            if (First != null)
                First.Previous = p;
            First = p;
            if (Last == null)
                Last = p;
        }
        public void AddLast(T x)
        {
            var p = new Node<T>(x, Last, null);
            if (Last != null)
                Last.Next = p;
            Last = p;
            if (First == null)
                First = p;
        }
        public bool IsSimmetrical()
        {
            var f = First;
            var l = Last;
            while (l != null)
            {
                if (f.Data.ToString() != l.Data.ToString())
                    return false;
                f = f.Next;
                l = l.Previous;
            }
            return true;
        }
        public void RemoveFirst()
        {
            First = First.Next;
            if (First == null)
                Last = null;
            else First.Previous = null;
        }
        public void RemoveLast()
        {
            Last = Last.Previous;
            if (Last == null)
                First = null;
            else Last.Next = null;
        }
        public void Remove(int n)
        {
            if (n < 1)
                throw new ArgumentException("Вы вышли за пределы списка!");
            var p = First;
            for (int i = 0; i < n - 1; i++)
            {
                if (p == null)
                    throw new ArgumentException("Вы вышли за пределы списка!");
                p = p.Next;
            }
            if (p == First)
                RemoveFirst();
            else if (p == Last)
                RemoveLast();
            else
            {
                p.Next.Previous = p.Previous;
                p.Previous.Next = p.Next;
            }
        }
        public void AddAfterNPosition(int n, T x)
        {
            if (n < 1)
                throw new ArgumentException("Вы вышли за пределы списка!");
            var p = First;
            for (int i = 0; i < n - 1; i++)
            {
                if (p == null)
                    throw new ArgumentException("Вы вышли за пределы списка!");
                p = p.Next;
            }

            if (p == Last)
                AddLast(x);
            else
            {
                var p1 = new Node<T>(x, p, p.Next);
                p.Next = p1;
                p1.Next.Previous = p1;
            }
        }
        public void ShiftLeft(int n)
        {
            for (int i = 0; i < n; i++)
            {
                AddLast(First.Data);
                RemoveFirst();
            }
        }
    }
}

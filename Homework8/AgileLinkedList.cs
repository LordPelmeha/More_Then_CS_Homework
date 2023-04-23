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
                if (f != l)
                    return false;
                f = f.Next;
                l = l.Previous;
            }
            return true;
        }


    }
}

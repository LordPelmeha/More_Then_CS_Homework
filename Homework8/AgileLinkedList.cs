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

    }
}

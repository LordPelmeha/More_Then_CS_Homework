namespace Homework8
{
    internal class Program
    {
        static void Main()
        {
            var a = new AgileLinkedList<int>(4, 1, 2, 3);
            Console.WriteLine(a);
            var b = new AgileLinkedList<int>(4, 1, 4);
            Console.WriteLine(b.IsSimmetrical());
            b.Remove(2);
            Console.WriteLine(b);
            b.AddAfter(1, 7);
            Console.WriteLine(b);
        }
    }
}
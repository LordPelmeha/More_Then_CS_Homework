namespace Homework2
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Hello, World!");
        }

        public static double ProductAtoB(int x, int y)
        {
            double prod = 1;
            for (Int64 i = Math.Min(x, y) + Math.Min(x, y) % 2; i <= Math.Max(x, y) - Math.Max(x, y) % 2; i += 2) prod *= i;
            return prod;
        }
    }
}
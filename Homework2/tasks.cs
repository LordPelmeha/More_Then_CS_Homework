using System.Diagnostics.CodeAnalysis;
namespace Homework2
{
    public class Program
    {
        static void Main()
        {
            Console.WriteLine("Задание 1");
            foreach (var x in MasFilter(new int[] { 1, 2, 3, 4, 4 }, x => x % 2 == 0))
                Console.Write(x + " ");
            Console.WriteLine();
            Console.WriteLine("Задание 2");
            int[] SecondArr = new int[] { 1, 2, 3, 4, 5, 6 };
            SwapParts(ref SecondArr);
            foreach (var x in SecondArr)
                Console.Write(x + " ");
            Console.WriteLine();
            Console.WriteLine("Задание 3");
            PrintLeftRight(new int[,] { { 1, 2, 3 }, { 4, 5, 6 }, { 7, 8, 9 }, { 1, 4, 7 } });
            Console.WriteLine();
            Console.WriteLine("Задание 4");
            Console.WriteLine(MaxRowSum(new int[][] {
                new int[]{ 1, 2, 3 },
                new int[]{ 4, 5, 6 },
                new int[]{ 7, 8, 9 },
                new int[]{ 1, 4, 7 } }));
            Console.WriteLine();
            Console.WriteLine("Задание 5");
            Console.WriteLine(MaxAverage(new int[][] {
                new int[]{ 1, 2, 3 },
                new int[]{ 4, 5 },
                new int[]{ 9 },
                new int[]{ 1, 4, 7 } }));
        }
        public static int[] MasFilter(int[] s, Func<int, bool> f)
        {
            int[] a = new int[s.Count()];
            int count = 0;
            for (int i = 0; i < s.Length; i++)
                if (f(s[i]))
                {
                    a[count] = s[i];
                    count++;
                }
            return a[..count];
        }
        public static void SwapParts(ref int[] s)
        {
            if (s.Length % 2 != 0) throw new ArgumentException("Длина массива должна быть чётной!");
            s = s[(s.Length / 2)..].Concat(s[..(s.Length / 2)]).ToArray();
        }
        public static void PrintLeftRight(int[,] s)
        {
            bool flag = true;
            for (int i = 0; i < s.GetLength(0); i++)
            {
                if (flag)
                    for (int j = 0; j < s.GetLength(1); j++)
                        Console.Write(s[i, j] + " ");
                else
                    for (int j = s.GetLength(1) - 1; j >= 0; j--)
                        Console.Write(s[i, j] + " ");
                flag = !flag;
                Console.WriteLine();
            }
        }
        public static (int, int) MaxRowSum(int[][] s)
        {
            int row = 0;
            int sum = 0;
            if (s == null || s.GetLength(0) == 0) return (row, sum);
            for (int i = 0; i < s.GetLength(0); i++)
            {
                if (s[i].Sum() > sum)
                {
                    row = i;
                    sum = s[i].Sum();
                }
            }
            return (row + 1, sum);
        }
        public static double MaxAverage(int[][] s)
        {
            double max = 0;
            if (s == null || s.GetLength(0) == 0) return max;
            for (int i = 0; i < s.GetLength(0); i++)
                if (s[i].Average() > max) max = s[i].Average();
            return max;
        }
    }
}
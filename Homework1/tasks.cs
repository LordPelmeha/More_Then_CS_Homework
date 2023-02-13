using System.Net.Security;

namespace Homework1
{
    internal class Program
    {
        enum Seasons { January, February, March, April, May, June, July, August, September, October, November, December };
        static void Main(string[] args)
        {
            Console.WriteLine("Задание 1");
            Console.WriteLine("Введите трёхзначное число");
            int num1 = int.Parse(Console.ReadLine());
            MiddleGoToZero(ref num1);
            Console.WriteLine(num1);
            Console.WriteLine();
            Console.WriteLine("Задание 2");
            Console.WriteLine("Введите координаты позиции на шахматной доске");
            int[] position = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            PositionColor(position[0], position[1]);
            Console.WriteLine();
            Console.WriteLine("Задане 3");
            Console.WriteLine("Введите три числа");
            int[] third = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            Console.WriteLine(NumOfRoots(third[0], third[1], third[2]));
            Console.WriteLine();
            Console.WriteLine("Задание 4");
            Console.WriteLine("Введите два вещественных числа");
            double[] fourth = Console.ReadLine().Split(" ").Select(x => double.Parse(x)).ToArray();
            Console.WriteLine(MinOfDouble(fourth[0], fourth[1]));
            Console.WriteLine();
            Console.WriteLine("Задание 5");
            Console.WriteLine("Введите два целых числа");
            int[] fifth = Console.ReadLine().Split(" ").Select(x => int.Parse(x)).ToArray();
            Console.WriteLine(ProductAtoB(fifth[0], fifth[1]));
            Console.WriteLine();
            Console.WriteLine("Задание 6");
            Console.WriteLine("Введите число К");
            int k = int.Parse(Console.ReadLine());
            Console.WriteLine("Введите последовательность чисел, заканчивающуяся 0");
            List<int> seq = Console.ReadLine().Split(" ").Select(x => Convert.ToInt32(x)).ToList();
            seq = seq.SkipLast(seq.Count - seq.IndexOf(0)).ToList();
            Console.WriteLine(LessAndDiv(k, seq));
            Console.WriteLine();
            Console.WriteLine("Задание 7");
            Console.WriteLine("Введите номер месяца");
            int num7 = int.Parse(Console.ReadLine());
            Console.WriteLine(Month(num7));
            Console.WriteLine();
            Console.WriteLine("Задание 8");
            Console.WriteLine("Введите число");
            int num8 = int.Parse(Console.ReadLine());
            RandomMonth(num8);
        }
        /// <summary>
        ///  Обнуляет в трёхзначном числе разряд десятков
        /// </summary>
        static void MiddleGoToZero(ref int num)
        {
            int flag = 1;
            if (num < 0) { flag = -1; num *= flag; }
            if (num < 100 || num > 1000) throw new ArgumentException("Число должно быть трёхзначным");
            num = flag * (num - num % 100 + num % 10);
        }
        /// <summary>
        /// Выводит, какой цвет имеет поле с заданными координатами
        /// </summary>
        static void PositionColor(int x, int y)
        {
            if (x < 1 || y < 1 || x > 8 || y > 8) throw new ArgumentException("Нет такой позиции!");
            string color = (x + y) % 2 == 0 ? "Чёрный" : "Белый";
            Console.WriteLine(color);
        }
        /// <summary>
        /// Возварщает количество вещественных корней квадратного уравнения A*x^2+B*x+c=0.
        /// </summary>
        static int NumOfRoots(int a, int b, int c)
        {
            if (a == 0) throw new ArgumentException("Числа A не должно быть равно 0");
            int d = b * b - 4 * a * c;
            if (d > 0) return 2;
            else if (d == 1) return 1;
            return 0;
        }
        /// <summary>
        /// Возвращает минимум из двух переданных вещественных чисел
        /// </summary>
        static double MinOfDouble(double x, double y) => Math.Min(x, y);
        /// <summary>
        /// Находит произведение всех чётных целых чисел от X до Y включительно
        /// </summary>
        static double ProductAtoB(int x, int y)
        {
            double prod = 1;
            for (Int64 i = Math.Min(x, y) + Math.Min(x, y) % 2; i <= Math.Max(x, y) - Math.Max(x, y) % 2; i += 2) prod *= i;
            return prod;
        }
        /// <summary>
        /// Вычисляет количество чисел в наборе, меньших K, а также количество чисел, делящихся на K нацело
        /// </summary>
        static (int, int) LessAndDiv(int k, List<int> s)
        {
            int less = 0;
            int div = 0;
            foreach (var x in s)
            {
                if (x < k) less++;
                if (x % k == 0) div++;
            }
            return (less, div);
        }
        /// <summary>
        /// По номеру месяца возвращает время года
        /// </summary>
        static Seasons Month(int k)
        {
            switch (k)
            {

                case 1: return Seasons.January; ;
                case 2: return Seasons.February;
                case 3: return Seasons.March;
                case 4: return Seasons.April;
                case 5: return Seasons.May;
                case 6: return Seasons.June;
                case 7: return Seasons.July;
                case 8: return Seasons.August;
                case 9: return Seasons.September;
                case 10: return Seasons.October;
                case 11: return Seasons.November;
                case 12: return Seasons.December;
                default: throw new ArgumentException("Нет месяца с таким номером!");
            }
        }
        /// <summary>
        /// Выводит на консоль N строк "Месяц №(номер месяца), его сезон: (сезон для этого месяца)". Номера месяцев генерируются случайно.
        /// </summary>
        static void RandomMonth(int n)
        {
            if (n < 0) throw new ArgumentException("Введите неотрицательное кол-во строк");
            Random r = new Random();
            int x = r.Next(1, 13);
            for (int i = 0; i < n; i++) { Console.WriteLine($"Месяц №{x}, его сезон: {Month(x)}"); x = r.Next(1, 13); }
        }
        static void fafa() => Console.WriteLine();
    }
}
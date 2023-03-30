using System.Diagnostics.SymbolStore;
using System.IO;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Text.RegularExpressions;

namespace Homework5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine();
            Console.WriteLine("задание 1");
            FormRandomBinaryFile("task1.dat");
            Console.WriteLine();
            Console.WriteLine("задание 2");
            Console.WriteLine(ReturnNumFromFile("input-files/task2.txt", 5));
            Console.WriteLine();
            Console.WriteLine("задание 3");
            Console.WriteLine(GetTrackAndArtistName("input-files/ответы на контрольную.mp3"));
            Console.WriteLine();
            Console.WriteLine("задание 5");
            Console.WriteLine(CountWordsInFile("input-files/task5.txt"));
            Console.WriteLine();
            Console.WriteLine("задание 6");
            // Console.WriteLine(GetListFromFile("input-files/IMDB.csv").Count());
            foreach (var movie in GetListFromFile("input-files/IMDB.csv"))
                Console.WriteLine($"{movie.Title} {movie.OriginalLanguage} {movie.VoteCount} {movie.VoteAverage}");
        }
        /// <summary>
        /// Формирует бинарный файл из N случайных вещественных чисел в диапазоне от a до b (a ≤ b)
        /// </summary>

        static void FormRandomBinaryFile(string path, int n = 20, int a = -50, int b = 50)
        {
            if (n < 0) throw new ArgumentException("Число элементов должно быть неотрицательным!");
            if (a > b) throw new ArgumentException("Левая граница не может быть больше правой!");
            if (path == null || path == "") throw new ArgumentNullException("Файл не должен быть пустым!");
            var r = new Random();
            using (var bw = new BinaryWriter(File.Open(path, FileMode.OpenOrCreate), Encoding.UTF8, false))
            {
                for (int i = 0; i < n; i++)
                    bw.Write(r.Next(a, b + 1) * r.NextDouble());
            }
        }
        /// <summary>
        /// Возвращает K-й элемент файла (элементы нумеруются с 0). Если такой элемент отсутствует, возвращает −1.
        /// </summary>
        static int ReturnNumFromFile(string path, int k)
        {
            var s = Regex.Split(File.ReadAllText(path), @"\s+").Select(x => int.Parse(x)).ToArray();
            if (k >= s.Length || k < 1) return -1;
            else return s[k];
        }
        /// <summary>
        /// Извлекает название трека, исполнителя и альбома из рикрола
        /// </summary>
        static string GetTrackAndArtistName(string path)
        {
            var a = File.ReadAllText(path);
            return $"{a[(a.Length - 125)..(a.Length - 101)]}-{a[(a.Length - 96)..(a.Length - 84)]}\nАыльбом - {a[(a.Length - 65)..(a.Length - 35)]}";
        }
        /// <summary>
        /// Считает количество слов в файле, разделённых пробелами или символами переноса строки
        /// </summary>
        static int CountWordsInFile(string path)
        {
            return Regex.Split(File.ReadAllText(path), @"\s+").Length;
        }
        /// <summary>
        /// Возвращает отсортированный по рейтингу список фильмов, у которых количество отзывов не менее 100.
        /// </summary>
        static List<Movie> GetListFromFile(string path)
        {
            var ar = File.ReadAllLines(path);
            List<Movie> MoviesList = new List<Movie>();
            foreach (var x in ar[1..])
            {
                var movie = new Movie(x);
                if (movie.VoteCount >= 100)
                    MoviesList.Add(movie);
                //Console.WriteLine($"{movie.Title} {movie.OriginalLanguage} {movie.VoteCount} {movie.VoteAverage}");
            }
            for (int i = 0; i < MoviesList.Count(); i++)
            {
                for (int j = 0; j < MoviesList.Count(); j++)
                {
                    if (MoviesList[i].VoteAverage < MoviesList[j].VoteAverage)
                        (MoviesList[i], MoviesList[j]) = (MoviesList[j], MoviesList[i]);
                }
            }
            return MoviesList;
        }
    }
    public class Movie
    {
        public string Title { get; private set; }
        public string OriginalLanguage { get; private set; }
        public int VoteCount { get; private set; }
        public double VoteAverage { get; private set; }
        public Movie(string s)
        {
            if (Regex.Split(s, @",").Length >= 5)
            {
                var ar = Regex.Match(s, @"((?<=,).*?(?=,\w{2},\d+,\d{1})),((?<=,)\w{2}(?=,)),(\d+),(\d\.?\d?)");

                Title = ar.Groups[1].ToString();
                OriginalLanguage = ar.Groups[2].ToString();
                //Console.WriteLine(ar[3] + " " + ar[4]);
                VoteCount = int.Parse(ar.Groups[3].ToString());
                VoteAverage = double.Parse(Regex.Replace(ar.Groups[4].ToString(), @"\.", @","));
            }
        }
    }
}
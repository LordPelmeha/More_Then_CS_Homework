using System.Text.RegularExpressions;

namespace Homework3
{
    internal class Program
    {
        static void Main()
        {
            Console.WriteLine();
            Console.WriteLine("задание 1");
            foreach (var x in RusCarNum(Console.ReadLine()))
                Console.Write(x + " ");
            Console.WriteLine();
            Console.WriteLine("задание 2");
            Console.WriteLine(StarToItalic(Console.ReadLine()));
            Console.WriteLine();
            Console.WriteLine("задание 3");
            foreach (var x in FindIP("0.0.0.0 255.255.255.255 188.168.215.30 199.195.252.5"))
                Console.Write(x + " ");
            Console.WriteLine();
            Console.WriteLine("задание 4");
            Console.WriteLine(SplitCamel(Console.ReadLine()));
            Console.WriteLine();
            Console.WriteLine("задание 5");
            PrintClientInfoLen(Console.ReadLine());
        }
        /// <summary>
        /// Находит в строке все автомобильные номера и возвращает массив из них.
        /// </summary>
        static string[] RusCarNum(string s) =>
            Regex.Matches(s, @"\b[АВЕКМНОРСТУХ]{1}[0-9]{3}[АВЕКМНОРСТУХ]{2}[0-9]{2}\b").Select(x => x.ToString()).ToArray();
        /// <summary>
        /// Преобразовывает текст, обрамленный в звездочки, в текст обрамленный тегом <em></em>
        /// </summary>
        static string StarToItalic(string s) =>
            Regex.Replace(s, @"((?<!\*)[*])([\w\s]+)([*](?!\*))", @"<em>$2</em>");
        /// <summary>
        /// Находит IP-адресс
        /// </summary>
        static string[] FindIP(string s) =>
            Regex.Matches(s, @"\b((25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b").Select(x => x.ToString()).ToArray();
        /// <summary>
        /// Раздяляет склеенные слова, начинающиеся с большой буквы, пробелами
        /// </summary>
        static string SplitCamel(string s) =>
            Regex.Replace(s, @"(\w(?=[A-Z]))", @"$1 ");
        /// <summary>
        /// Получает данные о всех клиентах и для каждого клиента 
        /// выводит его логин и длину его строки-описания (выражения внутри круглых скобок).
        /// </summary>
        static void PrintClientInfoLen(string s)
        {
            foreach (Match x in Regex.Matches(s, @"(?<name>(?<=\@)\w+(?=\=))=(?<info>\(.*?\))"))
                Console.WriteLine($"{x.Groups["name"]} => {x.Groups["info"].Length}");
        }
    }
}
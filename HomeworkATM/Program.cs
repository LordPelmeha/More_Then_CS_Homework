using System.Text.RegularExpressions;

namespace HomeworkATM
{
    internal class Program
    {
        static void Main()
        {
            var a = new Card("1234567890111111", "fafa", "11/23", "sdg", 100);
            Console.WriteLine(a.ToString());
        }
        static bool ChekBanknotes(Dictionary<string, int> dict, Dictionary<string, int> caset, string num, ref List<string> history)
        {
            foreach (var x in dict)
            {
                if (!caset.ContainsKey(x.Key) || x.Value < 1)
                {
                    history.Add($"{num}: пополнение (некорректная сумма) => вызвана полиция");
                    Console.WriteLine($"Вы пытаетьсь совершить незаконную операцию! Полиция уже едет за вами!");
                    return false;
                }
            }
            return true;
        }
        static bool ChekValid(string valid, string num, ref List<string> history)
        {
            if (int.Parse(valid.Split("/")[0]) <= DateTime.Now.Month && int.Parse(valid.Split("/")[1]) <= DateTime.Now.Year)
                return true;
            history.Add($"{num}: пополнение => карта просрочена");
            Console.WriteLine("Ваша карта просрочена!");
            return false;
        }
        static Dictionary<string, int> ReadBanknotes(string banknotes)
        {
            var ar = Regex.Split(banknotes, @"\s+").Select(x => x.ToString()).ToArray();
            var dict = new Dictionary<string, int>();
            foreach (var x in ar)
            {
                var b = Regex.Split(x, @",").Select(x => x.ToString()).ToArray();
                if (b.Length == 2 && !Regex.Match(b[0], @"\D").Success && !Regex.Match(b[1], @"\D").Success)
                {
                    if (dict.ContainsKey(b[0]))
                        dict[b[0]] += int.Parse(b[1]);
                    else dict[b[0]] = int.Parse(b[1]);
                }
                else throw new ArgumentException("Неправильный формат ввода купюр");
            }
            return dict;
        }
        static double AddCommission(string cardBank, string ATMBank) => cardBank == ATMBank ? 1.00 : 0.95;
        static void Replenishment(Card card, ATM atm, Dictionary<string, int> banknotes)
        {
            if (ChekBanknotes(banknotes, atm.Cassette, card.Num, ref atm.History))
            {
                if (ChekValid(card.Valid, card.Num, ref atm.History))
                {

                }
            }
            else
            {

            }
        }
    }
}
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
            history.Add($"{num} => карта просрочена");
            Console.WriteLine("Ваша карта просрочена!");
            return false;
        }
    }
}
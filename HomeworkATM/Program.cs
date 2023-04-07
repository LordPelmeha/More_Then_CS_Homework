using System;
using System.Text.RegularExpressions;

namespace HomeworkATM
{
    internal class Program
    {
        static void Main()
        {
            var a = new Card("1234567890111111", "Александр", "11/27", "sdg", 100);
            var b = new ATM("sdgh");
            Console.WriteLine(a.ToString());
            Console.WriteLine();
            Console.WriteLine(b.ToString());
            Console.WriteLine();
            Replenishment(a, b, ReadBanknotes(Console.ReadLine()));
            Console.WriteLine();
            Console.WriteLine(a.ToString());
            Console.WriteLine();
            Console.WriteLine(b.ToString());
            Console.WriteLine();
            Withdrawal(a, b, ChekSum(Console.ReadLine()));
            Console.WriteLine();
            Console.WriteLine(a.ToString());
            Console.WriteLine();
            Console.WriteLine(b.ToString());
            Console.WriteLine();
            PickUp(a, b, "WpmzoePirmzoeWomrypeTevwleo");

        }
        static bool ChekBanknotes(Dictionary<string, int> dict, Dictionary<string, int> caset, string num, List<string> history)
        {
            foreach (var x in dict)
            {
                if (!caset.ContainsKey(x.Key) || x.Value < 1)
                {
                    history.Add($"{num}: пополнение (некорректная сумма) => вызвана полиция\n");
                    Console.WriteLine($"Вы пытаетьсь совершить незаконную операцию! Полиция уже едет за вами!");
                    return false;
                }
            }
            return true;
        }
        static bool ChekValid(string valid, string num, List<string> history)
        {
            var month = int.Parse(valid.Split("/")[0]);
            var year = int.Parse(valid.Split("/")[1]) + 2000;
            if ((month >= DateTime.Now.Month && year == DateTime.Now.Year) || year >= DateTime.Now.Year)
                return true;
            history.Add($"{num}: пополнение => карта просрочена\n");
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
        static long CashSum(Dictionary<string, int> dict)
        {
            long sum = 0;
            foreach (var x in dict)
                sum += int.Parse(x.Key) * x.Value;
            return sum;
        }
        static double AddCommission(string cardBank, string ATMBank, string num, List<string> history)
        {
            if (cardBank == ATMBank)
                return 1.00;
            else
            {
                Console.WriteLine("Будет введена комиссия!");
                history.Add($"{num}: пополнение => введена комиссия\n");
                return 0.95;
            }
        }
        static void AddBanknotesToATM(Dictionary<string, int> dict, Dictionary<string, int> casette)
        {
            foreach (var x in dict)
                casette[x.Key] += x.Value;
        }
        static void Replenishment(Card card, ATM atm, Dictionary<string, int> banknotes)
        {
            if (ChekBanknotes(banknotes, atm.Cassette, card.Num, atm.History))
            {
                if (ChekValid(card.Valid, card.Num, atm.History))
                {
                    double AddMoney = CashSum(banknotes) * AddCommission(card.Bank, atm.Bank, card.Num, atm.History);
                    card.Sum += AddMoney;
                    AddBanknotesToATM(banknotes, atm.Cassette);
                    Console.WriteLine("Операция успешно завершена!");
                    atm.History.Add($"{card.Num}: пополнение на {AddMoney}=> операция успешно завершена\n");
                }
            }
        }
        static int ChekSum(string s)
        {
            if (!Regex.Match(s, @"\D").Success || s != "")
                if (int.Parse(s) % 50 == 0)
                    return int.Parse(s);
            throw new ArgumentException("Сумму для снятия введена в неправильном формате");
        }
        static bool CanGetMoney(int sum, Dictionary<string, int> dict, string num, List<string> history)
        {
            var count = 0;
            foreach (var x in dict)
            {
                for (var i = 0; i < x.Value; i++)
                    if (count + int.Parse(x.Key) <= sum)
                        count += int.Parse(x.Key);
            }
            if (sum == count)
                return true;
            Console.WriteLine("Банкомат не может выдать данную сумму!");
            history.Add($"{num}: снятие => невозможно выдать сумму");
            return false;
        }
        static void GiveMoney(int sum, Dictionary<string, int> dict)
        {
            var count = 0;
            foreach (var x in dict)
            {
                for (var i = 0; i < x.Value; i++)
                    if (count + int.Parse(x.Key) <= sum)
                    {
                        dict[x.Key] -= 1;
                        count += int.Parse(x.Key);
                    }
            }
        }
        static void Withdrawal(Card card, ATM atm, int sum)
        {
            if (ChekValid(card.Valid, card.Num, atm.History))
            {
                if (atm.CashAmount >= sum)
                {
                    if (card.Sum >= sum)
                    {
                        if (CanGetMoney(sum, atm.Cassette, card.Num, atm.History))
                        {
                            GiveMoney(sum, atm.Cassette);
                            card.Sum -= sum * AddCommission(card.Bank, atm.Bank, card.Num, atm.History);
                            Console.WriteLine("Операция успешно завершена!");
                            atm.History.Add($"{card.Num}: снитие {sum}=> операция успешно завершена\n");
                        }
                    }
                    else
                        Console.WriteLine("Недостаточно средств на карте!");
                }
                else
                {
                    Console.WriteLine("В банкомете недостаточно средств!");
                    atm.History.Add($"{card.Num}: снятие => недостаточно средств в банкомате");
                }
            }
        }
        static void PickUp(Card card, ATM atm, string code)
        {
            if (code.Length == atm.Key.Length)
            {
                int flag = 1;
                for (int i = 0; i < code.Length; i++)
                {
                    if (code[i] - 4 != atm.Key[i])
                        flag = 0;
                }
                if (flag == 1)
                {
                    var count = 0;
                    foreach (var x in atm.Cassette)
                        count += x.Value;
                    count /= 7;
                    foreach (var x in atm.Cassette)
                        atm.Cassette[x.Key] = count;
                    foreach (var x in atm.History)
                        Console.Write(x);
                    atm.History.Clear();
                }
                else
                {
                    for (int i = 0; i < code.Length; i++)
                    {
                        Console.WriteLine($"{code[i] - 4} {atm.Key[i] - 0}");
                    }
                }
            }
        }
    }
}
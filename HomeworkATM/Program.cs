using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace HomeworkATM
{
    internal class Program
    {
        static void Main()
        {
            Start();
        }
        static void Start()
        {
            var a = new Card("1234567890111111", "Александр", "11/27", "sdg", 100);
            var b = new ATM("sdgh");
            while (true)
            {
                var s = Console.ReadLine();
                Console.WriteLine(a.ToString());
                Console.WriteLine();
                Console.WriteLine(b.ToString());
                Console.WriteLine();
                if (s == "1")
                {
                    Replenishment(a, b, ReadBanknotes(Console.ReadLine()));
                    //Console.WriteLine();
                    //Console.WriteLine(a.ToString());
                    //Console.WriteLine();
                    //Console.WriteLine(b.ToString());
                    Console.WriteLine();
                }
                else if (s == "2")
                {
                    Withdrawal(a, b, ChekSum(Console.ReadLine()));
                    //Console.WriteLine();
                    //Console.WriteLine(a.ToString());
                    //Console.WriteLine();
                    //Console.WriteLine(b.ToString());
                    Console.WriteLine();
                }
                else if (s == "0")
                {
                    PickUp(a, b, "WpmzoePirmzoeWomrypeTevwleo");
                    //Console.WriteLine(a.ToString());
                    //Console.WriteLine();
                    // Console.WriteLine(b.ToString());
                    Console.WriteLine();
                    break;
                }
                else
                {
                    while (s != "1" || s != "2" || s != "0")
                        s = Console.ReadLine();
                }
            }
        }
        /// <summary>
        /// Проверяет правильность внесённых банкнот
        /// </summary>
        static bool ChekBanknotes(Stack<Banknote> dict, Stack<Banknote> caset, string num, List<string> history)
        {
            foreach (var x in dict)
            {
                if (int.Parse(x.Nominal) < 1 || !(ChekMoreThousand(x) && ChekMoreThousand(x)))
                {
                    history.Add($"{num}: пополнение (некорректная сумма) => вызвана полиция\n");
                    Console.WriteLine($"Вы пытаетьсь совершить незаконную операцию! Полиция уже едет за вами!");
                    return false;
                }
            }
            return true;
        }
        static bool ChekLessThousand(Banknote n)
        {
            if (int.Parse(n.Nominal) < 1000)
            {
                if (Math.Abs(n.Series[0] - n.Series[1]) % 2 == 0)
                {
                    int su = 0;
                    for (int i = 2; i < n.Series.Length; i++)
                    {
                        su += int.Parse(n.Series[i].ToString());
                    }
                    if (su % 2 == 0)
                        return true;
                }
            }
            return true;
        }
        static bool ChekMoreThousand(Banknote n)
        {
            if (int.Parse(n.Nominal) >= 1000)
            {
                if (Math.Abs(n.Series[0] - n.Series[1]) % 2 == 1)
                {
                    int su = 0;
                    for (int i = 2; i < n.Series.Length; i++)
                    {
                        su += int.Parse(n.Series[i].ToString());
                    }
                    if (su % 2 == 1)
                        return true;
                }
            }
            return true;
        }

        /// <summary>
        /// Проверяет, действительна ли карта
        /// </summary>
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
        /// <summary>
        /// Проверяет банкноты и возвращает словарь банкнот
        /// </summary>
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
        /// <summary>
        /// Сумма денег
        /// </summary>
        static long CashSum(Stack<Banknote> dict)
        {
            long sum = 0;
            foreach (var x in dict)
                sum += int.Parse(x.Nominal);
            return sum;
        }
        /// <summary>
        /// Добавляет комиссию, если не совпадают банки
        /// </summary>
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
        /// <summary>
        /// Заносит банкноты в касету
        /// </summary>
        static void AddBanknotesToATM(Stack<Banknote> dict, Stack<Banknote> casette)
        {
            foreach (var x in dict)
                casette.Push(x);
        }
        /// <summary>
        /// Пополнение карты
        /// </summary>
        static void Replenishment(Card card, ATM atm, Stack<Banknote> banknotes)
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
        /// <summary>
        /// Проверяет сумму, которую хотят снять
        /// </summary>
        static int ChekSum(string s)
        {
            if (!Regex.Match(s, @"\D").Success || s != "")
                if (int.Parse(s) % 50 == 0)
                    return int.Parse(s);
            throw new ArgumentException("Сумму для снятия введена в неправильном формате");
        }
        /// <summary>
        /// Проверяет, можно ли снять сумму
        /// </summary>
        static bool CanGetMoney(int sum, Stack<Banknote> dict, string num, List<string> history)
        {
            var count = 0;
            var ar = new List<int>();
            foreach (var x in dict)
                ar.Add(int.Parse(x.Nominal));
            ar.Order().Reverse();
            foreach (var x in ar)
            {
                for (var i = 0; i < ar.Count(); i++)
                    if (count + x <= sum)
                        count += x;
            }
            if (sum == count)
                return true;
            Console.WriteLine("Банкомат не может выдать данную сумму!");
            history.Add($"{num}: снятие => невозможно выдать сумму");
            return false;
        }
        /// <summary>
        /// Выдаёт деньги, убирая их из банкомата
        /// </summary>
        static void GiveMoney(int sum, Stack<Banknote> dict)
        {
            var count = 0;
            dict.OrderBy(x => x.Nominal).Reverse();
            foreach (var x in dict)
            {
                if (count + int.Parse(x.Nominal) <= sum)
                {
                    
                    count += int.Parse(x.Nominal);
                    dict.TryPop(x);
                }
            }
        }
        /// <summary>
        /// Снятие наличных
        /// </summary>
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
        /// <summary>
        /// Вызов инкасации
        /// </summary>
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
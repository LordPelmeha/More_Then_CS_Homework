using System;
using System.Text;
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
                    //Replenishment(a, b, ReadBanknotes(Console.ReadLine()));
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
        static bool ChekBanknotes(Dictionary<string, Stack<Banknote>> dict, Dictionary<string, Stack<Banknote>> caset, string num, List<string> history)
        {
            foreach (var x in dict)
            {
                foreach (var y in x.Value)
                {
                    if (!caset.ContainsKey(x.Key) || int.Parse(y.Nominal) < 1 || !(ChekMoreThousand(y) && ChekLessThousand(y)))
                    {
                        history.Add($"{num}: пополнение (некорректная сумма) => вызвана полиция\n");
                        Console.WriteLine($"Вы пытаетьсь совершить незаконную операцию! Полиция уже едет за вами!");
                        return false;
                    }
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
        /*
        /// <summary>
        /// Проверяет банкноты и возвращает словарь банкнот
        /// </summary>
        static Dictionary<string, Stack<Banknote>> ReadBanknotes(string banknotes)
        {
            var ar = Regex.Split(banknotes, @"\s+").Select(x => x.ToString()).ToArray();
            var dict = new Dictionary<string, Stack<Banknote>>();
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
        */
        /// <summary>
        /// Сумма денег на пополнение
        /// </summary>
        static long CashSum(Dictionary<string, Stack<Banknote>> dict)
        {
            long sum = 0;
            foreach (var x in dict)
                sum += int.Parse(x.Key) * x.Value.Count;
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
        static void AddBanknotesToATM(Dictionary<string, Stack<Banknote>> dict, Dictionary<string, Stack<Banknote>> casette)
        {
            foreach (var x in dict)
                foreach (var y in x.Value)
                    casette[x.Key].Push(y);
        }
        /// <summary>
        /// Проверяет корректность серии банкоты номиналом меньше 1000
        /// </summary>
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
                return false;
            }
            return true;
        }
        /// <summary>
        /// Проверяет корректность серии банкоты номиналом больше или равным 1000
        /// </summary>
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
                return false;
            }
            return true;
        }
        /// <summary>
        /// Пополнение карты
        /// </summary>
        static void Replenishment(Card card, ATM atm, Dictionary<string, Stack<Banknote>> banknotes)
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
        static bool CanGetMoney(int sum, Dictionary<string, Stack<Banknote>> dict, string num, List<string> history)
        {
            var count = 0;
            foreach (var x in dict)
            {
                for (var i = 0; i < x.Value.Count; i++)
                    if (count + int.Parse(x.Key) <= sum)
                        count += int.Parse(x.Key);
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
        static void GiveMoney(int sum, Dictionary<string, Stack<Banknote>> dict)
        {
            var count = 0;
            foreach (var x in dict)
            {
                for (var i = 0; i < x.Value.Count; i++)
                    if (count + int.Parse(x.Key) <= sum)
                    {
                        x.Value.Pop();
                        count += int.Parse(x.Key);
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
        static Stack<Banknote> GenBanknotes(string nominal, int n)
        {
            var st = new Stack<Banknote>();
            var r = new Random();
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                s.Append(Convert.ToChar(r.Next(97, 123)).ToString());
                s.Append(Convert.ToChar(r.Next(97, 123)).ToString());
                for (int j = 0; j < 9; j++)
                    s.Append(r.Next(0, 10).ToString());
                st.Push(new Banknote(nominal, s.ToString()));
                s = new StringBuilder();
            }
            return st;
        }
        static Stack<Banknote> GenGoodBanknotes(string nominal, int n)
        {
            var st = new Stack<Banknote>();
            var r = new Random();
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < n; i++)
            {
                s.Append(Convert.ToChar(r.Next(97, 123)).ToString());
                s.Append(Convert.ToChar(r.Next(97, 123)).ToString());
                for (int j = 0; j < 9; j++)
                    s.Append(r.Next(0, 10).ToString());
                var banknote = new Banknote(nominal, s.ToString());
                if (ChekMoreThousand(banknote) && ChekLessThousand(banknote))
                    st.Push(banknote);
                else
                {
                    if (!ChekMoreThousand(banknote))
                    {
                        if ((s[0] - s[1]) % 2 != 1)
                        {
                            if (s[1] != 97)
                                s[1] = Convert.ToChar(s[1] - 1);
                            else
                                s[1] = Convert.ToChar(s[1] + 1);
                        }
                        if (s.ToString().Skip(2).ToArray().Select(x => int.Parse(x.ToString())).Sum() % 2 != 1)
                        {

                        }

                    }
                    else
                    {

                    }
                }
                s = new StringBuilder();
            }
            return st;
        }

        /// <summary>
        /// Вызов инкасации
        /// </summary>
        static void PickUp(Card card, ATM atm, string code)
        {
            if (code.Length == atm.Key.Length)
            {
                //int flag = 1;
                //for (int i = 0; i < code.Length; i++)
                //{
                //    if (code[i] - 4 != atm.Key[i])
                //        flag = 0;
                //}
                //if (flag == 1)
                //{
                //    var count = 0;
                //    foreach (var x in atm.Cassette)
                //        count += x.Value;
                //    count /= 7;
                //    foreach (var x in atm.Cassette)
                //        atm.Cassette[x.Key] = count;
                //    foreach (var x in atm.History)
                //        Console.Write(x);
                //    atm.History.Clear();
                //}
                //else
                //{
                //    for (int i = 0; i < code.Length; i++)
                //    {
                //        Console.WriteLine($"{code[i] - 4} {atm.Key[i] - 0}");
                //    }
                //}
            }
        }
    }
}
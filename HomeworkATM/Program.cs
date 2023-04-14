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
            var card = new Card("1234567890111111", "Александр", "11/27", "sdg", 100);
            var atm = new ATM("sdgh");
            while (true)
            {
                var ss = Console.ReadLine();
                Console.WriteLine(card);
                Console.WriteLine();
                Console.WriteLine(atm);
                Console.WriteLine();
                if (ss == "1")
                {
                    Replenishment(card, atm, new Dictionary<string, Stack<Banknote>>
                    {
                        ["5000"] = GenGoodBanknotes("5000", 0),
                        ["2000"] = GenGoodBanknotes("2000", 0),
                        ["1000"] = GenGoodBanknotes("1000", 0),
                        ["500"] = GenGoodBanknotes("500", 100),
                        ["200"] = GenGoodBanknotes("200", 100),
                        ["100"] = GenGoodBanknotes("100", 100),
                        ["50"] = GenGoodBanknotes("50", 100)
                    });
                    Console.WriteLine();
                    Console.WriteLine(card);
                    Console.WriteLine();
                    Console.WriteLine(atm);
                    Console.WriteLine();
                }
                else if (ss == "2")
                {
                    Withdrawal(card, atm, CheckSum(Console.ReadLine()));
                    Console.WriteLine();
                    Console.WriteLine(card);
                    Console.WriteLine();
                    Console.WriteLine(atm);
                    Console.WriteLine();
                }
                else if (ss == "0")
                {

                    PickUp(card, atm, "1428394874562349");
                    Console.WriteLine(card);
                    Console.WriteLine();
                    Console.WriteLine(atm);
                    break;
                }
                else
                {
                    while (ss != "1" || ss != "2" || ss != "0")
                        ss = Console.ReadLine();
                }
            }
        }
        /// <summary>
        /// Проверяет правильность внесённых банкнот
        /// </summary>
        static bool CheckBanknotes(Dictionary<string, Stack<Banknote>> dict, Dictionary<string, Stack<Banknote>> caset, string num, List<string> history)
        {
            foreach (var x in dict)
            {
                foreach (var y in x.Value)
                {
                    if (!caset.ContainsKey(x.Key) || int.Parse(y.Nominal) < 1 || !(CheckMoreThousand(y) && CheckLessThousand(y)))
                    {
                        Console.WriteLine($"{!caset.ContainsKey(x.Key)} {int.Parse(y.Nominal) < 1} {CheckMoreThousand(y)} {CheckLessThousand(y)}");
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
        static bool CheckValid(string valid, string num, List<string> history)
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
        static bool CheckLessThousand(Banknote n)
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
        static bool CheckMoreThousand(Banknote n)
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
            if (CheckBanknotes(banknotes, atm.Cassette, card.Num, atm.History))
            {
                if (CheckValid(card.Valid, card.Num, atm.History))
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
        static int CheckSum(string s)
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
            if (CheckValid(card.Valid, card.Num, atm.History))
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
        /// Генерирует стэк купюр(без проверки на корректность серии)
        /// </summary>
        /// <param name="nominal"></param>
        /// <param name="n"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Генерирует стэк купюр с корректной серией
        /// </summary>
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
                if (CheckMoreThousand(banknote) && CheckLessThousand(banknote))
                    st.Push(banknote);
                else
                {
                    if (!CheckMoreThousand(banknote))
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
                            if (s[^1] != Convert.ToChar("9"))
                                s[^1] = Convert.ToChar(s[^1] + 1);
                            else
                                s[^1] = Convert.ToChar("8");
                        }
                    }
                    else if (!CheckLessThousand(banknote))
                    {

                        if ((s[0] - s[1]) % 2 != 0)
                        {
                            if (s[1] != 97)
                                s[1] = Convert.ToChar(s[1] - 1);
                            else
                                s[1] = Convert.ToChar(s[1] + 1);
                        }
                        if (s.ToString().Skip(2).ToArray().Select(x => int.Parse(x.ToString())).Sum() % 2 != 0)
                        {
                            if (s[^1] != Convert.ToChar("9"))
                                s[^1] = Convert.ToChar(s[^1] + 1);
                            else
                                s[^1] = Convert.ToChar("8");
                        }
                    }
                    banknote = new Banknote(nominal, s.ToString());
                    st.Push(banknote);
                }
                s = new StringBuilder();
            }
            return st;
        }
        /// <summary>
        /// Расшифровывает ключ инкасации и сравнивает его с ключом банкомата
        /// </summary>
        static bool CheсkKey(ATM atm, string key)
        {
            var s = key.ToArray().Select(x => int.Parse(x.ToString())).ToArray();
            var ans = new StringBuilder();
            var queue = new Queue<int>();
            foreach (int x in s)
            {
                if (queue.Count < 3)
                    queue.Enqueue(x);
                if (queue.Count == 3)
                {
                    ans.Append(queue.Sum() % 10);
                    queue.Dequeue();
                }
            }
            if (ans.ToString() == atm.Key.ToString())
                return true;
            return false;
        }
        /// <summary>
        /// Проверяет купюры банкомата на отсутсвие среди них купюр с одинаковой серией
        /// </summary>
        static bool CheсkSeries(Dictionary<string, Stack<Banknote>> cassette)
        {
            var hs = new HashSet<string>();
            foreach (var x in cassette)
            {
                foreach (var y in x.Value)
                {
                    if (hs.Contains(y.Series))
                    {
                        Console.WriteLine("В банкомате была фальшывая купюра!");
                        return false;
                    }
                    hs.Add(y.Series);
                }
            }
            return true;
        }
        /// <summary>
        /// Вызов инкасации
        /// </summary>
        static void PickUp(Card card, ATM atm, string key)
        {
            if (CheсkKey(atm, key))
            {
                if (CheсkSeries(atm.Cassette))
                {
                    int average = 0;
                    int count = 0;
                    foreach (var x in atm.Cassette)
                    {
                        average += x.Value.Count();
                        count += 1;
                    }
                    average /= count;
                    foreach (var x in atm.Cassette)
                    {
                        if (x.Value.Count() > average)
                        {
                            for (int i = 0; i < x.Value.Count - average; i++)
                                x.Value.Pop();
                        }
                        if (x.Value.Count() < average)
                        {
                            foreach (var y in GenGoodBanknotes(x.Key, average - x.Value.Count()))
                                x.Value.Push(y);
                        }

                    }
                    Console.WriteLine("Инксация завершила работу!");
                    atm.History.Clear();
                    return;
                }

            }
            Console.WriteLine("Инкасация не смогла завершить работу!");
            //if (code.Length == atm.Key.Length)
            //{
            //    int flag = 1;
            //    for (int i = 0; i < code.Length; i++)
            //    {
            //        if (code[i] - 4 != atm.Key[i])
            //            flag = 0;
            //    }
            //    if (flag == 1)
            //    {
            //        var count = 0;
            //        foreach (var x in atm.Cassette)
            //            count += x.Value;
            //        count /= 7;
            //        foreach (var x in atm.Cassette)
            //            atm.Cassette[x.Key] = count;
            //        foreach (var x in atm.History)
            //            Console.Write(x);
            //        atm.History.Clear();
            //    }
            //    else
            //    {
            //        for (int i = 0; i < code.Length; i++)
            //        {
            //            Console.WriteLine($"{code[i] - 4} {atm.Key[i] - 0}");
            //        }
            //    }
            //}
        }
    }
}
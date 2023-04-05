using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomeworkATM
{
    public class Card
    {
        private string num;
        private string name;
        private string valid;
        private string bank;
        private int sum;
        public string Num
        {
            get
            {
                return num;
            }
            private set
            {
                if (value.Length == 16 && !Regex.Match(value, @"[\D]").Success)
                    num = value;
            }
        }
        public string Name
        {
            get
            {
                return name;
            }
            private set
            {
                if (value != null && value != "" && !Regex.Match(value, @"[^a-zA-Zа-яА-Я]").Success)
                    name = value;
            }
        }
        public string Valid
        {
            get
            {
                return valid;
            }
            private set
            {
                var ar = Regex.Split(value, @"\/").Select(x => x.ToString()).ToArray();
                if (value != null && Regex.Match(value, @"\d{2}\/\d{2}").Success && ar[0] != "00" && int.Parse(ar[0]) < 13)
                    valid = value;
            }
        }
        public string Bank
        {
            get
            {
                return bank;
            }
            private set
            {
                if (value != null && value != "")
                    bank = value;
            }
        }
        public int Sum
        {
            get
            {
                return sum;
            }
            set
            {
                if (value >= 0)
                    sum = value;
            }
        }
        public Card(string Num, string Name, string Valid, string Bank, int Sum)
        {
            this.Num = Num;
            this.Name = Name;
            this.Valid = Valid;
            this.Bank = Bank;
            this.Sum = Sum;
        }
        public override string ToString() => $"Номер карты - {Num}\nИмя владельца - {Name}\n" +
            $"Месяц и год окончания действия карты - {Valid}\nБанк-эмитент карты - {Bank}\nСумма денег на счету - {Sum}";
    }

}

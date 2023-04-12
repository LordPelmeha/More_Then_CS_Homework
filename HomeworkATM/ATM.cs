using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HomeworkATM
{
    public class ATM
    {
        public long ID;
        private string bank;
        public Dictionary<string, Stack<Banknote>> Cassette;
        public List<string> History;
        public string Key;
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
        /// <summary>
        /// Количество денег в банкомате
        /// </summary>
        public long CashAmount
        {
            get
            {
                long sum = 0;
                foreach (var x in Cassette)
                    sum += int.Parse(x.Key) * x.Value.Count;
                return sum;
            }
        }
        public ATM(string Bank)
        {
            var r = new Random();
            ID = r.NextInt64(10000000, 100000000);
            this.Bank = Bank;
            Cassette = new Dictionary<string, Stack<Banknote>>();
            History = new List<string>();
            Key = "SlivkaLenivkaSkinulaParshak";
        }
        public override string ToString()
        {
            var cas = new StringBuilder();
            foreach (var x in Cassette)
                cas.Append($"Банкноты номиналом {x.Key} имеется в количестве {x.Value} штук\n");
            var his = new StringBuilder();
            foreach (var x in History)
                his.Append(x);
            return $"{ID}\n{Bank}\n{cas.ToString()}\n{his.ToString()}\n{Key}";
        }


    }
}

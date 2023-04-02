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
        private Dictionary<string, int> Cassette;
        private List<string> History;
        private string Key;
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
        public ATM(string Bank)
        {
            var r = new Random();
            ID = r.NextInt64(10000000, 100000000);
            this.Bank = Bank;
            Cassette = new Dictionary<string, int>();
            History = new List<string>();
            Key = "ILoveMMCS";
        }
        public override string ToString()
        {
            var cas=new StringBuilder();
            foreach (var x in Cassette) 
                cas.Append($"Банкноты номиналом {x.Key} имеется в количестве {x.Value}\n");
            var his=new StringBuilder();
            foreach (var x in History)
                his.Append(x);
            return $"{ID}\n{Bank}\n{Cassette}\n{History}\n{Key}";
        }
            
    }
}

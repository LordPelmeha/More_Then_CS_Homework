using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace HomeworkATM
{
    public class Banknote
    {
        private string nominal;
        private string series;
        public string Nominal
        {
            get
            {
                return nominal;
            }
            private set
            {
                if (!Regex.Match(value, @"\D").Success)
                    nominal = value;
                else throw new ArgumentException("Неправильный формат номинала купюры");
            }
        }
        public string Series
        {
            get
            {
                return series;
            }
            private set
            {
                if (Regex.Match(value, @"[a-z]{2}]\d{9}").Success)
                    series = value;
                else
                    throw new ArgumentException("Неправильный формат серии купюры");
            }
        }
        public Banknote(string Nominal, string Series)
        {
            this.Nominal = Nominal;
            this.Series = Series;
        }
        public override string ToString() => $"{Nominal} {Series}";
    }
}

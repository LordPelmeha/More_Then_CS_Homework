using System.Diagnostics.CodeAnalysis;

namespace Homework2
{
    public class Program
    {
        static void Main()
        {
            foreach (var x in MasFilter(new int[] {1,2,3,4,4},x=>x%2==0))
                Console.Write(x+" ");
        }
        public static int[] MasFilter(int[] s,Func<int,bool> f)
        {
            int[] a = new int[s.Count()];
            int count = 0;
            for (int i = 0; i < s.Length; i++)
                if (f(s[i]))
                {
                    a[count] = s[i];
                    count++;
                }
            return a[..count];
        }
    }
}
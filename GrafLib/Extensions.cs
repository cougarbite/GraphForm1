using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafLib
{
    public static class Extensions
    {
        private static Random rng = new Random();

        public static IList<T> Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
            return list;
        }

        public static IList<T> Reverse<T>(this IList<T> list)
        {
            int i = 0;
            int n = list.Count;
            T value = list[0];
            for (; i < n-1; i++)
            {
                list[i] = list[i + 1];
            }
            list[i] = value;

            return list;
        }
    }
}

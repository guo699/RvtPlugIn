using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Extensions
{
    public static class LinqEx
    {
        public static IEnumerable<T> Yield<T>(this T item)
        {
            yield return item;
        }
        public static List<TOut> ToList<TIn,TOut>(this IEnumerable<TIn> source,Func<TIn,TOut> func)
        {
            return source.Select(func).ToList();
        }

        public static T MaxBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> func, Comparer<TKey> compare)
        {
            if (compare == null)
                compare = Comparer<TKey>.Default;

            T max = source.FirstOrDefault();
            foreach (var item in source)
            {
                if (compare.Compare(func(item), func(max)) > 0)
                    max = item;
            }
            return max;
        }

        public static T MinBy<T, TKey>(this IEnumerable<T> source, Func<T, TKey> func, Comparer<TKey> compare)
        {
            if (compare == null)
                compare = Comparer<TKey>.Default;

            T min = source.FirstOrDefault();
            foreach (var item in source)
            {
                if (compare.Compare(func(item), func(min)) < 0)
                    min = item;
            }
            return min;
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source) where T:class
        {
            return source == null || source.Count() == 0;
        }
        
        public static IEnumerable<T> WhereNotNull<T>(this IEnumerable<T> source) where T:class
        {
            foreach (var item in source)
            {
                if (item != null)
                    yield return item;
            }
        }

        public static void Shuffle<T>(this IList<T> serials,int seed = 666)
        {
            //经典的洗牌算法
            Random r = new Random(666);
            for (int i = serials.Count; i > 0; i--)
            {
                int idx = r.Next(0, serials.Count);
                T temp = serials[i - 1];
                serials[i - 1] = serials[idx];
                serials[idx] = temp;
            }
        }

        public static IEnumerable<double> LinSpace(double start,double stop,int count)
        {
            double delta = (stop - start) / count;
            for (int i = 0; i < count; i++)
            {
                yield return i * delta;
            }
        }
    }
}

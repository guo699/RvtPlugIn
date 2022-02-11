using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Extensions
{
    public static class LinqEx
    {
        public static List<TOut> ToList<TIn,TOut>(this IEnumerable<TIn> source,Func<TIn,TOut> func)
        {
            return source.Select(func).ToList();
        }
    }
}

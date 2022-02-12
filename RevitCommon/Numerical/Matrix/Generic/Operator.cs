using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix
{
    class Operator
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Add<T>(T first,T second)
        {
            dynamic n = first;
            dynamic m = second;
            return (T)(n + m);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Sub<T>(T first,T second)
        {
            dynamic n = first;
            dynamic m = second;
            return (T)(n - m);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Mult<T>(T first, T second)
        {
            dynamic n = first;
            dynamic m = second;
            return (T)(n * m);
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T Div<T>(T first, T second)
        {
            dynamic n = first;
            dynamic m = second;
            return (T)(n / m);
        }

    }
}

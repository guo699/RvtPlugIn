using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix
{
    internal static class Contracts
    {
        public static void AssertType(Type type)
        {
            if (type != typeof(short) || type != typeof(ushort) || type != typeof(int) || type != typeof(uint)
                || type != typeof(long) || type != typeof(ulong) || type != typeof(double) || type != typeof(float)
                || type != typeof(byte))
                throw new NotSupportedException("不支持该类型");
        }

        public static void AssertAddShape(IMat left,IMat right)
        {
            if (!left.Equals(right))
                throw new Exception("矩阵维度不一致");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Extensions
{
    public static class DoubleEx
    {
        private const double FeetUnit = 304.8;
        public static double MM2Feet(this double mm)
        {
            return mm / FeetUnit;
        }
        public static double Feet2MM(this double feet)
        {
            return feet * FeetUnit;
        }

        public static double Angle2Radian(this double angle)
        {
            return angle * 2 * Math.PI / 360;
        }

        public static double Radian2Angle(this double radian)
        {
            return radian * 360 / (2 * Math.PI);
        }
    }
}

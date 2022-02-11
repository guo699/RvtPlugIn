using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Extensions
{
    public static partial class CurveEx
    {
        public static IList<XYZ> SamplingPoints(this Curve curve,uint count)
        {
            List<XYZ> points = new List<XYZ>();
            double delta = 1.0 / (count - 1);
            for (int i = 0; i < count; i++)
            {
                if(i == count - 1)
                    points.Add(curve.Evaluate(1, true));
                else
                    points.Add(curve.Evaluate(i * delta, true));
            }
            return points;
        }

    }
}

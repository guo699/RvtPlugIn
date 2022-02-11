using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Utilitis
{
    public class XYZToCSV
    {
        public static string ToString(IList<XYZ> points,char split = ',')
        {
            StringBuilder builder = new StringBuilder();

            XYZ point;
            for (int i = 0; i < points.Count; i++)
            {
                point = points[i];
                builder.Append(point.X);
                builder.Append(split);
                builder.Append(point.Y);
                builder.Append(split);
                builder.Append(point.Z);
                if (i < points.Count - 1)
                    builder.Append("\n");
            }
            return builder.ToString();
        }
    }
}

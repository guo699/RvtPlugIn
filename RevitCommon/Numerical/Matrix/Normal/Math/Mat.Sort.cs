using RevitCommon.Numerical.Matrix.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix.Normal
{
    public partial struct Mat
    {
        public static Mat ArgSort(Mat source,Axis axis = Axis.H)
        {
            Mat indices = new Mat(source.Shape);
            //每行排序，返回索引
            if(axis == Axis.H || source.IsRowVector())
            {

            }
            else if(axis == Axis.V || source.IsColVector())
            {

            }
            return indices;
        }
    }
}

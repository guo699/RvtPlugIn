using RevitCommon.Numerical.Matrix.Basic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix.Normal
{
    public partial class Mat
    {
        public static Mat ArgSort(Mat source,Axis axis = Axis.H)
        {
            Mat indices = new Mat(source.Shape);
            //每行排序，返回索引
            if(source.IsRowVector())
            {
                for (int i = 0; i < source.Shape.Col; i++)
                {
                    indices[0, i] = i;
                }

                double temp = 0;
                for (int i = source.Shape.Col-1; i >= 0; i--)
                {
                    for (int j = 0; j < i; j++)
                    {
                        if(source[0,j] > source[0,j+1])
                        {
                            temp = indices[0, j];
                            indices[0, j] = indices[0, j + 1];
                            indices[0, j + 1] = temp;
                        }
                    }
                }
            }
            else if(source.IsColVector())
            {

            }
            return indices;
        }
    }
}

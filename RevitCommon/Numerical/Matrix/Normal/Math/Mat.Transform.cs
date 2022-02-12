using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix.Normal
{
    public partial class Mat
    {
        public Mat T(Mat source)
        {
            Mat ret = new Mat(source.Shape.Col, source.Shape.Row);
            for (int i = 0; i < source.Shape.Row; i++)
            {
                for (int j = 0; j < source.Shape.Col; j++)
                {
                    ret[j, i] = source[i, j];
                }
            }
            return ret;
        }

        public Mat ReShape(Mat source,Shape newshape)
        {
            if (source.Shape.Size != newshape.Size)
                throw new NotSupportedException();
            Mat ret = new Mat(newshape);
            for (int i = 0; i < newshape.Row; i++)
            {
                for (int j = 0; j < newshape.Col; j++)
                {
                    ret[i, j] = source.MemoryStorage[i * source.Shape.Col + j];
                }
            }
            return ret;
        }
    }
}

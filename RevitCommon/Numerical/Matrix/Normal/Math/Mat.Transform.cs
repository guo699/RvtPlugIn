﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix.Normal
{
    public partial class Mat
    {
        public Mat T()
        {
            Mat ret = new Mat(this.Shape.Col, this.Shape.Row);
            for (int i = 0; i < this.Shape.Row; i++)
            {
                for (int j = 0; j < this.Shape.Col; j++)
                {
                    ret[j, i] = this[i, j];
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
        /// <summary>
        /// 若干个矩阵水平方向叠加
        /// </summary>
        public Mat HStack(params Mat[] mats)
        {
            int[] rowls = mats.Select(n => n.Shape.Row).ToArray();
            int[] colls = mats.Select(n => n.Shape.Col).ToArray();
            if (!rowls.All(n => n == rowls[0]))
                throw new NotSupportedException("矩阵行数不一致");
            int sumcol = colls.Sum();

            Mat ret = new Mat(rowls[0], sumcol);
            int col = 0;
            int catcol = 0;
            for (int i = 0; i < colls.Length; i++)
            {
                col = colls[i];
                for (int j = 0; j < rowls[0]; j++)
                {
                    for (int k = 0; k < col; k++)
                    {
                        ret[j, catcol + k] = mats[i][j, k];
                    }
                }
                catcol += col;
            }
            return ret;
        }
    }
}

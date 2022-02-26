using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix
{
    public enum MatTypeCode:short
    {
        /// <summary>
        /// 标量
        /// </summary>
        Scalar = 0,
        /// <summary>
        /// 行向量
        /// </summary>
        RowVector = 1,
        /// <summary>
        /// 列向量
        /// </summary>
        ColVector = 2,
        /// <summary>
        /// 矩阵
        /// </summary>
        Matrix = 3,
        /// <summary>
        /// 未知
        /// </summary>
        UnKnow = 100,
    }
}

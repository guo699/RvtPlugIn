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
        public MatTypeCode Kind
        {
            get
            {
                if (IsScalar())
                    return MatTypeCode.Scalar;
                else if (IsRowVector())
                    return MatTypeCode.RowVector;
                else if (IsColVector())
                    return MatTypeCode.ColVector;
                else if (IsMatrix())
                    return MatTypeCode.Matrix;
                else return MatTypeCode.UnKnow;
            }
        }
        public bool IsScalar() => this.Shape.Row == 1 && this.Shape.Col == 1;
        public bool IsRowVector()
        {
            return !IsScalar() && this.Shape.Row == 1;
        }
        public bool IsColVector()
        {
            return !IsScalar() && this.Shape.Col == 1;
        }
        /// <summary>
        /// 矩阵行数和列数均大于1才为矩阵
        /// </summary>
        public bool IsMatrix()
        {
            return this.Shape.Col > 1 && Shape.Col > 1;
        }
    }
}

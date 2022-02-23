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
        private bool IsScalar() => this.Shape.Row == 1 && this.Shape.Col == 1;
        private bool IsRowVector() => !IsScalar() && this.Shape.Row == 1;
        private bool IsColVector() => !IsScalar() && this.Shape.Col == 1;
        private bool IsMatrix() => this.Shape.Col > 1 && Shape.Col > 1;
    }
}

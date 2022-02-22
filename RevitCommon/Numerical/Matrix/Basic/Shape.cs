using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix
{
    public struct Shape
    {
        private long size;
        public int Row { get; private set; }
        public int Col { get; private set; }
        public long Size => size;
        public Shape(int row,int col)
        {
            Row = row;
            Col = col;
            size = row * col;
        }

        /// <summary>
        /// if 0 return Row,if 1 return Col
        /// </summary>
        /// <param name="index">o 0 or 1</param>
        /// <returns>Row or Col</returns>
        public int this[int index]
        {
            get
            {
                if (index == 0)
                    return Row;
                else if (index == 1)
                    return Col;
                else
                    throw new IndexOutOfRangeException();
            }
        }

        public static implicit operator Shape((int row,int col) shape)
        {
            return new Shape(shape.row, shape.col);
        }

        public override string ToString()
        {
            return $"({Row},{Col})";
        }

        public bool Equals(in Shape other)
        {
            return this.Row == other.Row && this.Col == other.Col;
        }
    }
}

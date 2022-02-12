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
            size = ((long)row) * ((long)col);
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

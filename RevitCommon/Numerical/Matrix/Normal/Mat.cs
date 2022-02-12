using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix.Normal
{
    /// <summary>
    /// 非泛型二维矩阵
    /// </summary>
    public sealed partial class Mat : IDisposable, IMat
    {
        private UnmgdMemoryBlock<double> MemoryStorage;
        private Shape _shape;
        public Shape Shape { get => _shape; set => throw new NotImplementedException(); }
        public Mat(int row, int col, double fillvalue = 0)
        {
            _shape = new Shape(row, col);
            MemoryStorage = new UnmgdMemoryBlock<double>(_shape.Size);
            FillValue(fillvalue);
        }
        public Mat(Shape shape)
        {
            _shape = shape;
            MemoryStorage = new UnmgdMemoryBlock<double>(_shape.Size);
        }
        public Mat(double[,] array)
        {
            _shape = new Shape(array.GetLength(0), array.GetLength(1));
            MemoryStorage = new UnmgdMemoryBlock<double>(_shape.Size);
            for (int i = 0; i < _shape.Row; i++)
            {
                for (int j = 0; j < _shape.Col; j++)
                {
                    this[i, j] = array[i, j];
                }
            }
        }
        public double this[int row, int col]
        {
            get{ return MemoryStorage[_shape.Row * row + col];}
            set{ MemoryStorage[_shape.Col * row + col] = value;}
        }
        private void FillValue(double fillvalue)
        {
            for (int i = 0; i < _shape.Size; i++)
            {
                MemoryStorage[i] = fillvalue;
            }
        }
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < _shape.Row; i++)
            {
                for (int j = 0; j < _shape.Col; j++)
                {
                    builder.Append(this[i, j]);
                    if (j < _shape.Col - 1)
                        builder.Append(",");
                }
                if (i < _shape.Row - 1)
                    builder.Append("\n");
            }
            return builder.ToString();
        }
        public void Dispose()
        {
            MemoryStorage.Free();
        }
        ~Mat()
        {
            MemoryStorage.Free();
        }
    }
}

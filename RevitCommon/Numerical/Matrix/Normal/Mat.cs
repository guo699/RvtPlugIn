using RevitCommon.Numerical.Matrix.Basic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix.Normal
{
    /// <summary>
    /// 非泛型二维矩阵
    /// </summary>
    public partial class Mat : IDisposable, IMat,IEnumerable
    {
        internal UnmgdMemoryBlock<double> MemoryStorage;
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
        public IEnumerator GetEnumerator()
        {
            return new MatEnumerator(this);
        }
        public double[] ToArray()
        {
            double[] array = new double[_shape.Size];
            for (int i = 0; i < _shape.Size; i++)
            {
                array[i] = MemoryStorage[i];
            }
            return array;
        }
        public Mat Copy()
        {
            Mat clone = new Mat(this.Shape);
            UnmgdMemoryBlock<double>.Copy(this.MemoryStorage, clone.MemoryStorage);
            return clone;
        }

        public static implicit operator Mat(double[,] array)
        {
            return new Mat(array);
        }
        ~Mat()
        {
            MemoryStorage.Free();
        }
    }

    class MatEnumerator : IEnumerator
    {
        private Mat _mat;
        private int _index;
        public object Current => _mat.MemoryStorage[_index];
        public MatEnumerator(Mat mat)
        {
            this._mat = mat;
            this._index = -1;
        }
        public bool MoveNext()
        {
            if(_index > -1 && _index < _mat.Shape.Size)
            {
                _index++;
                return true;
            }
            return false;
        }
        public void Reset()
        {
            _index = -1;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix
{
    /// <summary>
    /// 二维矩阵
    /// </summary>
    public sealed partial class Mat<T> :IDisposable,IMat where T:unmanaged
    {
        private UnmgdMemoryBlock<T> MemoryStorage;
        private Shape _shape;

        public Shape Shape { get => _shape; set => throw new NotImplementedException(); }

        public Mat(int row,int col,T fillvalue = default(T))
        {
            _shape = new Shape(row, col);
            MemoryStorage = new UnmgdMemoryBlock<T>(_shape.Size);
        }

        public Mat(Shape shape)
        {
            _shape = shape;
            MemoryStorage = new UnmgdMemoryBlock<T>(_shape.Size);
        }

        public Mat(T[,] array)
        {
            _shape = new Shape(array.GetLength(0), array.GetLength(1));
            MemoryStorage = new UnmgdMemoryBlock<T>(_shape.Size);
        }

        public T this[int row,int col]
        {
            get
            {
                return MemoryStorage[_shape.Row * row + col];
            }
            set
            {
                MemoryStorage[_shape.Col * row + col] = value;
            }
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

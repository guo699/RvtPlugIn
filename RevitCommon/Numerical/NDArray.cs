using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical
{
    public class NDArray<T> : IDisposable,IEnumerable where T:unmanaged
    {
        private IntPtr _ptr;
        private bool _disposed;
        private readonly unsafe void* _address;
        public NDArray(long count)
        {
            unsafe
            {
                _ptr = Marshal.AllocHGlobal(new IntPtr(sizeof(T) * count));
                _address = _ptr.ToPointer();
            }
            _disposed = false;
        }

        #region Getter
        public T GetValue(long index)
        {
            unsafe
            {
                return ((T*)_address)[index];
            }
        }
        public T this[long index]
        {
            get
            {
                unsafe
                {
                    return ((T*)_address)[index];
                }
            }
            set
            {
                unsafe
                {
                    ((T*)_address)[index] = value;
                }
            }
        }
        #endregion

        public void Dispose() => ReleaseUnmgdMemory();
        private void ReleaseUnmgdMemory()
        {
            if (_disposed)
                return;
            else
            {
                _disposed = true;
                Marshal.FreeHGlobal(_ptr);
            }
        }

        public IEnumerator GetEnumerator()
        {
            return new Enumerator();
        }

        ~NDArray() => ReleaseUnmgdMemory();
    }

    public class Enumerator : IEnumerator
    {
        public object Current => throw new NotImplementedException();

        public bool MoveNext()
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }
    }
}

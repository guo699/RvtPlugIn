using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Numerical.Matrix
{
    internal unsafe struct UnmgdMemoryBlock<T> where T:unmanaged
    {
        private T* _address;
        private void* _voidAddress;
        private IntPtr _ptr;
        private long _itemCount;

        public long ItemCount { get { return _itemCount; } }
        public bool Released { get; set; }
        public UnmgdMemoryBlock(long itemCount)
        {
            long bytecount = ((long)sizeof(T)) * itemCount;
            this._ptr = Marshal.AllocHGlobal(new IntPtr(bytecount));
            this._voidAddress = _ptr.ToPointer();
            this._address = (T*)_voidAddress;
            this._itemCount = itemCount;
            this.Released = false;
        }

        public T this[long index]
        {
            get 
            {
                if (index >= _itemCount)
                    throw new IndexOutOfRangeException();
                return _address[index]; 
            }
            set 
            {
                if (index >= _itemCount)
                    throw new IndexOutOfRangeException();
                _address[index] = value; 
            }
        }

        public void Free()
        {
            if (Released)
                return;
            Released = true;
            Marshal.FreeHGlobal(_ptr);
        }
    }
}

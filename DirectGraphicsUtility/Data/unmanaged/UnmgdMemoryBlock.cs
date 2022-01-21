using DirectGraphicsUtility.Data.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DirectGraphicsUtility.Data.unmanaged
{
    public unsafe struct UnmgdMemoryBlock<T>:IUnmgdMemoryBlock where T : unmanaged
    {
        private Disposer _disposer;
        private long _bytecount;
        private IntPtr _ptr;
        private void* _address;
        public UnmgdMemoryBlock(long count)
        {
            var itemsize = InfoOf<T>.Size;
            _bytecount = itemsize * count;
            _ptr = Marshal.AllocHGlobal(new IntPtr(_bytecount));
            _address = _ptr.ToPointer();
            _disposer = new Disposer(_ptr);
        }

        public void Free()
        {
            _disposer.Dispose();
        }

        public void Reallocate(long length, bool copyOldValues = false)
        {
            throw new NotImplementedException();
        }
    }
}

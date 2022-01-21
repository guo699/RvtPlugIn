using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DirectGraphicsUtility.Data.unmanaged
{
    public class Disposer : IDisposable
    {
        private IntPtr _ptr;
        private bool _released;
        public Disposer(IntPtr ptr)
        {
            this._ptr = ptr;
            this._released = false;
        }

        private void releaseUnmanagedMemory()
        {
            if (_released)
                return;
            _released = true;
            Marshal.FreeHGlobal(_ptr);
        }

        public void Dispose()
        {
            releaseUnmanagedMemory();
        }

        ~Disposer()
        {
            releaseUnmanagedMemory();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            NDArray array = new NDArray(254 * 1024 * 800);



            array.Dispose();
        }

        public static string GetMemory()
        {
            Process proc = Process.GetCurrentProcess();
            long b = proc.PrivateMemorySize64;
            for (int i = 0; i < 2; i++)
            {
                b /= 1024;
            }
            return b + "MB";
        }
    }

    class NDArray:IDisposable
    {
        private IntPtr _ptr;
        private bool _disposed;
        public NDArray(long count)
        {
            _ptr = Marshal.AllocHGlobal(new IntPtr(sizeof(int) * count));
            _disposed = false;
        }

        public int this[long index]
        {
            get
            {
            }
        }

        public void Dispose()
        {
            ReleaseUnmgdMemory();
        }

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
        ~NDArray()
        {
            ReleaseUnmgdMemory();
        }
    }
}

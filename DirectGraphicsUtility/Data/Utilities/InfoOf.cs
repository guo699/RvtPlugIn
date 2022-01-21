using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace DirectGraphicsUtility.Data.Utilities
{
    public class InfoOf<T> where T:unmanaged
    {
        public static readonly int Size;
        static InfoOf()
        {
            Size = Marshal.SizeOf<T>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DirectGraphicsUtility.Data.unmanaged
{
    interface IUnmgdMemoryBlock
    {
        void Reallocate(long length, bool copyOldValues = false);
        void Free();
    }
}

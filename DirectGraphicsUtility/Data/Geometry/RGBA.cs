using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

namespace DirectGraphicsUtility.Data.Geometry
{
    public struct RGBA
    {
        public uint R;
        public uint G;
        public uint B;
        public uint A;
        public RGBA(uint r,uint g,uint b,uint a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
        public RGBA(uint r,uint g,uint b)
        {
            R = r;
            G = g;
            B = b;
            A = 255;
        }

        public RGBA(ColorWithTransparency color)
        {
            R = color.GetRed();
            G = color.GetGreen();
            B = color.GetBlue();
            A = color.GetTransparency();
        }
    }
}

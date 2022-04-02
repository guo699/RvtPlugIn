using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Utilitis
{
    public static class ExtendStorageService
    {
        public static void Write(Element element,ExtendKey key,string content)
        {

        }
        public static void WriteBytes(Element element,ExtendKey key,byte[] content)
        {

        }
        public static string Read(Element element,ExtendKey key)
        {
            return string.Empty;
        }
        public static byte[] ReadBytes(Element element,ExtendKey key)
        {
            return new byte[100];
        }
    }
}

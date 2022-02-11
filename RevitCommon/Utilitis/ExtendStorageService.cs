using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Utilitis
{
    public sealed class ExtendStorageService
    {
        public static readonly ExtendStorageService Instance = new ExtendStorageService();
        private ExtendStorageService() { }
        public void Write(Element element,ExtendKey key,string content)
        {

        }
        public void WriteBytes(Element element,ExtendKey key,byte[] content)
        {

        }
        public string Read(Element element,ExtendKey key)
        {
            return string.Empty;
        }
        public byte[] ReadBytes(Element element,ExtendKey key)
        {
            return new byte[100];
        }
    }
}

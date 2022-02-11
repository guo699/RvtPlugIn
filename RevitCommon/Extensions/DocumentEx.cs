using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Extensions
{
    public static class DocumentEx
    {
        public static IList<Element> GetAllElements(this Document doc)
        {
            return new FilteredElementCollector(doc).ToElements();
        }

        public static IEnumerable<T> GetElements<T>(this Document doc)
        {
            return new FilteredElementCollector(doc).OfClass(typeof(T)).ToElements().OfType<T>();
        }
    }
}

using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Extensions
{
    public static partial class ElementEx
    {
        public static IList<Face> GetAllFaces(this Element element)
        {
            List<Face> faces = new List<Face>();
            return faces;
        }
    }
}

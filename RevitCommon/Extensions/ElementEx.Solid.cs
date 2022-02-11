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
        public static IList<Solid> GetSolids(this Element elem)
        {
            List<Solid> solids = new List<Solid>();
            Options options = new Options();
            options.DetailLevel = ViewDetailLevel.Fine;
            options.IncludeNonVisibleObjects = true;
            options.ComputeReferences = false;
            GeometryElement geometryElement = elem.get_Geometry(options);

            foreach (var item in geometryElement)
            {
                if(item is Solid solid)
                {
                    solids.Add(solid);
                }
            }

            return solids;
        }
    }
}

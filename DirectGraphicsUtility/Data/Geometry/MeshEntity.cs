using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.DB;

namespace DirectGraphicsUtility.Data.Geometry
{
    class MeshEntity
    {
        private IList<MeshTriangle> _triangles;
        public MeshEntity(IList<MeshTriangle> triangles)
        {
            this._triangles = triangles;
        }
    }
}

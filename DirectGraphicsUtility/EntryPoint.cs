using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.DirectContext3D;
using Autodesk.Revit.DB.ExternalService;
using Autodesk.Revit.UI;
using DirectGraphicsUtility.Core;
using DirectGraphicsUtility.Model;

namespace DirectGraphicsUtility
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class EntryPoint : IExternalCommand
    {
        private UIDocument _uidoc;
        private Document _doc;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            _uidoc = commandData.Application.ActiveUIDocument;
            _doc = _uidoc.Document;

            XYZ p1 = _uidoc.Selection.PickPoint("拾取第一个点");
            XYZ p2 = _uidoc.Selection.PickPoint("拾取第二个点");
            XYZ p3 = _uidoc.Selection.PickPoint("拾取第三个点");
            XYZ p4 = _uidoc.Selection.PickPoint("拾取第四个点");

            //Arc arc = Arc.Create(p1, p2, XYZ.Zero);
            //Line line = Line.CreateBound(p1, p2);
            //IDirectContext3DServer revitServer = new CurveDirectServer(line);
            //DirectServerRegister.AddDirect3DServer(revitServer);

            CTriangular triangular = new CTriangular();
            triangular.PointA = p1;
            triangular.PointB = p2;
            triangular.PointC = p3;
            triangular.Normal = (p1 - p2).CrossProduct(p3 - p2).Normalize();
            triangular.TColor = new ColorWithTransparency(0, 255, 0, 1);

            CTriangular triangular2 = new CTriangular();
            triangular2.PointA = p1;
            triangular2.PointB = p2;
            triangular2.PointC = p4;
            triangular2.Normal = (p1 - p2).CrossProduct(p4 - p2).Normalize();
            triangular2.TColor = new ColorWithTransparency(0, 255, 0, 1);

            CMesh mesh = new CMesh();
            mesh.Triangulars = new List<CTriangular>() { triangular, triangular2 };
            IDirectContext3DServer revitServer = new FaceDirectServer(mesh);
            //IDirectContext3DServer revitServer = new FaceDirectServer(triangular);
            DirectServerRegister.AddDirect3DServer(revitServer);

            _uidoc.UpdateAllOpenViews();
            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ClearBuffer : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            DirectServerRegister.ClearDirect3DServer();
            commandData.Application.ActiveUIDocument.UpdateAllOpenViews();
            return Result.Succeeded;
        }
    }
}

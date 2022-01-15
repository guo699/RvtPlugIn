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

            //CCude cube = new CCude(new XYZ(0, 0, 0), new XYZ(10, 10, 10));
            CSurface surface = new CSurface();
            IDirectContext3DServer revitServer = new FaceDirectServer(surface.Triangulars);
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

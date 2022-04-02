using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitCommon.Extensions;
using RevitCommon.Utilitis;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BaseTest.Command
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class CadTransCmd : IExternalCommand
    {
        private UIDocument UIDoc;
        private Document Doc;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDoc = commandData.Application.ActiveUIDocument;
            Doc = UIDoc.Document;

            Reference r = UIDoc.Selection.PickObject(ObjectType.PointOnElement);

            Element elem = Doc.GetElement(r);

            ImportInstance instance = elem as ImportInstance;

            GeometryObject obj = instance.GetGeometryObjectFromReference(r);

            GraphicsStyle gs = Doc.GetElement(obj.GraphicsStyleId) as GraphicsStyle;

            return Result.Succeeded;
        }
    }
}

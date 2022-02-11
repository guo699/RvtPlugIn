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

namespace BaseTest.Command
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class AboutCurve : IExternalCommand
    {
        private UIDocument UIDoc;
        private Document Doc;
        private const string path = @"C:\Users\IronBin\Desktop\points.csv";
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDoc = commandData.Application.ActiveUIDocument;
            Doc = UIDoc.Document;

            Reference r = UIDoc.Selection.PickObject(ObjectType.Element);
            DetailCurve dcurve = Doc.GetElement(r) as DetailCurve;
            Curve curve = dcurve.GeometryCurve;
            IList<XYZ> points = curve.SamplingPoints(20);
            //string str = XYZToCSV.ToString(points);

            //using(StreamWriter writer = new StreamWriter(path))
            //{
            //    writer.Write(str);
            //}

            XYZ v1 = new XYZ(-0.98894914, 0.1482552, 0);
            XYZ v2 = new XYZ(-0.1482552, -0.98894914, 0);

            XYZ pnt = curve.GetEndPoint(0);
            Line l1 = Line.CreateBound(pnt, pnt.Add(-v1*curve.Length));
            Line l2 = Line.CreateBound(pnt, pnt.Add(-v2*curve.Length));

            TransactionInvoker.Action(Doc, "XX", ()=> {

                Doc.Create.NewDetailCurve(Doc.ActiveView, l1);
                Doc.Create.NewDetailCurve(Doc.ActiveView, l2);
            });
            foreach (var item in points.ToList())
            {
                item.DrawCircle(Doc, 10.0.MM2Feet());
            }

            return Result.Succeeded;
        }
    }
}

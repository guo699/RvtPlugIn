using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

namespace BaseTest.Command
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class DocRegenerateCmd : IExternalCommand
    {
        UIDocument uidoc;
        Document doc;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            uidoc = commandData.Application.ActiveUIDocument;
            doc = uidoc.Document;

            Stopwatch sw1 = Stopwatch.StartNew();
            using(Transaction ts = new Transaction(doc))
            {
                ts.Start("Regenerate");

                Line line = Line.CreateBound(XYZ.Zero, new XYZ(1, 0, 0));
                Line target;
                for (int i = 0; i < 500; i++)
                {
                    target = line.CreateOffset(i / 304.8, XYZ.BasisZ) as Line;
                    doc.Create.NewDetailCurve(doc.ActiveView, target);
                    //doc.Regenerate();
                }

                ts.Commit();
            }
            sw1.Stop();
            double t1 = sw1.Elapsed.TotalMilliseconds;

            TaskDialog.Show("Time", t1.ToString());

            return Result.Succeeded;
        }
    }
}

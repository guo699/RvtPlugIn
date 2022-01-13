using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BaseTest.Projects;
using Autodesk.Revit.UI.Selection;
using BaseTest.Common;
using System.Diagnostics;

namespace BaseTest.Command
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class ExportStlCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            ElementToStlModel export = new ElementToStlModel(uidoc);

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class DimesionCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            Selection selection = uidoc.Selection;

            Reference ref1 = selection.PickObject(ObjectType.Element);
            Reference ref2 = selection.PickObject(ObjectType.Element);

            if(ref1 != null && ref2 != null)
            {
                MEPCurve p1 = doc.GetElement(ref1) as MEPCurve;
                MEPCurve p2 = doc.GetElement(ref2) as MEPCurve;
                
                using(Transaction ts = new Transaction(doc))
                {
                    ts.Start("Demision");
                    doc.Create.NewAlignment(doc.ActiveView, ref1, ref2);
                    ts.Commit();
                }
            }

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class DrawTable : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            DetailTableCreate create = new DetailTableCreate();
            create.SetColumnWidths(20,30,40,50,60,70,80);
            create.SetApartColumns(0,3,6);
            create.SetApartRows(0, 5);
            create.Rows = 30;

            Stopwatch s1 = Stopwatch.StartNew();
            List<ElementId> ids = new List<ElementId>();
            TransactionDelegate.Invoke(doc, () =>
             {
                 for (int i = 0; i < 100; i++)
                 {
                    create.CreateHLines(new UV(i*500/308.4,0));
                    create.CreateVLines(new UV(i*500/308.4,0));
                     foreach (var hlines in create.HLines)
                     {
                         foreach (var line in hlines)
                         {
                            var ele = doc.Create.NewDetailCurve(doc.ActiveView, line);
                             ids.Add(ele.Id);
                         }
                     }

                     foreach (var vlines in create.VLines)
                     {
                         foreach (var line in vlines)
                         {
                             var ele = doc.Create.NewDetailCurve(doc.ActiveView, line);
                             ids.Add(ele.Id);
                         }
                     }

                     //doc.Create.NewGroup(ids);
                     ids.Clear();
                 }
             });

            s1.Stop();
            TaskDialog.Show("Time", s1.Elapsed.TotalSeconds.ToString());

            return Result.Succeeded;
        }
    }

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class OffSetLines : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            var create = new OffsetLinesTest();


            double t1 = 0;
            double t2 = 0;

            int num = 5000;

            TransactionDelegate.Invoke(doc, () =>
            {
                Stopwatch s1 = Stopwatch.StartNew();
                var ls1 = create.CreateOffsetLines(num);
                foreach (var item in ls1)
                {
                    doc.Create.NewDetailCurve(doc.ActiveView, item);
                }
                s1.Stop();
                t1 = s1.Elapsed.TotalSeconds;


                Stopwatch s2 = Stopwatch.StartNew();
                var ls2 = create.CreateNewLines(num);
                foreach (var item in ls2)
                {
                    doc.Create.NewDetailCurve(doc.ActiveView, item);
                }
                s2.Stop();
                t2 = s1.Elapsed.TotalSeconds;
            });

            TaskDialog.Show("Time", t1.ToString()+"\n"+t2.ToString());

            return Result.Succeeded;
        }
    }
}

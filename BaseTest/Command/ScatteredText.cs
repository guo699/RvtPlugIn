using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitCommon.Extensions;
using RevitCommon.Utilitis;

namespace BaseTest.Command
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    class ScatteredText : IExternalCommand
    {
        private UIDocument UIDoc;
        private Document Doc;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDoc = commandData.Application.ActiveUIDocument;
            Doc = UIDoc.Document;

            //创建一定范围内零散的文字
            TextNoteType noteType = Doc.GetElements<TextNoteType>().FirstOrDefault();

            const double X = 2.8;
            const double Y = 3.1;
            this.CreateTexts(100, noteType,X,Y);


            return Result.Succeeded;
        }

        public List<TextNote> CreateTexts(int count, TextNoteType notetype,double xrange,double yrange)
        {
            List<TextNote> notes = new List<TextNote>(count);
            List<XYZ> points = RandomPoints(count,xrange,yrange);
            TextNote note;
            TextNoteOptions options = new TextNoteOptions(notetype.Id);
            Random r = new Random();
            TransactionInvoker.Action(Doc, "Create TextNote", () =>
            {
                for (int i = 0; i < count; i++)
                {
                    options.Rotation = r.NextDouble() * Math.PI;
                    note = TextNote.Create(Doc, Doc.ActiveView.Id, points[i], (i + 1).ToString("000"), options);
                    notes.Add(note);
                }

                Line l1 = Line.CreateBound(new XYZ(0,0,0), new XYZ(xrange, 0, 0));
                Line l2 = l1.CreateOffset(-yrange, Doc.ActiveView.ViewDirection) as Line;
                Line l3 = Line.CreateBound(new XYZ(0,0,0), new XYZ(0, yrange, 0));
                Line l4 = l3.CreateOffset(xrange, Doc.ActiveView.ViewDirection) as Line;
                Doc.Create.NewDetailCurve(Doc.ActiveView, l1);
                Doc.Create.NewDetailCurve(Doc.ActiveView, l2);
                Doc.Create.NewDetailCurve(Doc.ActiveView, l3);
                Doc.Create.NewDetailCurve(Doc.ActiveView, l4);
            });
            return notes;
        }

        public List<XYZ> RandomPoints(int count,double xrange,double yrange)
        {
            List<XYZ> points = new List<XYZ>(count);

            Random r = new Random();
            XYZ point;
            double x, y, z;
            for (int i = 0; i < count; i++)
            {
                x = r.NextDouble();
                y = r.NextDouble();
                z = r.NextDouble();
                point = new XYZ(x*xrange, y*yrange, z);
                points.Add(point);
            }
            return points;
        }
    }
}

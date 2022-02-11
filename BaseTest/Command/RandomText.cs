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
    internal class RandomText : IExternalCommand
    {
        private UIDocument UIDoc;
        private Document Doc;
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDoc = commandData.Application.ActiveUIDocument;
            Doc = UIDoc.Document;

            //TextNoteType noteType = Doc.GetElements<TextNoteType>().FirstOrDefault();
            //this.CreateTexts(40, noteType);

            double[] result = new double[5];
            for (int i = 0; i < 5; i++)
            {
                IList<Reference> refs = UIDoc.Selection.PickObjects(ObjectType.Element, new SelectionFilter(n => n is TextNote));
                List<TextNote> notes = refs.Select(Doc.GetElement).OfType<TextNote>().ToList();

                List<XYZ> points = notes.Select(n => n.Coord).ToList();

                List<double> xs = points.Select(n => n.X).ToList();
                List<double> ys = points.Select(n => n.Y).ToList();
                double stdx = Variance(xs);
                double stdy = Variance(ys);
                double stdplane = Math.Pow(stdx.Feet2MM(), 2) + Math.Pow(stdy.Feet2MM(), 2);
                result[i] = stdplane;
            }

            TaskDialog.Show("Dis", string.Join("\n",result));

            return Result.Succeeded;
        }

        public double Variance(List<double> values)
        {
            double mean = values.Sum() / (double)values.Count;
            return values.Select(n => Math.Pow(n - mean, 2)).Sum();
        }

        public List<double> OneToNearMeanDistance(List<XYZ> points,int neighbors)
        {
            List<List<double>> dissArr = EachToOtherDistance(points);
            List<double> nearDis = new List<double>(points.Count);
            double dis;
            foreach (var arr in dissArr)
            {
                dis = arr.OrderBy(n => n).ToList().GetRange(0, neighbors).Sum();
                nearDis.Add(dis);
            }
            return nearDis;
        }

        public List<List<double>> EachToOtherDistance(List<XYZ> points)
        {
            int count = points.Count;
            List<List<double>> dissLs = new List<List<double>>();
            List<double> diss;
            for (int i = 0; i < count; i++)
            {
                diss = OneToOtherDistance(i, points);
                dissLs.Add(diss);
            }
            return dissLs;
        }

        public List<double> OneToOtherDistance(int pntIndex,List<XYZ> points)
        {
            int count = points.Count;
            List<double> diss = new List<double>(count-1);

            double dis;
            XYZ target = points[pntIndex];
            for (int i = 0; i < count; i++)
            {
                if (pntIndex == i)
                    continue;
                else
                {
                    dis = target.DistanceTo(points[i]);
                    diss.Add(dis);
                }
            }
            return diss;
        }

        public List<XYZ> RandomPoints(int count)
        {
            List<XYZ> points = new List<XYZ>(count);

            Random r = new Random();
            XYZ point;
            double x, y, z;
            for (int i = 0; i < count; i++)
            {
                x = r.NextDouble()/10;
                y = r.NextDouble()/10;
                z = r.NextDouble()/10;
                point = new XYZ(x, y, z);
                points.Add(point);
            }
            return points;
        }

        public List<TextNote> CreateTexts(int count,TextNoteType notetype)
        {
            List<TextNote> notes = new List<TextNote>(count);
            List<XYZ> points = RandomPoints(count);
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

                 Line l1 = Line.CreateBound(new XYZ(), new XYZ(0.1, 0, 0));
                 Line l2 = l1.CreateOffset(-0.1, Doc.ActiveView.ViewDirection) as Line;
                 Line l3 = Line.CreateBound(new XYZ(), new XYZ(0, 0.1, 0));
                 Line l4 = l3.CreateOffset(0.1, Doc.ActiveView.ViewDirection) as Line;
                 Doc.Create.NewDetailCurve(Doc.ActiveView, l1);
                 Doc.Create.NewDetailCurve(Doc.ActiveView, l2);
                 Doc.Create.NewDetailCurve(Doc.ActiveView, l3);
                 Doc.Create.NewDetailCurve(Doc.ActiveView, l4);
             });
            return notes;
        }
    }
}

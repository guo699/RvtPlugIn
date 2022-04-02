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

        
    }
}

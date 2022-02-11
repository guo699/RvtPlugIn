using Autodesk.Revit.DB;
using RevitCommon.Utilitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitCommon.Extensions
{
    public static class XYZEx
    {
        public static DetailLine DrawVertical(this XYZ xyz,Document doc, double length)
        {
            DetailLine dline = null;
            View view = doc.ActiveView;
            Line vline = Line.CreateBound(xyz, xyz.Add(view.UpDirection)*length);
            TransactionInvoker.Action(doc, "DrawVertivalLine", () =>
            {
                dline = doc.Create.NewDetailCurve(view, vline) as DetailLine;
            });
            return dline;
        }

        public static DetailArc DrawCircle(this XYZ xyz,Document doc,double radius)
        {
            DetailArc darc = null;
            View view = doc.ActiveView;
            Arc arc = Arc.Create(xyz, radius, 0, Math.PI * 2, view.RightDirection, view.UpDirection);
            TransactionInvoker.Action(doc, "DrawCircleGivenPoint", () =>
            {
                darc = doc.Create.NewDetailCurve(view, arc) as DetailArc;
            });
            return darc;
        }
    }
}

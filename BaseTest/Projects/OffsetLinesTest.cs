using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseTest.Projects
{
    class OffsetLinesTest
    {
        private double rowHeight = 20/308.4;
        private double width = 100 / 308.4;
        public List<Line> CreateOffsetLines(int rowNum)
        {
            List<Line> lines = new List<Line>();
            Line baseLine = Line.CreateBound(new XYZ(), new XYZ(width, 0, 0));
            lines.Add(baseLine);
            for (int i = 0; i < rowNum; i++)
            {
                lines.Add(baseLine.CreateOffset((i + 1) * rowHeight, XYZ.BasisZ) as Line);
            }
            return lines;
        }

        public List<Line> CreateNewLines(int rowNum)
        {
            List<Line> lines = new List<Line>();
            Line baseLine = Line.CreateBound(new XYZ(), new XYZ(width, 0, 0));
            lines.Add(baseLine);
            for (int i = 0; i < rowNum; i++)
            {
                lines.Add(Line.CreateBound(new XYZ(0, (i + 1) * rowHeight, 0), new XYZ(width, (i + 1) * rowHeight, 0)));
            }
            return lines;
        }
    }
}

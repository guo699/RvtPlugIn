using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace BaseTest.Projects
{
    class DetailTableCreate
    {
        private List<int> _apartColumns;
        private List<int> _apartRows;

        private List<double> _columnWidths;
        private double _rowHeight = 20 / 304.8;

        public int Rows;

        public List<List<Line>> HLines;
        public List<List<Line>> VLines;

        public void SetApartColumns(params int[] cols)
        {
            if (_apartColumns == null)
            {
                _apartColumns = new List<int>(cols);
            }
            else
                return;
        }

        public void SetApartRows(params int[] rows)
        {
            if (_apartRows == null)
            {
                _apartRows = new List<int>(rows);
            }
            else
                return;
        }

        public void SetColumnWidths(params double[] widths)
        {
            if (_columnWidths == null)
            {
                _columnWidths = new List<double>(widths.Select(n=>n/304.8));
            }
            else
                return;
        }

        public void MergeCells(int row,int col,int rowCount,int colCount)
        {

        }

        public void CreateVLines(UV origin)
        {
            VLines = new List<List<Line>>();
            List<Line> baseLines = this.GetBaseVLines(origin);
            VLines.Add(baseLines);

            List<Line> offLines;
            double offX = 0;
            for (int i = 0; i < _columnWidths.Count; i++)
            {
                offX -= _columnWidths[i];
                offLines = baseLines.Select(n => n.CreateOffset(offX, XYZ.BasisZ) as Line).ToList();
                VLines.Add(offLines);
            }
        }

        public void CreateHLines(UV origin)
        {
            HLines = new List<List<Line>>();
            List<Line> baseLines = this.GetBaseHLines(origin);
            HLines.Add(baseLines);

            List<Line> offLines;
            for (int i = 0; i < Rows; i++)
            {
                offLines = baseLines.Select(n => n.CreateOffset((i + 1) * _rowHeight, XYZ.BasisZ) as Line).ToList();
                HLines.Add(offLines);
            }
        }

        private List<Line> GetBaseHLines(UV origin)
        {
            List<XYZ> points = new List<XYZ>();

            XYZ end = new XYZ(origin.U, origin.V, 0);

            points.Add(end);
            foreach (var len in _columnWidths)
            {
                end = end.Add(new XYZ(len, 0, 0));
                points.Add(end);
            }

            List<int> indeices = new List<int>() { 0, _columnWidths.Count};
            foreach (var idx in _apartColumns)
            {
                indeices.Add(idx);
                indeices.Add(idx + 1);
            }
            indeices = indeices.Distinct().OrderBy(n => n).ToList();

            List<Line> lines = new List<Line>();
            for (int i = 0; i < indeices.Count-1; i++)
            {
                lines.Add(Line.CreateBound(points[indeices[i]], points[indeices[i + 1]]));
            }

            return lines;
        }

        private List<Line> GetBaseVLines(UV origin)
        {
            List<XYZ> points = new List<XYZ>();

            XYZ end = new XYZ(origin.U, origin.V, 0);

            points.Add(end);
            for (int i = 0; i < Rows; i++)
            {
                end = end.Add(new XYZ(0, -_rowHeight, 0));
                points.Add(end);
            }

            List<int> indeices = new List<int>() { 0, Rows };
            foreach (var idx in _apartRows)
            {
                indeices.Add(idx);
                indeices.Add(idx + 1);
            }
            indeices = indeices.Distinct().OrderBy(n => n).ToList();

            List<Line> lines = new List<Line>();
            for (int i = 0; i < indeices.Count - 1; i++)
            {
                lines.Add(Line.CreateBound(points[indeices[i]], points[indeices[i + 1]]));
            }

            return lines;
        }

    }
}

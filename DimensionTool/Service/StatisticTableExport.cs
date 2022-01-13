using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Form = System.Windows.Forms;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System.IO;
using System.Windows.Forms;
using HorizontalAlignment = NPOI.SS.UserModel.HorizontalAlignment;

namespace DimensionTool.Service
{
    using RegionTable = List<(string, List<TableItemData>)>;
    class StatisticTableExport
    {
        public List<ExportTableData> TableDatas;

        private HSSFSheet _materialsheet;
        private HSSFSheet _serialsheet;

        private HSSFWorkbook _workBook;
        private ICellStyle _cellStyle;

        public StatisticTableExport()
        {
            _workBook = new HSSFWorkbook();
            _materialsheet = _workBook.CreateSheet("材料统计表") as HSSFSheet;
            _serialsheet = _workBook.CreateSheet("编号统计表") as HSSFSheet;
        }

        public void DoExport()
        {
            try
            {
                /*弹出保存对话框*/
                Form.SaveFileDialog saveDialog = new Form.SaveFileDialog();
                saveDialog.Filter = "导出Excel(*.xls)|*.xls";
                saveDialog.FileName = string.Format("{0}_{1}.xls", "管线材料统计表", "01");
                if (saveDialog.ShowDialog() == Form.DialogResult.OK)
                {
                    string fileName = saveDialog.FileName;

                    //填入数据
                    this.WriteTable();

                    using (FileStream file = new FileStream(@fileName, FileMode.Create))
                    {
                        _workBook.Write(file);
                        file.Close();
                    }
                    if (Form.DialogResult.OK == MessageBox.Show("导出成功，是否打开？"))
                    {
                        System.Diagnostics.Process.Start(saveDialog.FileName);
                    }

                    
                }
            }
            catch (Exception ex)
            {
                Form.MessageBox.Show(ex.Message);
                return;
            }
            finally
            {

            }
        }

        private void WriteTable()
        {
            CreateSerialTable();
            CreateMaterialTable();
        }

        private void CreateSerialTable()
        {
            this.InitTable(_serialsheet, new int[] { 1,1,2,2,1,1,1,1});
            int rowIndex = 0;
            //标题栏
            FillCellContent(_serialsheet, rowIndex, 0, 1, 8, "材料统计表-按编号统计");
            rowIndex += 1;
            //表头
            FillCellContent(_serialsheet, rowIndex, 0, "编号");
            FillCellContent(_serialsheet, rowIndex, 1, 1, 2, "类型名称");
            string[] headers = new string[] { "规格", "下料长度(m)", "数量(个)", "重量（kg）", "备注" };
            for (int i = 0; i < headers.Length; i++)
                FillCellContent(_serialsheet, rowIndex, i + 3, headers[i]);

            List<TableItemData> pipeRows;
            List<TableItemData> otherRows;
            Dictionary<string, List<TableItemData>> pipeGps;

            foreach (var tableData in TableDatas)
            {
                pipeRows = tableData.Items.FindAll(n => !string.IsNullOrEmpty(n.SubSerial));
                otherRows = tableData.Items.FindAll(n => string.IsNullOrEmpty(n.SubSerial));

                pipeGps = pipeRows.GroupBy(n => n.Name).ToDictionary(n => n.Key, p => p.ToList());

                //管道数据
                int count;
                TableItemData item;
                foreach (var gp in pipeGps)
                {
                    count = gp.Value.Count;
                    for (int i = 0; i < count; i++)
                    {
                        rowIndex += 1;
                        item = gp.Value[i];
                        FillCellContent(_serialsheet,rowIndex, 2, item.SubSerial);
                        FillCellContent(_serialsheet,rowIndex, 3, item.Specification);
                        FillCellContent(_serialsheet,rowIndex, 4, item.Length);
                        FillCellContent(_serialsheet,rowIndex, 5, item.Amount);
                        FillCellContent(_serialsheet,rowIndex, 6, item.Weight);
                        FillCellContent(_serialsheet,rowIndex, 7, item.Remarks);
                    }
                    FillCellContent(_serialsheet, rowIndex - count + 1, 1, count, 1, gp.Key);
                }

                //其他数据
                for (int i = 0; i < otherRows.Count; i++)
                {
                    rowIndex += 1;
                    item = otherRows[i];
                    FillCellContent(_serialsheet, rowIndex, 1, 1, 2, item.Name);
                    FillCellContent(_serialsheet, rowIndex, 3, 1, 1, item.Specification);
                    FillCellContent(_serialsheet, rowIndex, 4, 1, 1, item.Length);
                    FillCellContent(_serialsheet, rowIndex, 5, 1, 1, item.Amount);
                    FillCellContent(_serialsheet, rowIndex, 6, 1, 1, item.Weight);
                    FillCellContent(_serialsheet, rowIndex, 7, 1, 1, item.Remarks);
                }

                //编号
                int sumCount = tableData.Items.Count;
                FillCellContent(_serialsheet, rowIndex-sumCount+1, 0, sumCount, 1, tableData.Serial);
            }

        }

        private void CreateMaterialTable()
        {
            this.InitTable(_materialsheet, new int[] { 1, 1, 2, 2, 1, 1, 1, 1 });
            int rowIndex = 0;
            //标题栏
            FillCellContent(_materialsheet, rowIndex, 0, 1, 8, "材料统计表-按类型统计");
            rowIndex += 1;
            //表头
            FillCellContent(_materialsheet, rowIndex, 0, "分类");
            FillCellContent(_materialsheet, rowIndex, 1, 1, 2, "类型名称");
            string[] headers = new string[] { "规格", "下料长度(m)", "数量(个)", "重量（kg）", "备注" };
            for (int i = 0; i < headers.Length; i++)
                FillCellContent(_materialsheet, rowIndex, i + 3, headers[i]);

            (RegionTable pipeRegions, RegionTable fitRegions) = this.Summary();

            (string, List<TableItemData>) region;
            List<TableItemData> regionRows;

            int sumCount = 0;
            for (int i = 0; i < pipeRegions.Count; i++)
            {
                region = pipeRegions[i];
                regionRows = region.Item2;
                int count = regionRows.Count;
                sumCount += count;
                foreach (var item in regionRows)
                {
                    rowIndex += 1;
                    FillCellContent(_materialsheet,rowIndex, 2, item.SubSerial);
                    FillCellContent(_materialsheet,rowIndex, 3, item.Specification);
                    FillCellContent(_materialsheet,rowIndex, 4, item.Length);
                    FillCellContent(_materialsheet,rowIndex, 5, item.Amount);
                    FillCellContent(_materialsheet,rowIndex, 6, item.Weight);
                    FillCellContent(_materialsheet,rowIndex, 7, item.Remarks);
                }
                FillCellContent(_materialsheet, rowIndex - count + 1, 1, count, 1, region.Item1);
            }
            FillCellContent(_materialsheet, rowIndex - sumCount + 1, 0, sumCount, 1, "管道");

            for (int i = 0; i < fitRegions.Count; i++)
            {
                region = fitRegions[i];
                regionRows = region.Item2;
                int count = regionRows.Count;
                sumCount += count;
                foreach (var item in regionRows)
                {
                    rowIndex += 1;
                    FillCellContent(_materialsheet, rowIndex, 1, 1, 2, item.Name);
                    FillCellContent(_materialsheet, rowIndex, 3, item.Specification);
                    FillCellContent(_materialsheet, rowIndex, 4, item.Length);
                    FillCellContent(_materialsheet, rowIndex, 5, item.Amount);
                    FillCellContent(_materialsheet, rowIndex, 6, item.Weight);
                    FillCellContent(_materialsheet, rowIndex, 7, item.Remarks);
                }
                FillCellContent(_materialsheet, rowIndex-count+1, 0, count, 1, "管件");
            }
            
        }

        private (RegionTable,RegionTable) Summary()
        {
            List<TableItemData> pipeRows;
            List<TableItemData> otherRows;
            List<(string, List<TableItemData>)> pipeGps = new List<(string, List<TableItemData>)>(); //管道
            List<(string, List<TableItemData>)> fitGps = new List<(string, List<TableItemData>)>(); //管件等
            Dictionary<string, List<TableItemData>> gps;

            ExportTableData tableData;
            for (int i = 0; i < TableDatas.Count; i++)
            {
                tableData = TableDatas[i];
                pipeRows = tableData.Items.FindAll(n => !string.IsNullOrEmpty(n.SubSerial));
                otherRows = tableData.Items.FindAll(n => string.IsNullOrEmpty(n.SubSerial));

                //管道
                gps = pipeRows.GroupBy(n => n.Name).ToDictionary(n => n.Key, p => p.ToList());
                foreach (var item in gps)
                {
                    pipeGps.Add((item.Key, item.Value));
                }

                //附件
                gps = otherRows.GroupBy(n => n.Priority).ToDictionary(n => n.Key.ToString(), p => p.ToList());
                foreach (var item in gps)
                {
                    fitGps.Add((item.Key, item.Value));
                }
            }

            return (pipeGps, fitGps);
        }

        private void FillCellContent<T>(ISheet sheet,int rowIndex, int colIndex, int rowSpan, int colSpan, T text)
        {
            this.FillCellContent(sheet,rowIndex, colIndex, text);
            CellRangeAddress region = new CellRangeAddress(rowIndex, rowIndex + rowSpan - 1, colIndex, colIndex + colSpan - 1);
            sheet.AddMergedRegion(region);
        }

        private void FillCellContent<T>(ISheet sheet,int rowIndex, int colIndex, T text)
        {
            IRow row = sheet.GetRow(rowIndex);
            ICell cell = row.GetCell(colIndex);
            cell.SetCellValue(text.ToString());
            cell.CellStyle = _cellStyle;
        }

        private void InitTable(ISheet sheet,int[] widths)
        {
            int rowsCount = TableDatas.Select(n => n.Items.Count).Sum() + 20;
            IRow row;
            for (int i = 0; i < rowsCount + 10; i++)
            {
                row = sheet.CreateRow(i);
                for (int j = 0; j < widths.Length; j++)
                {
                    row.CreateCell(j);
                }
            }

            for (int i = 0; i < widths.Length; i++)
            {
                sheet.SetColumnWidth(i, widths[i] * 3000);
            }

            _cellStyle = _workBook.CreateCellStyle();
            _cellStyle.Alignment = HorizontalAlignment.Center;
            _cellStyle.VerticalAlignment = VerticalAlignment.Center;
        }
    }
}

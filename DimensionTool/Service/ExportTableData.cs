using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DimensionTool.Service
{
    internal class ExportTableData
    {
        /// <summary>
        /// 编号
        /// 管线编号或泵组编号
        /// </summary>
        internal string Serial { get; private set; }

        internal List<TableItemData> Items { get; private set; }

        /// <summary>
        /// 同一编号的一组数据
        /// </summary>
        internal ExportTableData(string serial, List<TableItemData> items)
        {
            Serial = serial;
            Items = items;
        }

        /// <summary>
        /// 统计表表头
        /// </summary>
        /// <returns></returns>
        internal static List<string> GetHeaders()
        {
            return new List<string> { "编号", "类型名称", "规格", "下料长度(m)", "数量(个)", "重量(kg)", "备注" };
        }
    }

    /// <summary>
    /// 统计表单行数据
    /// </summary>
    internal class TableItemData
    {
        private string _specification;
        private string _length;
        private int _amount;

        /// <summary>
        /// 同一个编号下的子编号
        /// </summary>
        public string SubSerial { get; set; }

        /// <summary>
        /// 类型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 规格
        /// </summary>
        public string Specification
        {
            get { return _specification ?? "/"; }
            set { _specification = value; }
        }

        /// <summary>
        /// 下料长度
        /// </summary>
        public string Length
        {
            get { return _length ?? "/"; }
            set { _length = value; }
        }

        /// <summary>
        /// 数量
        /// </summary>
        public int Amount
        {
            get { return _amount == 0 ? 1 : _amount; }
            set { _amount = value; }
        }

        /// <summary>
        /// 构件重量
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remarks { get; set; }

        /// <summary>
        /// 内部备注，处理数据用，不显示在统计表中
        /// </summary>
        public string InnerRemarks { get; set; }

        /// <summary>
        /// 统计表中的优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 对应的元素
        /// </summary>
        public Element Ele { get; set; }

        /// <summary>
        /// 无效数据，不应统计
        /// </summary>
        public bool InValid { get; set; }

        /// <summary>
        /// 统计表单行数据
        /// </summary>
        public TableItemData(Element ele)
        {
            Ele = ele;
        }

        public TableItemData(string name,string number,string spec,string len,int amount,double weight)
        {
            Name = name;
            SubSerial = number;
            Specification = spec;
            Length = len;
            Amount = amount;
            Weight = weight;
            Remarks = "NAN";
            Priority = string.IsNullOrEmpty(number) ? 2 : 1;
        }

        /// <summary>
        /// 用来比较合并
        /// </summary>
        /// <returns></returns>
        public string GetString()
        {
            return Name + (Specification ?? "") + (Length ?? "") + Amount.ToString() + (Remarks ?? "");
        }

        public static double GetWeight(Pipe pipe)
        {
            return 999.9;
        }
    }
}

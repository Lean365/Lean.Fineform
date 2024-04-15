using System;
using System.Data;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace LeanFine
{
    public class GridHelper
    {
        /// <summary>
        /// 模拟返回总项数
        /// </summary>
        /// <returns></returns>
        public static int GetTotalCount(IQueryable q)
        {
            return ConvertHelper.LinqConvertToDataTable(q).Rows.Count;
        }

        /// <summary>
        /// 模拟数据库分页
        /// </summary>
        /// <returns></returns>
        public static DataTable GetPagedDataTable(FineUIPro.Grid grid, IQueryable q)
        {
            int pageIndex = grid.PageIndex;
            int pageSize = grid.PageSize;

            string sortField = grid.SortField;
            string sortDirection = grid.SortDirection;

            DataTable table2 = ConvertHelper.LinqConvertToDataTable(q);

            DataView view2 = table2.DefaultView;
            view2.Sort = String.Format("{0} {1}", sortField, sortDirection);

            DataTable table = view2.ToTable();

            DataTable paged = table.Clone();

            int rowbegin = pageIndex * pageSize;
            int rowend = (pageIndex + 1) * pageSize;
            if (rowend > table.Rows.Count)
            {
                rowend = table.Rows.Count;
            }

            for (int i = rowbegin; i < rowend; i++)
            {
                paged.ImportRow(table.Rows[i]);
            }

            return paged;
        }

        /// <summary>
        /// DataTable分页并取出指定页码的数据
        /// </summary>
        /// <param name="dtAll">DataTable</param>
        /// <param name="pageNo">页码,注意：从1开始</param>
        /// <param name="pageSize">每页条数</param>
        /// <returns>指定页码的DataTable数据</returns>
        public static DataTable getOnePageTable(DataTable dtAll, int pageNo, int pageSize)
        {
            var totalCount = dtAll.Rows.Count;
            var totalPage = getTotalPage(totalCount, pageSize);
            var currentPage = pageNo;
            currentPage = (currentPage > totalPage ? totalPage : currentPage);//如果PageNo过大，则较正PageNo=PageCount
            currentPage = (currentPage <= 0 ? 1 : currentPage);//如果PageNo<=0，则改为首页
            //----克隆表结构到新表
            var onePageTable = dtAll.Clone();
            //----取出1页数据到新表
            var rowBegin = (currentPage - 1) * pageSize;
            var rowEnd = currentPage * pageSize;
            rowEnd = (rowEnd > totalCount ? totalCount : rowEnd);
            for (var i = rowBegin; i <= rowEnd - 1; i++)
            {
                var newRow = onePageTable.NewRow();
                var oldRow = dtAll.Rows[i];
                foreach (DataColumn column in dtAll.Columns)
                {
                    newRow[column.ColumnName] = oldRow[column.ColumnName];
                }
                onePageTable.Rows.Add(newRow);
            }
            return onePageTable;
        }

        /// <summary>
        /// 返回分页后的总页数
        /// </summary>
        /// <param name="totalCount">总记录条数</param>
        /// <param name="pageSize">每页显示条数</param>
        /// <returns>总页数</returns>
        public static int getTotalPage(int totalCount, int pageSize)
        {
            var totalPage = (totalCount / pageSize) + (totalCount % pageSize > 0 ? 1 : 0);
            return totalPage;
        }

        public static DataTable GetPagedDataTabled(FineUIPro.Grid grid, DataTable dt)
        {
            int pageIndex = grid.PageIndex;
            int pageSize = grid.PageSize;

            string sortField = grid.SortField;
            string sortDirection = grid.SortDirection;

            DataTable table2 = dt;

            DataView view2 = table2.DefaultView;
            view2.Sort = String.Format("{0} {1}", sortField, sortDirection);

            DataTable table = view2.ToTable();

            DataTable paged = table.Clone();

            int rowbegin = pageIndex * pageSize;
            int rowend = (pageIndex + 1) * pageSize;
            if (rowend > table.Rows.Count)
            {
                rowend = table.Rows.Count;
            }

            for (int i = rowbegin; i < rowend; i++)
            {
                paged.ImportRow(table.Rows[i]);
            }

            return paged;
        }

        #region 取DataTable列名至表格

        public static void AddDefColumInGrid(DataColumnCollection cols, FineUIPro.Grid grid)
        {
            foreach (DataColumn col in cols)
            {
                FineUIPro.BoundField bf = new FineUIPro.BoundField();
                grid.Columns.Add(bf);
                bf.HeaderText = col.ColumnName;
                bf.Width = 100;
                //bf.DataToolTipField = col.ColumnName;
                //bf.DataToolTipFormatString = col.ColumnName + ":{0}";
                bf.DataField = col.ColumnName;
                bf.SortField = col.ColumnName;
                bf.TextAlign = FineUIPro.TextAlign.Center;
            }
        }

        #endregion 取DataTable列名至表格

        //合计表格
        /// <summary>
        /// 表格合计
        /// </summary>
        /// <param name="source 数据源"></param>
        /// <param name="grid 表格 "></param>
        /// <param name="sumTotala 合计字段"></param>
        /// <param name="sumTotalb 合计字段"></param>
        /// <param name="sumTotalc sumTotalb/sumTotala"></param>
        public static void GridSummaryData(DataTable source, FineUIPro.Grid grid, string sumTotala, string sumTotalb, string sumTotalc)
        {
            Decimal pTotal = 0.0m;
            Decimal rTotal = 0.0m;
            Decimal ratio = 0.0m;

            foreach (DataRow row in source.Rows)
            {
                pTotal += Convert.ToDecimal(row["sumTotala"]);
                rTotal += Convert.ToDecimal(row["sumTotalb"]);
                ratio = rTotal / pTotal;
            }

            JObject summary = new JObject();
            //summary.Add("major", "全部合计");

            summary.Add("sumTotala", pTotal.ToString("F2"));
            summary.Add("sumTotalb", rTotal.ToString("F2"));
            summary.Add("sumTotalc", ratio.ToString("p0"));

            grid.SummaryData = summary;
        }
    }
}
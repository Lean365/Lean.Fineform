using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Xml;
using System.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;


namespace Lean.Fineform.Lf_Report
{
    public partial class p2d_data_line : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限,空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DOutputView";
            }
        }

        #endregion
        #region Page_Init

        // 注意：动态创建的代码需要放置于Page_Init（不是Page_Load），这样每次构造页面时都会执行
        protected void Page_Init(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void InitGrid()
        {
            //SQL语句
            //string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");
            var q = (from a in DB.Pp_P2d_OutputSubs
                     where a.Prorealqty != 0
                     //where a.Prodate.CompareTo(edate) == 0
                     group a by new
                     {
                         Date = a.Prodate,
                         Lot = a.Prolot,
                         Line = a.Prolinename,
                     }
                     into g
                     select new

                     {
                         g.Key.Date,
                         g.Key.Lot,
                         g.Key.Line,
                         Qty = g.Sum(p => p.Prorealqty),
                     }).ToList();

            List<string> DimensionList = new List<string>() { "Date", "Lot" };

            string DynamicColumn = "Line";
            List<string> AllDynamicColumn = null;

            DataTable result = ConvertHelper.DataTableRowToCol(ConvertHelper.IEnumerableConvertToDataTable(q), DimensionList, DynamicColumn, out AllDynamicColumn);


            GridHelper.AddDefColumInGrid(result.Columns,Grid1);


        }

        #endregion
        #region Page_Load
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                LoadData();
            }
        }
        private void LoadData()
        {
            DPend.SelectedDate = DateTime.Now;//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();
            BindGrid();
        }
        private void BindGrid()
        {
            string edate = DPend.SelectedDate.Value.ToString("yyyyMMdd");
            //SQL语句
            var q = (from a in DB.Pp_P2d_OutputSubs
                    where a.Prorealqty != 0
                    where a.Prodate.CompareTo(edate) == 0

                    group a by new
                    {
                        Date = a.Prodate,
                        Lot = a.Prolot,
                        Line = a.Prolinename,
                    }
                     into g
                    select new

                    {
                        Date = g.Key.Date,
                        Lot = g.Key.Lot,
                        Line = g.Key.Line,
                        Qty = g.Sum(p => p.Prorealqty),
                    }).ToList();
            if (q.Count()!=0)
            {
                List<string> DimensionList = new List<string>() { "Date", "Lot" };

                string DynamicColumn = "Line";
                List<string> AllDynamicColumn = null;

                DataTable result = ConvertHelper.DataTableRowToCol(ConvertHelper.IEnumerableConvertToDataTable(q), DimensionList, DynamicColumn, out AllDynamicColumn);

                Grid1.RecordCount = result.Rows.Count;
                if (Grid1.RecordCount != 0)
                {
                    var qs = from a in result.AsEnumerable().AsQueryable()
                             select a;



                    DataTable table = GridHelper.GetPagedDataTabled(Grid1, result);
                    Grid1.DataSource = (table);
                    Grid1.DataBind();

                }
            }

        }
        #endregion
        #region Event
        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid();
        }

        protected void Grid1_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid1.PageIndex = e.NewPageIndex;
            BindGrid();
        }
        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }
        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
            {

                BindGrid();
            }
        }

        #endregion
    }
}
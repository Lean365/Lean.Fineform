using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;

namespace LeanFine.Lf_Manufacturing.PP.daily.P2D
{
    public partial class p2d_data_time : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DOutputView";
            }
        }

        #endregion ViewPower

        #region Page_Init

        // 注意：动态创建的代码需要放置于Page_Init（不是Page_Load），这样每次构造页面时都会执行
        protected void Page_Init(object sender, EventArgs e)
        {
            InitGrid();
        }

        private void InitGrid()
        {
            //SQL语句
            var q = (from a in DB.Pp_P2d_OutputSubs
                     where a.Prorealqty != 0
                     orderby a.Prostime + "~" + a.Proetime
                     select new
                     {
                         Date = a.Prodate,
                         Time = a.Prostime + "~" + a.Proetime,
                         Lot = a.Prolot,
                         Line = a.Prolinename,
                         Qty = a.Prorealqty,
                     }).ToList();

            if (q.Any())
            {
                List<string> DimensionList = new List<string>() { "Date", "Lot", "Line" };

                string DynamicColumn = "Time";
                List<string> AllDynamicColumn = null;

                DataTable result = ConvertHelper.DataTableRowToCol(ConvertHelper.IEnumerableConvertToDataTable(q), DimensionList, DynamicColumn, out AllDynamicColumn);

                GridHelper.AddDefColumInGrid(result.Columns, Grid1);
            }
        }

        #endregion Page_Init

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
            //BindDDLData();
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreOphDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreOphNew", btnP1dNew);
            //CheckPowerWithButton("CoreProophp1dNew", btnPrint);
            //CheckPowerWithButton("CoreProophp1dEdit", btnP1dEdit);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1dNew.OnClientClick = Window1.GetShowReference("~/oneProduction/oneDaliy/oph_new.aspx", "新增") + Window1.GetMaximizeReference();
            //PageContext.RegisterStartupScript(Window1.GetMaximizeReference());
            //Window1.GetMaximizeReference();
            //btnPrint.OnClientClick = Window1.GetShowReference("~~/oneProduction/oneTimesheet/oph_report.aspx", "打印报表");
            //btnP1dEdit.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_edit.aspx?id={0}", "修改");

            DPend.SelectedDate = DateTime.Now;//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            // 每页记录数
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
                     orderby a.Prostime + "~" + a.Proetime
                     select new
                     {
                         Date = a.Prodate,
                         Time = a.Prostime + "~" + a.Proetime,
                         Lot = a.Prolot,
                         Line = a.Prolinename,
                         Qty = a.Prorealqty,
                     }).ToList();

            if (q.Any())
            {
                List<string> DimensionList = new List<string>() { "Date", "Lot", "Line" };

                string DynamicColumn = "Time";
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

        #endregion Page_Load

        #region Events

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

        #endregion Events
    }
}
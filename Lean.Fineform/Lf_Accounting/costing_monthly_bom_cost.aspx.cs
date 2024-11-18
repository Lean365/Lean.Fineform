using System;
using System.Data;
using System.Linq;
using FineUIPro;

namespace LeanFine.Lf_Accounting
{
    public partial class costing_monthly_bom_cost : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreFICOChart";
            }
        }

        #endregion ViewPower

        #region Load

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            // 每页记录数
            Grid1.PageSize = 50;
            //ddlGridPageSize.SelectedValue = "5000";
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreProdataDelete", btnDeleteSelected);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);
            DpEndDate.SelectedDate = DateTime.Now.AddMonths(-1);//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            BindGrid();
        }

        private void BindGrid()
        {
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

            var q_all = from a in DB.Fico_Monthly_Inventorys
                        where a.Bc_YM.CompareTo(edate) <= 0
                        select a;

            var q_Moving = from a in q_all
                           group a by a.Bc_Item into grp
                           let maxFM = grp.Max(a => a.Bc_YM)
                           from b in grp
                           where b.Bc_YM == maxFM
                           select new
                           {
                               b.Bc_Item,
                               b.Bc_MovingAverage
                           };

            var q_Bom = from a in DB.Fico_Monthly_Bom_Costs
                        where a.Bc_Balancedate.Substring(0, 6).CompareTo(edate) == 0
                        select a;

            var q = from a in q_Bom
                    join b in q_Moving on a.Bc_Item equals b.Bc_Item
                    select new
                    {
                        a.Bc_Item,
                        a.Bc_ItemText,
                        a.Bc_MovingCost,
                        Bc_MovingAverage = b.Bc_MovingAverage / 1000,
                        Bc_Diff = b.Bc_MovingAverage / 1000 - a.Bc_MovingCost,
                    };

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = GridHelper.GetTotalCount(q);
            if (Grid1.RecordCount != 0)
            {
                // 排列和数据库分页
                //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                //Grid1.RecordCount = GetTotalCount();

                // 2.获取当前分页数据
                DataTable table = GridHelper.GetPagedDataTable(Grid1, q);

                Grid1.DataSource = table;
                Grid1.DataBind();
            }
            else
            {
                Grid1.DataSource = "";
                Grid1.DataBind();
            }
        }

        #endregion Load

        #region Event

        //protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

        //    BindGrid();
        //}
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

        protected void DpEndDate_TextChanged(object sender, EventArgs e)
        {
            if (DpEndDate.SelectedDate.HasValue)
            {
                BindGrid();
                //getdate();
                //BindGrid();
                //PageContext.RegisterStartupScript("<script>updateChartInTabStrip();</script>");

                //如果没有就如下代码
                // PageContext.RegisterStartupScript("<script language='javascript'>updateChartInTabStrip();</script>");
            }
        }

        private string getdate()
        {
            string strDate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");
            return strDate;
        }

        #endregion Event

        #region Export

        protected void BtnExport_Click(object sender, EventArgs e)
        {            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Prefix_XlsxName, Export_FileName;

            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Prefix_XlsxName = DpEndDate.SelectedDate.Value.ToString("yyyyMM") + "_物料需求表";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
            Export_FileName = Prefix_XlsxName + ".xlsx";

            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

            var q_all = from a in DB.Fico_Monthly_Inventorys
                        where a.Bc_YM.CompareTo(edate) <= 0
                        select a;

            var q_Moving = from a in q_all
                           group a by a.Bc_Item into grp
                           let maxFM = grp.Max(a => a.Bc_YM)
                           from b in grp
                           where b.Bc_YM == maxFM
                           select new
                           {
                               b.Bc_Item,
                               b.Bc_MovingAverage
                           };

            var q_Bom = from a in DB.Fico_Monthly_Bom_Costs
                        where a.Bc_Balancedate.Substring(0, 6).CompareTo(edate) == 0
                        select a;

            var q = from a in q_Bom
                    join b in q_Moving on a.Bc_Item equals b.Bc_Item
                    select new
                    {
                        物料 = a.Bc_Item,
                        物料描述 = a.Bc_ItemText,
                        材料费 = a.Bc_MovingCost,
                        移动平均 = b.Bc_MovingAverage / 1000,
                        生产成本 = b.Bc_MovingAverage / 1000 - a.Bc_MovingCost,
                    };

            if (q.Any())
            {
                ConvertHelper.LinqConvertToDataTable(q);

                Grid1.AllowPaging = false;
                ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(q), Prefix_XlsxName, Export_FileName);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        #endregion Export
    }
}
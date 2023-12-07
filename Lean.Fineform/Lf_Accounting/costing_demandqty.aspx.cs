using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;

namespace Lean.Fineform.Lf_Accounting
{
    public partial class costing_demandqty : PageBase
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

        #endregion
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
            DPend.SelectedDate = DateTime.Now.AddMonths(-1);//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            BindGrid();

        }

        private void BindGrid()
        {
            IQueryable<Sd_MrpData> q = DB.Sd_MrpDatas; //.Include(u => u.Dept);


            //string sdate = this.DPstart.SelectedDate.Value.ToString("yyyyMM");

            //q.Where(u => u.Prodate.Contains(sdate));


            // 在用户名称中搜索
            string edate = DPend.SelectedDate.Value.ToString("yyyyMM");



            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.Bc_YM.CompareTo(edate) == 0);
            }

            //q = q.Where(u => u.Bc_MaterialType.CompareTo("FERT") == 0);


            // q = q.Where(u => u.Promodel != "0");
            //if (GetIdentityName() != "admin")
            //{)
            //    q = q.Where(u => u.Name != "admin");
            //}

            // 过滤启用状态
            //if (rblEnableStatus.SelectedValue != "all")
            //{
            //    q = q.Where(u => u.Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
            //}

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和数据库分页
            q = SortAndPage<Sd_MrpData>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
            //ttbSearchMessage.Text = "";
        }
        #endregion
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
        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
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
            string strDate = DPend.SelectedDate.Value.ToString("yyyyMM");
            return strDate;
        }

        #endregion

        #region Export
        protected void BtnExport_Click(object sender, EventArgs e)
        {            // 在操作之前进行权限检查
            if (!CheckPower("CoreKitOutput"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName;

            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_P1d_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Xlsbomitem = "MRP_" + DPend.SelectedDate.Value.ToString("yyyyMM");
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";
            //ExportHelper.GetGridDataTable(Exgrid);
            if (Grid1.RecordCount != 0)
            {
                //DataTable source = GetDataTable.Getdt(mysql);
                //导出2007格式
                //ExportHelper.EpplustoXLSXfile(Exdt, Xlsbomitem, ExportFileName);
                Grid1.AllowPaging = false;
                ExportHelper.EpplustoXLSXfile(ExportHelper.GetGridDataTable(Grid1), Xlsbomitem, ExportFileName);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }

        }
        #endregion
    }

}
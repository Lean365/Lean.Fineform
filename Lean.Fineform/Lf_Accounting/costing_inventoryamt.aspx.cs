using System;
using System.Data;
using System.Linq;
using FineUIPro;

namespace LeanFine.Lf_Accounting
{
    public partial class costing_inventoryamt : PageBase
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

        #region Page_Load

        public static string mysql, myrexname, xlsname;
        public static DataTable table;
        //

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            //rbtnFirstAuto.Text=global::Resources.GlobalResource.Unenforced;
            //本月第一天
            DpEndDate.SelectedDate = DateTime.Now.AddMonths(-1);//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreEcnDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreProbadp1dNew", btnP1d);
            //CheckPowerWithButton("CoreProbadp2dNew", btnP2d);
            CheckPowerWithButton("CoreFineExport", BtnExport);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1d.OnClientClick = Window1.GetShowReference("~/PlutoProinfo/probad_p1d_new.aspx", "P1D新增不良记录");
            //btnP2d.OnClientClick = Window1.GetShowReference("~/PlutoProinfo/probad_p2d_new.aspx", "P2D新增不良记录");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }

        private void BindGrid()
        {
            if (rbtnFirstAuto.Checked)
            {
                //IQueryable<Fico_Costing_HistoryMoving> q = DB.Fico_Costing_HistoryMovings; //.Include(u => u.Dept);
                string FY = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

                var q = from a in DB.Fico_Monthly_Inventorys
                            //where a.Bc_Financialym.CompareTo(FY) == 0
                        where a.isDeleted == 0
                        select a;

                //string sdate = this.DpStartDate.SelectedDate.Value.ToString("yyyyMM");

                //q.Where(u => u.Prodate.Contains(sdate));

                // 在用户名称中搜索

                if (!string.IsNullOrEmpty(FY))
                {
                    q = q.Where(u => u.Bc_YM.CompareTo(FY) == 0);
                }

                var q_count = from a in q
                              group a by new { a.Bc_Assessment, a.Bc_YM }
                            into g
                              select new
                              {
                                  g.Key.Bc_YM,
                                  Bc_Assessment = (g.Key.Bc_Assessment.CompareTo("Z792") == 0 ? "成品" : (g.Key.Bc_Assessment.CompareTo("Z300") == 0 ? "原材料" : (g.Key.Bc_Assessment.CompareTo("Z790") == 0 ? "半成品" : g.Key.Bc_Assessment))),
                                  Bc_Totalinventory = g.Sum(a => a.Bc_Totalinventory),
                                  Bc_Totalamount = g.Sum(a => a.Bc_Totalamount),
                              };

                //q = q.Where(u => u.Bc_MaterialType.CompareTo("FERT") == 0);

                // q = q.Where(u => u.Promodel != "0");
                //if (GetIdentityName() != "admin")
                //{)
                //    q = q.Where(u => u.Name != "admin");
                //}

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = GridHelper.GetTotalCount(q_count);
                if (Grid1.RecordCount != 0)
                {
                    // 排列和数据库分页
                    //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                    // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                    //Grid1.RecordCount = GetTotalCount();

                    // 2.获取当前分页数据
                    DataTable table = GridHelper.GetPagedDataTable(Grid1, q_count);

                    Grid1.DataSource = table;
                    Grid1.DataBind();
                }
                else
                {
                    Grid1.DataSource = "";
                    Grid1.DataBind();
                }
            }
            if (rbtnSecondAuto.Checked)
            {
                //IQueryable<Fico_Costing_HistoryMoving> q = DB.Fico_Costing_HistoryMovings; //.Include(u => u.Dept);
                string FY = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

                var q = from a in DB.Fico_Monthly_Inventorys
                            //where a.Bc_Financialym.CompareTo(FY) == 0
                        where a.isDeleted == 0
                        select a;

                //string sdate = this.DpStartDate.SelectedDate.Value.ToString("yyyyMM");

                //q.Where(u => u.Prodate.Contains(sdate));

                // 在用户名称中搜索

                if (!string.IsNullOrEmpty(FY))
                {
                    q = q.Where(u => u.Bc_YM.CompareTo(FY) == 0);
                }

                var q_count = from a in q
                              group a by new { a.Bc_Assessment, a.Bc_YM, a.Bc_Item }
                            into g
                              select new
                              {
                                  g.Key.Bc_YM,
                                  g.Key.Bc_Item,
                                  Bc_Assessment = (g.Key.Bc_Assessment.CompareTo("Z792") == 0 ? "成品" : (g.Key.Bc_Assessment.CompareTo("Z300") == 0 ? "原材料" : (g.Key.Bc_Assessment.CompareTo("Z790") == 0 ? "半成品" : g.Key.Bc_Assessment))),
                                  Bc_Totalinventory = g.Sum(a => a.Bc_Totalinventory),
                                  Bc_Totalamount = g.Sum(a => a.Bc_Totalamount),
                              };

                //q = q.Where(u => u.Bc_MaterialType.CompareTo("FERT") == 0);

                // q = q.Where(u => u.Promodel != "0");
                //if (GetIdentityName() != "admin")
                //{)
                //    q = q.Where(u => u.Name != "admin");
                //}

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = GridHelper.GetTotalCount(q_count);
                if (Grid1.RecordCount != 0)
                {
                    // 排列和数据库分页
                    //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                    // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                    //Grid1.RecordCount = GetTotalCount();

                    // 2.获取当前分页数据
                    DataTable table = GridHelper.GetPagedDataTable(Grid1, q_count);

                    Grid1.DataSource = table;
                    Grid1.DataBind();
                }
                else
                {
                    Grid1.DataSource = "";
                    Grid1.DataBind();
                }
            }
        }

        protected void rbtnAuto_CheckedChanged(object sender, CheckedEventArgs e)
        {
            // 单选框按钮的CheckedChanged事件会触发两次，一次是取消选中的菜单项，另一次是选中的菜单项；
            // 不处理取消选中菜单项的事件，从而防止此函数重复执行两次
            if (!e.Checked)
            {
                return;
            }

            if (rbtnFirstAuto.Checked)
            {
                BindGrid();
            }
            else if (rbtnSecondAuto.Checked)
            {
                BindGrid();
            }
        }

        #endregion Page_Load

        #region Event

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

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

        protected void Grid1_PreRowDataBound(object sender, GridPreRowEventArgs e)
        {
        }

        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
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

        #endregion Event

        #region Export

        protected void BtnExport_Click(object sender, EventArgs e)
        {            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            ////DataTable Exp = new DataTable();
            ////在库明细查询SQL
            string Xlsbomitem, ExportFileName;

            //// mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Xlsbomitem = DpEndDate.SelectedDate.Value.ToString("yyyyMM") + "_库存表";
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
            Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
        }

        #endregion Export
    }
}
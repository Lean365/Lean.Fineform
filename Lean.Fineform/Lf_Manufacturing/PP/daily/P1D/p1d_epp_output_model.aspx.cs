using FineUIPro;
using Newtonsoft.Json.Linq;
using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;

namespace LeanFine.Lf_Manufacturing.PP.daily
{
    public partial class p1d_epp_output_model : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP1DOutputView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static DataTable Exdt;
        public static FineUIPro.Grid Exgrid;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreOphDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreOphNew", btnP1dNew);
            //CheckPowerWithButton("CoreOphNew", btnP1dNew);
            CheckPowerWithButton("CoreFineExport", BtnExport);
            //CheckPowerWithButton("CoreFineExport", BtnRepair);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/oph_p1d_new.aspx", "P1D新增不良记录");
            //btnP2d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/oph_p2d_new.aspx", "P2D新增不良记录");

            //本月第一天
            DpStartDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DpEndDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            // 每页记录数
            Grid1.PageSize = 1000;
            ddlGridPageSize.SelectedValue = "1000";

            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                var q1 = from p in DB.Pp_P1d_Epp_Date_OutputSubs
                             //join b in DB.Pp_P1d_Outputs on p.Parent.ID equals b.ID
                         where p.IsDeleted == 0
                         where p.Prorealtime != 0 || p.Prolinestopmin != 0
                         select new
                         {
                             Prodate = p.ProStartdate,
                             p.Prolinename,
                             p.Prodirect,
                             p.Proindirect,
                             p.Prolot,
                             p.Prohbn,
                             p.Prost,
                             p.Promodel,
                             p.Prorealtime,
                             p.Prostdcapacity,
                             p.Prorealqty,
                         };
                var q2 = from p in DB.Pp_P1d_Epp_OutputSubs
                             //join b in DB.Pp_P1d_Outputs on p.Parent.ID equals b.ID
                         where p.IsDeleted == 0
                         where p.Prorealtime != 0 || p.Prolinestopmin != 0
                         select new
                         {
                             p.Prodate,
                             p.Prolinename,
                             p.Prodirect,
                             p.Proindirect,
                             p.Prolot,
                             p.Prohbn,
                             p.Prost,
                             p.Promodel,
                             p.Prorealtime,
                             p.Prostdcapacity,
                             p.Prorealqty,
                         };
                var q_all = q1.Union(q2);
                var q =
                    from p in q_all
                    group p by new { Prodate = p.Prodate.Substring(0, 6), p.Promodel } into g
                    select new
                    {
                        Prodate = g.Key.Prodate,
                        Promodel = g.Key.Promodel,
                        Prodirect = g.Average(p => p.Prodirect),

                        Prost = g.Average(p => p.Prost),
                        Proworktime = g.Sum(p => p.Prorealtime),
                        Proplanqty = g.Sum(p => p.Prostdcapacity),
                        Proworkqty = g.Sum(p => p.Prorealqty),
                        Proworkst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / (g.Sum(p => p.Prorealqty) * 1.00m) * 0.85m : 0),
                        Prodiffst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / (g.Sum(p => p.Prorealqty) * 1.00m) - g.Average(p => p.Prost) : g.Average(p => p.Prost)),
                        Prodiffqty = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) - g.Sum(p => p.Prostdcapacity) : 0),
                        Proactivratio = (g.Sum(p => p.Prostdcapacity) != 0 ? (g.Sum(p => p.Prorealqty) * 1.00m) / g.Sum(p => p.Prostdcapacity) : 0),
                    };
                var qs = q.Select(E => new
                {
                    E.Prodate,
                    E.Promodel,
                    E.Prodirect,

                    E.Prost,
                    E.Proworktime,
                    E.Proplanqty,
                    E.Proworkqty,
                    E.Proworkst,
                    E.Prodiffst,
                    E.Prodiffqty,
                    E.Proactivratio
                }).ToList().Distinct();

                //qs.Count();

                //IQueryable<pp_defect> q = DB.pp_defects; //.Include(u => u.Dept);

                // 在用户名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Promodel.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                }

                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.Prodate.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
                }

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid1.RecordCount = GridHelper.GetTotalCount(q);
                if (Grid1.RecordCount != 0)
                {
                    // 排列和数据库分页
                    //q = SortAndPage<Pp_P1d_Epp_OutputSub>(q, Grid1);

                    // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                    //Grid1.RecordCount = GetTotalCount();

                    // 2.获取当前分页数据
                    DataTable table = GridHelper.GetPagedDataTable(Grid1, q);

                    Grid1.DataSource = q;
                    Grid1.DataBind();

                    ConvertHelper.LinqConvertToDataTable(q);
                    // 当前页的合计
                    GridSummaryData(ConvertHelper.LinqConvertToDataTable(q));
                }
                else
                {
                    Grid1.DataSource = q;
                    Grid1.DataBind();
                }
                ConvertHelper.LinqConvertToDataTable(q);
                // 当前页的合计
                GridSummaryData(ConvertHelper.LinqConvertToDataTable(q));
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("异常1:" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("异常2:" + Message);
            }
            catch (Exception Message)
            {
                Alert.ShowInTop("异常3:" + Message);
            }
        }

        #endregion Page_Load

        #region Events

        protected void ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            //BindDdlData();
            //DdlLine.Items.Clear();
            ttbSearchMessage.ShowTrigger1 = true;
            BindGrid();
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            //BindDdlData();
            //DdlLine.Items.Clear();
            ttbSearchMessage.Text = String.Empty;
            ttbSearchMessage.ShowTrigger1 = false;
            BindGrid();
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("CoreOphDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");
        }

        protected void Grid1_PreRowDataBound(object sender, FineUIPro.GridPreRowEventArgs e)
        {
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

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            BindGrid();
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void rblEnableStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid1.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid();
        }

        #endregion Events

        protected void BtnExport_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Prefix_XlsxName, Export_FileName, SheetName;
            SheetName = "D" + DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Prefix_XlsxName = DpStartDate.SelectedDate.Value.ToString("yyyyMM") + "_Epp(Model)_Output_Report";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
            Export_FileName = Prefix_XlsxName + ".xlsx";

            try
            {
                //Decimal ra = 0.85m;//Math.Round((double )(g.Sum(c => c.capacity)),1, MidpointRounding.AwayFromZero )//  保留一位
                var q1 = from p in DB.Pp_P1d_Epp_Date_OutputSubs
                             //join b in DB.Pp_P1d_Outputs on p.Parent.ID equals b.ID
                         where p.IsDeleted == 0
                         where p.Prorealtime != 0 || p.Prolinestopmin != 0
                         select new
                         {
                             Prodate = p.ProStartdate,
                             p.Prolinename,
                             p.Prodirect,
                             p.Proindirect,
                             p.Prolot,
                             p.Prohbn,
                             p.Prost,
                             p.Promodel,
                             p.Prorealtime,
                             p.Prostdcapacity,
                             p.Prorealqty,
                         };
                var q2 = from p in DB.Pp_P1d_Epp_OutputSubs
                             //join b in DB.Pp_P1d_Outputs on p.Parent.ID equals b.ID
                         where p.IsDeleted == 0
                         where p.Prorealtime != 0 || p.Prolinestopmin != 0
                         select new
                         {
                             p.Prodate,
                             p.Prolinename,
                             p.Prodirect,
                             p.Proindirect,
                             p.Prolot,
                             p.Prohbn,
                             p.Prost,
                             p.Promodel,
                             p.Prorealtime,
                             p.Prostdcapacity,
                             p.Prorealqty,
                         };
                var q_normal = q1.Union(q2);

                var q =
                    from p in q_normal
                    group p by new { Prodate = p.Prodate.Substring(0, 6), p.Promodel } into g
                    select new
                    {
                        Prodate = g.Key.Prodate,
                        Promodel = g.Key.Promodel,
                        Prodirect = (double)g.Average(p => p.Prodirect),
                        Prost = g.Average(p => p.Prost),
                        Proworktime = g.Sum(p => p.Prorealtime),
                        Proplanqty = g.Sum(p => p.Prostdcapacity),
                        Proworkqty = g.Sum(p => p.Prorealqty),
                        Proworkst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / (g.Sum(p => p.Prorealqty) * 1.00m) * 0.85m : 0),
                        Prodiffst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / (g.Sum(p => p.Prorealqty) * 1.00m) - g.Average(p => p.Prost) : g.Average(p => p.Prost)),
                        Prodiffqty = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) - g.Sum(p => p.Prostdcapacity) : 0),
                        Proactivratio = (g.Sum(p => p.Prostdcapacity) != 0 ? (g.Sum(p => p.Prorealqty) * 1.00m) / g.Sum(p => p.Prostdcapacity) : 0),
                    };
                var qs = q.Select(E => new
                {
                    E.Prodate,
                    E.Promodel,
                    E.Prodirect,

                    E.Prost,
                    E.Proworktime,
                    E.Proplanqty,
                    E.Proworkqty,
                    E.Proworkst,
                    E.Prodiffst,
                    E.Prodiffqty,
                    E.Proactivratio
                }).ToList().Distinct();

                //qs.Count();

                //IQueryable<pp_defect> q = DB.pp_defects; //.Include(u => u.Dept);

                // 在用户名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Promodel.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                }
                else
                {
                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

                    if (!string.IsNullOrEmpty(sdate))
                    {
                        q = q.Where(u => u.Prodate.CompareTo(sdate) >= 0);
                    }
                    if (!string.IsNullOrEmpty(sdate))
                    {
                        q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
                    }
                }

                if (q.Any())
                {
                    var qss = from p in q
                                .OrderBy(s => s.Prodate)
                              select new
                              {
                                  生产日期 = p.Prodate,
                                  机种名称 = p.Promodel,
                                  ST = p.Prost,
                                  计划台数 = p.Proplanqty,
                                  生产工数 = p.Proworktime,
                                  实绩台数 = p.Proworkqty,
                                  直接人数 = (int)Math.Ceiling(p.Prodirect),

                                  实绩ST = (decimal)Math.Round(p.Proworkst, 2),
                                  ST差异 = (decimal)Math.Round(p.Prodiffst, 2),
                                  台数差异 = (decimal)Math.Round(p.Prodiffqty, 2),
                                  达成率 = (decimal)Math.Round(p.Proactivratio, 4),
                              };
                    ExportHelper.EppModel_XlsxFile(ConvertHelper.LinqConvertToDataTable(qss), "M" + DpStartDate.SelectedDate.Value.ToString("yyyyMM"), Export_FileName, DpStartDate.SelectedDate.Value.ToString("yyyyMM"));
                }
                else

                {
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
                }
            }
            catch (ArgumentNullException Message)
            {
                Alert.ShowInTop("异常1:" + Message);
            }
            catch (InvalidCastException Message)
            {
                Alert.ShowInTop("异常2:" + Message);
            }
            catch (Exception Message)
            {
                Alert.ShowInTop("异常3:" + Message);
            }
        }

        protected void DpStartDate_TextChanged(object sender, EventArgs e)
        {
            if (DpStartDate.SelectedDate.HasValue)
            {
                BindGrid();
            }
        }

        protected void DpEndDate_TextChanged(object sender, EventArgs e)
        {
            if (DpEndDate.SelectedDate.HasValue)
            {
                BindGrid();
            }
        }

        //合计表格
        private void GridSummaryData(DataTable source)
        {
            Decimal pTotal = 0.0m;
            Decimal rTotal = 0.0m;
            Decimal ratio = 0.0m;

            foreach (DataRow row in source.Rows)
            {
                pTotal += Convert.ToDecimal(row["Proplanqty"]);
                rTotal += Convert.ToDecimal(row["Proworkqty"]);
                ratio = rTotal / pTotal;
            }

            JObject summary = new JObject();
            //summary.Add("major", "全部合计");

            summary.Add("Proplanqty", pTotal.ToString("F2"));
            summary.Add("Proworkqty", rTotal.ToString("F2"));
            summary.Add("Proactivratio", ratio.ToString("p0"));

            Grid1.SummaryData = summary;
        }
    }
}
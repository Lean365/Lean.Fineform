using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lf_Manufacturing.PP.daily.P2D
{
    public partial class p2d_output_line : PageBase
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
            //BindDdlLine();
        }

        private void BindGrid()
        {
            {
                try
                {
                    var q_all = from p in DB.Pp_P2d_OutputSubs
                                    //join b in DB.Pp_P2d_Outputs on p.Parent.ID equals b.ID
                                where p.isDeleted == 0
                                where p.Prorealqty != 0 //|| p.Prolinestopmin != 0
                                select new
                                {
                                    p.Prodate,
                                    p.Prolot,
                                    p.Proorderqty,
                                    p.Promodel,
                                    p.Propcbatype,
                                    p.Propcbaside,
                                    p.Prost,
                                    p.Proshort,
                                    p.Prorate,
                                    p.Prolinename,
                                    p.Prorealqty,
                                    p.Prorealtotal,
                                    p.Protime,
                                    p.Prohandoffnum,
                                    p.Prohandofftime,
                                    p.Prodowntime,
                                    p.Prolosstime,
                                    p.Promaketime,
                                    p.Propcbastated,
                                    p.UDF51,
                                    p.UDF52,
                                    p.UDF53,
                                };

                    var q =
                        from p in q_all
                        group p by new { p.Propcbastated, Prodate = p.Prodate.Substring(0, 8), p.Prolinename, p.Promodel, p.Propcbatype, p.Propcbaside, p.Prolot, p.Proorderqty } into g
                        select new
                        {
                            g.Key.Prolinename,
                            g.Key.Prolot,
                            g.Key.Proorderqty,
                            g.Key.Prodate,
                            g.Key.Promodel,
                            g.Key.Propcbatype,
                            g.Key.Propcbaside,
                            g.Key.Propcbastated,
                            Prost = g.Average(p => p.Prost),
                            Proshort = g.Average(p => p.Proshort),
                            Prorate = g.Average(p => p.Prorate),
                            Prorealqty = g.Sum(p => p.Prorealqty),
                            Prorealtotal = g.Sum(p => p.Prorealtotal),
                            Protime = g.Sum(p => p.Protime),
                            Prohandoffnum = g.Sum(p => p.Prohandoffnum),
                            Prohandofftime = g.Sum(p => p.Prohandofftime),
                            Prodowntime = g.Sum(p => p.Prodowntime),
                            Prolosstime = g.Sum(p => p.Prolosstime),
                            Promaketime = g.Sum(p => p.Promaketime),
                            UDF51 = g.Sum(p => p.UDF51),
                            UDF52 = g.Sum(p => p.UDF52),
                            UDF53 = g.Sum(p => p.UDF53),
                            //Proworktime = g.Sum(p => p.Prorealtime),
                            //Proplanqty = g.Sum(p => p.Prostdcapacity),
                            //Proworkqty = g.Sum(p => p.Prorealqty),
                            //Proworkst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / (g.Sum(p => p.Prorealqty) * 1.00m) * 0.85m : 0),
                            //Prodiffst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / (g.Sum(p => p.Prorealqty) * 1.00m) - g.Average(p => p.Prost) : g.Average(p => p.Prost)),
                            //Prodiffqty = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) - g.Sum(p => p.Prostdcapacity) : 0),
                            //Proactivratio = (g.Sum(p => p.Prostdcapacity) != 0 ? (g.Sum(p => p.Prorealqty) * 1.00m) / g.Sum(p => p.Prostdcapacity) : 0),
                        };
                    var qs = q.Select(p => new
                    {
                        p.Prolinename,
                        p.Prodate,
                        p.Prolot,
                        p.Proorderqty,
                        p.Promodel,
                        p.Propcbatype,
                        p.Propcbaside,
                        p.Prost,
                        p.Proshort,
                        p.Prorate,
                        p.Prorealqty,
                        p.Prorealtotal,
                        p.Protime,
                        p.Prohandoffnum,
                        p.Prohandofftime,
                        p.Prodowntime,
                        p.Prolosstime,
                        p.Promaketime,
                        p.Propcbastated,
                        p.UDF51,
                        p.UDF52,
                        p.UDF53,
                    }).ToList().Distinct();

                    //qs.Count();

                    //IQueryable<Pp_Defect> q = DB.Pp_Defects; //.Include(u => u.Dept);

                    // 在用户名称中搜索
                    string searchText = ttbSearchMessage.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Prolot.Contains(searchText) || u.Promodel.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
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
                    if (DdlLine.SelectedIndex != 0 && DdlLine.SelectedIndex != -1)
                    {
                        q = q.Where(u => u.Prolinename.ToString().Contains(DdlLine.SelectedItem.Text));
                    }
                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(q);
                    if (Grid1.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P2d_Outputsub>(q, Grid1);

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
        }

        public void BindDdlLine()
        {
            //查询LINQ去重复
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
            var q = from a in DB.Pp_P2d_OutputSubs
                        //join b in DB.Ec_Subs on a.Porderhbn equals b.Ec_bomitem
                    where a.Prodate.CompareTo(sdate) >= 0
                    where a.Prodate.CompareTo(edate) <= 0
                    select new
                    {
                        a.Prolinename
                    };

            var qs = q.Select(E => new { E.Prolinename, }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DdlLine.DataSource = qs;
            DdlLine.DataTextField = "Prolinename";
            DdlLine.DataValueField = "Prolinename";
            DdlLine.DataBind();
            this.DdlLine.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
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
            string Xlsbomitem, ExportFileName;

            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_P2d_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Xlsbomitem = DpStartDate.SelectedDate.Value.ToString("yyyyMM") + "_P2d_Line_Output_Report";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            var q_all = from p in DB.Pp_P2d_OutputSubs
                            //join b in DB.Pp_P2d_Outputs on p.Parent.ID equals b.ID
                        where p.isDeleted == 0
                        where p.Prorealqty != 0 //|| p.Prolinestopmin != 0
                        select new
                        {
                            p.Prodate,
                            p.Prolot,
                            p.Proorderqty,
                            p.Promodel,
                            p.Propcbatype,
                            p.Propcbaside,
                            p.Prost,
                            p.Proshort,
                            p.Prorate,
                            p.Prolinename,
                            p.Prorealqty,
                            p.Prorealtotal,
                            p.Protime,
                            p.Prohandoffnum,
                            p.Prohandofftime,
                            p.Prodowntime,
                            p.Prolosstime,
                            p.Promaketime,
                            p.Propcbastated,
                            p.UDF51,
                            p.UDF52,
                            p.UDF53,
                        };

            var q =
                from p in q_all
                group p by new { p.Propcbastated, Prodate = p.Prodate.Substring(0, 8), p.Prolinename, p.Promodel, p.Propcbatype, p.Propcbaside, p.Prolot, p.Proorderqty } into g
                select new
                {
                    g.Key.Prolinename,
                    g.Key.Prolot,
                    g.Key.Proorderqty,
                    g.Key.Prodate,
                    g.Key.Promodel,
                    g.Key.Propcbatype,
                    g.Key.Propcbaside,
                    g.Key.Propcbastated,
                    Prost = g.Average(p => p.Prost),
                    Proshort = g.Average(p => p.Proshort),
                    Prorate = g.Average(p => p.Prorate),
                    Prorealqty = g.Sum(p => p.Prorealqty),
                    Prorealtotal = g.Sum(p => p.Prorealtotal),
                    Protime = g.Sum(p => p.Protime),
                    Prohandoffnum = g.Sum(p => p.Prohandoffnum),
                    Prohandofftime = g.Sum(p => p.Prohandofftime),
                    Prodowntime = g.Sum(p => p.Prodowntime),
                    Prolosstime = g.Sum(p => p.Prolosstime),
                    Promaketime = g.Sum(p => p.Promaketime),
                    UDF51 = g.Sum(p => p.UDF51),
                    UDF52 = g.Sum(p => p.UDF52),
                    UDF53 = g.Sum(p => p.UDF53),
                    //Proworktime = g.Sum(p => p.Prorealtime),
                    //Proplanqty = g.Sum(p => p.Prostdcapacity),
                    //Proworkqty = g.Sum(p => p.Prorealqty),
                    //Proworkst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / (g.Sum(p => p.Prorealqty) * 1.00m) * 0.85m : 0),
                    //Prodiffst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / (g.Sum(p => p.Prorealqty) * 1.00m) - g.Average(p => p.Prost) : g.Average(p => p.Prost)),
                    //Prodiffqty = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) - g.Sum(p => p.Prostdcapacity) : 0),
                    //Proactivratio = (g.Sum(p => p.Prostdcapacity) != 0 ? (g.Sum(p => p.Prorealqty) * 1.00m) / g.Sum(p => p.Prostdcapacity) : 0),
                };
            var qs = q.Select(p => new
            {
                p.Prolinename,
                p.Prodate,
                p.Prolot,
                p.Proorderqty,
                p.Promodel,
                p.Propcbatype,
                p.Propcbaside,
                p.Prost,
                p.Proshort,
                p.Prorate,
                p.Prorealqty,
                p.Prorealtotal,
                p.Protime,
                p.Prohandoffnum,
                p.Prohandofftime,
                p.Prodowntime,
                p.Prolosstime,
                p.Promaketime,
                p.Propcbastated,
                p.UDF51,
                p.UDF52,
                p.UDF53,
            }).ToList().Distinct();

            //qs.Count();

            //IQueryable<Pp_Defect> q = DB.Pp_Defects; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Prolot.Contains(searchText) || u.Promodel.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
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
            if (DdlLine.SelectedIndex != 0 && DdlLine.SelectedIndex != -1)
            {
                q = q.Where(u => u.Prolinename.ToString().Contains(DdlLine.SelectedItem.Text));
            }
            if (q.Any())
            {
                var qss = from p in q
                            .OrderBy(s => s.Prodate)
                          select new
                          {
                              班组 = p.Prolinename,
                              生产日期 = p.Prodate,
                              生产批次 = p.Prolot,
                              LOT数量 = p.Proorderqty,
                              机种 = p.Promodel,
                              Pcb类别 = p.Propcbatype,
                              板面 = p.Propcbaside,
                              //ST=p.Prost,
                              //SHort=p.Proshort,
                              //汇率=p.Prorate,
                              生产实绩 = p.Prorealqty,
                              累计生产 = p.Prorealtotal,
                              完成状态 = p.Propcbastated,
                              生产工数 = p.Protime,
                              切换次数 = p.Prohandoffnum,
                              切换时间 = p.Prohandofftime,
                              切停机时间 = p.Prodowntime,
                              损失工数 = p.Prolosstime,
                              投入工数 = p.Promaketime,
                              不良台数 = p.UDF51,
                              修正仕损 = p.UDF52,
                              手插仕损 = p.UDF53,
                          };
                ExportHelper.LineOutput_XlsxFile(ConvertHelper.LinqConvertToDataTable(qss), "L" + DpStartDate.SelectedDate.Value.ToString("yyyyMM"), ExportFileName, DpStartDate.SelectedDate.Value.ToString("yyyyMM"));

                //Grid1.AllowPaging = false;
                //ExportHelper.EpplustoXLSXfile(ExportHelper.GetGridDataTable(Grid1), Xlsbomitem, ExportFileName);
                //Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        protected void DdlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = "";

            BindGrid();
        }

        protected void DpStartDate_TextChanged(object sender, EventArgs e)
        {
            if (DpStartDate.SelectedDate.HasValue)
            {
                BindGrid();
                BindDdlLine();
            }
        }

        protected void DpEndDate_TextChanged(object sender, EventArgs e)
        {
            if (DpEndDate.SelectedDate.HasValue)
            {
                BindGrid();
                BindDdlLine();
            }
        }

        //合计表格
        private void GridSummaryData(DataTable source)
        {
            Decimal Prorealqtys = 0.0m;
            Decimal Prorealtotals = 0.0m;
            Decimal Protimes = 0.0m;//生产工数
            Decimal Prohandoffnums = 0.0m;//切换次数
            Decimal Prohandofftimes = 0.0m;//切换时间
            Decimal Prodowntimes = 0.0m;//切停机时间
            Decimal Prolosstimes = 0.0m;//损失工数
            Decimal Promaketimes = 0.0m;//投入工数
                                        //Decimal ratio = 0.0m;

            foreach (DataRow row in source.Rows)
            {
                Prorealqtys += Convert.ToDecimal(row["Prorealqty"]);
                Prorealtotals += Convert.ToDecimal(row["Prorealtotal"]);
                Protimes += Convert.ToDecimal(row["Protime"]);
                Prohandoffnums += Convert.ToDecimal(row["Prohandoffnum"]);
                Prohandofftimes += Convert.ToDecimal(row["Prohandofftime"]);
                Prodowntimes += Convert.ToDecimal(row["Prodowntime"]);
                Prolosstimes += Convert.ToDecimal(row["Prolosstime"]);
                Promaketimes += Convert.ToDecimal(row["Promaketime"]);

                //ratio = rTotal / pTotal;
            }

            JObject summary = new JObject();
            //summary.Add("major", "全部合计");

            summary.Add("Prorealqty", Prorealqtys.ToString("F2"));
            summary.Add("Prorealtotal", Prorealtotals.ToString("F2"));
            summary.Add("Protime", Protimes.ToString("F2"));
            summary.Add("Prohandoffnum", Prohandoffnums.ToString("F2"));
            summary.Add("Prohandofftime", Prohandofftimes.ToString("F2"));
            summary.Add("Prodowntime", Prodowntimes.ToString("F2"));
            summary.Add("Prolosstime", Prolosstimes.ToString("F2"));
            summary.Add("Promaketime", Promaketimes.ToString("F2"));

            //summary.Add("Proactivratio", ratio.ToString("p0"));

            Grid1.SummaryData = summary;
        }
    }
}
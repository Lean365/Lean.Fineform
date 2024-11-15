﻿using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lf_Manufacturing.PP.daily.P2D
{
    public partial class p2d_output_line_qty : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP21DOutputView";
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
            CheckPowerWithButton("CoreFineExport", BtnRepair);

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
            BindDdlLine();
        }

        private void BindGrid()
        {
            try
            {
                //行列转换
                string MonthSql = "declare @sql varchar(8000) " +
            "set @sql = 'select left(Prodate,6)Prodate' " +
            "select @sql = @sql + ',isnull (sum(case Prolinename when ''' + Prolinename + ''' then Prorealqty end),0) as [' + Prolinename + ']' " +
            "from(select distinct Prolinename from Pp_P2d_OutputSub) as a " +
            "select @sql = @sql + ' from Pp_P2d_OutputSub group by left(Prodate,6) order by left(Prodate,6)' " +
            "exec(@sql)";

                DataTable dtMonth = ConvertHelper.GetDataTable(MonthSql);

                var Month = from x in dtMonth.AsEnumerable()
                            select x;

                string LotSql = "declare @sql varchar(8000) " +
                            "set @sql = 'select left(Prodate,6)Prodate,Prolot' " +
                            "select @sql = @sql + ',isnull (sum(case Prolinename when ''' + Prolinename + ''' then Prorealqty end),0) as [' + Prolinename + ']' " +
                            "from(select distinct Prolinename from Pp_P2d_OutputSub) as a " +
                            "select @sql = @sql + ' from Pp_P2d_OutputSub group by left(Prodate,6),Prolot order by left(Prodate,6),Prolot' " +
                            "exec(@sql)";
                DataTable dtLot = ConvertHelper.GetDataTable(LotSql);
                var Lot = from x in dtLot.AsEnumerable()
                          select x;

                if (rbtnFirstAuto.Checked)
                {
                    var q_all = from p in DB.Pp_P2d_OutputSubs
                                    //join b in DB.Pp_P2d_Outputs on p.Parent.ID equals b.ID
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

                    var q =
                        from p in q_all
                        group p by new { p.Prodate, p.Prolinename, p.Prolot, p.Promodel, p.Prohbn, p.Prost, p.Prodirect, p.Proindirect } into g
                        select new
                        {
                            Prodate = g.Key.Prodate,
                            Prolinename = g.Key.Prolinename,
                            Prodirect = g.Key.Prodirect,
                            Proindirect = g.Key.Proindirect,
                            Prolot = g.Key.Prolot,
                            Prohbn = g.Key.Prohbn,
                            Prost = g.Key.Prost,
                            Promodel = g.Key.Promodel,
                            Proworktime = g.Sum(p => p.Prorealtime),
                            Proplanqty = g.Sum(p => p.Prostdcapacity),
                            Proworkqty = g.Sum(p => p.Prorealqty),
                            Proworkst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / g.Sum(p => p.Prorealqty) * 0.85m : 0),
                            Prodiffst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / g.Sum(p => p.Prorealqty) - g.Key.Prost : g.Key.Prost),
                            Prodiffqty = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) - g.Sum(p => p.Prostdcapacity) : 0),
                            Proactivratio = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) / g.Sum(p => p.Prostdcapacity) : 0),
                        };

                    //qs.Count();

                    //IQueryable<Pp_Defect> q = DB.Pp_Defects; //.Include(u => u.Dept);

                    // 在用户名称中搜索
                    string searchText = ttbSearchMessage.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Prolot.Contains(searchText) || u.Prohbn.Contains(searchText) || u.Promodel.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                    }
                    //else
                    //{
                    //    //当前日期
                    //    string dd = DateTime.Now.ToString("yyyyMMdd");
                    //    q = q.Where(u => u.Prodate.ToString().Contains(dd));
                    //}

                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

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

                        Grid1.DataSource = table;
                        Grid1.DataBind();
                    }
                    else
                    {
                        Grid1.DataSource = "";
                        Grid1.DataBind();
                    }

                    ConvertHelper.LinqConvertToDataTable(q);
                    // 当前页的合计
                    GridSummaryData(ConvertHelper.LinqConvertToDataTable(q));
                }
                if (rbtnSecondAuto.Checked)
                {
                    var q_normal = from p in DB.Pp_P2d_OutputSubs
                                       //join b in DB.Pp_P2d_Outputs on p.Parent.ID equals b.ID
                                   where p.IsDeleted == 0
                                   where p.Prorealtime != 0 || p.Prolinestopmin != 0
                                   where p.Proorder.Substring(0, 2).CompareTo("44") == 0
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

                    var q =
                        from p in q_normal
                        group p by new { p.Prodate, p.Prolinename, p.Prolot, p.Promodel, p.Prohbn, p.Prost, p.Prodirect, p.Proindirect } into g
                        select new
                        {
                            Prodate = g.Key.Prodate,
                            Prolinename = g.Key.Prolinename,
                            Prodirect = g.Key.Prodirect,
                            Proindirect = g.Key.Proindirect,
                            Prolot = g.Key.Prolot,
                            Prohbn = g.Key.Prohbn,
                            Prost = g.Key.Prost,
                            Promodel = g.Key.Promodel,
                            Proworktime = g.Sum(p => p.Prorealtime),
                            Proplanqty = g.Sum(p => p.Prostdcapacity),
                            Proworkqty = g.Sum(p => p.Prorealqty),
                            Proworkst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / g.Sum(p => p.Prorealqty) * 0.85m : 0),
                            Prodiffst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / g.Sum(p => p.Prorealqty) - g.Key.Prost : g.Key.Prost),
                            Prodiffqty = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) - g.Sum(p => p.Prostdcapacity) : 0),
                            Proactivratio = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) / g.Sum(p => p.Prostdcapacity) : 0),
                        };

                    //qs.Count();

                    //IQueryable<Pp_Defect> q = DB.Pp_Defects; //.Include(u => u.Dept);

                    // 在用户名称中搜索
                    string searchText = ttbSearchMessage.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Prolot.Contains(searchText) || u.Prohbn.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                    }
                    //else
                    //{
                    //    //当前日期
                    //    string dd = DateTime.Now.ToString("yyyyMMdd");
                    //    q = q.Where(u => u.Prodate.ToString().Contains(dd));
                    //}

                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

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

                        Grid1.DataSource = table;
                        Grid1.DataBind();
                    }
                    else
                    {
                        Grid1.DataSource = "";
                        Grid1.DataBind();
                    }

                    ConvertHelper.LinqConvertToDataTable(q);
                    // 当前页的合计
                    GridSummaryData(ConvertHelper.LinqConvertToDataTable(q));
                }
                if (rbtnThirdAuto.Checked)
                {
                    var q_rework = from p in DB.Pp_P2d_OutputSubs
                                       //join b in DB.Pp_P2d_Outputs on p.Parent.ID equals b.ID
                                   where p.IsDeleted == 0
                                   where p.Prorealtime != 0 || p.Prolinestopmin != 0
                                   where p.Proorder.Substring(0, 2).CompareTo("44") != 0
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

                    var q =
                        from p in q_rework
                        group p by new { p.Prodate, p.Prolinename, p.Prolot, p.Promodel, p.Prohbn, p.Prost, p.Prodirect, p.Proindirect } into g
                        select new
                        {
                            g.Key.Prodate,
                            g.Key.Prolinename,
                            g.Key.Prodirect,
                            g.Key.Proindirect,
                            g.Key.Prolot,
                            g.Key.Prohbn,
                            g.Key.Prost,
                            g.Key.Promodel,
                            Proworktime = g.Sum(p => p.Prorealtime),
                            Proplanqty = g.Sum(p => p.Prostdcapacity),
                            Proworkqty = g.Sum(p => p.Prorealqty),
                            Proworkst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / g.Sum(p => p.Prorealqty) * 0.85m : 0),
                            Prodiffst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / g.Sum(p => p.Prorealqty) - g.Key.Prost : g.Key.Prost),
                            Prodiffqty = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) - g.Sum(p => p.Prostdcapacity) : 0),
                            Proactivratio = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) / g.Sum(p => p.Prostdcapacity) : 0),
                        };

                    //qs.Count();

                    //IQueryable<Pp_Defect> q = DB.Pp_Defects; //.Include(u => u.Dept);

                    // 在用户名称中搜索
                    string searchText = ttbSearchMessage.Text.Trim();
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Prolot.Contains(searchText) || u.Prohbn.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                    }
                    //else
                    //{
                    //    //当前日期
                    //    string dd = DateTime.Now.ToString("yyyyMMdd");
                    //    q = q.Where(u => u.Prodate.ToString().Contains(dd));
                    //}

                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

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

                        Grid1.DataSource = table;
                        Grid1.DataBind();
                    }
                    else
                    {
                        Grid1.DataSource = "";
                        Grid1.DataBind();
                    }

                    ConvertHelper.LinqConvertToDataTable(q);
                    // 当前页的合计
                    GridSummaryData(ConvertHelper.LinqConvertToDataTable(q));
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

        public void BindDdlLine()
        {
            //查询LINQ去重复
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
            var q = from a in DB.Pp_P2d_Outputs
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

        protected void rbtnAuto_CheckedChanged(object sender, CheckedEventArgs e)
        {
            // 单选框按钮的CheckedChanged事件会触发两次，一次是取消选中的菜单项，另一次是选中的菜单项；
            // 不处理取消选中菜单项的事件，从而防止此函数重复执行两次
            if (!e.Checked)
            {
                return;
            }

            string checkedValue = String.Empty;
            if (rbtnFirstAuto.Checked)
            {
                BindGrid();
            }
            else if (rbtnSecondAuto.Checked)
            {
                BindGrid();
            }
            else if (rbtnThirdAuto.Checked)
            {
                BindGrid();
            }
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
            Xlsbomitem = DpStartDate.SelectedDate.Value.ToString("yyyyMM") + "_Line_Output_Report";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            var q_normal = from p in DB.Pp_P2d_OutputSubs
                               //join b in DB.Pp_P2d_Outputs on p.Parent.ID equals b.ID
                           where p.IsDeleted == 0
                           where p.Prorealtime != 0 || p.Prolinestopmin != 0
                           where p.Proorder.Substring(0, 2).Contains("44")
                           select new
                           {
                               Prodate = p.Prodate,
                               Prolinename = p.Prolinename,
                               Prodirect = p.Prodirect,
                               Proindirect = p.Proindirect,
                               Prolot = p.Prolot,
                               Prohbn = p.Prohbn,
                               Prost = p.Prost,
                               Promodel = p.Promodel,
                               Prorealtime = p.Prorealtime,
                               Prostdcapacity = p.Prostdcapacity,
                               Prorealqty = p.Prorealqty,
                           };

            var q =
                from p in q_normal
                group p by new { p.Prodate, p.Prolinename, p.Prolot, p.Promodel, p.Prohbn, p.Prost, p.Prodirect, p.Proindirect } into g
                select new
                {
                    Prodate = g.Key.Prodate,
                    Prolinename = g.Key.Prolinename,
                    Prodirect = g.Key.Prodirect,
                    Proindirect = g.Key.Proindirect,
                    Prolot = g.Key.Prolot,
                    Prohbn = g.Key.Prohbn,
                    Prost = g.Key.Prost,
                    Promodel = g.Key.Promodel,
                    Proworktime = g.Sum(p => p.Prorealtime),
                    Proplanqty = g.Sum(p => p.Prostdcapacity),
                    Proworkqty = g.Sum(p => p.Prorealqty),
                    Proworkst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / g.Sum(p => p.Prorealqty) * 0.85m : 0),
                    Prodiffst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / g.Sum(p => p.Prorealqty) - g.Key.Prost : g.Key.Prost),
                    Prodiffqty = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) - g.Sum(p => p.Prostdcapacity) : 0),
                    Proactivratio = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) / g.Sum(p => p.Prostdcapacity) : 0),
                };
            var qs = q.Select(E => new
            {
                E.Prodate,
                E.Prolinename,
                E.Prodirect,
                E.Proindirect,
                E.Prolot,
                E.Promodel,
                E.Prohbn,
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

            //IQueryable<Pp_Defect> q = DB.Pp_Defects; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Prolot.Contains(searchText) || u.Prohbn.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }
            //else
            //{
            //    //当前日期
            //    string dd = DateTime.Now.ToString("yyyyMMdd");
            //    q = q.Where(u => u.Prodate.ToString().Contains(dd));
            //}

            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

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
                              生产班组 = p.Prolinename,
                              生产日期 = p.Prodate,
                              生产批次 = p.Prolot,
                              物料 = p.Prohbn,

                              机种 = p.Promodel,
                              ST = p.Prost,
                              计划台数 = p.Proplanqty,
                              直接 = p.Prodirect,
                              间接 = p.Proindirect,
                              工数 = p.Proworktime,

                              实绩台数 = p.Proworkqty,

                              实绩ST = (decimal)Math.Round(p.Proworkst, 2),
                              ST差异 = (decimal)Math.Round(p.Prodiffst, 2),
                              台数差异 = (decimal)Math.Round(p.Prodiffqty, 2),
                              达成率 = (decimal)Math.Round(p.Proactivratio, 4),
                          };
                ExportHelper.LineOutput_XlsxFile(ConvertHelper.LinqConvertToDataTable(qss), "ACTUAL" + DpStartDate.SelectedDate.Value.ToString("yyyyMM"), ExportFileName, DpStartDate.SelectedDate.Value.ToString("yyyyMM"));

                //Grid1.AllowPaging = false;
                //ExportHelper.EpplustoXLSXfile(ExportHelper.GetGridDataTable(Grid1), Xlsbomitem, ExportFileName);
                //Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        protected void BtnRepair_Click(object sender, EventArgs e)
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
            Xlsbomitem = DpStartDate.SelectedDate.Value.ToString("yyyyMM") + "_Modify(Line)_Output_Report";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            var q_rework = from p in DB.Pp_P2d_OutputSubs
                               //join b in DB.Pp_P2d_Outputs on p.Parent.ID equals b.ID
                           where p.IsDeleted == 0
                           where p.Prorealtime != 0 || p.Prolinestopmin != 0
                           where p.Proorder.Substring(0, 2).Contains("54")
                           select new
                           {
                               Prodate = p.Prodate,
                               Prolinename = p.Prolinename,
                               Prodirect = p.Prodirect,
                               Proindirect = p.Proindirect,
                               Prolot = p.Prolot,
                               Prohbn = p.Prohbn,
                               Prost = p.Prost,
                               Promodel = p.Promodel,
                               Prorealtime = p.Prorealtime,
                               Prostdcapacity = p.Prostdcapacity,
                               Prorealqty = p.Prorealqty,
                           };

            var q =
                from p in q_rework
                group p by new { p.Prodate, p.Prolinename, p.Prolot, p.Promodel, p.Prohbn, p.Prost, p.Prodirect, p.Proindirect } into g
                select new
                {
                    Prodate = g.Key.Prodate,
                    Prolinename = g.Key.Prolinename,
                    Prodirect = g.Key.Prodirect,
                    Proindirect = g.Key.Proindirect,
                    Prolot = g.Key.Prolot,
                    Prohbn = g.Key.Prohbn,
                    Prost = g.Key.Prost,
                    Promodel = g.Key.Promodel,
                    Proworktime = g.Sum(p => p.Prorealtime),
                    Proplanqty = g.Sum(p => p.Prostdcapacity),
                    Proworkqty = g.Sum(p => p.Prorealqty),
                    Proworkst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / g.Sum(p => p.Prorealqty) * 0.85m : 0),
                    Prodiffst = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Prorealtime) / g.Sum(p => p.Prorealqty) - g.Key.Prost : g.Key.Prost),
                    Prodiffqty = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) - g.Sum(p => p.Prostdcapacity) : 0),
                    Proactivratio = (g.Sum(p => p.Prostdcapacity) != 0 ? g.Sum(p => p.Prorealqty) / g.Sum(p => p.Prostdcapacity) : 0),
                };
            var qs = q.Select(E => new
            {
                E.Prodate,
                E.Prolinename,
                E.Prodirect,
                E.Proindirect,
                E.Prolot,
                E.Promodel,
                E.Prohbn,
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

            //IQueryable<Pp_Defect> q = DB.Pp_Defects; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Prolot.Contains(searchText) || u.Prohbn.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }
            //else
            //{
            //    //当前日期
            //    string dd = DateTime.Now.ToString("yyyyMMdd");
            //    q = q.Where(u => u.Prodate.ToString().Contains(dd));
            //}

            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

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
                              生产班组 = p.Prolinename,
                              生产日期 = p.Prodate,
                              生产批次 = p.Prolot,
                              物料 = p.Prohbn,

                              机种 = p.Promodel,
                              ST = p.Prost,
                              计划台数 = p.Proplanqty,
                              直接 = p.Prodirect,
                              间接 = p.Proindirect,
                              工数 = p.Proworktime,

                              实绩台数 = p.Proworkqty,
                              实绩ST = (decimal)Math.Round(p.Proworkst, 2),
                              ST差异 = (decimal)Math.Round(p.Prodiffst, 2),
                              台数差异 = (decimal)Math.Round(p.Prodiffqty, 2),
                              达成率 = (decimal)Math.Round(p.Proactivratio, 4),
                          };
                ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qss), Xlsbomitem, ExportFileName);

                //Grid1.AllowPaging = false;
                //ExportHelper.EpplustoXLSXfile(ExportHelper.GetGridDataTable(Grid1), Xlsbomitem, ExportFileName);
                //Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }
    }
}
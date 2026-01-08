using System;
using System.Data;
using System.Linq;
using FineUIPro;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lf_Manufacturing.PP.poor
{
    public partial class p2d_repair_defect_monthly : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP2DDefectView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static string formNo, issueNo, strDpStartDate, strDpEndDate, strDate, strPline, strPlot, strDpdate, strDqty, strRqty, strPmodel, strPdept, strBrate, strCrate;

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
            //CheckPowerWithButton("CoreFineExport", BtnDetail);

            //CheckPowerWithButton("CoreFineExport", Btn2003);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/oph_p1d_new.aspx", "P1D新增不良记录");
            //btnP2d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/oph_p2d_new.aspx", "P2D新增不良记录");

            //本月第一天
            //DpStartDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DpEndDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            // 每页记录数
            Grid1.PageSize = 300;
            ddlGridPageSize.SelectedValue = "300";

            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                string Pdate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");
                //查询在特定日期的全部工单
                var q = from a in DB.Pp_P2d_Manufacturing_Defects

                            //join b in DB.Pp_P1d_Outputs on a.Prolot equals b.Prolot
                        where a.IsDeleted == 0
                        //where b.IsDeleted == 0
                        //where a.Proorder.Substring(0, 2).Contains("44")
                        where a.Prodate.Contains(Pdate)
                        //where a.Prolinename.Contains("制")
                        //where a.Proorderqty == a.Prorealqty
                        select a;

                //qs.Count();

                //IQueryable<proDefect> q = DB.proDefects; //.Include(u => u.Dept);

                // 在用户名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Promodel.Contains(searchText) || u.Proorder.Contains(searchText) || u.Prolot.Contains(searchText) || u.Propcbtype.Contains(searchText) || u.Prolinename.Contains(searchText) || u.Propcbcheckout.Contains(searchText) || u.Probadresponsibility.Contains(searchText) || u.Prorepairman.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                }
                q = q.Distinct();
                //else
                //{
                //    //当前日期
                //    string dd = DateTime.Now.ToString("yyyyMMdd");
                //    q = q.Where(u => u.Prodate.ToString().Contains(dd));
                //}

                //string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                //string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

                //if (!string.IsNullOrEmpty(edate))
                //{
                //    q = q.Where(u => u.Prodate.Contains(edate));
                //}

                //查询在特定日期的全部批次并统计
                var count = from a in q
                            select a;
                //where a.Proorderqty == a.Prorealqty
                //  group a by new
                //  {
                //      a.Prolot,
                //  }
                //into g
                //  select new
                //  {
                //      Prolot = g.Key.Prolot,
                //      Prolinename = "",
                //      Prodate = "",

                //      Prolotqty = g.Sum(p => p.Proorderqty),
                //      Prorealqty = g.Sum(p => p.Prorealqty),
                //      Prodzeroefects = g.Sum(p => p.Prodzeroefects),
                //      Probadtotal = g.Sum(p => p.Probadtotal),
                //      Prodirectrate = (g.Sum(p => p.Prodzeroefects) != 0 ? (g.Sum(p => p.Prodzeroefects) * 1.0m / g.Sum(p => p.Prorealqty)) : 0),
                //      Probadrate = (g.Sum(p => p.Probadtotal) != 0 ? (g.Sum(p => p.Probadtotal) * 1.0m / g.Sum(p => p.Prorealqty)) : 0),
                //  };

                DataTable dtCount = ConvertHelper.LinqConvertToDataTable(count);

                //向DataTable中更新日期，班组，机种
                //for (int f = 0; f < dtCount.Rows.Count; f++)
                //{
                //    string UpdatePline = "";
                //    string UpdatePdate = "";
                //    string clot = dtCount.Rows[f][0].ToString();
                //    var countlist = (from a in DB.Pp_Defect_Totals
                //                     where a.Prolot == clot
                //                     //where a.Prodate.Contains("202002")
                //                     select a).ToList();
                //    if (countlist.Any())
                //    {
                //        for (int i = 0; i < countlist.Count(); i++)
                //        {
                //            UpdatePline += countlist[i].Prolinename.ToString() + ",";
                //            UpdatePdate += countlist[i].Prodate.ToString() + ",";
                //        }
                //        UpdatePline = String.Join(",", UpdatePline.Split(',').Distinct());
                //        UpdatePline = UpdatePline.Remove(UpdatePline.LastIndexOf(","), 1);

                //        UpdatePdate = String.Join(",", UpdatePdate.Split(',').Distinct());
                //        UpdatePdate = UpdatePdate.Remove(UpdatePdate.LastIndexOf(","), 1).Replace("~", ",").Replace("-", ",");
                //        UpdatePdate = UpdatePdate.Split(',').Min(item => Convert.ToInt32(item)) + "~" + UpdatePdate.Split(',').Max(item => Convert.ToInt32(item));
                //        //更新DataTable
                //        DataRow[] foundRows = dtCount.Select("Prolot = '" + clot + "'");

                //        DateTime dt = DateTime.Now;
                //        for (int i = 0; i < foundRows.Length; i++)
                //        {
                //            DataRow row = foundRows[i];
                //            row["Prolinename"] = UpdatePline;//统一对含_time的属性设置值.
                //            row["Prodate"] = UpdatePdate;
                //        }
                //    }
                //}

                // 在查询添加之后，排序和分页之前获取总记录数
                //Grid1.DataSource = qs;
                //Grid1.DataBind();

                Grid1.RecordCount = dtCount.Rows.Count;
                // 排列和数据库分页
                // 2.获取当前分页数据
                if (dtCount.Rows.Count > 0)
                {
                    // 排列和数据库分页

                    //DataTable tables = ConvertHelper.LinqConvertToDataTable(qs);
                    //DataTable table = GridHelper.GetPagedDataTable(Grid1, qs);
                    // 3.绑定到Grid
                    Grid1.DataSource = dtCount;
                    Grid1.DataBind();
                    //GridSummaryData(ConvertHelper.LinqConvertToDataTable(q));
                }
                else
                {
                    // 3.绑定到Grid
                    Grid1.DataSource = "";
                    Grid1.DataBind();
                }

                // ConvertHelper.LinqConvertToDataTable(qs);
                // 当前页的合计
                //GridSummaryData(ConvertHelper.LinqConvertToDataTable(qs));
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
            //CheckPowerWithLinkButtonField("CoreFinePrint", Grid1, "printField");
            //CheckPowerWithLinkButtonField("CoreFinePrint", Grid1, "printField");
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

        //不良导出
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
            SheetName = "D" + DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Prefix_XlsxName = DpEndDate.SelectedDate.Value.ToString("yyyyMM") + "_P2d_Repair_Report";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
            Export_FileName = Prefix_XlsxName + ".xlsx";

            try
            {
                var q = from a in DB.Pp_P2d_Manufacturing_Defects
                            //where p.Prorealqty==p.Prolotqty
                            //where p.Prodate.Substring(10, 6).CompareTo(strDpdate) <= 0
                        select a;

                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Promodel.Contains(searchText) || u.Proorder.Contains(searchText) || u.Prolot.Contains(searchText) || u.Propcbtype.Contains(searchText) || u.Prolinename.Contains(searchText) || u.Propcbcheckout.Contains(searchText) || u.Probadresponsibility.Contains(searchText) || u.Prorepairman.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                }
                //else
                //{
                //    //当前日期
                //    string dd = DateTime.Now.ToString("yyyyMMdd");
                //    q = q.Where(u => u.Prodate.ToString().Contains(dd));
                //}

                //string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.Prodate.Substring(0, 6).CompareTo(edate) == 0);
                }

                if (q.Any())
                {
                    var qs = q.Select(E => new
                    {
                        生产日期 = E.Prodate,
                        生产机种 = E.Promodel,
                        生产订单 = E.Proorder,
                        生产LOT = E.Prolot,
                        订单台数 = E.Proorderqty,
                        板别 = E.Propcbtype,
                        生产实绩 = E.Prorealqty,
                        生产组别 = E.Prolinename,
                        卡号 = E.Propcbcardno,
                        不良症状 = E.Prodefectsymptom,
                        检出工程 = E.Propcbcheckout,
                        不良原因 = E.Prodefectcause,
                        不良数量 = E.Probadqty,
                        不良台数 = E.Probadtotal,
                        责任归属 = E.Probadresponsibility,
                        不良性质 = E.Prodefectnature,
                        序列号 = E.Probadserial,
                        修理 = E.Prorepairman,
                    });

                    ExportHelper.Repair_XlsxFile(ConvertHelper.LinqConvertToDataTable(qs), "Pr" + DpEndDate.SelectedDate.Value.ToString("yyyyMM"), Export_FileName, DpEndDate.SelectedDate.Value.ToString("yyyyMM"));
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

            //Grid1.AllowPaging = false;
            //ExportHelper.EpplusToExcel(ExportHelper.GetGridDataTable(Grid1), Prefix_XlsxName, Export_FileName);
            //Grid1.AllowPaging = true;
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
            //Decimal ratio = 0.0m;

            foreach (DataRow row in source.Rows)
            {
                pTotal += Convert.ToDecimal(row["Prolotqty"]);
                rTotal += Convert.ToDecimal(row["Prorealqty"]);
                //ratio = rTotal / pTotal;
            }

            JObject summary = new JObject();
            //summary.Add("major", "全部合计");

            summary.Add("Prolotqty", pTotal.ToString("F2"));
            summary.Add("Prorealqty", rTotal.ToString("F2"));
            //summary.Add("Proactivratio", ratio.ToString("p0"));

            //Grid1.SummaryData = summary;
        }
    }
}
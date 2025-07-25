using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lf_Manufacturing.PP.poor
{
    public partial class p1d_defect_lot_finished : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreP1DDefectView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public static string formNo, issueNo, strDpStartDate, strDpEndDate, strDate, strPline, strPlot, strOrder, strDpdate, strDqty, strRqty, strPmodel, strPdept, strBrate, strCrate;

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
            CheckPowerWithButton("CoreFineExport", BtnDetail);

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
                var q = from a in DB.Pp_Defect_P1d_Orders
                        join b in DB.Pp_P1d_Outputs on
                new { a.Prolot, } equals
                new { b.Prolot, }
                //join b in DB.Pp_P1d_Outputs on a.Prolot equals b.Prolot
                        where a.IsDeleted == 0
                        where a.Prodept == ("ASSY")
                        where b.IsDeleted == 0
                        //where a.Proorder.Substring(0, 2).Contains("44")
                        //where b.Prodate.Contains(Pdate)
                        //where a.Proorderqty == a.Prorealqty
                        //where !a.Prolinename.Contains("制")
                        select new
                        {
                            a.Prolot,
                            a.Promodel,
                            a.Prolinename,
                            a.Prodate,
                            a.Proorder,
                            a.Proorderqty,
                            a.Prorealqty,
                            a.Pronobadqty,
                            a.Probadtotal,
                        };

                //qs.Count();

                //IQueryable<proDefect> q = DB.proDefects; //.Include(u => u.Dept);

                // 在用户名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Prolot.Contains(searchText) || u.Prolinename.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                }
                else
                {
                    q = q.Where(u => u.Prodate.Contains(Pdate));
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

                                //where a.Proorderqty == a.Prorealqty
                            group a by new
                            {
                                a.Prolot,
                                //a.Proorder
                            }
                          into g
                            select new
                            {
                                g.Key.Prolot,
                                Proorder = "",
                                Prolinename = "",
                                Prodate = "",
                                Prolotqty = g.Sum(p => p.Proorderqty),
                                Prorealqty = g.Sum(p => p.Prorealqty),
                                Pronobadqty = g.Sum(p => p.Pronobadqty),
                                Probadtotal = g.Sum(p => p.Probadtotal),
                                Prodirectrate = (g.Sum(p => p.Prorealqty) != 0 ? (g.Sum(p => p.Pronobadqty) * 1.0m / g.Sum(p => p.Prorealqty)) : 0),
                                Probadrate = (g.Sum(p => p.Prorealqty) != 0 ? (g.Sum(p => p.Probadtotal) * 1.0m / g.Sum(p => p.Prorealqty)) : 0),
                            };

                DataTable dtCount = ConvertHelper.LinqConvertToDataTable(count);

                //向DataTable中更新日期，班组，机种
                for (int f = 0; f < dtCount.Rows.Count; f++)
                {
                    string UpdatePline = "";
                    string UpdatePdate = "";
                    string UpdateOrders = "";
                    string clot = dtCount.Rows[f][0].ToString();
                    var countlist = (from a in DB.Pp_Defect_P1d_Orders
                                     where a.Prolot == clot
                                     where a.Prodept == ("ASSY")
                                     //where a.Prodate.Contains("202002")
                                     select a).ToList();
                    if (countlist.Any())
                    {
                        for (int i = 0; i < countlist.Count(); i++)
                        {
                            UpdatePline += countlist[i].Prolinename.ToString() + ",";
                            UpdatePdate += countlist[i].Prodate.ToString() + ",";
                            UpdateOrders += countlist[i].Proorder.ToString() + ",";
                        }
                        UpdatePline = String.Join(",", UpdatePline.Split(',').Distinct());
                        UpdatePline = UpdatePline.Remove(UpdatePline.LastIndexOf(","), 1);

                        UpdatePdate = String.Join(",", UpdatePdate.Split(',').Distinct());
                        UpdatePdate = UpdatePdate.Remove(UpdatePdate.LastIndexOf(","), 1).Replace("~", ",").Replace("-", ",");
                        UpdatePdate = UpdatePdate.Split(',').Min(item => Convert.ToInt32(item)) + "~" + UpdatePdate.Split(',').Max(item => Convert.ToInt32(item));

                        UpdateOrders = UpdateOrders.Remove(UpdateOrders.LastIndexOf(","), 1);

                        //更新DataTable
                        DataRow[] foundRows = dtCount.Select("Prolot = '" + clot + "'");

                        DateTime dt = DateTime.Now;
                        for (int i = 0; i < foundRows.Length; i++)
                        {
                            DataRow row = foundRows[i];
                            row["Prolinename"] = UpdatePline;//统一对含_time的属性设置值.
                            row["Prodate"] = UpdatePdate;
                            row["Proorder"] = UpdateOrders;
                        }
                    }
                }

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

        //不集计
        protected void BtnDetail_Click(object sender, EventArgs e)
        {
            GridtoExcel();

            //// 在操作之前进行权限检查
            //if (!CheckPower("CoreFineExport"))
            //{
            //    CheckPowerFailWithAlert();
            //    return;
            //}
            ////DataTable Exp = new DataTable();
            ////在库明细查询SQL
            //string Prefix_XlsxName, Export_FileName,SheetName;

            //// mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            //Prefix_XlsxName = DpEndDate.SelectedDate.Value.ToString("yyyyMM") + "Defect_DATA";
            ////mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
            //Export_FileName = Prefix_XlsxName + ".xlsx";
            //List<DataTable> tables = new List<DataTable>();
            //DataTable ma=new DataTable();
            //string pdate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

            //var q = from a in DB.Pp_Defect_P1d_Orders
            //        where a.Prongbdel == false
            //        where a.Prorealqty == a.Prolotqty
            //        where a.Pronobadqty != 0
            //        group a by new { a.Prolot, a.Udf001, a.Prolinename } into g
            //        select new
            //        {
            //            Prolot = g.Key.Prolot,
            //            Promodel = g.Key.Udf001,
            //            Prolinename = g.Key.Prolinename,
            //            Prodate = g.Min(a => a.Prodate.Substring(0, 8)) + "~" + g.Max(a => a.Prodate.Substring(9, 8)),
            //            Prolotqty = g.Sum(a => a.Prolotqty),
            //            Prorealqty = g.Sum(a => a.Prorealqty),
            //            Pronobadqty = g.Sum(a => a.Pronobadqty),
            //            Probadtotal = g.Sum(a => a.Probadtotal),
            //            Prodirectrate = Math.Round((decimal)g.Sum(a => a.Pronobadqty) / g.Sum(a => a.Prorealqty),2),
            //            Probadrate = Math.Round((decimal)g.Sum(a => a.Probadtotal) / g.Sum(a => a.Prorealqty),2),
            //        };

            ////qs.Count();

            ////IQueryable<proDefect> q = DB.proDefects; //.Include(u => u.Dept);

            //// 在用户名称中搜索
            //string searchText = ttbSearchMessage.Text.Trim();
            //if (!String.IsNullOrEmpty(searchText))
            //{
            //    q = q.Where(u => u.Prolot.Contains(searchText) || u.Promodel.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            //}
            ////else
            ////{
            ////    //当前日期
            ////    string dd = DateTime.Now.ToString("yyyyMMdd");
            ////    q = q.Where(u => u.Prodate.ToString().Contains(dd));
            ////}

            ////string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            //string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

            //if (!string.IsNullOrEmpty(edate))
            //{
            //    q = q.Where(u => u.Prodate.Substring(9, 6).CompareTo(edate) == 0);
            //}

            //ma = ConvertHelper.LinqConvertToDataTable(q);
            //if (q.Any())
            //{
            //    for (int i = 0; i < ma.Rows.Count; i++)
            //    {
            //        strPlot = ma.Rows[i][0].ToString();
            //        var q2 =
            //                from p in DB.proDefects
            //                //.Where(s => s.Prodate.Contains(dd))
            //                //.Where(s => s.Prodate.Substring(0, 8).CompareTo(strDpStartDate) >= 0)
            //                .Where(s => s.Prodate.Substring(9, 8).CompareTo(edate) <= 0)
            //                //.Where(s => s.Prolinename.Contains(strPline))
            //                .Where(s => s.Prolot.Contains(strPlot))
            //                .OrderBy(s => s.Prongdept)
            //                select new
            //                {
            //                    p.Prongdept,
            //                    p.Probadnote,
            //                    p.Probadreason,
            //                    p.Probadqty,
            //                };

            //        //IEnumerable 转换IQueryable//AsEnumerable//AsQueryable
            //        var qsub = from p in q2
            //                 .OrderBy(s => s.Prongdept)
            //                   select new
            //                   {
            //                       不良区分 = p.Prongdept,
            //                       不良症状 = p.Probadnote,
            //                       不良原因 = p.Probadreason,
            //                       不良件数 = p.Probadqty,
            //                   };
            //        DataTable ex = ConvertHelper.LinqConvertToDataTable(qsub);
            //        ex.TableName = strPlot;

            //        tables.Add(ex);
            //    }
            //}

            //ExportHelper.TableListToExcels(tables, ma, Export_FileName, 6);
        }

        private void GridtoExcel()
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }
            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Prefix_XlsxName, Export_FileName;
            string FsearchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(FsearchText))
            {
                Prefix_XlsxName = DpEndDate.SelectedDate.Value.ToString("yyyyMM") + FsearchText + "_" + "DefectReport";
            }
            else

            {
                Prefix_XlsxName = DpEndDate.SelectedDate.Value.ToString("yyyyMM") + "_LotDefectReport";
            }
            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";

            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
            Export_FileName = Prefix_XlsxName + ".xlsx";
            List<DataTable> tables = new List<DataTable>();
            DataTable ma = new DataTable();
            //string pdate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");
            string Pdate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");
            var q = from a in DB.Pp_Defect_P1d_Orders
                    join b in DB.Pp_P1d_Outputs on
                new { a.Prolot, } equals
                new { b.Prolot, }
                    where a.IsDeleted == 0
                    where b.IsDeleted == 0
                    where a.Prodept == ("ASSY")
                    //where a.Proorder.Substring(0, 2).Contains("44")
                    //where b.Prodate.Substring(0, 6).CompareTo(Pdate) == 0
                    //where a.Proorderqty == a.Prorealqty
                    select new
                    {
                        a.Prolot,
                        a.Promodel,
                        a.Prolinename,
                        a.Prodate,
                        a.Proorder,
                        a.Proorderqty,
                        a.Prorealqty,
                        a.Pronobadqty,
                        a.Probadtotal,
                    };

            //qs.Count();

            //IQueryable<proDefect> q = DB.proDefects; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Prolot.Contains(searchText) || u.Prolinename.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }
            else
            {
                q = q.Where(u => u.Prodate.ToString().Contains(Pdate));
            }

            //string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            //string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

            //if (!string.IsNullOrEmpty(edate))
            //{
            //    q = q.Where(u => u.Prodate.Contains(edate));
            //}
            q = q.Distinct();
            var counts = from a in q
                             //where a.IsDeleted == 0
                             //where a.Proorderqty == a.Prorealqty
                         group a by new
                         {
                             a.Prolot,
                             //a.Proorder
                         }
                      into g
                         select new
                         {
                             g.Key.Prolot,
                             Proorder = "",
                             Prolinename = "",
                             Prodate = "",
                             Promodel = "",
                             Prolotqty = g.Sum(p => p.Proorderqty),
                             Prorealqty = g.Sum(p => p.Prorealqty),
                             Pronobadqty = g.Sum(p => p.Pronobadqty),
                             Probadtotal = g.Sum(p => p.Probadtotal),
                             Prodirectrate = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Pronobadqty) * 1.0m / g.Sum(p => p.Prorealqty) : 0),
                             Probadrate = (g.Sum(p => p.Prorealqty) != 0 ? g.Sum(p => p.Probadtotal) * 1.0m / g.Sum(p => p.Prorealqty) : 0),
                         };

            var count = (from a in counts
                         select a)
           .ToList().Select(
                p => new
                {

                    p.Prolot,
                    p.Prolinename,
                    p.Prodate,
                    p.Promodel,
                    p.Prolotqty,
                    p.Prorealqty,
                    p.Pronobadqty,
                    p.Probadtotal,
                    p.Prodirectrate,// = p.Prodirectrate.ToString("0.00"),
                    p.Probadrate,// = p.Probadrate.ToString("0.00"),// + "%",
                    p.Proorder,
                }).ToList();
            DataTable dtCount = ConvertHelper.LinqConvertToDataTable(count.AsQueryable());
            //向DataTable中更新日期，班组，机种
            for (int f = 0; f < dtCount.Rows.Count; f++)
            {
                strDpStartDate = "";
                strDpEndDate = "";
                string UpdatePline = "";
                string UpdatePdate = "";
                string UpdatePmodel = "";
                string UpdaterPorder = "";
                //string Corder = dtCount.Rows[f][10].ToString();
                string clot = dtCount.Rows[f][0].ToString();
                var countlist = (from a in DB.Pp_Defect_P1d_Orders
                                 where a.Prolot == clot
                                 where a.Prodept == ("ASSY")
                                 //where a.Prodate.Contains(edate)
                                 select a).ToList();
                if (countlist.Any())
                {
                    for (int i = 0; i < countlist.Count(); i++)
                    {
                        UpdatePline += countlist[i].Prolinename.ToString() + ",";
                        UpdatePdate += countlist[i].Prodate.ToString() + ",";
                        UpdatePmodel += countlist[i].Promodel.ToString() + ",";
                        UpdaterPorder += countlist[i].Proorder.ToString() + ",";
                    }
                    UpdatePline = String.Join(",", UpdatePline.Split(',').Distinct());
                    UpdatePline = UpdatePline.Remove(UpdatePline.LastIndexOf(","), 1);

                    UpdatePdate = String.Join(",", UpdatePdate.Split(',').Distinct());
                    UpdatePdate = UpdatePdate.Remove(UpdatePdate.LastIndexOf(","), 1).Replace("~", ",").Replace("-", ",");
                    UpdatePdate = UpdatePdate.Split(',').Min(item => Convert.ToInt32(item)) + "~" + UpdatePdate.Split(',').Max(item => Convert.ToInt32(item));

                    UpdatePmodel = String.Join(",", UpdatePmodel.Split(',').Distinct());
                    UpdatePmodel = UpdatePmodel.Remove(UpdatePmodel.LastIndexOf(","), 1);

                    UpdaterPorder = UpdaterPorder.Remove(UpdaterPorder.LastIndexOf(","), 1);
                    //更新DataTable
                    DataRow[] foundRows = dtCount.Select("Prolot = '" + clot + "'");

                    DateTime dt = DateTime.Now;
                    for (int i = 0; i < foundRows.Length; i++)
                    {
                        DataRow row = foundRows[i];
                        row["Prolinename"] = UpdatePline;//统一对含_time的属性设置值.
                        row["Prodate"] = UpdatePdate;
                        row["Promodel"] = UpdatePmodel;
                        row["Proorder"] = UpdaterPorder;
                    }
                }
            }
            //向DataTable中的字段设定类型
            foreach (DataRow drExcel in dtCount.Rows)
            {
                Decimal Prodirectrate = decimal.Parse(drExcel["Prodirectrate"].ToString());
                //drExcel["Prodirectrate"] = decimal.Parse(Prodirectrate.ToString()).ToString("0.00") + "%";
                Decimal Probadrate = decimal.Parse(drExcel["Probadrate"].ToString());
                //drExcel["Probadrate"] = decimal.Parse(Probadrate.ToString()).ToString("0.00") + "%";
            }
            ma = dtCount;
            if (q.Any())
            {
                for (int i = 0; i < ma.Rows.Count; i++)
                {
                    strPlot = ma.Rows[i][0].ToString();
                    strOrder = ma.Rows[i][10].ToString();
                    //string sorder = ma.Rows[i][1].ToString();
                    //strPline= ma.Rows[i][2].ToString();
                    strDpStartDate = ma.Rows[i][2].ToString().Substring(0, 8);
                    strDpEndDate = ma.Rows[i][2].ToString().Substring(9, 8);
                    var q2 =
                            from p in DB.Pp_P1d_Defects
                            .Where(s => s.IsDeleted == 0)
                            //.Where(s => s.Prodate.Contains(dd))
                            //.Where(s => s.Prodate.Substring(0, 8).CompareTo(strDpStartDate) >= 0)
                            .Where(s => s.Prodate.CompareTo(strDpStartDate) >= 0)
                            .Where(s => s.Prodate.CompareTo(strDpEndDate) <= 0)
                            //.Where(s => s.Proorder.Contains(sorder))
                            .Where(s => (s.Prolot).Contains(strPlot))
                            //.Where(s => (s.Proorder).Contains(strOrder))
                            //.Where(s => s.Prorealqty==s.Proorderqty)
                            .OrderBy(s => s.Prongdept)
                            select new
                            {
                                p.Prongdept,
                                p.Probadnote,
                                p.Probadset,
                                p.Probadreason,
                                p.Probadqty,
                            };

                    //IEnumerable 转换IQueryable//AsEnumerable//AsQueryable
                    var qsub = from p in q2
                         .OrderBy(s => s.Prongdept)
                               select new
                               {
                                   区分 = p.Prongdept,
                                   不良症状 = p.Probadnote,
                                   不良个所 = p.Probadset,
                                   不良原因 = p.Probadreason,
                                   件数 = p.Probadqty,
                               };
                    DataTable ex = ConvertHelper.LinqConvertToDataTable(qsub);
                    //sheet名称不能包含以下字符"[","]","\","/","?",":","*"
                    ex.TableName = strPlot + "_" + strOrder.Replace(",", "_"); // + "_" + i;

                    tables.Add(ex);
                }

                //ExportHelper.TableListToExcels(tables, ma, Export_FileName, 6);

                ExportHelper.LotExportMultipleSheets(tables, ma, Export_FileName);
            }
            else
            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        //两个Data合并将两个DataTable 横拼接在一起
        private DataTable UniteDataTable(DataTable dt1, DataTable dt2, string DTName)
        {
            DataTable dt3 = dt1.Clone();
            for (int i = 0; i < dt2.Columns.Count; i++)
            {
                dt3.Columns.Add(dt2.Columns[i].ColumnName);
            }
            object[] obj = new object[dt3.Columns.Count];

            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                dt1.Rows[i].ItemArray.CopyTo(obj, 0);
                dt3.Rows.Add(obj);
            }

            if (dt1.Rows.Count >= dt2.Rows.Count)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    for (int j = 0; j < dt2.Columns.Count; j++)
                    {
                        dt3.Rows[i][j + dt1.Columns.Count] = dt2.Rows[i][j].ToString();
                    }
                }
            }
            else
            {
                DataRow dr3;
                for (int i = 0; i < dt2.Rows.Count - dt1.Rows.Count; i++)
                {
                    dr3 = dt3.NewRow();
                    dt3.Rows.Add(dr3);
                }
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    for (int j = 0; j < dt2.Columns.Count; j++)
                    {
                        dt3.Rows[i][j + dt1.Columns.Count] = dt2.Rows[i][j].ToString();
                    }
                }
            }
            dt3.TableName = DTName;
            return dt3;
        }

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
            string Prefix_XlsxName, Export_FileName;

            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Prefix_XlsxName = DpEndDate.SelectedDate.Value.ToString("yyyyMM") + "_DefectDetails";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
            Export_FileName = Prefix_XlsxName + ".xlsx";

            try
            {
                string Pdate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");
                var q =
                (from p in DB.Pp_P1d_Defects
                     //where p.Prorealqty==p.Prolotqty
                     //where p.Prodate.Substring(0, 6).CompareTo(Pdate) == 0
                 select p);

                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Prolot.Contains(searchText) || u.Prolinename.Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                }
                else
                {
                    //当前日期
                    //string dd = DateTime.Now.ToString("yyyyMMdd");
                    q = q.Where(u => u.Prodate.ToString().Contains(Pdate));
                }

                //string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                //string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

                //if (!string.IsNullOrEmpty(edate))
                //{
                //    q = q.Where(u => u.Prodate.Substring(9, 6).CompareTo(edate) == 0);
                //}

                if (q.Any())
                {
                    var qs = q.Select(E => new
                    {
                        工单号 = E.Proorder,
                        批次 = E.Prolot,
                        班组 = E.Prolinename,
                        日期 = E.Prodate,
                        生产数量 = E.Prorealqty,
                        无不良台数 = E.Pronobadqty,
                        不良件数 = E.Probadtotal,
                        症状 = E.Probadnote,
                        个所 = E.Probadset,
                        原因 = E.Probadreason,
                    });

                    ExportHelper.EpplusToExcel(ConvertHelper.LinqConvertToDataTable(qs), Prefix_XlsxName, Export_FileName, "DTA 生产不良日报");
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
                ttbSearchMessage.Text = String.Empty;
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
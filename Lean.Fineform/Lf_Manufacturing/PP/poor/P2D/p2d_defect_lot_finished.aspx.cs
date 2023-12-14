using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;

using System.Data.SqlClient;
using System.Data;
using System.Xml;
using Newtonsoft.Json.Linq;
namespace Lean.Fineform.Lf_Manufacturing.PP.poor.P2D
{
    public partial class p2d_defect_lot_finished : PageBase
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

        #endregion

        #region Page_Load

        public static string formNo, issueNo, strDPstart, strDPend, strDate, strPline, strPlot, strDpdate, strDqty, strRqty, strPmodel, strPdept, strBrate, strCrate;

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
            CheckPowerWithButton("CoreKitOutput", BtnExport);
            CheckPowerWithButton("CoreKitOutput", BtnDetail);

            //CheckPowerWithButton("CoreKitOutput", Btn2003);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/oph_p1d_new.aspx", "P1D新增不良记录");
            //btnP2d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/oph_p2d_new.aspx", "P2D新增不良记录");


            //本月第一天
            //DPstart.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DPend.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            // 每页记录数
            Grid1.PageSize = 300;
            ddlGridPageSize.SelectedValue = "300";

            BindGrid();

        }



        private void BindGrid()
        {
            try
            {
                string Pdate = DPend.SelectedDate.Value.ToString("yyyyMM");
                //查询在特定日期的全部工单
                var q = from a in DB.Pp_DefectTotals

                        join b in DB.Pp_P2d_Outputs on a.Prolot equals b.Prolot
                        where a.isDelete == 0
                        where b.isDelete == 0
                        //where a.Proorder.Substring(0, 2).Contains("44")
                        where b.Prodate.Contains(Pdate)
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
                    q = q.Where(u => u.Prolot.Contains(searchText) || u.Prolinename.Contains(searchText)); //|| u.CreateTime.Contains(searchText));
                }
                q = q.Distinct();
                //else
                //{
                //    //当前日期
                //    string dd = DateTime.Now.ToString("yyyyMMdd");
                //    q = q.Where(u => u.Prodate.ToString().Contains(dd));
                //}

                //string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                //string edate = DPend.SelectedDate.Value.ToString("yyyyMM");



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
                            }
                          into g
                            select new
                            {
                                Prolot = g.Key.Prolot,
                                Prolinename = "",
                                Prodate = "",

                                Prolotqty = g.Sum(p => p.Proorderqty),
                                Prorealqty = g.Sum(p => p.Prorealqty),
                                Pronobadqty = g.Sum(p => p.Pronobadqty),
                                Probadtotal = g.Sum(p => p.Probadtotal),
                                Prodirectrate = (g.Sum(p => p.Pronobadqty) != 0 ? (g.Sum(p => p.Pronobadqty) * 1.0m / g.Sum(p => p.Prorealqty)) : 0),
                                Probadrate = (g.Sum(p => p.Probadtotal) != 0 ? (g.Sum(p => p.Probadtotal) * 1.0m / g.Sum(p => p.Prorealqty)) : 0),
                            };


                DataTable dtCount = ConvertHelper.LinqConvertToDataTable(count);

                //向DataTable中更新日期，班组，机种
                for (int f = 0; f < dtCount.Rows.Count; f++)
                {
                    string UpdatePline = "";
                    string UpdatePdate = "";
                    string clot = dtCount.Rows[f][0].ToString();
                    var countlist = (from a in DB.Pp_DefectTotals
                                     where a.Prolot == clot
                                     //where a.Prodate.Contains("202002")
                                     select a).ToList();
                    if (countlist.Any())
                    {
                        for (int i = 0; i < countlist.Count(); i++)
                        {
                            UpdatePline += countlist[i].Prolinename.ToString() + ",";
                            UpdatePdate += countlist[i].Prodate.ToString() + ",";
                        }
                        UpdatePline = String.Join(",", UpdatePline.Split(',').Distinct());
                        UpdatePline = UpdatePline.Remove(UpdatePline.LastIndexOf(","), 1);

                        UpdatePdate = String.Join(",", UpdatePdate.Split(',').Distinct());
                        UpdatePdate = UpdatePdate.Remove(UpdatePdate.LastIndexOf(","), 1).Replace("~", ",").Replace("-", ",");
                        UpdatePdate = UpdatePdate.Split(',').Min(item => Convert.ToInt32(item)) + "~" + UpdatePdate.Split(',').Max(item => Convert.ToInt32(item));
                        //更新DataTable
                        DataRow[] foundRows = dtCount.Select("Prolot = '" + clot + "'");


                        DateTime dt = DateTime.Now;
                        for (int i = 0; i < foundRows.Length; i++)
                        {
                            DataRow row = foundRows[i];
                            row["Prolinename"] = UpdatePline;//统一对含_time的属性设置值.
                            row["Prodate"] = UpdatePdate;
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
                    //OutputSummaryData(ConvertHelper.LinqConvertToDataTable(q));
                }
                else
                {
                    // 3.绑定到Grid
                    Grid1.DataSource = "";
                    Grid1.DataBind();
                }

                // ConvertHelper.LinqConvertToDataTable(qs);
                // 当前页的合计
                //OutputSummaryData(ConvertHelper.LinqConvertToDataTable(qs));

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

        #endregion

        #region Events

        protected void ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            //BindDDLData();
            //DDLline.Items.Clear();
            ttbSearchMessage.ShowTrigger1 = true;
            BindGrid();
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            //BindDDLData();
            //DDLline.Items.Clear();
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
            //CheckPowerWithLinkButtonField("CoreKitPrint", Grid1, "printField");
            //CheckPowerWithLinkButtonField("CoreKitPrint", Grid1, "printField");
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

        #endregion

        //不集计
        protected void BtnDetail_Click(object sender, EventArgs e)
        {

            GridtoExcel();

            //// 在操作之前进行权限检查
            //if (!CheckPower("CoreKitOutput"))
            //{
            //    CheckPowerFailWithAlert();
            //    return;
            //}
            ////DataTable Exp = new DataTable();
            ////在库明细查询SQL
            //string Xlsbomitem, ExportFileName;

            //// mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            //Xlsbomitem = DPend.SelectedDate.Value.ToString("yyyyMM") + "Defect_DATA";
            ////mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            //ExportFileName = Xlsbomitem + ".xlsx";
            //List<DataTable> tables = new List<DataTable>();
            //DataTable ma=new DataTable();
            //string pdate = DPend.SelectedDate.Value.ToString("yyyyMM");

            //var q = from a in DB.Pp_DefectTotals
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
            //    q = q.Where(u => u.Prolot.Contains(searchText) || u.Promodel.Contains(searchText)); //|| u.CreateTime.Contains(searchText));
            //}
            ////else
            ////{
            ////    //当前日期
            ////    string dd = DateTime.Now.ToString("yyyyMMdd");
            ////    q = q.Where(u => u.Prodate.ToString().Contains(dd));
            ////}

            ////string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
            //string edate = DPend.SelectedDate.Value.ToString("yyyyMM");



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
            //                //.Where(s => s.Prodate.Substring(0, 8).CompareTo(strDPstart) >= 0)
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

            //ExportHelper.TableListToExcels(tables, ma, ExportFileName, 6);

        }

        private void GridtoExcel()
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreKitOutput"))
            {
                CheckPowerFailWithAlert();
                return;
            }
            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName;
            string FsearchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(FsearchText))
            {
                Xlsbomitem = DPend.SelectedDate.Value.ToString("yyyyMM") + "_" + FsearchText + "_" +  "DefectDetail";
            }
            else

            {
                Xlsbomitem = DPend.SelectedDate.Value.ToString("yyyyMM")+"_DefectDetails" ;
            }
            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";

            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";
            List<DataTable> tables = new List<DataTable>();
            DataTable ma = new DataTable();
            //string pdate = DPend.SelectedDate.Value.ToString("yyyyMM");
            string Pdate = DPend.SelectedDate.Value.ToString("yyyyMM");
            var q = from a in DB.Pp_DefectTotals
                    join b in DB.Pp_P1d_Outputs on a.Prolot equals b.Prolot
                    where a.isDelete == 0
                    where b.isDelete == 0
                    //where a.Proorder.Substring(0, 2).Contains("44")
                    where b.Prodate.Substring(0, 6).CompareTo(Pdate) == 0
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
                q = q.Where(u => u.Prolot.Contains(searchText) || u.Prolinename.Contains(searchText)); //|| u.CreateTime.Contains(searchText));
            }
            //else
            //{
            //    //当前日期
            //    string dd = DateTime.Now.ToString("yyyyMMdd");
            //    q = q.Where(u => u.Prodate.ToString().Contains(dd));
            //}

            //string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
            //string edate = DPend.SelectedDate.Value.ToString("yyyyMM");



            //if (!string.IsNullOrEmpty(edate))
            //{
            //    q = q.Where(u => u.Prodate.Contains(edate));
            //}
            q = q.Distinct();
            var counts = from a in q
                             //where a.isDelete == 0
                             //where a.Proorderqty == a.Prorealqty
                         group a by new
                         {
                             a.Prolot,
                         }
                      into g
                         select new
                         {
                             Prolot = g.Key.Prolot,
                             Prolinename = "",
                             Prodate = "",
                             Promodel = "",
                             Prolotqty = g.Sum(p => p.Proorderqty),
                             Prorealqty = g.Sum(p => p.Prorealqty),
                             Pronobadqty = g.Sum(p => p.Pronobadqty),
                             Probadtotal = g.Sum(p => p.Probadtotal),
                             Prodirectrate = (g.Sum(p => p.Pronobadqty) != 0 ? g.Sum(p => p.Pronobadqty) * 1.0m / g.Sum(p => p.Prorealqty) : 0),
                             Probadrate = (g.Sum(p => p.Probadtotal) != 0 ? g.Sum(p => p.Probadtotal) * 1.0m / g.Sum(p => p.Prorealqty) : 0),
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
                    Prodirectrate = p.Prodirectrate.ToString("0.00"),
                    Probadrate = p.Probadrate.ToString("0.00"),// + "%",
                }).ToList();
            DataTable dtCount = ConvertHelper.LinqConvertToDataTable(count.AsQueryable());
            //向DataTable中更新日期，班组，机种
            for (int f = 0; f < dtCount.Rows.Count; f++)
            {
                strDPstart = "";
                strDPend = "";
                string UpdatePline = "";
                string UpdatePdate = "";
                string UpdatePmodel = "";
                string clot = dtCount.Rows[f][0].ToString();
                var countlist = (from a in DB.Pp_DefectTotals
                                 where a.Prolot == clot
                                 //where a.Prodate.Contains(edate)
                                 select a).ToList();
                if (countlist.Any())
                {
                    for (int i = 0; i < countlist.Count(); i++)
                    {
                        UpdatePline += countlist[i].Prolinename.ToString() + ",";
                        UpdatePdate += countlist[i].Prodate.ToString() + ",";
                        UpdatePmodel += countlist[i].Promodel.ToString() + ",";
                    }
                    UpdatePline = String.Join(",", UpdatePline.Split(',').Distinct());
                    UpdatePline = UpdatePline.Remove(UpdatePline.LastIndexOf(","), 1);

                    UpdatePdate = String.Join(",", UpdatePdate.Split(',').Distinct());
                    UpdatePdate = UpdatePdate.Remove(UpdatePdate.LastIndexOf(","), 1).Replace("~", ",").Replace("-", ",");
                    UpdatePdate = UpdatePdate.Split(',').Min(item => Convert.ToInt32(item)) + "~" + UpdatePdate.Split(',').Max(item => Convert.ToInt32(item));

                    UpdatePmodel = String.Join(",", UpdatePmodel.Split(',').Distinct());
                    UpdatePmodel = UpdatePmodel.Remove(UpdatePmodel.LastIndexOf(","), 1);
                    //更新DataTable
                    DataRow[] foundRows = dtCount.Select("Prolot = '" + clot + "'");


                    DateTime dt = DateTime.Now;
                    for (int i = 0; i < foundRows.Length; i++)
                    {
                        DataRow row = foundRows[i];
                        row["Prolinename"] = UpdatePline;//统一对含_time的属性设置值.
                        row["Prodate"] = UpdatePdate;
                        row["Promodel"] = UpdatePmodel;
                    }
                }

            }
            //向DataTable中的字段设定类型
            foreach (DataRow drExcel in dtCount.Rows)
            {
                Decimal Prodirectrate = decimal.Parse(drExcel["Prodirectrate"].ToString()) * 100;
                drExcel["Prodirectrate"] = decimal.Parse(Prodirectrate.ToString()).ToString("0.00") + "%";
                Decimal Probadrate = decimal.Parse(drExcel["Probadrate"].ToString()) * 100;
                drExcel["Probadrate"] = decimal.Parse(Probadrate.ToString()).ToString("0.00") + "%";
            }
            ma = dtCount;
            if (q.Any())
            {

                for (int i = 0; i < ma.Rows.Count; i++)
                {
                    strPlot = ma.Rows[i][0].ToString();
                    //string sorder = ma.Rows[i][1].ToString();
                    //strPline= ma.Rows[i][2].ToString();
                    strDPstart = ma.Rows[i][2].ToString().Substring(0, 8);
                    strDPend = ma.Rows[i][2].ToString().Substring(9, 8);
                    var q2 =
                            from p in DB.Pp_P2d_Defects
                            .Where(s => s.isDelete == 0)
                            //.Where(s => s.Prodate.Contains(dd))
                            //.Where(s => s.Prodate.Substring(0, 8).CompareTo(strDPstart) >= 0)
                            .Where(s => s.Prodate.CompareTo(strDPstart) >= 0)
                            .Where(s => s.Prodate.CompareTo(strDPend) <= 0)
                            //.Where(s => s.Proorder.Contains(sorder))
                            .Where(s => (s.Prolot).Contains(strPlot))
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
                    ex.TableName = strPlot + "_" + i;


                    tables.Add(ex);
                }

                ExportHelper.TableListToExcels(tables, ma, ExportFileName, 6);
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
            if (!CheckPower("CoreKitOutput"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName;

            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Xlsbomitem = DPend.SelectedDate.Value.ToString("yyyyMM") + "Defect_DATA";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            try
            {

                var q =
                (from p in DB.Pp_DefectTotals
                     //where p.Prorealqty==p.Prolotqty
                     //where p.Prodate.Substring(10, 6).CompareTo(strDpdate) <= 0
                 group p by new
                 {
                     p.Prolot,
                     p.Prolinename,
                     p.Prodate
                 }
                into g
                 select new
                 {
                     Prolot = g.Key.Prolot,
                     Prolinename = g.Key.Prolinename,
                     Prodate = g.Key.Prodate,

                     Prolotqty = g.Sum(p => p.Proorderqty),
                     Prorealqty = g.Sum(p => p.Prorealqty),
                     Pronobadqty = g.Sum(p => p.Pronobadqty),
                     Probadtotal = g.Sum(p => p.Probadtotal),
                     Prodirectrate = ((decimal)(g.Sum(p => p.Pronobadqty) != 0 ? g.Sum(p => p.Pronobadqty) / g.Sum(p => p.Prorealqty) : 0)).ToString(),
                     Probadrate = ((decimal)(g.Sum(p => p.Probadtotal) != 0 ? g.Sum(p => p.Probadtotal) / g.Sum(p => p.Prorealqty) : 0)).ToString(),

                 });


                string searchText = ttbSearchMessage.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Prolot.Contains(searchText) || u.Prolinename.Contains(searchText)); //|| u.CreateTime.Contains(searchText));
                }
                //else
                //{
                //    //当前日期
                //    string dd = DateTime.Now.ToString("yyyyMMdd");
                //    q = q.Where(u => u.Prodate.ToString().Contains(dd));
                //}

                //string sdate = DPstart.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DPend.SelectedDate.Value.ToString("yyyyMM");



                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.Prodate.Substring(9, 6).CompareTo(edate) == 0);
                }


                if (q.Any())
                {
                    var qs = q.Select(E => new
                    {
                        批次 = E.Prolot,
                        班组 = E.Prolinename,
                        日期 = E.Prodate,
                        E.Prolotqty,
                        生产数量 = E.Prorealqty,
                        无不良台数 = E.Pronobadqty,
                        不良件数 = E.Probadtotal,
                        直行率 = E.Prodirectrate,
                        不良率 = E.Probadrate,
                    });


                    ExportHelper.EpplustoXLSXfile(ConvertHelper.LinqConvertToDataTable(qs), Xlsbomitem, ExportFileName);


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
            //ExportHelper.EpplustoXLSXfile(ExportHelper.GetGridDataTable(Grid1), Xlsbomitem, ExportFileName);
            //Grid1.AllowPaging = true;
        }
        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
            {
                BindGrid();
            }
        }
        //合计表格
        private void OutputSummaryData(DataTable source)
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

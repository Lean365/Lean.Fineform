using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lf_Manufacturing.PP.timesheet
{
    public partial class p1d_times : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreTimeView";
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
            //BindDdlData();
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreOphDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreOphNew", btnP1dNew);
            //CheckPowerWithButton("CoreProophp1dNew", btnPrint);
            ////CheckPowerWithButton("CoreProophp1dEdit", btnP1dEdit);
            CheckPowerWithButton("CoreFineExport", BtnExport);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1dNew.OnClientClick = Window1.GetShowReference("~/oneProduction/oneDaliy/oph_new.aspx", "新增") + Window1.GetMaximizeReference();
            //PageContext.RegisterStartupScript(Window1.GetMaximizeReference());
            //Window1.GetMaximizeReference();
            //btnPrint.OnClientClick = Window1.GetShowReference("~~/oneProduction/oneTimesheet/oph_report.aspx", "打印报表");
            //btnP1dEdit.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_edit.aspx?id={0}", "修改");

            //本月第一天
            DpStartDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DpEndDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            // 每页记录数
            Grid1.PageSize = 5000;
            ddlGridPageSize.SelectedValue = "5000";

            BindGrid();
            BindDdlLine();
        }

        private void BindGrid()
        {
            try
            {
                var q =
                    from p in DB.Pp_P1d_OutputSubs
                        //join b in DB.Pp_P1d_Outputs on p.Parent.ID equals b.ID
                    where p.isDeleted == 0
                    where p.Prorealtime != 0 || p.Prolinestopmin != 0
                    group p by new { p.Prodate, p.Prolinename, p.Prolot, p.Promodel, p.Prohbn } into g
                    select new
                    {
                        Prodate = g.Key.Prodate,
                        Prolinename = g.Key.Prolinename,
                        Prolot = g.Key.Prolot,
                        Promodel = g.Key.Promodel,
                        Prohbn = g.Key.Prohbn,
                        Proworktime = g.Sum(p => p.Prorealtime),
                        Prolosstime = g.Sum(p => p.Prolinestopmin),
                        Prospendtime = (Decimal)g.Sum(p => p.Prorealtime) + (Decimal)g.Sum(p => p.Prolinestopmin),
                    };

                //var qss =
                //    from p in DB.proOutputsubs
                //    group p by new
                //    {
                //        p.Prolinename,
                //        p.Prodate,
                //        p.Prodirect,
                //        p.Prolot,
                //        p.Prohbn,
                //        p.Promodel,
                //        p.Prorealqty,
                //        p.Prorealtime,
                //    }
                //    into g
                //    select new
                //    {
                //        g.Key,
                //        Qty = g.Sum(p => p.Prorealqty),
                //        Min = g.Sum(p => p.Prorealtime),
                //    };

                //IQueryable<proOutput> q = DB.proOutputs; //.Include(u => u.Dept);

                // 在用户名称中搜索
                string searchText = ttbSearchMessage.Text.Trim();

                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Promodel.ToString().Contains(searchText) || u.Prohbn.ToString().Contains(searchText) || u.Prolot.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                }

                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.Prodate.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
                }
                if (DdlLine.SelectedIndex != 0 && DdlLine.SelectedIndex != -1)
                {
                    q = q.Where(u => u.Prolinename.ToString().Contains(DdlLine.SelectedItem.Text));
                }

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
                Grid1.RecordCount = GridHelper.GetTotalCount(q);
                if (Grid1.RecordCount != 0)
                {
                    // 排列和数据库分页
                    //q = SortAndPage<proOutputsub>(q, Grid1);

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
                ttbSearchMessage.Text = "";
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

        private void BindList()
        {
            var q =
                from p in DB.Pp_P1d_OutputSubs
                    //join b in DB.Pp_P1d_Outputs on p.OPHID equals b.OPHID
                where p.isDeleted == 0
                //where p.Prorealqty != 0
                group p by new { p.Prodate, p.Prolinename, p.Prorealtime, p.Prostopcou, p.Prostopmemo, p.Probadcou, p.Probadmemo, p.Prolinemin, p.Prolinestopmin }
                into g
                select new
                {
                    Prodate = g.Key.Prodate,
                    Prolinename = g.Key.Prolinename,
                    Proworktime = g.Key.Prorealtime,
                    Prostopcou = g.Key.Prostopcou,
                    Prostopmemo = g.Key.Prostopmemo,
                    Probadcou = g.Key.Probadcou,
                    Probadmemo = g.Key.Probadmemo,
                    Prolinemin = g.Key.Prolinemin,
                    Prolinestopmin = g.Key.Prolinestopmin,
                };

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();

            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Prolinename.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
            }
            else
            {
                string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
                string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

                if (!string.IsNullOrEmpty(sdate))
                {
                    q = q.Where(u => u.Prodate.CompareTo(sdate) >= 0);
                }
                if (!string.IsNullOrEmpty(edate))
                {
                    q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
                }
                if (DdlLine.SelectedIndex != 0 && DdlLine.SelectedIndex != -1)
                {
                    q = q.Where(u => u.Prolinename.ToString().Contains(DdlLine.SelectedItem.Text));
                }
            }

            ConvertHelper.LinqConvertToDataTable(q);
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
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "printField");
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "subeditField");
            //CheckPowerWithWindowField("CoreOphEdit", Grid1, "editField");
            //CheckPowerWithLinkButtonField("CoreOphDelete", Grid1, "deleteField");
            //CheckPowerWithWindowField("CoreUserChangePassword", Grid1, "changePasswordField");
            //CheckPowerWithLinkButtonField("CoreOphEdit", Grid1, "printField");
            //CheckPowerWithLinkButtonField("CoreOphEdit", Grid1, "subeditField");
            //CheckPowerWithLinkButtonField("CoreOphEdit", Grid1, "editField");
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

        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{
        //    // 在操作之前进行权限检查
        //    if (!CheckPower("CoreOphDelete"))
        //    {
        //        CheckPowerFailWithAlert();
        //        return;
        //    }

        //    // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
        //    List<int> ids = GetSelectedDataKeyIDs(Grid1);
        //    InsNetOperateNotes();
        //    // 执行数据库操作
        //    //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
        //    //DB.SaveChanges();
        //    DB.proOutputsubs.Where(u => ids.Contains(u.Parent.ID)).Delete();
        //    DB.proOutputs.Where(u => ids.Contains(u.ID)).Delete();

        //    // 重新绑定表格
        //    BindGrid();

        //}

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            //if (e.CommandName == "PrintOph")
            //{
            //    object[] keys = Grid1.DataKeys[e.RowIndex];
            //    //labResult.Text = keys[0].ToString();
            //    PageContext.RegisterStartupScript(Window1.GetShowReference("~/onePrint/oph_report.aspx?ID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());

            //}
            //if (e.CommandName == "EditOph")
            //{
            //    object[] keys = Grid1.DataKeys[e.RowIndex];
            //    //labResult.Text = keys[0].ToString();
            //    PageContext.RegisterStartupScript(Window1.GetShowReference("~/oneProduction/oneDaliy/oph_edit.aspx?ID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());

            //}
            //if (e.CommandName == "EditOphsub")
            //{
            //    object[] keys = Grid1.DataKeys[e.RowIndex];
            //    //labResult.Text = keys[0].ToString();
            //    PageContext.RegisterStartupScript(Window1.GetShowReference("~/oneProduction/oneDaliy/oph_sub_new.aspx?OPHID=" + keys[1].ToString() + "&type=1") + Window1.GetMaximizeReference());

            //}
            //int del_ID = GetSelectedDataKeyID(Grid1);

            //if (e.CommandName == "Delete")
            //{
            //    // 在操作之前进行权限检查
            //    if (!CheckPower("CoreOphDelete"))
            //    {
            //        CheckPowerFailWithAlert();
            //        return;
            //    }

            //    //删除日志
            //    //int userID = GetSelectedDataKeyID(Grid1);
            //    proOutput current = DB.proOutputs.Find(del_ID);
            //    string Deltext = current.OPHID;
            //    string OperateType = current.ID.ToString();
            //    string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";
            //    NetCountHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH删除", OperateNotes);

            //    DB.proOutputsubs.Where(l => l.Parent.ID == del_ID).Delete();
            //    DB.proOutputs.Where(l => l.ID == del_ID).Delete();

            //}
            //BindGrid();
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

        public decimal Allcount;

        protected void DdlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = "";

            BindGrid();
        }

        public void BindDdlLine()
        {
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");
            //查询LINQ去重复
            var q = from a in DB.Pp_P1d_OutputSubs
                    where a.Prodate.CompareTo(sdate) >= 0
                    where a.Prodate.CompareTo(edate) <= 0
                    where a.isDeleted == 0
                    //join b in DB.Pp_Ecs on a.Porderhbn equals b.Ec_bomitem
                    //where b.Ec_no == strecn
                    // where a.lineclass == "M"

                    select new
                    {
                        //a.linecode,
                        a.Prolinename,
                    };

            var qs = q.Select(E => new { E.Prolinename }).ToList().Distinct();
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

        protected void DpStartDate_TextChanged(object sender, EventArgs e)
        {
            if (DpStartDate.SelectedDate.HasValue)
            {
                ttbSearchMessage.Text = "";
                BindGrid();
                BindDdlLine();
            }
        }

        protected void DpEndDate_TextChanged(object sender, EventArgs e)
        {
            if (DpEndDate.SelectedDate.HasValue)
            {
                ttbSearchMessage.Text = "";
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
                pTotal += Convert.ToDecimal(row["Proworktime"]);
                rTotal += Convert.ToDecimal(row["Prolosstime"]);
                ratio += Convert.ToDecimal(row["Prospendtime"]);
            }

            JObject summary = new JObject();
            //summary.Add("major", "全部合计");

            summary.Add("Proworktime", pTotal.ToString("F0"));
            summary.Add("Prolosstime", rTotal.ToString("F0"));
            summary.Add("Prospendtime", ratio.ToString("F0"));

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

            string updateTime;
            if (DpStartDate.SelectedDate.Value.ToString("yyyyMM") == DpEndDate.SelectedDate.Value.ToString("yyyyMM"))
            {
                updateTime = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            }
            else
            {
                updateTime = DpStartDate.SelectedDate.Value.ToString("yyyyMM") + "~" + DpEndDate.SelectedDate.Value.ToString("yyyyMM");
            }
            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName, Sheetname;

            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Xlsbomitem = updateTime + "_Workinghours";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";
            Sheetname = "W" + DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            //ExportHelper.GetGridDataTable(Exgrid);
            if (Grid1.RecordCount != 0)
            {
                //DataTable source = GetDataTable.Getdt(mysql);
                //导出2007格式
                //ExportHelper.EpplustoXLSXfile(Exdt, Xlsbomitem, ExportFileName);
                Grid1.AllowPaging = false;

                ExportHelper.EpplustoXLSXfiles(ExportHelper.GetGridDataTable(Grid1), Sheetname, ExportFileName, updateTime);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        protected void BtnLoss_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName, Sheetname;
            string updateTime;
            if (DpStartDate.SelectedDate.Value.ToString("yyyyMM") == DpEndDate.SelectedDate.Value.ToString("yyyyMM"))
            {
                updateTime = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            }
            else
            {
                updateTime = DpStartDate.SelectedDate.Value.ToString("yyyyMM") + "~" + DpEndDate.SelectedDate.Value.ToString("yyyyMM");
            }
            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Xlsbomitem = updateTime + "_Losshours";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";
            Sheetname = "L" + DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            var q =
                from p in DB.Pp_P1d_OutputSubs
                    //join b in DB.proOutputs on p.OPHID equals b.OPHID
                where p.isDeleted == 0
                where p.Prorealtime != 0 || p.Prolinestopmin != 0
                group p by new { p.Promodel, p.Prohbn, p.Prolot, p.Prodate, p.Prostime, p.Proetime, p.Prolinename, p.Prorealtime, p.Prostopcou, p.Prostopmemo, p.Probadcou, p.Probadmemo, p.Prolinemin, p.Prolinestopmin }
                into g
                select new
                {
                    Prodate = g.Key.Prodate,
                    Prostime = g.Key.Prostime,
                    Proetime = g.Key.Proetime,
                    Prolinename = g.Key.Prolinename,
                    Promodel = g.Key.Promodel,
                    Prohbn = g.Key.Prohbn,
                    Prolot = g.Key.Prolot,
                    Prorealtime = g.Key.Prorealtime,
                    Prostopcou = g.Key.Prostopcou,
                    Prostopmemo = g.Key.Prostopmemo,
                    Probadcou = g.Key.Probadcou,
                    Probadmemo = g.Key.Probadmemo,
                    Prolinemin = g.Key.Prolinemin,
                    Prolinestopmin = g.Key.Prolinestopmin,
                };

            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMMdd");

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.Prodate.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
            }
            if (DdlLine.SelectedIndex != 0 && DdlLine.SelectedIndex != -1)
            {
                q = q.Where(u => u.Prolinename.ToString().Contains(DdlLine.SelectedItem.Text));
            }

            if (q.Any())
            {
                var qs = from g in q
                         orderby g.Prodate
                         select new
                         {
                             生产日期 = g.Prodate,
                             时间段 = g.Prostime + "-" + g.Proetime,
                             班组 = g.Prolinename,
                             机种 = g.Promodel,
                             物料 = g.Prohbn,
                             批次 = g.Prolot,
                             生产工数 = g.Prorealtime,
                             停线原因 = g.Prostopcou,
                             原因说明 = g.Prostopmemo,
                             未达成 = g.Probadcou,
                             未达成原因 = g.Probadmemo,
                             损失工数 = g.Prolinestopmin,
                         };

                ConvertHelper.LinqConvertToDataTable(qs);

                Grid1.AllowPaging = false;

                ExportHelper.EpplustoXLSXfiles(ConvertHelper.LinqConvertToDataTable(qs), Sheetname, ExportFileName, updateTime);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        protected void BtnReason_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName, Sheetname;
            string updateTime;
            if (DpStartDate.SelectedDate.Value.ToString("yyyyMM") == DpEndDate.SelectedDate.Value.ToString("yyyyMM"))
            {
                updateTime = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            }
            else
            {
                updateTime = DpStartDate.SelectedDate.Value.ToString("yyyyMM") + "~" + DpEndDate.SelectedDate.Value.ToString("yyyyMM");
            }
            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Xlsbomitem = updateTime + "_Reason";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            Sheetname = "R" + DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");
            var q =
                from p in DB.Pp_P1d_OutputSubs
                    //join b in DB.proOutputs on p.OPHID equals b.OPHID
                where p.isDeleted == 0
                //where p.Prorealtime != 0 || p.Prolinestopmin != 0

                group p by new { Prodate = p.Prodate.Substring(0, 6), p.Probadcou, p.Probadmemo }
                into g
                select new
                {
                    Prodate = g.Key.Prodate,
                    Probadcou = g.Key.Probadcou,
                    Probadmemo = g.Key.Probadmemo,
                    numProstopcou = g.Count(),
                };

            var qs_c =
                (from p in q
                     //join b in DB.proOutputs on p.OPHID equals b.OPHID
                     //where p.isDeleted == 0
                 where p.Prodate.CompareTo(sdate) == 0 && !string.IsNullOrEmpty(p.Probadcou)

                 group p by new { Prodate = sdate }
                into g
                 select new
                 {
                     g.Key.Prodate,
                     Allcount = g.Sum(p => p.numProstopcou)
                 }).ToList();
            if (qs_c.Any())
            {
                Allcount = qs_c[0].Allcount;
            }

            // 在用户名称中搜索

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.Prodate.CompareTo(sdate) == 0);
            }
            //if (!string.IsNullOrEmpty(edate))
            //{
            //    q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
            //}
            q = q.Where(u => !string.IsNullOrEmpty(u.Probadcou));
            q = q.Where(u => !string.IsNullOrEmpty(u.Probadmemo));
            q = q.Where(u => u.Probadcou != "NULL");
            q = q.Where(u => u.Probadmemo != "NULL");
            if (q.Any())
            {
                var qs = from g in q

                         orderby g.numProstopcou descending
                         select new
                         {
                             日期 = g.Prodate,
                             原因 = g.Probadcou,
                             说明 = g.Probadmemo,
                             次数 = g.numProstopcou,
                             比例 = (decimal)Math.Round((g.numProstopcou / Allcount) * 100, 4) + "%",
                         };

                ConvertHelper.LinqConvertToDataTable(qs);

                Grid1.AllowPaging = false;

                ExportHelper.EpplustoXLSXfiles(ConvertHelper.LinqConvertToDataTable(qs), Sheetname, ExportFileName, updateTime);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        protected void BtnRework_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string Xlsbomitem, ExportFileName, Sheetname;
            string updateTime;
            if (DpStartDate.SelectedDate.Value.ToString("yyyyMM") == DpEndDate.SelectedDate.Value.ToString("yyyyMM"))
            {
                updateTime = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            }
            else
            {
                updateTime = DpStartDate.SelectedDate.Value.ToString("yyyyMM") + "~" + DpEndDate.SelectedDate.Value.ToString("yyyyMM");
            }
            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Xlsbomitem = updateTime + "_Rework";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";
            Sheetname = "R" + DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            var q =
                from p in DB.Pp_P1d_OutputSubs
                    //join b in DB.proOutputs on p.OPHID equals b.OPHID
                where p.isDeleted == 0
                //where p.Prorealtime != 0 || p.Prolinestopmin != 0
                where p.Prostopmemo != ""
                where p.Prostopmemo != null
                where p.Proordertype.Substring(0, 2) == "54"
                //where p.Probadcou != ""
                //where p.Probadcou != null
                group p by new { Prodate = p.Prodate.Substring(0, 6), p.Probadcou, p.Probadmemo, p.Prostopmemo }
                into g
                select new
                {
                    Prodate = g.Key.Prodate,
                    Prostopmemo = g.Key.Prostopmemo,
                    Probadcou = g.Key.Probadcou,
                    Probadmemo = g.Key.Probadmemo,
                    numProstopcou = g.Count(),
                };

            // 在用户名称中搜索

            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.Prodate.CompareTo(sdate) == 0);
            }
            //if (!string.IsNullOrEmpty(edate))
            //{
            //    q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
            //}

            if (q.Any())
            {
                var qs = from g in q

                         orderby g.numProstopcou descending
                         select new
                         {
                             日期 = g.Prodate,
                             改修 = g.Prostopmemo,
                             原因 = g.Probadcou,
                             说明 = g.Probadmemo,
                             次数 = g.numProstopcou,
                         };

                ConvertHelper.LinqConvertToDataTable(qs);

                Grid1.AllowPaging = false;

                ExportHelper.EpplustoXLSXfiles(ConvertHelper.LinqConvertToDataTable(qs), Sheetname, ExportFileName, updateTime);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }

        protected void BtnReasonSub_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreFineExport"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            string XlsTitle, ExportFileName, Sheetname;
            string updateTime;
            if (DpStartDate.SelectedDate.Value.ToString("yyyyMM") == DpEndDate.SelectedDate.Value.ToString("yyyyMM"))
            {
                updateTime = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            }
            else
            {
                updateTime = DpStartDate.SelectedDate.Value.ToString("yyyyMM") + "~" + DpEndDate.SelectedDate.Value.ToString("yyyyMM");
            }
            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[proOutputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            XlsTitle = updateTime + "_ReasonTime";
            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = XlsTitle + ".xlsx";
            Sheetname = "Rt" + DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            var q =
                from p in DB.Pp_P1d_OutputSubs
                    //join b in DB.proOutputs on p.OPHID equals b.OPHID
                where p.isDeleted == 0
                where p.Prolinestopmin > 0

                group p by new { Prodate = p.Prodate.Substring(0, 6), p.Prostopcou }
                into g
                select new
                {
                    Prodate = g.Key.Prodate,
                    Prostopcou = (g.Key.Prostopcou == "其它" ? "其他" : (string.IsNullOrEmpty(g.Key.Prostopcou) ? "其他" : g.Key.Prostopcou)),
                    Prolinestopmin = g.Sum(p => p.Prolinestopmin),
                };

            // 在用户名称中搜索

            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.Prodate.CompareTo(sdate) == 0);
            }
            //if (!string.IsNullOrEmpty(edate))
            //{
            //    q = q.Where(u => u.Prodate.CompareTo(edate) <= 0);
            //}
            //q = q.Where(u => !string.IsNullOrEmpty(u.Prostopcou));

            //q = q.Where(u => u.Prostopcou != "NULL");

            if (q.Any())
            {
                var qs = from p in q

                         orderby p.Prolinestopmin descending
                         group p by new { Prodate = p.Prodate.Substring(0, 6), p.Prostopcou }
                into g

                         select new
                         {
                             日期 = g.Key.Prodate,
                             原因 = g.Key.Prostopcou,
                             损失工数 = g.Sum(p => p.Prolinestopmin),
                         };

                ConvertHelper.LinqConvertToDataTable(qs);

                Grid1.AllowPaging = false;

                ExportHelper.EpplustoXLSXfiles(ConvertHelper.LinqConvertToDataTable(qs), Sheetname, ExportFileName, updateTime);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }
    }
}
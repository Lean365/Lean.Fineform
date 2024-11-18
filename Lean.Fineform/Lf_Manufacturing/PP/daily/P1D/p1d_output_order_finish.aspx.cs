using System;
using System.Data;
using System.Linq;
using FineUIPro;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lf_Manufacturing.PP.daily
{
    public partial class p1d_output_order_finish : PageBase
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
            //CheckPowerWithButton("CoreProophp1dEdit", btnP1dEdit);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1dNew.OnClientClick = Window1.GetShowReference("~/oneProduction/oneDaliy/oph_new.aspx", "新增") + Window1.GetMaximizeReference();
            //PageContext.RegisterStartupScript(Window1.GetMaximizeReference());
            //Window1.GetMaximizeReference();
            //btnPrint.OnClientClick = Window1.GetShowReference("~~/oneProduction/oneTimesheet/oph_report.aspx", "打印报表");
            //btnP1dEdit.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_edit.aspx?id={0}", "修改");

            DpStartDate.SelectedDate = DateTime.Now;

            // 每页记录数
            Grid1.PageSize = 1000;
            ddlGridPageSize.SelectedValue = "1000";

            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                if (rbtnFirstAuto.Checked)
                {
                    var q =
                    from p in DB.Pp_P1d_OutputSubs
                    join b in DB.Pp_P1d_Outputs on p.Parent equals b.ID
                    where p.IsDeleted == 0
                    where p.Prorealtime != 0 || p.Prolinestopmin != 0
                    group p by new { Prodate = b.Prodate.Substring(0, 6), b.Proorder, b.Proorderqty, b.Promodel, b.Prohbn, b.Prolot, b.Prost } into g
                    select new
                    {
                        Prodate = g.Key.Prodate,
                        Proorder = g.Key.Proorder,
                        Prolotqty = g.Key.Proorderqty,
                        Promodel = g.Key.Promodel,
                        Prohbn = g.Key.Prohbn,
                        Prolot = g.Key.Prolot,
                        Prost = g.Key.Prost,
                        Prorealqty = g.Sum(p => p.Prorealqty),
                    };

                    // 在用户名称中搜索
                    string searchText = ttbSearchMessage.Text.Trim();

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Prolot.ToString().Contains(searchText) || u.Prohbn.ToString().Contains(searchText) || u.Promodel.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                    }
                    if (DpStartDate.SelectedDate.HasValue)
                    {
                        string dsdate = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
                        q = q.Where(u => u.Prodate.Contains(dsdate));
                    }

                    var qcount = from p in q
                                 group p by new { p.Prolot } into g
                                 select new
                                 {
                                     Prolotqty = g.Sum(p => p.Prolotqty),
                                     Prolot = g.Key.Prolot,

                                     Prorealqty = g.Sum(p => p.Prorealqty),
                                 };
                    var qs = from a in qcount
                             where a.Prolotqty - a.Prorealqty != 0
                             select new
                             {
                                 a.Prolotqty,
                                 a.Prolot,
                                 a.Prorealqty,
                                 Prostatus = (a.Prolotqty == a.Prorealqty ? "◎已完成" : "◎未完成"),
                                 Prodiff = a.Prolotqty - a.Prorealqty,
                             };

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
                    Grid1.RecordCount = GridHelper.GetTotalCount(qs);
                    if (GridHelper.GetTotalCount(qs) > 0)
                    {
                        // 排列和数据库分页
                        DataTable table = GridHelper.GetPagedDataTable(Grid1, qs);
                        // 3.绑定到Grid
                        Grid1.DataSource = table;
                        Grid1.DataBind();
                    }
                    else
                    {
                        // 3.绑定到Grid
                        Grid1.DataSource = "";
                        Grid1.DataBind();
                    }

                    ConvertHelper.LinqConvertToDataTable(qs);
                    // 当前页的合计
                    GridSummaryData(ConvertHelper.LinqConvertToDataTable(qs));
                    ttbSearchMessage.Text = "";
                }
                if (rbtnSecondAuto.Checked)
                {
                    var q =
                            from p in DB.Pp_P1d_OutputSubs
                            join b in DB.Pp_P1d_Outputs on p.Parent equals b.ID
                            where p.IsDeleted == 0
                            where p.Prorealtime != 0 || p.Prolinestopmin != 0
                            group p by new { Prodate = b.Prodate.Substring(0, 6), b.Proorder, b.Proorderqty, b.Promodel, b.Prohbn, b.Prolot, b.Prost } into g

                            select new
                            {
                                Prodate = g.Key.Prodate,
                                Proorder = g.Key.Proorder,
                                Prolotqty = g.Key.Proorderqty,
                                Promodel = g.Key.Promodel,
                                Prohbn = g.Key.Prohbn,
                                Prolot = g.Key.Prolot,
                                Prost = g.Key.Prost,
                                Prorealqty = g.Sum(p => p.Prorealqty),
                            };

                    // 在用户名称中搜索
                    string searchText = ttbSearchMessage.Text.Trim();

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Prolot.ToString().Contains(searchText) || u.Prohbn.ToString().Contains(searchText) || u.Promodel.ToString().Contains(searchText) || u.Prodate.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                    }
                    string dsdate = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
                    q = q.Where(u => u.Prodate.Contains(dsdate));

                    var qcount = from p in q
                                 group p by new { p.Prolot } into g
                                 select new
                                 {
                                     Prolotqty = g.Sum(p => p.Prolotqty),
                                     Prolot = g.Key.Prolot,

                                     Prorealqty = g.Sum(p => p.Prorealqty),
                                 };
                    var qs = from a in qcount
                             where a.Prolotqty - a.Prorealqty == 0
                             select new
                             {
                                 a.Prolotqty,
                                 a.Prolot,
                                 a.Prorealqty,
                                 Prostatus = (a.Prolotqty == a.Prorealqty ? "◎已完成" : "◎未完成"),
                                 Prodiff = a.Prolotqty - a.Prorealqty,
                             };

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(qs);
                    if (GridHelper.GetTotalCount(qs) > 0)
                    {
                        // 排列和数据库分页
                        DataTable table = GridHelper.GetPagedDataTable(Grid1, qs);
                        // 3.绑定到Grid
                        Grid1.DataSource = table;
                        Grid1.DataBind();
                    }
                    else
                    {
                        // 3.绑定到Grid
                        Grid1.DataSource = "";
                        Grid1.DataBind();
                    }

                    ConvertHelper.LinqConvertToDataTable(qs);
                    // 当前页的合计
                    GridSummaryData(ConvertHelper.LinqConvertToDataTable(qs));
                    ttbSearchMessage.Text = "";
                }
                if (rbtnThirdAuto.Checked)
                {
                    var q =
                            from p in DB.Pp_P1d_OutputSubs
                            join b in DB.Pp_P1d_Outputs on p.Parent equals b.ID
                            where p.IsDeleted == 0
                            where p.Prorealtime != 0 || p.Prolinestopmin != 0
                            group p by new { Prodate = b.Prodate.Substring(0, 6), b.Proorder, b.Proorderqty, b.Promodel, b.Prohbn, b.Prolot, b.Prost } into g
                            select new
                            {
                                Prodate = g.Key.Prodate,
                                Proorder = g.Key.Proorder,
                                Prolotqty = g.Key.Proorderqty,
                                Promodel = g.Key.Promodel,
                                Prohbn = g.Key.Prohbn,
                                Prolot = g.Key.Prolot,
                                Prost = g.Key.Prost,
                                Prorealqty = g.Sum(p => p.Prorealqty),
                            };

                    // 在用户名称中搜索
                    string searchText = ttbSearchMessage.Text.Trim();

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Prolot.ToString().Contains(searchText) || u.Prohbn.ToString().Contains(searchText) || u.Promodel.ToString().Contains(searchText) || u.Prodate.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                    }

                    string dsdate = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
                    q = q.Where(u => u.Prodate.Contains(dsdate));
                    var qcount = from p in q
                                 group p by new { p.Prolot } into g
                                 select new
                                 {
                                     Prolotqty = g.Sum(p => p.Prolotqty),
                                     Prolot = g.Key.Prolot,

                                     Prorealqty = g.Sum(p => p.Prorealqty),
                                 };
                    var qs = from a in qcount
                             select new
                             {
                                 a.Prolotqty,
                                 a.Prolot,
                                 a.Prorealqty,
                                 Prostatus = (a.Prolotqty == a.Prorealqty ? "◎已完成" : "◎未完成"),
                                 Prodiff = a.Prolotqty - a.Prorealqty,
                             };

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(qs);
                    if (GridHelper.GetTotalCount(qs) > 0)
                    {
                        // 排列和数据库分页
                        DataTable table = GridHelper.GetPagedDataTable(Grid1, qs);
                        // 3.绑定到Grid
                        Grid1.DataSource = table;
                        Grid1.DataBind();
                    }
                    else
                    {
                        // 3.绑定到Grid
                        Grid1.DataSource = "";
                        Grid1.DataBind();
                    }

                    ConvertHelper.LinqConvertToDataTable(qs);
                    // 当前页的合计
                    GridSummaryData(ConvertHelper.LinqConvertToDataTable(qs));
                    ttbSearchMessage.Text = "";
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
        //    DB.Pp_P1d_OutputSubs.Where(u => ids.Contains(u.Parent.ID)).Delete();
        //    DB.Pp_P1d_Outputs.Where(u => ids.Contains(u.ID)).Delete();

        //    // 重新绑定表格
        //    BindGrid();

        //}
        protected void Grid1_RowDataBound(object sender, GridRowEventArgs e)
        {
            DataRowView row = e.DataItem as DataRowView;

            // 状态
            string eProstatus = row["Prostatus"].ToString();
            BoundField cProstatus = Grid1.FindColumn("Prostatus") as BoundField;
            if (eProstatus == "◎已完成")
            {
                e.CellCssClasses[cProstatus.ColumnIndex] = "color1";
            }
            else
            {
                e.CellCssClasses[cProstatus.ColumnIndex] = "color2";
            }

            // 差异
            int eProdiff = Convert.ToInt32(row["Prodiff"]);
            BoundField cProdiff = Grid1.FindColumn("Prodiff") as BoundField;
            if (eProdiff < 0)
            {
                e.CellCssClasses[cProdiff.ColumnIndex] = "color2";
            }
            if (eProdiff == 0)
            {
                e.CellCssClasses[cProdiff.ColumnIndex] = "color3";
            }
            if (eProdiff > 0)
            {
                e.CellCssClasses[cProdiff.ColumnIndex] = "color4";
            }
        }

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
            //    Pp_Output current = DB.Pp_P1d_Outputs.Find(del_ID);
            //    string Contectext = current.OPHID;
            //    string OperateType = current.ID.ToString();
            //    string OperateNotes = "Del* " + Contectext + "*Del 的记录已被删除";
            //    NetCountHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH删除", OperateNotes);

            //    DB.Pp_P1d_OutputSubs.Where(l => l.Parent.ID == del_ID).Delete();
            //    DB.Pp_P1d_Outputs.Where(l => l.ID == del_ID).Delete();

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

        protected void DpStartDate_TextChanged(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = "";
            BindGrid();
        }

        #endregion Events

        protected void DdlLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = "";

            BindGrid();
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
                //ratio = rTotal/ pTotal;
            }

            JObject summary = new JObject();
            //summary.Add("major", "全部合计");

            summary.Add("Prolotqty", pTotal.ToString("F0"));
            summary.Add("Prorealqty", rTotal.ToString("F0"));
            //summary.Add("Proratio", ratio.ToString("p0"));

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
            string Prefix_XlsxName, Export_FileName, SheetName;
            SheetName = "D" + DpStartDate.SelectedDate.Value.ToString("yyyyMMdd");
            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Prefix_XlsxName = "Lot_Status";

            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
            Export_FileName = Prefix_XlsxName + ".xlsx";

            //ExportHelper.GetGridDataTable(Exgrid);
            if (Grid1.RecordCount != 0)
            {
                //DataTable source = GetDataTable.Getdt(mysql);
                //导出2007格式
                //ExportHelper.EpplusToExcel(Exdt, Prefix_XlsxName, Export_FileName);
                Grid1.AllowPaging = false;
                ExportHelper.EpplusToExcel(ExportHelper.GetGridDataTable(Grid1), Prefix_XlsxName, Export_FileName);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }
    }
}
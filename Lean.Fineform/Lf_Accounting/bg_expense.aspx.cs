using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lf_Accounting
{
    public partial class bg_expense : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreBudgetView";
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
            BindDdlDept();
        }

        private void BindGrid()
        {
            try
            {
                if (rbtnFirstAuto.Checked)
                {
                    var q =
                    from p in DB.Fico_Expenses
                        //join b in DB.Pp_P1d_Outputs on p.Parent.ID equals b.ID
                    where p.UDF01 != "Exs."
                    where p.IsDeleted == 0
                    select new
                    {
                        p.GUID,
                        p.Bedept,
                        p.Befy,
                        p.Befm,
                        p.Betitle,
                        p.Beclass,
                        p.Betitlesub,
                        p.Beclasssub,
                        p.Beclassmemo,
                        p.UDF01,
                        p.Bebtmoney,
                        p.Beatmoney,
                        p.Bediffmoney,
                    };

                    //var qss =
                    //    from p in DB.Pp_P1d_OutputSubs
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

                    //IQueryable<Pp_Output> q = DB.Pp_P1d_Outputs; //.Include(u => u.Dept);

                    // 在用户名称中搜索
                    string searchText = ttbSearchMessage.Text.Trim();

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Bedept.ToString().Contains(searchText) || u.Beclasssub.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                    }

                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

                    if (!string.IsNullOrEmpty(sdate))
                    {
                        q = q.Where(u => u.Befm.CompareTo(sdate) >= 0);
                    }
                    if (!string.IsNullOrEmpty(edate))
                    {
                        q = q.Where(u => u.Befm.CompareTo(edate) <= 0);
                    }
                    if (DDLDept.SelectedIndex != 0 && DDLDept.SelectedIndex != -1)
                    {
                        q = q.Where(u => u.Bedept.ToString().Contains(DDLDept.SelectedItem.Text));
                    }                // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(q);
                    if (Grid1.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

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
                }
                if (rbtnSecondAuto.Checked)
                {
                    var q = from p in DB.Fico_Expenses
                                //join b in DB.Pp_P1d_Outputs on p.Parent.ID equals b.ID
                            where p.IsDeleted == 0
                            where p.UDF01 == "Exs."
                            //where p.Bsdept != "DTA"
                            select new
                            {
                                p.GUID,
                                p.Bedept,
                                p.Befy,
                                p.Befm,
                                p.Betitle,
                                p.Beclass,
                                p.Betitlesub,
                                p.Beclasssub,
                                p.Beclassmemo,
                                p.UDF01,
                                p.Bebtmoney,
                                p.Beatmoney,
                                p.Bediffmoney,
                            };

                    //var qss =
                    //    from p in DB.Pp_P1d_OutputSubs
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

                    //IQueryable<Pp_Output> q = DB.Pp_P1d_Outputs; //.Include(u => u.Dept);

                    // 在用户名称中搜索
                    string searchText = ttbSearchMessage.Text.Trim();

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Bedept.ToString().Contains(searchText) || u.Beclasssub.ToString().Contains(searchText)); //|| u.CreateDate.Contains(searchText));
                    }

                    string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
                    string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

                    if (!string.IsNullOrEmpty(sdate))
                    {
                        q = q.Where(u => u.Befm.CompareTo(sdate) >= 0);
                    }
                    if (!string.IsNullOrEmpty(edate))
                    {
                        q = q.Where(u => u.Befm.CompareTo(edate) <= 0);
                    }
                    if (DDLDept.SelectedIndex != 0 && DDLDept.SelectedIndex != -1)
                    {
                        q = q.Where(u => u.Bedept.ToString().Contains(DDLDept.SelectedItem.Text));
                    }
                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(q);
                    if (Grid1.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

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
        //    DB.Pp_P1d_OutputSubs.Where(u => ids.Contains(u.Parent.ID)).Delete();
        //    DB.Pp_P1d_Outputs.Where(u => ids.Contains(u.ID)).Delete();

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
        }

        #endregion Events

        protected void DDLDept_SelectedIndexChanged(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = "";

            BindGrid();
        }

        public void BindDdlDept()
        {
            //查询LINQ去重复
            var q = from a in DB.Fico_Expenses
                        //join b in DB.Pp_Ecs on a.Porderhbn equals b.Ec_bomitem
                        //where b.Ec_no == strecn
                        //where a.lineclass == "M"
                    select new
                    {
                        //a.lineclass,
                        a.Bedept
                    };

            var qs = q.Select(E => new { E.Bedept }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            DDLDept.DataSource = qs;
            DDLDept.DataTextField = "Bedept";
            DDLDept.DataValueField = "Bedept";
            DDLDept.DataBind();
            this.DDLDept.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }

        protected void DpStartDate_TextChanged(object sender, EventArgs e)
        {
            if (DpStartDate.SelectedDate.HasValue)
            {
                ttbSearchMessage.Text = "";
                BindGrid();
            }
        }

        protected void DpEndDate_TextChanged(object sender, EventArgs e)
        {
            if (DpEndDate.SelectedDate.HasValue)
            {
                ttbSearchMessage.Text = "";
                BindGrid();
            }
        }

        //合计表格
        private void GridSummaryData(DataTable source)
        {
            Decimal pTotal = 0.0m;
            Decimal rTotal = 0.0m;
            Decimal ratio = 0.0m;
            Decimal Ytoal = 0.0m;
            foreach (DataRow row in source.Rows)
            {
                Ytoal += Convert.ToDecimal(row["Bebtmoney"]);
                pTotal += Convert.ToDecimal(row["Beatmoney"]);
                rTotal += Convert.ToDecimal(row["Bediffmoney"]);
                ratio = rTotal / pTotal;
            }

            JObject summary = new JObject();
            //summary.Add("major", "全部合计");
            summary.Add("Bebtmoney", Ytoal.ToString("F2"));
            summary.Add("Beatmoney", pTotal.ToString("F2"));
            summary.Add("Bediffmoney", rTotal.ToString("F2") + "(差异%：" + ratio.ToString("p2") + ")");
            //summary.Add("Bediffmoney", ratio.ToString("p0"));

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
            string Prefix_XlsxName, Export_FileName;

            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            Prefix_XlsxName = "Budget_Expense";

            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Prefix_XlsxName + "'";
            Export_FileName = Prefix_XlsxName + ".xlsx";

            //ExportHelper.GetGridDataTable(Exgrid);
            if (Grid1.RecordCount != 0)
            {
                //DataTable source = GetDataTable.Getdt(mysql);
                //导出2007格式
                //ExportHelper.EpplusToExcel(Exdt, Prefix_XlsxName, Export_FileName);
                Grid1.AllowPaging = false;
                ExportHelper.EpplusToExcel(ExportHelper.GetGridDataTable(Grid1), Prefix_XlsxName, Export_FileName, "DTA费用明细");
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }
    }
}
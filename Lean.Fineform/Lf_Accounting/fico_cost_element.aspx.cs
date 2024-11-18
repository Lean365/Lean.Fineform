using System;
using System.Data;
using System.Linq;
using System.Web.UI.WebControls;
using FineUIPro;
using Newtonsoft.Json.Linq;

namespace LeanFine.Lf_Accounting
{
    public partial class Fico_cost_element : PageBase
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

        public string Prefix_XlsxName, Export_FileName,SheetName;

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

            //本月最后一天
            //DpEndDate.SelectedDate = DateTime.Now.AddMonths(-1);//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

            // 每页记录数
            Grid1.PageSize = 5000;
            ddlGridPageSize.SelectedValue = "5000";

            BindGrid();
        }

        private void BindGrid()
        {
            try
            {
                string searchText = ttbSearchMessage.Text.Trim();
                if (rbtnFirstAuto.Checked)
                {
                    //IQueryable<Fico_Title> q = DB.Fico_Titles; //.Include(u => u.Dept);

                    var q = from a in DB.Fico_Titles
                            where a.Bacnname.CompareTo("未启用") != 0
                            where a.UDF01.Contains("2300")
                            select a;

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Bacode.ToString().Contains(searchText) || u.Bacnname.Contains(searchText));
                    }
                    q = q.OrderBy(u => u.Bacode);
                    var qs = q.Select(E => new
                    {
                        Bc_CorpCode = 2300,
                        Bc_CorpName = "DTA",
                        Bc_CostCode = "",
                        Bc_CostName = "",
                        Bc_TitleCode = E.Bacode.ToString(),
                        Bc_TitleName = E.Bacnname
                    }).ToList().Distinct();
                    // 当前页的合计
                    //GridSummaryData(ConvertHelper.LinqConvertToDataTable(q));

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(qs.AsQueryable());
                    if (Grid1.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

                        // 2.获取当前分页数据
                        DataTable table = GridHelper.GetPagedDataTable(Grid1, qs.AsQueryable());

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
                    //IQueryable<Fico_Title> q = DB.Fico_Titles; //.Include(u => u.Dept);

                    var q = from a in DB.Fico_ProfitCenters

                            select a;

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.PC_Code.ToString().Contains(searchText) || u.PC_Name.Contains(searchText));
                    }
                    q = q.OrderBy(u => u.PC_Code);
                    var qs = q.Select(E => new
                    {
                        Bc_CorpCode = 2300,
                        Bc_CorpName = "DTA",
                        Bc_CostCode = E.PC_Code,
                        Bc_CostName = E.PC_Name,
                        Bc_TitleCode = "",
                        Bc_TitleName = "",
                    }).ToList().Distinct();
                    // 当前页的合计
                    //GridSummaryData(ConvertHelper.LinqConvertToDataTable(q));

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(qs.AsQueryable());
                    if (Grid1.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

                        // 2.获取当前分页数据
                        DataTable table = GridHelper.GetPagedDataTable(Grid1, qs.AsQueryable());

                        Grid1.DataSource = table;
                        Grid1.DataBind();
                    }
                    else
                    {
                        Grid1.DataSource = "";
                        Grid1.DataBind();
                    }
                }
                if (rbtnThirdAuto.Checked)
                {
                    //IQueryable<Fico_Title> q = DB.Fico_Titles; //.Include(u => u.Dept);

                    var q = from a in DB.Fico_Monthly_Actual_Costs
                            where a.Bc_ExpCategory.Contains("Exs.")
                            select a;

                    // 在用户名称中搜索

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Bc_TitleCode.ToString().Contains(searchText) || u.Bc_TitleName.Contains(searchText) || u.Bc_CostName.Contains(searchText) || u.Bc_CostCode.ToString().Contains(searchText));
                    }
                    var qs = q.Select(E => new
                    {
                        Bc_CorpCode = 2300,
                        Bc_CorpName = "DTA",
                        Bc_CostCode = E.Bc_CostCode.ToString(),
                        E.Bc_CostName,
                        Bc_TitleCode = E.Bc_TitleCode.ToString(),
                        E.Bc_TitleName
                    }).ToList().Distinct();

                    var qss = from a in qs
                              orderby a.Bc_CostCode
                              select new
                              {
                                  a.Bc_CorpCode,
                                  a.Bc_CorpName,
                                  Bc_CostCode = (a.Bc_CostCode.CompareTo("1000") == 0 ? "D1000" : "D0" + a.Bc_CostCode),
                                  a.Bc_CostName,
                                  a.Bc_TitleCode,
                                  a.Bc_TitleName,
                              };

                    // 当前页的合计
                    //GridSummaryData(ConvertHelper.LinqConvertToDataTable(q));

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = GridHelper.GetTotalCount(qss.AsQueryable());
                    if (Grid1.RecordCount != 0)
                    {
                        // 排列和数据库分页
                        //q = SortAndPage<Pp_P1d_Outputsub>(q, Grid1);

                        // 1.设置总项数（特别注意：数据库分页一定要设置总记录数RecordCount）
                        //Grid1.RecordCount = GetTotalCount();

                        // 2.获取当前分页数据
                        DataTable table = GridHelper.GetPagedDataTable(Grid1, qss.AsQueryable());

                        Grid1.DataSource = table;
                        Grid1.DataBind();
                    }
                    else
                    {
                        Grid1.DataSource = "";
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
            else if (rbtnThirdAuto.Checked)
            {
                BindGrid();
            }
        }

        #endregion Events

        //合计表格
        private void GridSummaryData(DataTable source)
        {
            Decimal pTotal = 0.0m;
            Decimal rTotal = 0.0m;
            Decimal ratio = 0.0m;
            Decimal Ytoal = 0.0m;
            foreach (DataRow row in source.Rows)
            {
                Ytoal += Convert.ToDecimal(row["BC_BudgetAmt"]);
                pTotal += Convert.ToDecimal(row["Bc_ActualAmt"]);
                rTotal += Convert.ToDecimal(row["Bc_DiffAmt"]);
                ratio = rTotal / Ytoal;
            }

            JObject summary = new JObject();
            summary.Add("Bc_TitleName", "(CNY)合计");
            summary.Add("BC_BudgetAmt", Ytoal.ToString("F2"));
            summary.Add("Bc_ActualAmt", pTotal.ToString("F2"));
            summary.Add("Bc_DiffAmt", "(差异%：" + ratio.ToString("p2") + ")");
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

            if (rbtnFirstAuto.Checked)
            {
                Prefix_XlsxName = global::Resources.GlobalResource.menu_Fico_Titles;
            }
            if (rbtnSecondAuto.Checked)
            {
                Prefix_XlsxName = global::Resources.GlobalResource.rpt_Charts_Cost_Accountingtitle;
            }
            if (rbtnThirdAuto.Checked)
            {
                Prefix_XlsxName = global::Resources.GlobalResource.rpt_Charts_Cost_element;
            }

            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";

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
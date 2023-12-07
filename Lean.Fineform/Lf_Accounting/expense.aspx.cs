using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;

using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Text;
using System.ComponentModel;
using System.Reflection;
using System.Threading.Tasks;

namespace Lean.Fineform.Lf_Accounting
{
    public partial class expense : PageBase
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

        #endregion

        #region Page_Load
        public string Xlsbomitem, ExportFileName;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadData();
            }
        }

        private void LoadData()
        {
            //BindDDLData();
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreOphDelete", btnDeleteSelected);
            //CheckPowerWithButton("CoreOphNew", btnP1dNew);
            //CheckPowerWithButton("CoreProophp1dNew", btnPrint);
            ////CheckPowerWithButton("CoreProophp1dEdit", btnP1dEdit);
            CheckPowerWithButton("CoreKitOutput", BtnExport);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1dNew.OnClientClick = Window1.GetShowReference("~/oneProduction/oneDaliy/oph_new.aspx", "新增") + Window1.GetMaximizeReference();
            //PageContext.RegisterStartupScript(Window1.GetMaximizeReference());
            //Window1.GetMaximizeReference();
            //btnPrint.OnClientClick = Window1.GetShowReference("~~/oneProduction/oneTimesheet/oph_report.aspx", "打印报表");
            //btnP1dEdit.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_edit.aspx?id={0}", "修改");



            //本月最后一天
            DPend.SelectedDate = DateTime.Now.AddMonths(-1);//.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);

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
                    IQueryable<Fico_Costing_ActualCost> q = DB.Fico_Costing_ActualCosts; //.Include(u => u.Dept);

                    // 在用户名称中搜索
                    string edate = DPend.SelectedDate.Value.ToString("yyyyMM");
                    q = q.Where(u => u.Bc_YM.CompareTo(edate) == 0);
                    q = q.Where(u => u.Bc_TitleNote.Contains("财务费用"));
                    q = q.Where(u => u.Bc_CostCode != 9999);

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Bc_TitleNote.Contains(searchText) || u.Bc_TitleName.Contains(searchText) || u.Bc_CostName.Contains(searchText));
                    }
                    // 当前页的合计
                    OutputSummaryData(ConvertHelper.LinqConvertToDataTable(q));

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = q.Count();

                    // 排列和数据库分页
                    q = SortAndPage<Fico_Costing_ActualCost>(q, Grid1);

                    Grid1.DataSource = q;
                    Grid1.DataBind();
                }
                if (rbtnSecondAuto.Checked)
                {
                    IQueryable<Fico_Costing_ActualCost> q = DB.Fico_Costing_ActualCosts; //.Include(u => u.Dept);

                    // 在用户名称中搜索
                    string edate = DPend.SelectedDate.Value.ToString("yyyyMM");
                    q = q.Where(u => u.Bc_YM.CompareTo(edate) == 0);
                    q = q.Where(u => u.Bc_TitleNote.Contains("管理费用"));
                    q = q.Where(u => u.Bc_CostCode != 8888);

                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Bc_TitleNote.Contains(searchText) || u.Bc_TitleName.Contains(searchText) || u.Bc_CostName.Contains(searchText));
                    }
                    // 当前页的合计
                    OutputSummaryData(ConvertHelper.LinqConvertToDataTable(q));
                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = q.Count();

                    // 排列和数据库分页
                    q = SortAndPage<Fico_Costing_ActualCost>(q, Grid1);

                    Grid1.DataSource = q;
                    Grid1.DataBind();
                }
                if (rbtnThirdAuto.Checked)
                {
                    //Lean.Fineform.Lf_Business.Models.YF.LeanSerialEntities DBSerial = new Lean.Fineform.Lf_Business.Models.YF.LeanSerialEntities();

                    IQueryable<Fico_Costing_ActualCost> q = DB.Fico_Costing_ActualCosts; //.Include(u => u.Dept);

                    // 在用户名称中搜索
                    string edate = DPend.SelectedDate.Value.ToString("yyyyMM");
                    q = q.Where(u => u.Bc_YM.CompareTo(edate) == 0);
                    q = q.Where(u => u.Bc_TitleNote.Contains("制造费用"));
                    q = q.Where(u => u.Bc_CostCode != 5555);
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Bc_TitleNote.Contains(searchText) || u.Bc_TitleName.Contains(searchText) || u.Bc_CostName.Contains(searchText));
                    }
                    // 当前页的合计
                    OutputSummaryData(ConvertHelper.LinqConvertToDataTable(q));

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = q.Count();

                    // 排列和数据库分页
                    q = SortAndPage<Fico_Costing_ActualCost>(q, Grid1);

                    Grid1.DataSource = q;
                    Grid1.DataBind();
                }
                if (rbtnFourthAuto.Checked)
                {
                    //Lean.Fineform.Lf_Business.Models.YF.LeanSerialEntities DBSerial = new Lean.Fineform.Lf_Business.Models.YF.LeanSerialEntities();

                    IQueryable<Fico_Costing_ActualCost> q = DB.Fico_Costing_ActualCosts; //.Include(u => u.Dept);

                    // 在用户名称中搜索
                    string edate = DPend.SelectedDate.Value.ToString("yyyyMM");
                    q = q.Where(u => u.Bc_YM.CompareTo(edate) == 0);
                    q = q.Where(u => u.Bc_TitleNote.Contains("营业费用"));
                    q = q.Where(u => u.Bc_CostCode != 6666);
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Bc_TitleNote.Contains(searchText) || u.Bc_TitleName.Contains(searchText) || u.Bc_CostName.Contains(searchText));
                    }
                    // 当前页的合计
                    OutputSummaryData(ConvertHelper.LinqConvertToDataTable(q));
                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = q.Count();

                    // 排列和数据库分页
                    q = SortAndPage<Fico_Costing_ActualCost>(q, Grid1);

                    Grid1.DataSource = q;
                    Grid1.DataBind(); 
                }
                if (rbtnFifthAuto.Checked)
                {
                    //Lean.Fineform.Lf_Business.Models.YF.LeanSerialEntities DBSerial = new Lean.Fineform.Lf_Business.Models.YF.LeanSerialEntities();

                    IQueryable<Fico_Costing_ActualCost> q = DB.Fico_Costing_ActualCosts; //.Include(u => u.Dept);

                    // 在用户名称中搜索
                    string edate = DPend.SelectedDate.Value.ToString("yyyyMM");
                    q = q.Where(u => u.Bc_YM.CompareTo(edate) == 0);
                    q = q.Where(u => u.Bc_TitleNote.Contains("生产成本"));
                    q = q.Where(u => u.Bc_CostCode != 7777);
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Bc_TitleNote.Contains(searchText) || u.Bc_TitleName.Contains(searchText) || u.Bc_CostName.Contains(searchText));
                    }
                    // 当前页的合计
                    OutputSummaryData(ConvertHelper.LinqConvertToDataTable(q));

                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = q.Count();

                    // 排列和数据库分页
                    q = SortAndPage<Fico_Costing_ActualCost>(q, Grid1);

                    Grid1.DataSource = q;
                    Grid1.DataBind();
                }
                if (rbtnSixthAuto.Checked)
                {
                    //Lean.Fineform.Lf_Business.Models.YF.LeanSerialEntities DBSerial = new Lean.Fineform.Lf_Business.Models.YF.LeanSerialEntities();

                    IQueryable<Fico_Costing_ActualCost> q = DB.Fico_Costing_ActualCosts; //.Include(u => u.Dept);

                    // 在用户名称中搜索
                    string edate = DPend.SelectedDate.Value.ToString("yyyyMM");
                    q = q.Where(u => u.Bc_YM.CompareTo(edate) == 0);
                    q = q.Where(u => u.Bc_ExpCategory.CompareTo("DTA")!=0);
                    if (!String.IsNullOrEmpty(searchText))
                    {
                        q = q.Where(u => u.Bc_TitleNote.Contains(searchText) || u.Bc_TitleName.Contains(searchText) || u.Bc_CostName.Contains(searchText));
                    }
                    // 当前页的合计
                    OutputSummaryData(ConvertHelper.LinqConvertToDataTable(q));
                    // 在查询添加之后，排序和分页之前获取总记录数
                    Grid1.RecordCount = q.Count();

                    // 排列和数据库分页
                    q = SortAndPage<Fico_Costing_ActualCost>(q, Grid1);

                    Grid1.DataSource = q;
                    Grid1.DataBind();
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
        //    DB.Pp_P1d_Outputsubs.Where(u => ids.Contains(u.Parent.ID)).Delete();
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
            //    Pp_P1d_Output current = DB.Pp_P1d_Outputs.Find(del_ID);
            //    string Contectext = current.OPHID;
            //    string OperateType = current.ID.ToString();
            //    string OperateNotes = "Del* " + Contectext + "*Del 的记录已被删除";
            //    NetCountHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "生产管理", "OPH删除", OperateNotes);

            //    DB.Pp_P1d_Outputsubs.Where(l => l.Parent.ID == del_ID).Delete();
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
            else if (rbtnFourthAuto.Checked)
            {
                BindGrid();
            }
            else if (rbtnFifthAuto.Checked)
            {
                BindGrid();
            }
            else if (rbtnSixthAuto.Checked)
            {
                BindGrid();
            }
        }


        #endregion



        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
            {
                ttbSearchMessage.Text = "";
                BindGrid();
            }
        }
        //合计表格
        private void OutputSummaryData(DataTable source)
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
                if (Ytoal == 0)
                {
                    ratio = 0;
                }
                else
                {
                    ratio = rTotal / Ytoal;
                }
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
            if (!CheckPower("CoreKitOutput"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            //DataTable Exp = new DataTable();
            //在库明细查询SQL
            

            if (rbtnFirstAuto.Checked)
            {
                Xlsbomitem = global::Resources.GlobalResource.sys_Tab_Fico_Expense_Finance + "Exs_" + DPend.SelectedDate.Value.ToString("yyyyMM");
            }
             if (rbtnSecondAuto.Checked)
            {
                Xlsbomitem = global::Resources.GlobalResource.sys_Tab_Fico_Expense_General + "_" + DPend.SelectedDate.Value.ToString("yyyyMM");
            }
             if (rbtnThirdAuto.Checked)
            {
                Xlsbomitem = global::Resources.GlobalResource.sys_Tab_Fico_Expense_Manufacturing + "_" + DPend.SelectedDate.Value.ToString("yyyyMM");
            }
             if (rbtnFourthAuto.Checked)
            {
                Xlsbomitem = global::Resources.GlobalResource.sys_Tab_Fico_Expense_Operation + "_" + DPend.SelectedDate.Value.ToString("yyyyMM");
            }
             if (rbtnFifthAuto.Checked)
            {
                Xlsbomitem = global::Resources.GlobalResource.sys_Tab_Fico_Expense_ProdCost + "_" + DPend.SelectedDate.Value.ToString("yyyyMM");
            }
             if (rbtnSixthAuto.Checked)
            {
                Xlsbomitem =  "费用明细对比_" + DPend.SelectedDate.Value.ToString("yyyyMM");
            }
            // mysql = "SELECT [Prodate] 日付,[Prohbn] 品目,[Prost] ST,[Proplanqty] 計画台数,[Proworktime] 投入工数,[Proworkqty] 実績台数,[Prodirect] 直接人数,[Proworkst] 実績ST,[Prodiffst] ST差異,[Prodiffqty] 台数差異,[Proactivratio] 稼働率  FROM [dbo].[Pp_P1d_Outputlinedatas] where left(Prodate,6)='" + DDLdate.SelectedText + "'";
            

            //mysql = "EXEC DTA.dbo.SP_BOM_EXPAND '" + Xlsbomitem + "'";
            ExportFileName = Xlsbomitem + ".xlsx";

            //ExportHelper.GetGridDataTable(Exgrid);
            if (Grid1.RecordCount != 0)
            {
                //DataTable source = GetDataTable.Getdt(mysql);
                //导出2007格式
                //ExportHelper.EpplustoXLSXfile(Exdt, Xlsbomitem, ExportFileName);
                Grid1.AllowPaging = false;
                ExportHelper.EpplustoXLSXfile(ExportHelper.GetGridDataTable(Grid1), Xlsbomitem, ExportFileName);
                Grid1.AllowPaging = true;
            }
            else

            {
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Msg_Nodata, global::Resources.GlobalResource.sys_Alert_Title_Warning, MessageBoxIcon.Warning);
            }
        }
    }
}

using System;
using System.Data;
using System.Linq;
using FineUIPro;
using LeanFine.Lf_Business.Models.QM;

namespace LeanFine.Lf_Manufacturing.QM.cost
{
    public partial class rework_cost : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreReworkCostView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public string mysql, SearchDate;

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
            // CheckPowerWithButton("CoreQcCostDelete", btnDeleteSelected);
            CheckPowerWithButton("CoreReworkCostNew", btnNew1);
            CheckPowerWithButton("CoreOperationCostNew", btnNew2);
            CheckPowerWithButton("CoreWasteCostNew", btnNew2);
            //CheckPowerWithButton("CoreQcCostPrint", btnPreview1);
            //CheckPowerWithButton("CoreQcCostPrint", btnPreview2);
            //CheckPowerWithButton("CoreProophp2dNew", btnP1dNew);
            //CheckPowerWithButton("CoreQcCostExport", BtnExport);
            //CheckPowerWithButton("CoreQcCostExport", Btn2003);
            //本月第一天
            DpStartDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DpEndDate.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);
            btnNew1.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/QM/cost/rework_cost_new.aspx", "新增") + Window1.GetMaximizeReference();
            btnNew2.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/QM/cost/operation_cost_new.aspx", "新增") + Window1.GetMaximizeReference();
            btnNew3.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/QM/cost/waste_cost_new.aspx", "新增") + Window1.GetMaximizeReference();
            //btnPreview2.OnClientClick = Window1.GetShowReference("~/oneProduction/onePrint/Qc_Waste_report.aspx", "物料事故报告");
            //btnPreview1.OnClientClick = Window1.GetShowReference("~/oneProduction/onePrint/Qc_Cost_report.aspx", "品质成本报告");
            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1dNew.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_new.aspx", "新增P1D生产日报");
            //btnP2dNew.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p2d_new.aspx", "新增P2D生产日报");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }

        private void BindGrid()
        {
            IQueryable<Qm_Rework> q = DB.Qm_Reworks; //.Include(u => u.Dept);

            // 在用户名称中搜索
            //string searchText = ttbSearchMessage.Text.Trim();
            string sdate = DpStartDate.SelectedDate.Value.ToString("yyyyMM");
            string edate = DpEndDate.SelectedDate.Value.ToString("yyyyMM");

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.Qcrd001.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.Qcrd001.CompareTo(edate) <= 0);
            }
            //q = q.Where(u => u.LA002 > 0);

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
            Grid1.RecordCount = q.Count();

            // 排列和数据库分页
            q = SortAndPage<Qm_Rework>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        #endregion Page_Load

        #region Events

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            CheckPowerWithLinkButtonField("CoreReworkCostEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreReworkCostDelete", Grid1, "deleteField");
            //CheckPowerWithLinkButtonField("CoreFinePrint", Grid1, "printField");

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

        //protected void btnDeleteSelected_Click(object sender, EventArgs e)
        //{
        //    // 在操作之前进行权限检查
        //    if (!CheckPower("CoreQcCostDelete"))
        //    {
        //        CheckPowerFailWithAlert();
        //        return;
        //    }

        //    // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
        //    List<int> ids = GetSelectedDataKeyIDs(Grid1);

        //    // 执行数据库操作
        //    //DB.Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Users.Remove(u));
        //    //DB.SaveChanges();
        //    DB.pqQachecks.Where(u => ids.Contains(u.ID)).Delete();

        //    // 重新绑定表格
        //    BindGrid();
        //    NetLogRecord();
        //}

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            string tracestr;
            object[] keys = Grid1.DataKeys[e.RowIndex];

            if (keys[0] == null)
            {
                tracestr = ",";
            }
            else
            {
                tracestr = keys[0].ToString() + ",";
            }

            if (keys[1] == null)
            {
                tracestr = tracestr + ",";
            }
            else
            {
                tracestr = tracestr + keys[1].ToString() + ",";
            }

            //if (e.CommandName == "Print")
            //{
            //    //labResult.Text = keys[0].ToString();
            //    PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/QM/cost/rework_cost_edit.aspx?GUID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());

            //}
            if (e.CommandName == "Edit")
            {
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/QM/cost/rework_cost_edit.aspx?GUID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());
            }
            Guid del_ID = Guid.Parse(GetSelectedDataKeyGUID(Grid1));

            if (e.CommandName == "Delete")
                // 在操作之前进行权限检查
                if (!CheckPower("CoreReworkCostDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

            //删除日志
            //int userID = GetSelectedDataKeyID(Grid1);
            Qm_Rework current = DB.Qm_Reworks.Find(del_ID);
            string Deltext = current.Qcrd001 + "," + current.Qcrd002 + "," + current.Qcrd003 + "," + current.Qcrd004 + "," + current.Qcrd005 + "," + current.Qcrd006;
            string OperateType = "删除";
            string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";
            OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "改修数据删除", OperateNotes);

            current.IsDeleted = 0;
            current.Modifier = GetIdentityName();
            current.ModifyDate = DateTime.Now;
            DB.SaveChanges();

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
            }
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

        protected void DDLdate_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindGrid();
        }

        #endregion Events
    }
}
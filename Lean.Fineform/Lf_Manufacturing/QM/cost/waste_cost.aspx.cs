using FineUIPro;
using LeanFine.Lf_Business.Models.QM;
using System;
using System.Data;
using System.Linq;

namespace LeanFine.Lf_Manufacturing.QM.cost
{
    public partial class waste_cost : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreWasteCostView";
            }
        }

        #endregion ViewPower

        #region Page_Load

        public string SearchDate, DownSql;

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
            CheckPowerWithButton("CoreReworkCostNew", btnNew2);
            //CheckPowerWithButton("CoreQcCostPrint", btnPreview1);
            //CheckPowerWithButton("CoreQcCostPrint", btnPreview2);
            //CheckPowerWithButton("CoreProophp2dNew", btnP1dNew);
            //CheckPowerWithButton("CoreQcCostExport", BtnExport);
            //CheckPowerWithButton("CoreQcCostExport", Btn2003);
            //本月第一天
            DPstart.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date;
            //本月最后一天
            DPend.SelectedDate = DateTime.Now.AddDays(1 - DateTime.Now.Day).Date.AddMonths(1).AddSeconds(-1);
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

            //DDL和Grid处理顺序，1.DDL填充 2.Grid填充

            BindGrid();
        }

        private void BindGrid()
        {
            IQueryable<Qm_Waste> q = DB.Qm_Wastes; //.Include(u => u.Dept);

            // 在用户名称中搜索
            //string searchText = DDLdate.Text.Trim();
            string sdate = DPstart.SelectedDate.Value.ToString("yyyyMM");
            string edate = DPend.SelectedDate.Value.ToString("yyyyMM");

            if (!string.IsNullOrEmpty(sdate))
            {
                q = q.Where(u => u.Qcwd001.CompareTo(sdate) >= 0);
            }
            if (!string.IsNullOrEmpty(edate))
            {
                q = q.Where(u => u.Qcwd001.CompareTo(edate) <= 0);
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
            q = SortAndPage<Qm_Waste>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        #endregion Page_Load

        #region Events

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            CheckPowerWithLinkButtonField("CoreWasteCostEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreWasteCostDelete", Grid1, "deleteField");
            CheckPowerWithLinkButtonField("CoreKitPrint", Grid1, "printField");
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

            if (e.CommandName == "Print")
            {
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/QM/cost/waste_cost_edit.aspx?GUID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());
            }
            if (e.CommandName == "Edit")
            {
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/QM/cost/waste_cost_edit.aspx?GUID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());
            }
            Guid del_ID = Guid.Parse(GetSelectedDataKeyGUID(Grid1));

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreWasteCostDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Qm_Waste current = DB.Qm_Wastes.Find(del_ID);
                string Deltext = current.Qcwd001 + "," + current.Qcwd002 + "," + current.Qcwd003 + "," + current.Qcwd004 + "," + current.Qcwd005 + "," + current.Qcwd006;
                string OperateType = "删除";
                string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "质量管理", "产品检验删除", OperateNotes);

                current.isDeleted = 0;
                current.Modifier = GetIdentityName();
                current.ModifyDate = DateTime.Now;
                DB.SaveChanges();

                BindGrid();
            }
        }

        protected void DPstart_TextChanged(object sender, EventArgs e)
        {
            if (DPstart.SelectedDate.HasValue)
            {
                BindGrid();
            }
        }

        protected void DPend_TextChanged(object sender, EventArgs e)
        {
            if (DPend.SelectedDate.HasValue)
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
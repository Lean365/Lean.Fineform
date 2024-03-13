using FineUIPro;
using LeanFine.Lf_Business.Models.QM;
using System;

//using EntityFramework.Extensions;
using System.Data;
using System.Linq;

namespace LeanFine.Lf_Manufacturing.QM.cost
{
    public partial class wagerate : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreWagesView";
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
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            // CheckPowerWithButton("CoreProdataDelete", btnDeleteSelected);
            CheckPowerWithButton("CoreWagesNew", btnNew);
            //CheckPowerWithButton("CoreProophp2dNew", btnP1dNew);
            //CheckPowerWithButton("CoreWorkExport", Btn2007);
            //CheckPowerWithButton("CoreWorkExport", Btn2003);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);
            btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Manufacturing/QM/cost/wagerate_new.aspx", "新增");
            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnP1dNew.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p1d_new.aspx", "新增P1D生产日报");
            //btnP2dNew.OnClientClick = Window1.GetShowReference("~/cgwProinfo/prooph_p2d_new.aspx", "新增P2D生产日报");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }

        #region BindData

        private void BindGrid()
        {
            IQueryable<Qm_Wagerate> q = DB.Qm_Wagerates; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Qcsd001.ToString().Contains(searchText));
            }
            q = q.Where(u => u.isDeleted == 0);
            //else
            //{
            //    //当前日期
            //    string dd = DateTime.Now.ToString("yyyyMM");
            //    q = q.Where(u => u.Qcsd001.ToString().Contains(dd));
            //}
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
            q = SortAndPage<Qm_Wagerate>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
            if (q.Count() > 0)
            {
                //Btn2007.Visible = true;
                //Btn2003.Visible = true;
            }
            else
            {
                //Btn2007.Visible = false;
                //Btn2003.Visible = false;
            }
        }

        #endregion BindData

        #endregion Page_Load

        #region Events

        protected void ttbSearchMessage_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearchMessage.ShowTrigger1 = true;
            BindGrid();
        }

        protected void ttbSearchMessage_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchMessage.Text = String.Empty;
            ttbSearchMessage.ShowTrigger1 = false;
            BindGrid();
        }

        protected void Grid1_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            CheckPowerWithLinkButtonField("CoreWagesEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreWagesDelete", Grid1, "deleteField");
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
        //    if (!CheckPower("CoreProdataDelete"))
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
            if (e.CommandName == "Edit")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Manufacturing/QM/cost/wagerate_edit.aspx?GUID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());
            }
            Guid del_ID = Guid.Parse(GetSelectedDataKeyGUID(Grid1));

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreWagesDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                //删除日志

                Qm_Wagerate current = DB.Qm_Wagerates.Find(del_ID);
                string Deltext = current.Qcsd001 + "," + current.Qcsd002 + "," + current.Qcsd003 + "," + current.Qcsd004 + "," + current.Qcsd005 + "," + current.Qcsd006;
                string OperateType = "删除";
                string OperateNotes = "Del* " + Deltext + "*Del 的记录已被删除";

                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "品质业务", "工资率数据删除", OperateNotes);

                current.isDeleted = 1;
                //current.Endtag = 1;
                current.Modifier = GetIdentityName();
                current.ModifyDate = DateTime.Now;
                DB.SaveChanges();

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

        #endregion Events
    }
}
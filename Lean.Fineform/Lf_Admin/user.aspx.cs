using FineUIPro;
using System;
using System.Collections.Generic;
using System.Linq;

//using EntityFramework.Extensions;

namespace LeanFine.Lf_Admin
{
    public partial class user : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreUserView";
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
            CheckPowerWithButton("CoreUserEdit", btnChangeEnableUsers);
            CheckPowerWithButton("CoreUserDelete", btnDeleteSelected);
            CheckPowerWithButton("CoreUserNew", btnNew);

            ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Admin/user_new.aspx", "新增用户");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid();
        }

        private void ResolveEnableStatusButtonForGrid(MenuButton btn, Grid grid, bool enabled)
        {
            string enabledStr = "启用";
            if (!enabled)
            {
                enabledStr = "禁用";
            }
            btn.OnClientClick = grid.GetNoSelectionAlertInParentReference("请至少应该选择一项记录！");
            btn.ConfirmText = String.Format("确定要{1}选中的<span class=\"highlight\"><script>{0}</script></span>项记录吗？", grid.GetSelectedCountReference(), enabledStr);
            btn.ConfirmTarget = FineUIPro.Target.Top;
        }

        private void BindGrid()
        {
            IQueryable<Adm_User> q = DB.Adm_Users; //.Include(u => u.Dept);

            // 在用户名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(u => u.Name.Contains(searchText) || u.ChineseName.Contains(searchText) || u.EnglishName.Contains(searchText) || u.Address.Contains(searchText));
            }

            //if (GetIdentityName() != "admin")
            //{
            //    q = q.Where(u => u.Name != "admin");
            //}
            q = q.Where(u => u.Name != "admin");
            // 过滤启用状态
            if (rblEnableStatus.SelectedValue != "all")
            {
                q = q.Where(u => u.Enabled == (rblEnableStatus.SelectedValue == "enabled" ? true : false));
            }

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和数据库分页
            q = SortAndPage<Adm_User>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
        }

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

            CheckPowerWithLinkButtonField("CoreUserView", Grid1, "viewField");
            CheckPowerWithLinkButtonField("CoreUserEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreUserDelete", Grid1, "deleteField");
            CheckPowerWithLinkButtonField("CoreUserChangePassword", Grid1, "pwdeditField");
        }

        protected void Grid1_PreRowDataBound(object sender, FineUIPro.GridPreRowEventArgs e)
        {
            Adm_User user = e.DataItem as Adm_User;

            // 不能删除超级管理员
            if (user.Name == "admin")
            {
                FineUIPro.LinkButtonField deleteField = Grid1.FindColumn("deleteField") as FineUIPro.LinkButtonField;
                deleteField.Enabled = false;
                deleteField.ToolTip = "不能删除超级管理员！";
            }
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

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreUserDelete"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
            List<int> ids = GetSelectedDataKeyIDs(Grid1);

            // 执行数据库操作
            //DB.Adm_Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => DB.Adm_Users.Remove(u));
            //DB.SaveChanges();
            DB.Adm_Users.Where(u => ids.Contains(u.ID)).DeleteFromQuery();

            // 重新绑定表格
            BindGrid();
        }

        protected void btnEnableUsers_Click(object sender, EventArgs e)
        {
            SetSelectedUsersEnableStatus(true);
        }

        protected void btnDisableUsers_Click(object sender, EventArgs e)
        {
            SetSelectedUsersEnableStatus(false);
        }

        private void SetSelectedUsersEnableStatus(bool enabled)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreUserEdit"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
            List<int> ids = GetSelectedDataKeyIDs(Grid1);

            // 执行数据库操作
            //DB.Adm_Users.Where(u => ids.Contains(u.UserID)).ToList().ForEach(u => u.Enabled = enabled);
            //DB.SaveChanges();
            DB.Adm_Users.Where(u => ids.Contains(u.ID)).UpdateFromQuery(u => new Adm_User { Enabled = enabled });

            // 重新绑定表格
            BindGrid();
        }

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            if (e.CommandName == "View")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Admin/user_view.aspx?ID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());
            }
            if (e.CommandName == "PwdEdit")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Admin/user_changepassword.aspx?ID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());
            }
            if (e.CommandName == "Edit")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Admin/user_edit.aspx?ID=" + keys[0].ToString() + "&type=1") + Window1.GetMaximizeReference());
            }
            int userID = GetSelectedDataKeyID(Grid1);
            string userName = GetSelectedDataKey(Grid1, 1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreUserDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                if (userName == "admin")
                {
                    Alert.ShowInTop("不能删除默认的系统管理员（admin）！");
                }
                else
                {
                    DB.Adm_Users.Where(u => u.ID == userID).DeleteFromQuery();

                    BindGrid();
                }
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
﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FineUIPro;

namespace LeanFine.Lf_Admin
{
    public partial class role_user : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreRoleUserView";
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
            CheckPowerWithButton("CoreRoleUserNew", btnNew);
            CheckPowerWithButton("CoreRoleUserDelete", btnDeleteSelected);

            ResolveDeleteButtonForGrid(btnDeleteSelected, Grid2, "确定要从当前角色移除选中的{0}项记录吗？");

            BindGrid1();

            // 默认选中第一个角色
            Grid1.SelectedRowIndex = 0;

            // 每页记录数
            Grid2.PageSize = ConfigHelper.PageSize;
            ddlGridPageSize.SelectedValue = ConfigHelper.PageSize.ToString();

            BindGrid2();
        }

        private void BindGrid1()
        {
            IQueryable<Adm_Role> q = DB.Adm_Roles;

            // 排列
            q = Sort<Adm_Role>(q, Grid1);

            Grid1.DataSource = q;
            Grid1.DataBind();
        }

        private void BindGrid2()
        {
            int roleID = GetSelectedDataKeyID(Grid1);

            if (roleID == -1)
            {
                Grid2.RecordCount = 0;

                Grid2.DataSource = null;
                Grid2.DataBind();
            }
            else
            {
                IQueryable<Adm_User> q = DB.Adm_Users;

                // 在用户名称中搜索
                string searchText = ttbSearchUser.Text.Trim();
                if (!String.IsNullOrEmpty(searchText))
                {
                    q = q.Where(u => u.Name.Contains(searchText));
                }

                q = q.Where(u => u.Name != "admin");

                // 过滤选中角色下的所有用户
                q = q.Where(u => u.Roles.Any(r => r.ID == roleID));

                // 在查询添加之后，排序和分页之前获取总记录数
                Grid2.RecordCount = q.Count();

                // 排列和分页
                q = SortAndPage<Adm_User>(q, Grid2);

                Grid2.DataSource = q;
                Grid2.DataBind();
            }
        }

        #endregion Page_Load

        #region Events

        protected void ddlGridPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            Grid2.PageSize = Convert.ToInt32(ddlGridPageSize.SelectedValue);

            BindGrid2();
        }

        #endregion Events

        #region Grid1 Events

        protected void Grid1_Sort(object sender, GridSortEventArgs e)
        {
            Grid1.SortDirection = e.SortDirection;
            Grid1.SortField = e.SortField;
            BindGrid1();

            // 默认选中第一个角色
            Grid1.SelectedRowIndex = 0;

            BindGrid2();
        }

        protected void Grid1_RowClick(object sender, FineUIPro.GridRowClickEventArgs e)
        {
            BindGrid2();
        }

        #endregion Grid1 Events

        #region Grid2 Events

        protected void ttbSearchUser_Trigger2Click(object sender, EventArgs e)
        {
            ttbSearchUser.ShowTrigger1 = true;
            BindGrid2();
        }

        protected void ttbSearchUser_Trigger1Click(object sender, EventArgs e)
        {
            ttbSearchUser.Text = String.Empty;
            ttbSearchUser.ShowTrigger1 = false;
            BindGrid2();
        }

        protected void Grid2_PreDataBound(object sender, EventArgs e)
        {
            // 数据绑定之前，进行权限检查
            CheckPowerWithLinkButtonField("CoreRoleUserDelete", Grid2, "deleteField");
        }

        protected void Grid2_Sort(object sender, GridSortEventArgs e)
        {
            Grid2.SortDirection = e.SortDirection;
            Grid2.SortField = e.SortField;
            BindGrid2();
        }

        protected void Grid2_PageIndexChange(object sender, GridPageEventArgs e)
        {
            Grid2.PageIndex = e.NewPageIndex;
            BindGrid2();
        }

        protected void btnDeleteSelected_Click(object sender, EventArgs e)
        {
            // 在操作之前进行权限检查
            if (!CheckPower("CoreRoleUserDelete"))
            {
                CheckPowerFailWithAlert();
                return;
            }

            // 从每个选中的行中获取ID（在Grid1中定义的DataKeyNames）
            int roleID = GetSelectedDataKeyID(Grid1);
            List<int> userIDs = GetSelectedDataKeyIDs(Grid2);

            Adm_Role role = DB.Adm_Roles.Where(r => r.ID == roleID)
                .FirstOrDefault();

            foreach (int userID in userIDs)
            {
                Adm_User user = Attach<Adm_User>(userID);
                role.Users.Remove(user);
            }
            /*
            Role role = DB.Roles.Include(r => r.Users)
                .Where(r => r.ID == roleID)
                .FirstOrDefault();

            //role.Users.Where(u => userIDs.Contains(u.ID)).ToList().ForEach(u => role.Users.Remove(u));
            foreach (int userID in userIDs)
            {
                User user = role.Users.Where(u => u.ID == userID).FirstOrDefault();
                if (user != null)
                {
                    role.Users.Remove(user);
                }
            }
            */

            DB.SaveChanges();

            // 清空当前选中的项
            Grid2.SelectedRowIndexArray = null;

            // 重新绑定表格
            BindGrid2();
        }

        protected void Grid2_RowCommand(object sender, GridCommandEventArgs e)
        {
            object[] values = Grid2.DataKeys[e.RowIndex];
            int userID = Convert.ToInt32(values[0]);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreRoleUserDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                int roleID = GetSelectedDataKeyID(Grid1);

                Adm_Role role = DB.Adm_Roles.Include(r => r.Users)
                    .Where(r => r.ID == roleID)
                    .FirstOrDefault();

                Adm_User tobeRemovedUser = role.Users.Where(u => u.ID == userID).FirstOrDefault();
                if (tobeRemovedUser != null)
                {
                    role.Users.Remove(tobeRemovedUser);
                    DB.SaveChanges();
                }

                BindGrid2();
            }
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid2();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            int roleID = GetSelectedDataKeyID(Grid1);
            string addUrl = String.Format("~/Lf_Admin/role_user_addnew.aspx?id={0}", roleID);

            PageContext.RegisterStartupScript(Window1.GetShowReference(addUrl, "添加用户到当前角色"));
        }

        #endregion Grid2 Events
    }
}
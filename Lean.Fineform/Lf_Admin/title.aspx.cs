﻿//using EntityFramework.Extensions;
using System;
using System.Linq;
using FineUIPro;

namespace LeanFine.Lf_Admin
{
    public partial class title : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreTitleView";
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
            CheckPowerWithButton("CoreTitleNew", btnNew);
            //CheckPowerDeleteWithButton(btnDeleteSelected);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Admin/title_new.aspx", "新增职务");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;

            BindGrid();
        }

        private void BindGrid()
        {
            IQueryable<Adm_Title> q = DB.Adm_Titles;

            // 在职务名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(t => t.Name.Contains(searchText));
            }

            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和分页
            q = SortAndPage<Adm_Title>(q, Grid1);

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
            CheckPowerWithWindowField("CoreTitleEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreTitleDelete", Grid1, "deleteField");
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

        protected void Grid1_RowCommand(object sender, GridCommandEventArgs e)
        {
            int titleID = GetSelectedDataKeyID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreTitleDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }

                int userCount = DB.Adm_Users.Where(u => u.Titles.Any(t => t.ID == titleID)).Count();
                if (userCount > 0)
                {
                    Alert.ShowInTop("删除失败！需要先清空拥有此职务的用户！");
                    return;
                }

                DB.Adm_Titles.Where(t => t.ID == titleID).DeleteFromQuery();

                BindGrid();
            }
        }

        protected void Window1_Close(object sender, EventArgs e)
        {
            BindGrid();
        }

        #endregion Events
    }
}
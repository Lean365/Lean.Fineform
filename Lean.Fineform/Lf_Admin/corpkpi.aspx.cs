﻿using System;
using System.Linq;
using FineUIPro;

namespace LeanFine.Lf_Admin
{
    public partial class corpkpi : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreCorpkpiView";
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
            CheckPowerWithButton("CoreCorpkpiNew", btnNew);
            //CheckPowerDeleteWithButton(btnDeleteSelected);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            btnNew.OnClientClick = Window1.GetShowReference("~/Lf_Admin/corpkpi_new.aspx", "新增");

            // 每页记录数
            Grid1.PageSize = ConfigHelper.PageSize;

            BindGrid();
        }

        private void BindGrid()
        {
            IQueryable<Adm_Corpkpi> q = DB.Adm_Corpkpis;

            // 在名称中搜索
            string searchText = ttbSearchMessage.Text.Trim();
            if (!String.IsNullOrEmpty(searchText))
            {
                q = q.Where(r => r.CorpAbbrName.Contains(searchText) || r.CorpAnnual.Contains(searchText));
            }
            q = q.Where(r => r.IsDeleted == 0);
            // 在查询添加之后，排序和分页之前获取总记录数
            Grid1.RecordCount = q.Count();

            // 排列和数据库分页
            q = SortAndPage<Adm_Corpkpi>(q, Grid1);

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
            CheckPowerWithLinkButtonField("CoreCorpkpiEdit", Grid1, "editField");
            CheckPowerWithLinkButtonField("CoreCorpkpiDelete", Grid1, "deleteField");
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
            if (e.CommandName == "Edit")
            {
                object[] keys = Grid1.DataKeys[e.RowIndex];
                //labResult.Text = keys[0].ToString();
                PageContext.RegisterStartupScript(Window1.GetShowReference("~/Lf_Admin/corpkpi_edit.aspx?GUID=" + keys[0].ToString() + "&type=1"));// + Window1.GetMaximizeReference());
            }
            //int del_ID = GetSelectedDataKeyID(Grid1);

            string del_guid = GetSelectedDataKeyGUID(Grid1);

            if (e.CommandName == "Delete")
            {
                // 在操作之前进行权限检查
                if (!CheckPower("CoreCorpkpiDelete"))
                {
                    CheckPowerFailWithAlert();
                    return;
                }
                //删除日志
                //int userID = GetSelectedDataKeyID(Grid1);
                Adm_Institution current = DB.Adm_Institutions.Find(Guid.Parse(del_guid));

                string Newtext = current.ShortName;
                string OperateType = "删除";//操作标记
                string OperateNotes = "Del管理员* " + Newtext + " *Del 的记录已删除";
                OperateLogHelper.InsNetOperateNotes(GetIdentityName(), OperateType, "公司目标", "公司目标删除", OperateNotes);

                current.IsDeleted = 1;
                current.isEnabled = 1;
                DB.SaveChanges();

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
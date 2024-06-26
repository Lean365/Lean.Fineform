﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FineUIPro;

namespace LeanFine.Lf_Admin
{
    public partial class dept_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreDeptEdit";
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
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            int id = GetQueryIntValue("id");
            Adm_Dept current = DB.Adm_Depts.Include(d => d.Parent)
                .Where(d => d.ID == id).FirstOrDefault();
            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            tbxName.Text = current.Name;
            tbxSortIndex.Text = current.SortIndex.ToString();
            tbxRemark.Text = current.Remark;

            // 绑定下拉列表
            BindDdl(current);
        }

        private void BindDdl(Adm_Dept current)
        {
            List<Adm_Dept> mys = ResolveDDL<Adm_Dept>(DeptHelper.Adm_Depts, current.ID);

            // 绑定到下拉列表（启用模拟树功能和不可选择项功能）
            ddlParent.EnableSimulateTree = true;
            ddlParent.DataTextField = "Name";
            ddlParent.DataValueField = "ID";
            ddlParent.DataSimulateTreeLevelField = "TreeLevel";
            ddlParent.DataEnableSelectField = "Enabled";
            ddlParent.DataSource = mys;
            ddlParent.DataBind();

            if (current.Parent != null)
            {
                // 选中当前节点的父节点
                ddlParent.SelectedValue = current.Parent.ID.ToString();
            }
        }

        #endregion Page_Load

        #region Events

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            int id = GetQueryIntValue("id");
            Adm_Dept item = DB.Adm_Depts.Include(d => d.Parent).Where(d => d.ID == id).FirstOrDefault();
            item.Name = tbxName.Text.Trim();
            item.SortIndex = Convert.ToInt32(tbxSortIndex.Text.Trim());
            item.Remark = tbxRemark.Text.Trim();

            int parentID = Convert.ToInt32(ddlParent.SelectedValue);
            if (parentID == -1)
            {
                item.Parent = null;
            }
            else
            {
                Adm_Dept dept = Attach<Adm_Dept>(parentID);
                item.Parent = dept;
            }

            DB.SaveChanges();

            //FineUIPro.Alert.ShowInTop("保存成功！", String.Empty, FineUIPro.Alert.DefaultIcon, FineUIPro.ActiveWindow.GetHidePostBackReference());
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #endregion Events
    }
}
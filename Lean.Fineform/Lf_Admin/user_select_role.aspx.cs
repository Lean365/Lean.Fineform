﻿using System;
using FineUIPro;

namespace LeanFine.Lf_Admin
{
    public partial class user_select_role : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreRoleView";
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

            string ids = GetQueryValue("ids");

            // 绑定角色复选框列表
            BindDdlRole();

            // 初始化角色复选框列表的选择项
            cblRole.SelectedValueArray = ids.Split(',');
        }

        private void BindDdlRole()
        {
            cblRole.DataTextField = "Name";
            cblRole.DataValueField = "ID";
            cblRole.DataSource = DB.Adm_Roles;
            cblRole.DataBind();
        }

        #endregion Page_Load

        #region Events

        //protected void btnSaveClose_Click(object sender, EventArgs e)
        //{
        //    string roleValues = String.Join(",", cblRole.SelectedItemArray.Select(c => c.Value));
        //    string roleTexts = String.Join(",", cblRole.SelectedItemArray.Select(c => c.Text));

        //    //PageContext.RegisterStartupScript(ActiveWindow.GetWriteBackValueReference(roleValues, roleTexts)
        //    //    + ActiveWindow.GetHideReference());

        //    PageContext.RegisterStartupScript(String.Format("selectRole({0},{1});", JsHelper.Enquote(roleTexts), JsHelper.Enquote(roleValues)));
        //}

        #endregion Events
    }
}
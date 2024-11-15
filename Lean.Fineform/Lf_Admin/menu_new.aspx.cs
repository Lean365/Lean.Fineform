﻿using System;
using System.Collections.Generic;
using System.Linq;
using FineUIPro;

namespace LeanFine.Lf_Admin
{
    public partial class menu_new : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreMenuNew";
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

            //// 模块名称列表
            //ddlModules.DataSource = ModuleTypeHelper.GetAppModules();
            //ddlModules.DataBind();

            //ddlModules.SelectedValue = ModuleTypeHelper.Module2String(ModuleType.None);

            BindDdl();

            InitIconList(iconList);
        }

        public void InitIconList(FineUIPro.RadioButtonList iconList)
        {
            string[] icons = new string[] { "tag_yellow", "tag_red", "tag_purple", "tag_pink", "tag_orange", "tag_green", "tag_blue" };
            foreach (string icon in icons)
            {
                string value = String.Format("~/Lf_Resources/icon/{0}.png", icon);
                string text = String.Format("<img style=\"vertical-align:bottom;\" src=\"{0}\" />&nbsp;{1}", ResolveUrl(value), icon);

                iconList.Items.Add(new RadioItem(text, value));
            }
        }

        private void BindDdl()
        {
            List<Adm_Menu> menus = ResolveDDL<Adm_Menu>(MenuHelper.Adm_Menus);

            // 绑定到下拉列表（启用模拟树功能）
            ddlParent.EnableSimulateTree = true;
            ddlParent.DataTextField = "Name";
            ddlParent.DataValueField = "ID";
            ddlParent.DataSimulateTreeLevelField = "TreeLevel";
            ddlParent.DataSource = menus;
            ddlParent.DataBind();

            // 选中根节点
            ddlParent.SelectedValue = "0";
        }

        #endregion Page_Load

        #region Events

        private void SaveItem()
        {
            Adm_Menu item = new Adm_Menu();
            item.Name = tbxName.Text.Trim();
            item.NavigateUrl = tbxUrl.Text.Trim();
            item.SortIndex = Convert.ToInt32(tbxSortIndex.Text.Trim());
            item.Remark = tbxRemark.Text.Trim();

            int parentID = Convert.ToInt32(ddlParent.SelectedValue);
            if (parentID == -1)
            {
                item.Parent = null;
            }
            else
            {
                Adm_Menu menu = Attach<Adm_Menu>(parentID);
                item.Parent = menu;
            }

            string viewPowerName = tbxViewPower.Text.Trim();
            if (String.IsNullOrEmpty(viewPowerName))
            {
                item.ViewPower = null;
            }
            else
            {
                item.ViewPower = DB.Adm_Powers.Where(p => p.Name == viewPowerName).FirstOrDefault();
            }

            DB.Adm_Menus.Add(item);
            DB.SaveChanges();
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            SaveItem();

            //Alert.ShowInTop("添加成功！", String.Empty, ActiveWindow.GetHidePostBackReference());
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #endregion Events
    }
}
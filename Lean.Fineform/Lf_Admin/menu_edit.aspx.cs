using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using FineUIPro;

namespace LeanFine.Lf_Admin
{
    public partial class menu_edit : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreMenuEdit";
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
            Adm_Menu current = DB.Adm_Menus
                .Include(m => m.Parent).Include(m => m.ViewPower)
                .Where(m => m.ID == id).FirstOrDefault();
            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            tbxName.Text = current.Name;
            tbxUrl.Text = current.NavigateUrl;
            tbxSortIndex.Text = current.SortIndex.ToString();
            tbxIcon.Text = current.ImageUrl;
            tbxRemark.Text = current.Remark;
            if (current.ViewPower != null)
            {
                tbxViewPower.Text = current.ViewPower.Name;
            }

            // 绑定上级菜单下拉列表
            BindDdl(current);

            // 预置图标列表
            InitIconList(iconList);

            if (!String.IsNullOrEmpty(current.ImageUrl))
            {
                iconList.SelectedValue = current.ImageUrl;
            }
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

        private void BindDdl(Adm_Menu current)
        {
            List<Adm_Menu> mys = ResolveDDL<Adm_Menu>(MenuHelper.Adm_Menus, current.ID);

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
            Adm_Menu item = DB.Adm_Menus
                .Include(m => m.Parent).Include(m => m.ViewPower)
                .Where(m => m.ID == id).FirstOrDefault();
            item.Name = tbxName.Text.Trim();
            item.NavigateUrl = tbxUrl.Text.Trim();
            item.SortIndex = Convert.ToInt32(tbxSortIndex.Text.Trim());
            item.ImageUrl = tbxIcon.Text;
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

            DB.SaveChanges();

            //FineUIPro.Alert.ShowInTop("保存成功！", String.Empty, FineUIPro.Alert.DefaultIcon, FineUIPro.ActiveWindow.GetHidePostBackReference());
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #endregion Events
    }
}
using FineUIPro;
using System;

namespace LeanFine.Lf_Admin
{
    public partial class role_new : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreRoleNew";
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
        }

        #endregion Page_Load

        #region Events

        private void SaveItem()
        {
            Adm_Role item = new Adm_Role();
            item.Name = tbxName.Text.Trim();
            item.Remark = tbxRemark.Text.Trim();

            DB.Adm_Roles.Add(item);
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
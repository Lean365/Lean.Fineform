using FineUIPro;
using System;
using System.Linq;

namespace LeanFine.Lf_Admin
{
    public partial class changepassword : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "";
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
        }

        #endregion Page_Load

        #region Events

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            // 检查当前密码是否正确
            string oldPass = tbxOldPassword.Text.Trim();
            string newPass = tbxNewPassword.Text.Trim();
            string confirmNewPass = tbxConfirmNewPassword.Text.Trim();

            if (newPass != confirmNewPass)
            {
                tbxConfirmNewPassword.MarkInvalid("确认密码和新密码不一致！");
                return;
            }

            Adm_User user = DB.Adm_Users.Where(u => u.Name == User.Identity.Name).FirstOrDefault();

            if (user != null)
            {
                if (!PasswordUtil.ComparePasswords(user.Password, oldPass))
                {
                    tbxOldPassword.MarkInvalid("当前密码不正确！");
                    return;
                }

                user.Password = PasswordUtil.CreateDbPassword(newPass);
                DB.SaveChanges();

                Alert.ShowInTop("修改密码成功！");
            }
        }

        #endregion Events
    }
}
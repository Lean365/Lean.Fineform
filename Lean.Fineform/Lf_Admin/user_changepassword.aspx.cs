using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;using System.Data.Entity.Validation;
using System.Linq;
using FineUIPro;

namespace Lean.Fineform.Lf_Admin
{
    public partial class user_changepassword : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreUserChangePassword";
            }
        }

        #endregion

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
            string uid = GetIdentityName();
            var q = from a in DB.Adm_Users
                    where a.Name == uid

                    select a;
            if (q.Any())
            {
                var qs = q.ToList();

                    labUserName.Text = qs[0].Name;
                    labUserRealName.Text = qs[0].ChineseName;


            }
            else
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            
            //if (current.Name == "admin" && GetIdentityName() != "admin")
            //{
            //    Alert.ShowInTop("你无权编辑超级管理员！", String.Empty, ActiveWindow.GetHideReference());
            //    return;
            //}


        }

        #endregion

        #region Events

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            int id = GetQueryIntValue("id");
            Adm_User item = DB.Adm_Users.Find(id);
            item.Password = PasswordUtil.CreateDbPassword(tbxPassword.Text.Trim());
            DB.SaveChanges();

            //Alert.ShowInTop("保存成功！", String.Empty, Alert.DefaultIcon, ActiveWindow.GetHidePostBackReference());
            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }
        #endregion

    }
}

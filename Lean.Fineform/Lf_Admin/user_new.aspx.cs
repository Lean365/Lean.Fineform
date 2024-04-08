using System;
using System.Collections.Generic;
using System.Linq;
using FineUIPro;

namespace LeanFine.Lf_Admin
{
    public partial class user_new : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreUserNew";
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

            // 初始化用户所属角色
            InitUserRole();

            // 初始化用户所属部门
            InitUserDept();

            // 初始化用户所属职称
            InitUserTitle();
        }

        #region InitUserRole

        private void InitUserDept()
        {
            //// 打开编辑窗口
            //string selectDeptURL = String.Format("./user_select_dept.aspx?ids=<script>{0}</script>", hfSelectedDept.GetValueReference());
            //tbSelectedDept.OnClientTriggerClick = Window1.GetSaveStateReference(hfSelectedDept.ClientID, tbSelectedDept.ClientID)
            //        + Window1.GetShowReference(selectDeptURL, "选择用户所属的部门");
        }

        #endregion InitUserRole

        #region InitUserRole

        private void InitUserRole()
        {
            //// 打开编辑角色的窗口
            //string selectRoleURL = String.Format("./user_select_role.aspx?ids=<script>{0}</script>", hfSelectedRole.GetValueReference());
            //tbSelectedRole.OnClientTriggerClick = Window1.GetSaveStateReference(hfSelectedRole.ClientID, tbSelectedRole.ClientID)
            //        + Window1.GetShowReference(selectRoleURL, "选择用户所属的角色");
        }

        #endregion InitUserRole

        #region InitUserJobTitle

        private void InitUserTitle()
        {
            //// 打开编辑角色的窗口
            //string selectJobTitleURL = String.Format("./user_select_title.aspx?ids=<script>{0}</script>", hfSelectedTitle.GetValueReference());
            //tbSelectedTitle.OnClientTriggerClick = Window1.GetSaveStateReference(hfSelectedTitle.ClientID, tbSelectedTitle.ClientID)
            //        + Window1.GetShowReference(selectJobTitleURL, "选择用户拥有的职称");
        }

        #endregion InitUserJobTitle

        #endregion Page_Load

        #region Events

        private void SaveItem()
        {
            Adm_User item = new Adm_User();
            item.Name = tbxName.Text.Trim();
            item.Password = PasswordUtil.CreateDbPassword(tbxPassword.Text.Trim());
            item.ChineseName = tbxChineseName.Text.Trim();
            item.EnglishName = tbxEnglishName.Text.Trim();
            item.Gender = ddlGender.SelectedValue;
            item.CompanyEmail = tbxCompanyEmail.Text.Trim();
            item.Email = tbxEmail.Text.Trim();
            item.OfficePhone = tbxOfficePhone.Text.Trim();
            item.OfficePhoneExt = tbxOfficePhoneExt.Text.Trim();
            item.HomePhone = tbxHomePhone.Text.Trim();
            item.CellPhone = tbxCellPhone.Text.Trim();
            item.Address = tbxAddress.Text.Trim();
            item.Remark = tbxRemark.Text.Trim();
            item.Enabled = cbxEnabled.Checked;
            item.CreateDate = DateTime.Now;

            // 添加所有部门
            if (!String.IsNullOrEmpty(hfSelectedDept.Text))
            {
                Adm_Dept dept = Attach<Adm_Dept>(Convert.ToInt32(hfSelectedDept.Text));
                item.Dept = dept;
            }

            // 添加所有角色
            if (!String.IsNullOrEmpty(hfSelectedRole.Text))
            {
                item.Roles = new List<Adm_Role>();
                int[] roleIDs = StringUtil.GetIntArrayFromString(hfSelectedRole.Text);

                AddEntities<Adm_Role>(item.Roles, roleIDs);
            }

            // 添加所有职称
            if (!String.IsNullOrEmpty(hfSelectedTitle.Text))
            {
                item.Titles = new List<Adm_Title>();
                int[] titleIDs = StringUtil.GetIntArrayFromString(hfSelectedTitle.Text);

                AddEntities<Adm_Title>(item.Titles, titleIDs);
            }

            DB.Adm_Users.Add(item);
            DB.SaveChanges();
        }

        protected void btnSaveClose_Click(object sender, EventArgs e)
        {
            string inputUserName = tbxName.Text.Trim();

            Adm_User user = DB.Adm_Users.Where(u => u.Name == inputUserName).FirstOrDefault();

            if (user != null)
            {
                Alert.ShowInTop("用户 " + inputUserName + " 已经存在！");
                return;
            }

            SaveItem();

            PageContext.RegisterStartupScript(ActiveWindow.GetHidePostBackReference());
        }

        #endregion Events
    }
}
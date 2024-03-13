using FineUIPro;
using System;

namespace LeanFine.Lf_Admin.help
{
    public partial class help_map : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreSysView";
            }
        }

        #endregion ViewPower

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

            CheckPowerWithButton("CoreSysView", Btn_sys_Help_Htmlcolor);
            CheckPowerWithButton("CoreSysView", Btn_sys_Help_Encryption);
            CheckPowerWithButton("CoreSysView", Btn_sys_Help_FontIcon);
            CheckPowerWithButton("CoreSysView", Btn_sys_Help_Random);
            CheckPowerWithButton("CoreSysView", Btn_sys_Help_Wiki);
            CheckPowerWithButton("CoreSysView", Btn_sys_Help_Manual);
            //btnConfig.OnClientClick = Window1.GetShowReference("~/Lf_Admin/dept_new.aspx", "新增部门");
        }

        #region event

        protected void Btn_sys_Help_Htmlcolor_Click(object sender, EventArgs e)
        {
            string sys_Help_Htmlcolor = global::Resources.GlobalResource.sys_Help_Htmlcolor;
            PageContext.RegisterStartupScript("top.addExampleTab('sys_Help_Htmlcolor','/Lf_Admin/help/htmlcolorcodes.aspx','" + sys_Help_Htmlcolor + "', '/res/icon/htmlcolor.png', '', true, ''); ");
        }

        protected void Btn_sys_Help_Encryption_Click(object sender, EventArgs e)
        {
            string sys_Help_Encryption = global::Resources.GlobalResource.sys_Help_Encryption;
            PageContext.RegisterStartupScript("top.addExampleTab('sys_Help_Encryption','/Lf_Admin/help/encryption.aspx','" + sys_Help_Encryption + "', '/res/icon/encryption.png', '', true, ''); ");
        }

        protected void Btn_sys_Help_FontIcon_Click(object sender, EventArgs e)
        {
            string sys_Help_FontIcon = global::Resources.GlobalResource.sys_Help_FontIcon;
            PageContext.RegisterStartupScript("top.addExampleTab('sys_Help_FontIcon','/Lf_Admin/help/fontawesome.aspx','" + sys_Help_FontIcon + "', '/res/icon/fonticon.png', '', true, ''); ");
        }

        protected void Btn_sys_Help_Random_Click(object sender, EventArgs e)
        {
            string sys_Help_Random = global::Resources.GlobalResource.sys_Help_Random;
            PageContext.RegisterStartupScript("top.addExampleTab('sys_Help_Random','/Lf_Admin/help/Random.aspx','" + sys_Help_Random + "', '/res/icon/Random.png', '', true, ''); ");
        }

        protected void Btn_sys_Help_Wiki_Click(object sender, EventArgs e)
        {
            string menu_Sys_Wiki = global::Resources.GlobalResource.sys_Help_Wiki;
            PageContext.RegisterStartupScript("top.addExampleTab('menu_Sys_Wiki','/Lf_Admin/help/wiki.aspx','" + menu_Sys_Wiki + "', '/res/icon/world.png', '', true, ''); ");
        }

        protected void Btn_sys_Help_Manual_Click(object sender, EventArgs e)
        {
            string sys_Help_Manual = global::Resources.GlobalResource.menu_Sys_Help;
            PageContext.RegisterStartupScript("top.addExampleTab('sys_Help_Manual','/Lf_Documents/helper/Lc_Manual.pdf','" + sys_Help_Manual + "', '/res/icon/help.png', '', true, ''); ");
        }

        #endregion event
    }
}
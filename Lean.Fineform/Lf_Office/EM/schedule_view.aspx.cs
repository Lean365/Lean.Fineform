using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Linq;
using System.Data.Entity;
using FineUIPro;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Drawing;
namespace Lean.Fineform.Lf_Office.EM
{

    public partial class schedule_view : PageBase
    {

        
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreEventView";
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
            //Publisher.Text = GetIdentityName();
            btnClose.OnClientClick = ActiveWindow.GetHideReference();

            int id = GetQueryIntValue("id");
            Em_Event current = DB.Em_Events.Find(id);

            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }


            this.attypename.Text = current.attypename;// "1";
            attitle.Text = current.attitle;
            atcontent.Text = current.atcontent;

            this.atsdate.Text = current.atsdate.ToString();
            this.atedate.Text = current.atedate.ToString();


        }


        #endregion

        #region Events
        #endregion


    }
}

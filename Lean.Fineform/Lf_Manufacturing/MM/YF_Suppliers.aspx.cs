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
using System.Data.Entity.Validation;

namespace Fine.Lf_Manufacturing.MM
{

    public partial class YF_Suppliers : PageBase
    {


        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreMMView";
            }
        }

        #endregion

        #region Page_Load
        Fine.Lf_Business.Models.YF.Yifei_DTAEntities DBCYF = new Fine.Lf_Business.Models.YF.Yifei_DTAEntities();
        Fine.Lf_Business.Models.YF.Yifei_TACEntities DBHYF = new Fine.Lf_Business.Models.YF.Yifei_TACEntities();

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
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();
            BindDDLH_code();
            BindDDLC_code();


        }
        private void BindDataC()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();


            if (C_Code.SelectedIndex != 0 && C_Code.SelectedIndex != -1)
            {
                string searchText = C_Code.SelectedItem.Text.Trim().ToUpper();

                Fine.Lf_Business.Models.YF.PURMA Ccurrent = DBCYF.PURMA.Find(searchText); //.Include(u => u.Dept);

                if (Ccurrent == null)
                {
                    // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                    return;
                }

                C_MA001.Text = Ccurrent.MA001;
                C_MA002.Text = Ccurrent.MA002;
                C_MA003.Text = Ccurrent.MA003;
                C_MA008.Text = Ccurrent.MA008;
                C_MA009.Text = Ccurrent.MA009;
                C_MA010.Text = Ccurrent.MA010;
                C_MA011.Text = Ccurrent.MA011;
                C_MA012.Text = Ccurrent.MA012;
                C_MA013.Text = Ccurrent.MA013;
                C_MA014.Text = Ccurrent.MA014;
                C_MA015.Text = Ccurrent.MA015;
                C_MA021.Text = Ccurrent.MA021;
                C_MA023.Text = Ccurrent.MA023;
                C_MA024.Text = Ccurrent.MA024;
                C_MA025.Text = Ccurrent.MA025;
                C_MA028.Text = Ccurrent.MA028;
                C_MA047.Text = Ccurrent.MA047;
                C_MA048.Text = Ccurrent.MA048;
                C_MA049.Text = Ccurrent.MA049;
                C_MA051.Text = Ccurrent.MA051;
                C_MA055.Text = Ccurrent.MA055;



            }
        }
        private void BindDataH()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            if (H_Code.SelectedIndex != 0 && H_Code.SelectedIndex != -1)
            {
                string searchText = H_Code.SelectedItem.Text.Trim().ToUpper();

                Fine.Lf_Business.Models.YF.PURMA Hcurrent = DBHYF.PURMA.Find(searchText); //.Include(u => u.Dept);

                if (Hcurrent == null)
                {
                    // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                    Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                    return;
                }
                H_MA001.Text = Hcurrent.MA001;
                H_MA002.Text = Hcurrent.MA002;
                H_MA003.Text = Hcurrent.MA003;
                H_MA008.Text = Hcurrent.MA008;
                H_MA009.Text = Hcurrent.MA009;
                H_MA010.Text = Hcurrent.MA010;
                H_MA011.Text = Hcurrent.MA011;
                H_MA012.Text = Hcurrent.MA012;
                H_MA013.Text = Hcurrent.MA013;
                H_MA014.Text = Hcurrent.MA014;
                H_MA015.Text = Hcurrent.MA015;
                H_MA021.Text = Hcurrent.MA021;
                H_MA023.Text = Hcurrent.MA023;
                H_MA024.Text = Hcurrent.MA024;
                H_MA025.Text = Hcurrent.MA025;
                H_MA028.Text = Hcurrent.MA028;
                H_MA047.Text = Hcurrent.MA047;
                H_MA048.Text = Hcurrent.MA048;
                H_MA049.Text = Hcurrent.MA049;
                H_MA051.Text = Hcurrent.MA051;
                H_MA055.Text = Hcurrent.MA055;



            }

        }

        private void BindDDLH_code()
        {

            IQueryable<Fine.Lf_Business.Models.YF.PURMA> q = DBHYF.PURMA;



            var qs = q.Select(E => new { E.MA001 }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            H_Code.DataSource = qs;
            H_Code.DataTextField = "MA001";
            H_Code.DataValueField = "MA001";
            H_Code.DataBind();

            this.H_Code.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }


        private void BindDDLC_code()
        {
            IQueryable<Fine.Lf_Business.Models.YF.PURMA> q = DBCYF.PURMA;



            var qs = q.Select(E => new { E.MA001 }).ToList().Distinct();
            //var list = (from c in DB.ProSapPorders
            //                where c.D_SAP_COOIS_C006- c.D_SAP_COOIS_C005< 0
            //                select c.D_SAP_COOIS_C002+"//"+c.D_SAP_COOIS_C003 + "//" + c.D_SAP_COOIS_C004).ToList();
            //3.2.将数据绑定到下拉框
            C_Code.DataSource = qs;
            C_Code.DataTextField = "MA001";
            C_Code.DataValueField = "MA001";
            C_Code.DataBind();
            this.C_Code.Items.Insert(0, new FineUIPro.ListItem(global::Resources.GlobalResource.Query_Select, ""));
        }












        #endregion

        #region Events
        protected void H_Code_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataH();
        }

        protected void C_Code_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDataC();
        }
        #endregion


    }
}

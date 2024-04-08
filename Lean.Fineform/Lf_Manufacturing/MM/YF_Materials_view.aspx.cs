using System;
using FineUIPro;
using LeanFine.Lf_Business.Models.YF;

namespace LeanFine.Lf_Manufacturing.MM
{
    public partial class YF_Materials_view : PageBase
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
            //Publisher.Text = GetIdentityName();
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            // 初始化用户
            //InitNoticeUser();

            // 初始化用户所属部门
            //InitNoticeDept();

            BindData();
        }

        private void BindData()
        {
            //btnClose.OnClientClick = ActiveWindow.GetHideReference();

            string id = GetQueryValue("MB001");

            Lf_Business.Models.YF.Yifei_DTA_Entities DBCYF = new Lf_Business.Models.YF.Yifei_DTA_Entities();

            INVMB Ccurrent = DBCYF.INVMB.Find(id); //.Include(u => u.Dept);

            if (Ccurrent == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            CMB001.Text = Ccurrent.MB001;
            CMB002.Text = Ccurrent.MB002;
            CMB003.Text = Ccurrent.MB003;
            CMB005.Text = Ccurrent.MB005;
            CMB007.Text = Ccurrent.MB007;
            CMB009.Text = Ccurrent.MB009;

            CMB014.Text = Ccurrent.MB014.ToString();
            CMB015.Text = Ccurrent.MB015;
            CMB017.Text = Ccurrent.MB017;
            CMB019.Text = Ccurrent.MB019;

            CMB025.Text = Ccurrent.MB025;
            CMB028.Text = Ccurrent.MB028;

            CMB032.Text = Ccurrent.MB032;

            CMB036.Text = Ccurrent.MB036.ToString();
            CMB039.Text = Ccurrent.MB039.ToString();

            CMB048.Text = Ccurrent.MB048;
            CMB049.Text = Ccurrent.MB049.ToString();
            CMB050.Text = Ccurrent.MB050.ToString();

            CMSMV CName = DBCYF.CMSMV.Find(Ccurrent.MB067);
            if (CName != null)
            {
                CMB067.Text = CName.MV002;
            }
            CMB068.Text = Ccurrent.MB068;

            CMB080.Text = Ccurrent.MB080;

            CMB110.Text = Ccurrent.MB110;
            CUDF01.Text = Ccurrent.UDF01;
            CUDF02.Text = Ccurrent.UDF02;
            CUDF04.Text = Ccurrent.UDF04;
            CUDF05.Text = Ccurrent.UDF05;

            CUDF51.Text = Ccurrent.UDF51.ToString();

            Lf_Business.Models.YF.Yifei_TAC_Entities DBHYF = new Lf_Business.Models.YF.Yifei_TAC_Entities();

            INVMB Hcurrent = DBHYF.INVMB.Find(id); //.Include(u => u.Dept);

            if (Hcurrent == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.ShowInTop(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }

            HMB001.Text = Hcurrent.MB001;
            HMB002.Text = Hcurrent.MB002;
            HMB003.Text = Hcurrent.MB003;
            HMB005.Text = Hcurrent.MB005;
            HMB007.Text = Hcurrent.MB007;
            HMB009.Text = Hcurrent.MB009;

            HMB014.Text = Hcurrent.MB014.ToString();
            HMB015.Text = Hcurrent.MB015;
            HMB017.Text = Hcurrent.MB017;
            HMB019.Text = Hcurrent.MB019;

            HMB025.Text = Hcurrent.MB025;
            HMB028.Text = Hcurrent.MB028;

            HMB032.Text = Hcurrent.MB032;

            HMB036.Text = Hcurrent.MB036.ToString();
            HMB039.Text = Hcurrent.MB039.ToString();

            HMB048.Text = Hcurrent.MB048;
            HMB049.Text = Hcurrent.MB049.ToString();
            HMB050.Text = Hcurrent.MB050.ToString();

            CMSMV HName = DBHYF.CMSMV.Find(Ccurrent.MB067);
            if (HName != null)
            {
                HMB067.Text = HName.MV002;
            }
            //HMB067.Text = Hcurrent.MB067;

            HMB068.Text = Hcurrent.MB068;
            HMB080.Text = Hcurrent.MB080;

            HMB110.Text = Hcurrent.MB110;
            HUDF01.Text = Hcurrent.UDF01;
            HUDF02.Text = Hcurrent.UDF02;
            HUDF04.Text = Hcurrent.UDF04;
            HUDF05.Text = Hcurrent.UDF05;

            HUDF51.Text = Hcurrent.UDF51.ToString();
        }

        #endregion Page_Load
    }
}
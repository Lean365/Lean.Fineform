using FineUIPro;
using System;

namespace LeanFine.Lf_Report
{
    public partial class pp_daily_report : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreModelsView";
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
            // 权限检查
            //CheckPowerWithButton("CoreNoticeEdit", btnChangeEnableUsers);
            //CheckPowerWithButton("CoreProdataDelete", btnDeleteSelected);
            CheckPowerWithButton("CoreKitPrint", btn_PrintPreview);
            CheckPowerWithButton("CoreKitDesgin", btn_PrintDesign);
            //CheckPowerWithButton("CoreKitOutput", BtnExport);
            //CheckPowerWithButton("CoreKitOutput", Btn2003);
            //CheckPowerWithButton("CoreProdataNew", btnP2d);

            //ResolveDeleteButtonForGrid(btnDeleteSelected, Grid1);

            //ResolveEnableStatusButtonForGrid(btnEnableUsers, Grid1, true);
            //ResolveEnableStatusButtonForGrid(btnDisableUsers, Grid1, false);

            //btnNew.OnClientClick = Window1.GetShowReference("~/LB_Manufacturing/Master/PP_models_region_new.aspx", "新增");
            //btnP2d.OnClientClick = Window1.GetShowReference("~/oneProduction/oneTimesheet/bad_p2d_new.aspx", "P2D新增不良记录");
        }

        #endregion Page_Load

        #region Event

        protected void btn_PrintPreview_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript("javascript:CreatePrintPage();LODOP.PREVIEW();");
        }

        protected void btn_PrintDesign_Click(object sender, EventArgs e)
        {
            PageContext.RegisterStartupScript("javascript:CreatePrintPage();LODOP.PRINT_DESIGN();");
        }

        #endregion Event
    }
}
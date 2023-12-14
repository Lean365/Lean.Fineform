using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
using System.Linq;
using System.Data.Entity;

using System.Data.SqlClient;
using System.Data;
using System.Xml;

namespace Lean.Fineform.Lf_Manufacturing.Master
{
    public partial class Pp_models_region_view : PageBase
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
            CheckPowerWithButton("CoreKitPrint", btn_PrintPreview);
            CheckPowerWithButton("CoreKitDesgin", btn_PrintDesign);
            CheckPowerWithButton("CoreKitDesgin", btn_PrintSetup);
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
            BindData();
        }
        private void BindData()
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Pp_SapModelDest current = DB.Pp_SapModelDests
                .Where(u => u.GUID == id).FirstOrDefault();
            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.Show(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }
            lblD_SAP_DEST_Z001.Text = current.D_SAP_DEST_Z001;

            lblD_SAP_DEST_Z003.Text = current.D_SAP_DEST_Z002;
            lblD_SAP_DEST_Z004.Text = current.D_SAP_DEST_Z003;
            //lblUdf004.Text = current.Udf004.ToString();
            //imgModelQrcode.ImageUrl = current.Udf001;
            this.imgModelQrcode.ImageWidth = Unit.Pixel(64);
            this.imgModelQrcode.ImageHeight = Unit.Pixel(64);

        }
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {

        }


        #endregion


    }
}

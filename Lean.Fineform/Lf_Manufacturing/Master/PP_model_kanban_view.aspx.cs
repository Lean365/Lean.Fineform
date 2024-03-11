using Fine.Lf_Business.Models.PP;
using FineUIPro;
using System;
using System.Data;
using System.Linq;

namespace Fine.Lf_Manufacturing.Master
{
    public partial class Pp_model_kanban_view : PageBase
    {
        #region ViewPower

        /// <summary>
        /// 本页面的浏览权限，空字符串表示本页面不受权限控制
        /// </summary>
        public override string ViewPower
        {
            get
            {
                return "CoreKanbanView";
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
            //CheckPowerWithButton("CoreKitPrint", btn_PrintPreview);
            //CheckPowerWithButton("CoreKitDesgin", btn_PrintDesign);
            //CheckPowerWithButton("CoreKitDesgin", btn_PrintSetup);
            btnClose.OnClientClick = ActiveWindow.GetHideReference();
            BindData();
        }
        private void BindData()
        {
            Guid id = Guid.Parse(GetQueryValue("GUID"));
            Pp_Kanban current = DB.Pp_Kanbans
                .Where(u => u.GUID == id).FirstOrDefault();
            if (current == null)
            {
                // 参数错误，首先弹出Alert对话框然后关闭弹出窗口
                Alert.Show(global::Resources.GlobalResource.sys_Parameter_Error, String.Empty, ActiveWindow.GetHideReference());
                return;
            }
            lblP_Kanban_Date.Text = current.P_Kanban_Date;

            lblP_Kanban_Line.Text = current.P_Kanban_Line;
            lblP_Kanban_Order.Text = current.P_Kanban_Order;
            lblP_Kanban_Item.Text= current.P_Kanban_Item;
            lblP_Kanban_Lot.Text=current.P_Kanban_Lot;
            lblP_Kanban_Model.Text = current.P_Kanban_Model;
            lblP_Kanban_Region.Text = current.P_Kanban_Region;
            lblP_Kanban_Process.Text= current.P_Kanban_Process.ToString();

        }
        protected void Window1_Close(object sender, WindowCloseEventArgs e)
        {

        }


        #endregion


    }
}

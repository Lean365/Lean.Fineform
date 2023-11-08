using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FineUIPro;
namespace Lean.Fineform.Lf_Manufacturing.MM
{
    public partial class Yf_Map : PageBase
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
            CheckPowerWithButton("CoreMMView", Btn_Yf_Material);
            CheckPowerWithButton("CoreMMView", Btn_Yf_BOM);
            CheckPowerWithButton("CoreMMView", Btn_Yf_PurchaseOrder);
            CheckPowerWithButton("CoreMMView", Btn_Yf_Requisition);
            CheckPowerWithButton("CoreMMView", Btn_Yf_PurchasingPrice);
            CheckPowerWithButton("CoreMMView", Btn_Yf_PurchasingPriceVal);
            CheckPowerWithButton("CoreMMView", Btn_Yf_Supplier);
            CheckPowerWithButton("CoreMMView", Btn_Yf_MaterialSupplier);
            CheckPowerWithButton("CoreMMView", Btn_Yf_change);
            CheckPowerWithButton("CoreMMView", Btn_Yf_replace);
            CheckPowerWithButton("CoreMMView", Btn_Yf_Special_Material);
            CheckPowerWithButton("CoreMMView", Btn_Yf_General_Material);
            CheckPowerWithButton("CoreMMView", Btn_Shipment);
            CheckPowerWithButton("CoreMMView", Btn_SalesOrder);
            CheckPowerWithButton("CoreMMView", Btn_SalesSlip);
            CheckPowerWithButton("CoreMMView", Btn_Assets);
            CheckPowerWithButton("CoreMMView", Btn_Bo);
            //btnConfig.OnClientClick = Window1.GetShowReference("~/Lf_Admin/dept_new.aspx", "新增部门");


        }
        #region event


        protected void Btn_Yf_Material_Click(object sender, EventArgs e)
        {
            // 添加示例标签页
            // tabOptions: 选项卡参数
            // tabOptions.id： 选项卡ID
            // tabOptions.iframeUrl: 选项卡IFrame地址 
            // tabOptions.title： 选项卡标题
            // tabOptions.icon： 选项卡图标
            // tabOptions.createToolbar： 创建选项卡前的回调函数（接受tabOptions参数）
            // tabOptions.refreshWhenExist： 添加选项卡时，如果选项卡已经存在，是否刷新内部IFrame
            // tabOptions.iconFont： 选项卡图标字体
            // tabOptions.activeIt： 是否激活当前添加的选项卡
            // actived: 是否激活选项卡（默认为true）string menu_Sys_Menu= global::Resources.GlobalResource.;
            string Yf_Material = global::Resources.GlobalResource.Yf_Material;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_Material','/Lf_Manufacturing/MM/Yf_Materials.aspx','" + Yf_Material + "', '/res/icon/yf1.png', '', true, ''); ");
        }


        protected void Btn_Yf_BOM_Click(object sender, EventArgs e)
        {
            string Yf_BOM = global::Resources.GlobalResource.Yf_BOM;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_BOM','/Lf_Manufacturing/MM/Yf_BOM.aspx','" + Yf_BOM + "', '/res/icon/yf2.png', '', true, ''); ");
        }
        protected void Btn_Yf_PurchaseOrder_Click(object sender, EventArgs e)
        {
            string Yf_PurchaseOrder = global::Resources.GlobalResource.Yf_PurchaseOrder;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_PurchaseOrder','/Lf_Manufacturing/MM/Yf_PurchaseOrder.aspx','" + Yf_PurchaseOrder + "', '/res/icon/yf3.png', '', true, ''); ");
        }
        protected void Btn_Yf_Requisition_Click(object sender, EventArgs e)
        {
            string Yf_Requisition = global::Resources.GlobalResource.Yf_Requisition;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_Requisition','/Lf_Manufacturing/MM/Yf_Requisition.aspx','" + Yf_Requisition + "', '/res/icon/yf4.png', '', true, ''); ");
        }
        protected void Btn_Yf_PurchasingPrice_Click(object sender, EventArgs e)
        {
            string Yf_PurchasingPrice = global::Resources.GlobalResource.Yf_PurchasingPrice;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_PurchasingPrice','/Lf_Manufacturing/MM/Yf_PurchasingPrice.aspx','" + Yf_PurchasingPrice + "', '/res/icon/yf5.png', '', true, ''); ");
        }
        protected void Btn_Yf_PurchasingPriceVal_Click(object sender, EventArgs e)
        {
            string Yf_PurchasingPriceVal = global::Resources.GlobalResource.Yf_PurchasingPriceVal;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_PurchasingPriceVal','/Lf_Manufacturing/MM/Yf_PurchasingPriceVal.aspx','" + Yf_PurchasingPriceVal + "', '/res/icon/yf6.png', '', true, ''); ");
        }
        protected void Btn_Yf_Supplier_Click(object sender, EventArgs e)
        {
            string Yf_Supplier = global::Resources.GlobalResource.Yf_Supplier;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_Supplier','/Lf_Manufacturing/MM/Yf_Suppliers.aspx','" + Yf_Supplier + "', '/res/icon/yf7.png', '', true, ''); ");
        }
        protected void Btn_Yf_MaterialSupplier_Click(object sender, EventArgs e)
        {
            string Yf_MaterialSupplier = global::Resources.GlobalResource.Yf_MaterialSupplier;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_MaterialSupplier','/Lf_Manufacturing/MM/Yf_MaterialSupplier.aspx','" + Yf_MaterialSupplier + "', '/res/icon/yf8.png', '', true, ''); ");
        }
        protected void Btn_Yf_change_Click(object sender, EventArgs e)
        {
            string Yf_Ec = global::Resources.GlobalResource.Yf_Ec;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_Ec','/Lf_Manufacturing/MM/Yf_EC.aspx','" + Yf_Ec + "', '/res/icon/yf7.png', '', true, ''); ");
        }
        protected void Btn_Yf_replace_Click(object sender, EventArgs e)
        {
            string Yf_Substitute = global::Resources.GlobalResource.Yf_Substitute;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_Substitute','/Lf_Manufacturing/MM/Yf_Substitute.aspx','" + Yf_Substitute + "', '/res/icon/yf8.png', '', true, ''); ");
        }
        protected void Btn_Yf_Special_Material_Click(object sender, EventArgs e)
        {
            string Yf_Special = global::Resources.GlobalResource.Yf_Special;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_Special','/Lf_Manufacturing/MM/Yf_Special_Material.aspx','" + Yf_Special + "', '/res/icon/yf8.png', '', true, ''); ");
        }
        protected void Btn_Yf_General_Material_Click(object sender, EventArgs e)
        {
            string Yf_General = global::Resources.GlobalResource.Yf_General;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_General','/Lf_Manufacturing/MM/Yf_General_Material.aspx','" + Yf_General + "', '/res/icon/yf8.png', '', true, ''); ");
        }
        protected void Btn_Shipment_Click(object sender, EventArgs e)
        {
            string Yf_Shipment = global::Resources.GlobalResource.Yf_Shipment;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_Shipment','/Lf_Manufacturing/MM/Yf_ShipmentOrder.aspx','" + Yf_Shipment + "', '/res/icon/yf8.png', '', true, ''); ");
        }
        protected void Btn_SalesOrder_Click(object sender, EventArgs e)
        {
            string Yf_Sales = global::Resources.GlobalResource.Yf_Sales;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_Sales','/Lf_Manufacturing/MM/Yf_SalesOrder.aspx','" + Yf_Sales + "', '/res/icon/yf8.png', '', true, ''); ");
        }
        protected void Btn_SalesSlip_Click(object sender, EventArgs e)
        {
            string Yf_Slip = global::Resources.GlobalResource.Yf_Slip;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_Slip','/Lf_Manufacturing/MM/Yf_SalesSlip.aspx','" + Yf_Slip + "', '/res/icon/yf8.png', '', true, ''); ");
        }
        protected void Btn_Assets_Click(object sender, EventArgs e)
        {
            string Yf_Assets = global::Resources.GlobalResource.Yf_Assets;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_Assets','/Lf_Manufacturing/MM/Yf_Assets.aspx','" + Yf_Assets + "', '/res/icon/yf8.png', '', true, ''); ");
        }
        protected void Btn_Bo_Click(object sender, EventArgs e)
        {
            string Yf_StockOrder = global::Resources.GlobalResource.Yf_StockOrder;
            PageContext.RegisterStartupScript("top.addExampleTab('Yf_StockOrder','/Lf_Manufacturing/MM/Yf_StockOrder.aspx','" + Yf_StockOrder + "', '/res/icon/yf8.png', '', true, ''); ");
        }
        #endregion
    }
}
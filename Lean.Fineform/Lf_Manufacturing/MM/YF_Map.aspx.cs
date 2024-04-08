using System;
using FineUIPro;

namespace LeanFine.Lf_Manufacturing.MM
{
    public partial class YF_Map : PageBase
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
            CheckPowerWithButton("CoreMMView", Btn_yf_Material);
            CheckPowerWithButton("CoreMMView", Btn_yf_BOM);
            CheckPowerWithButton("CoreMMView", Btn_yf_PurchaseOrder);
            CheckPowerWithButton("CoreMMView", Btn_yf_Requisition);
            CheckPowerWithButton("CoreMMView", Btn_yf_PurchasingPrice);
            CheckPowerWithButton("CoreMMView", Btn_yf_PurchasingPriceVal);
            CheckPowerWithButton("CoreMMView", Btn_yf_Supplier);
            CheckPowerWithButton("CoreMMView", Btn_yf_MaterialSupplier);
            CheckPowerWithButton("CoreMMView", Btn_yf_change);
            CheckPowerWithButton("CoreMMView", Btn_yf_replace);
            CheckPowerWithButton("CoreMMView", Btn_yf_Special_Material);
            CheckPowerWithButton("CoreMMView", Btn_yf_General_Material);
            CheckPowerWithButton("CoreMMView", Btn_Shipment);
            CheckPowerWithButton("CoreMMView", Btn_SalesOrder);
            CheckPowerWithButton("CoreMMView", Btn_SalesSlip);
            CheckPowerWithButton("CoreMMView", Btn_Assets);
            CheckPowerWithButton("CoreMMView", Btn_Bo);
            //btnConfig.OnClientClick = Window1.GetShowReference("~/Lf_Admin/dept_new.aspx", "新增部门");
        }

        #region event

        protected void Btn_yf_Material_Click(object sender, EventArgs e)
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
            string yf_Material = global::Resources.GlobalResource.yf_Material;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_Material','/Lf_Manufacturing/MM/YF_Materials.aspx','" + yf_Material + "', '/res/icon/yf1.png', '', true, ''); ");
        }

        protected void Btn_yf_BOM_Click(object sender, EventArgs e)
        {
            string yf_BOM = global::Resources.GlobalResource.yf_BOM;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_BOM','/Lf_Manufacturing/MM/YF_BOM.aspx','" + yf_BOM + "', '/res/icon/yf2.png', '', true, ''); ");
        }

        protected void Btn_yf_PurchaseOrder_Click(object sender, EventArgs e)
        {
            string yf_PurchaseOrder = global::Resources.GlobalResource.yf_PurchaseOrder;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_PurchaseOrder','/Lf_Manufacturing/MM/YF_PurchaseOrder.aspx','" + yf_PurchaseOrder + "', '/res/icon/yf3.png', '', true, ''); ");
        }

        protected void Btn_yf_Requisition_Click(object sender, EventArgs e)
        {
            string yf_Requisition = global::Resources.GlobalResource.yf_Requisition;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_Requisition','/Lf_Manufacturing/MM/YF_Requisition.aspx','" + yf_Requisition + "', '/res/icon/yf4.png', '', true, ''); ");
        }

        protected void Btn_yf_PurchasingPrice_Click(object sender, EventArgs e)
        {
            string yf_PurchasingPrice = global::Resources.GlobalResource.yf_PurchasingPrice;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_PurchasingPrice','/Lf_Manufacturing/MM/YF_PurchasingPrice.aspx','" + yf_PurchasingPrice + "', '/res/icon/yf5.png', '', true, ''); ");
        }

        protected void Btn_yf_PurchasingPriceVal_Click(object sender, EventArgs e)
        {
            string yf_PurchasingPriceVal = global::Resources.GlobalResource.yf_PurchasingPriceVal;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_PurchasingPriceVal','/Lf_Manufacturing/MM/YF_PurchasingPriceVal.aspx','" + yf_PurchasingPriceVal + "', '/res/icon/yf6.png', '', true, ''); ");
        }

        protected void Btn_yf_Supplier_Click(object sender, EventArgs e)
        {
            string yf_Supplier = global::Resources.GlobalResource.yf_Supplier;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_Supplier','/Lf_Manufacturing/MM/YF_Suppliers.aspx','" + yf_Supplier + "', '/res/icon/yf7.png', '', true, ''); ");
        }

        protected void Btn_yf_MaterialSupplier_Click(object sender, EventArgs e)
        {
            string yf_MaterialSupplier = global::Resources.GlobalResource.yf_MaterialSupplier;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_MaterialSupplier','/Lf_Manufacturing/MM/YF_MaterialSupplier.aspx','" + yf_MaterialSupplier + "', '/res/icon/yf8.png', '', true, ''); ");
        }

        protected void Btn_yf_change_Click(object sender, EventArgs e)
        {
            string yf_Ec = global::Resources.GlobalResource.yf_Ec;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_Ec','/Lf_Manufacturing/MM/YF_EC.aspx','" + yf_Ec + "', '/res/icon/yf7.png', '', true, ''); ");
        }

        protected void Btn_yf_replace_Click(object sender, EventArgs e)
        {
            string yf_Substitute = global::Resources.GlobalResource.yf_Substitute;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_Substitute','/Lf_Manufacturing/MM/YF_Substitute.aspx','" + yf_Substitute + "', '/res/icon/yf8.png', '', true, ''); ");
        }

        protected void Btn_yf_Special_Material_Click(object sender, EventArgs e)
        {
            string yf_Special = global::Resources.GlobalResource.yf_Special;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_Special','/Lf_Manufacturing/MM/YF_Special_Material.aspx','" + yf_Special + "', '/res/icon/yf8.png', '', true, ''); ");
        }

        protected void Btn_yf_General_Material_Click(object sender, EventArgs e)
        {
            string yf_General = global::Resources.GlobalResource.yf_General;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_General','/Lf_Manufacturing/MM/YF_General_Material.aspx','" + yf_General + "', '/res/icon/yf8.png', '', true, ''); ");
        }

        protected void Btn_Shipment_Click(object sender, EventArgs e)
        {
            string yf_Shipment = global::Resources.GlobalResource.yf_Shipment;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_Shipment','/Lf_Manufacturing/MM/YF_ShipmentOrder.aspx','" + yf_Shipment + "', '/res/icon/yf8.png', '', true, ''); ");
        }

        protected void Btn_SalesOrder_Click(object sender, EventArgs e)
        {
            string yf_Sales = global::Resources.GlobalResource.yf_Sales;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_Sales','/Lf_Manufacturing/MM/YF_SalesOrder.aspx','" + yf_Sales + "', '/res/icon/yf8.png', '', true, ''); ");
        }

        protected void Btn_SalesSlip_Click(object sender, EventArgs e)
        {
            string yf_Slip = global::Resources.GlobalResource.yf_Slip;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_Slip','/Lf_Manufacturing/MM/YF_SalesSlip.aspx','" + yf_Slip + "', '/res/icon/yf8.png', '', true, ''); ");
        }

        protected void Btn_Assets_Click(object sender, EventArgs e)
        {
            string yf_Assets = global::Resources.GlobalResource.yf_Assets;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_Assets','/Lf_Manufacturing/MM/YF_Assets.aspx','" + yf_Assets + "', '/res/icon/yf8.png', '', true, ''); ");
        }

        protected void Btn_Bo_Click(object sender, EventArgs e)
        {
            string yf_StockOrder = global::Resources.GlobalResource.yf_StockOrder;
            PageContext.RegisterStartupScript("top.addExampleTab('yf_StockOrder','/Lf_Manufacturing/MM/YF_StockOrder.aspx','" + yf_StockOrder + "', '/res/icon/yf8.png', '', true, ''); ");
        }

        #endregion event
    }
}
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YF_Map.aspx.cs" Inherits="Fine.Lf_Manufacturing.MM.YF_Map" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <!-- reference your copy Font Awesome here (from our CDN or by hosting yourself) -->
    <link href="~/Lf_Resources/fontawesome/css/fontawesome.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/fontawesome.min.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/brands.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/solid.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/svg-with-js.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/v4-shims.css" rel="stylesheet" />
    <link href="~/Lf_Resources/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Lf_Resources/css/map.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" IsFluid="false" CssClass="blockpanel" runat="server" ShowBorder="false" EnableCollapse="false"
            BodyPadding="1px" ShowHeader="false" Title="" AutoScroll="false" Margin="1px">
            <Items>
                <f:ContentPanel runat="server" ShowHeader="false">
                    <div class="w-100" style="margin-bottom: 20px"></div>
                    <div class="container">
                        <div class="row row-cols-4 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_yf_Material" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf1.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_Material%>" OnClick="Btn_yf_Material_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_yf_BOM" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf8.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_BOM%>" OnClick="Btn_yf_BOM_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_yf_PurchaseOrder" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf2.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_PurchaseOrder%>" OnClick="Btn_yf_PurchaseOrder_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_yf_Requisition" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf3.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_Requisition%>" OnClick="Btn_yf_Requisition_Click">
                                </f:Button>
                            </div>

                        </div>
                        <div class="w-100" style="margin-bottom: 20px"></div>
                        <div class="row row-cols-4 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_yf_PurchasingPrice" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf4.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_PurchasingPrice%>" OnClick="Btn_yf_PurchasingPrice_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_yf_PurchasingPriceVal" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf5.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_PurchasingPriceVal%>" OnClick="Btn_yf_PurchasingPriceVal_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_yf_Supplier" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf6.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_Supplier%>" OnClick="Btn_yf_Supplier_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_yf_MaterialSupplier" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf7.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_MaterialSupplier%>" OnClick="Btn_yf_MaterialSupplier_Click">
                                </f:Button>
                            </div>
                        </div>
                        <div class="w-100" style="margin-bottom: 20px"></div>
                        <div class="row row-cols-4 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_yf_change" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf4.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_Ec%>" OnClick="Btn_yf_change_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_yf_replace" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf5.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_Substitute%>" OnClick="Btn_yf_replace_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_yf_Special_Material" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf6.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_Special%>" OnClick="Btn_yf_Special_Material_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_yf_General_Material" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf7.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_General%>" OnClick="Btn_yf_General_Material_Click">
                                </f:Button>
                            </div>

                        </div>
                        <div class="w-100" style="margin-bottom: 20px"></div>
                        <div class="row row-cols-4 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_Shipment" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf7.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_Shipment%>" OnClick="Btn_Shipment_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_SalesOrder" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf7.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_Sales%>" OnClick="Btn_SalesOrder_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_SalesSlip" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf7.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_Slip%>" OnClick="Btn_SalesSlip_Click">
                                </f:Button>
                            </div>
                            <div class="col icon-font">
                                <f:Button ID="Btn_Assets" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf7.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_Assets%>" OnClick="Btn_Assets_Click">
                                </f:Button>
                            </div>
                        </div>
                                                <div class="w-100" style="margin-bottom: 20px"></div>
                        <div class="row row-cols-4 align-self-center">
                            <div class="col icon-font">
                                <f:Button ID="Btn_Bo" EnablePostBack="true" IconAlign="Top" IconUrl="~/Lf_Resources/icon/yf7.png" runat="server"
                                    Text="<%$ Resources:GlobalResource,yf_StockOrder%>" OnClick="Btn_Bo_Click">
                                </f:Button>
                            </div>

                        </div>
                    </div>
                </f:ContentPanel>
            </Items>
        </f:Panel>
    </form>
</body>
</html>

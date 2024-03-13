<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="costing_demandqty.aspx.cs" Inherits="LeanFine.Lf_Accounting.costing_demandqty" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .mycontentpanel .f-panel-body > div {
            width: 100%;
            height: 100%;
        }

        #MaterialcostChart {
            height: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
        <f:Panel ID="Panel1" IsFluid="true" ShowBorder="false" ShowHeader="false" CssClass="blockpanel" runat="server" Title="表格与表单" Layout="VBox" Height="50%">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    ForceFit="true" EnableTextSelection="true" DataKeyNames="Bc_YM" AllowSorting="true"
                    OnSort="Grid1_Sort" SortField="Bc_YM" SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true"
                    OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:DatePicker ID="DPend" Readonly="false" DateFormatString="yyyyMM" AutoPostBack="true"
                                    Label="<%$ Resources:GlobalResource,Query_Select_Date%>"
                                    runat="server" ShowRedStar="True" OnTextChanged="DPend_TextChanged">
                                </f:DatePicker>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
                                <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnExport_Click" CssClass="marginr">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:BoundField DataField="Bc_YM" HeaderText="日期" />
                        <f:BoundField DataField="Bc_Material" HeaderText="品目" />
                        <f:BoundField DataField="Bc_PurchaseGroup" HeaderText="PGr" />
                        <f:BoundField DataField="Bc_AvailableQty" HeaderText="可用数量" />
                        <f:BoundField DataField="Bc_PurchaseOrder" HeaderText="発注_Qty" />
                        <f:BoundField DataField="Bc_RequestQty" HeaderText="購買依頼_Qty" />
                        <f:BoundField DataField="Bc_PlannedQty" HeaderText="計画手配_Qty" />
                        <f:BoundField DataField="Bc_InventoryQty" HeaderText="在庫_Qty" />
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
    </form>

</body>
</html>

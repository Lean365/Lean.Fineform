<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="costing_monthly_bom_cost.aspx.cs" Inherits="LeanFine.Lf_Accounting.costing_monthly_bom_cost" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
        <f:Panel ID="Panel1" IsFluid="true" ShowBorder="false" ShowHeader="false" CssClass="blockpanel" runat="server" Title="" Layout="VBox" Height="50%">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    ForceFit="true" EnableTextSelection="true" DataKeyNames="Bc_Item" AllowSorting="true"
                    OnSort="Grid1_Sort" SortField="Bc_Item" SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true"
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
                        <f:BoundField DataField="Bc_Item" HeaderText="品目" />
                        <f:BoundField DataField="Bc_ItemText" HeaderText="品目Text" />
                        <f:BoundField DataField="Bc_MovingCost" HeaderText="材料费" />
                        <f:BoundField DataField="Bc_MovingAverage" HeaderText="移动平均" DataFormatString="{0:F5}" />
                        <f:BoundField DataField="Bc_Diff" HeaderText="生产成本" DataFormatString="{0:F5}" />
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
    </form>

</body>
</html>

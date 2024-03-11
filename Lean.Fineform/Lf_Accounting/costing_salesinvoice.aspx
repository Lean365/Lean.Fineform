<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="costing_salesinvoice.aspx.cs" Inherits="Fine.Lf_Accounting.costing_salesinvoice" %>

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

        #SalesChart {
            height: 100%;
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
        <f:Panel ID="Panel1" IsFluid="true" ShowBorder="false" ShowHeader="false" CssClass="blockpanel" runat="server" Title="" Layout="VBox" Height="50%">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    ForceFit="true" EnableTextSelection="true" DataKeyNames="Bc_ProfitCenter" AllowSorting="true" EnableSummary="true" SummaryPosition="Bottom"
                    OnSort="Grid1_Sort" SortField="Bc_ProfitCenter" SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true"
                    OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:DatePicker ID="DPend" Readonly="false" DateFormatString="yyyyMM" AutoPostBack="true"
                                    Label="<%$ Resources:GlobalResource,Query_Select_Date%>"
                                    runat="server" ShowRedStar="True" OnTextChanged="DPend_TextChanged">
                                </f:DatePicker>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                <f:RadioButton ID="rbtnFirstAuto" Label="" Checked="true" GroupName="MyRadioGroup2" Text="<%$ Resources:GlobalResource,rpt_Stutas_Total%>" runat="server"
                                    OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                </f:RadioButton>
                                <f:RadioButton ID="rbtnSecondAuto" GroupName="MyRadioGroup2" ShowEmptyLabel="true" Text="<%$ Resources:GlobalResource,rpt_Status_Details%>" runat="server"
                                    OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                </f:RadioButton>
                                <f:ToolbarFill ID="ToolbarFill3" runat="server"></f:ToolbarFill>

                                <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnExport_Click" CssClass="marginr">
                                </f:Button>

                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:BoundField DataField="Bc_SalesItem" HeaderText="物料" />
                        <f:BoundField DataField="Bc_SalesItemText" HeaderText="物料Text" />
                        <f:BoundField DataField="Bc_ProfitCenter" HeaderText="BuCode" />
                        <f:BoundField DataField="Bc_SalesQty" HeaderText="销售量" ColumnID="Bc_SalesQty" />
                        <f:BoundField DataField="Bc_BusinessAmount" HeaderText="销售额" ColumnID="Bc_BusinessAmount" />
                        <f:BoundField DataField="Bc_MovingCost" HeaderText="材料成本" />
                        <f:BoundField DataField="Materialfee" HeaderText="材料费" ColumnID="Materialfee" />
                        <f:BoundField DataField="Materialrate" HeaderText="材料费比率" ColumnID="Materialrate" DataFormatString="{0:p2}" />
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
    </form>

</body>
</html>

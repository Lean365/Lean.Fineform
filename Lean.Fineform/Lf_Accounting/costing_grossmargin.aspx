<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="costing_grossmargin.aspx.cs" Inherits="Lean.Fineform.Lf_Accounting.costing_grossmargin" %>

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
            width: 100%;
        }
    </style>
    
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
        <f:Panel ID="Panel1" IsFluid="true" ShowBorder="false" ShowHeader="false" CssClass="blockpanel" runat="server" Layout="VBox">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    ForceFit="true" EnableTextSelection="true" DataKeyNames="Bc_ForecastItem" AllowSorting="true"
                    OnSort="Grid1_Sort" SortField="Bc_ForecastItem" SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true"
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
                        <f:BoundField DataField="Bc_ForecastItem" HeaderText="品目" />
                        <%--<f:BoundField DataField="Bc_ForecastItemText" HeaderText="品目Text" />--%>
                        <f:BoundField DataField="Bc_ForecastModelName" HeaderText="机种名" />
                        <%--<f:BoundField DataField="Bc_Discontinued" HeaderText="停产" />--%>
                        <%--<f:BoundField DataField="Bc_TcjFob" HeaderText="TCJ" />--%>
                        <%--<f:BoundField DataField="Bc_DtaFob" HeaderText="DTA" />--%>
                        <%--<f:BoundField DataField="Bc_MovingAverage" HeaderText="移动平均" />--%>
                        <%--<f:BoundField DataField="Bc_ExchangeRate" HeaderText="汇率" />--%>
                        <f:BoundField DataField="DTA_GrossProfit" HeaderText="DTA毛利" DataFormatString="{0:F5}"/>
                        <f:BoundField DataField="DTA_GrossProfitRate" HeaderText="DTA毛利率" DataFormatString="{0:p2}"/>
                        <%--<f:BoundField DataField="TAC_FOB" HeaderText="TAC" DataFormatString="{0:F5}"/>--%>
                        <f:BoundField DataField="TAC_GrossProfit" HeaderText="TAC毛利" DataFormatString="{0:F5}"/>
                        <f:BoundField DataField="TAC_GrossProfitRate" HeaderText="TAC毛利率" DataFormatString="{0:p2}"/>
                        <f:BoundField DataField="DTA_TAC_GrossProfit" HeaderText="DTA_TAC毛利" DataFormatString="{0:F5}"/>
                        <f:BoundField DataField="DTA_TAC_GrossProfitRate" HeaderText="DTA_TAC毛利率"  DataFormatString="{0:p2}"/>
                    </Columns>
                </f:Grid>

            </Items>
        </f:Panel>
    </form>
        
</body>
</html>

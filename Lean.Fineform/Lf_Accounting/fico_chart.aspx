<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Fico_chart.aspx.cs" Inherits="Fine.Lf_Accounting.Fico_chart" %>

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

        #TreeBar {
            height: 100%;
            width: 100%;
        }
    </style>
    <%--<script src="/res/js/jquery.min.js" type="text/javascript"></script>--%>
    <%--jquery.js--%>
    <script type="text/javascript" src="/Lf_Resources/echarts/package/dist/echarts.min.js"></script>
    <script src="/Lf_Report/ajax_Fico_data.js" type="text/javascript"></script>
    <script src="/Lf_Report/ajax_Pp_data.js" type="text/javascript"></script>
    <script src="/Lf_Report/ajax_Sd_data.js" type="text/javascript"></script>
    <script src="/Lf_Report/ajax_Qm_data.js" type="text/javascript"></script>
    <script src="/Lf_Report/bind_Fico_echarts.js" type="text/javascript"></script>
    <script src="/Lf_Report/bind_Pp_echarts.js" type="text/javascript"></script>
    <script src="/Lf_Report/bind_Qm_echarts.js" type="text/javascript"></script>
    <script src="/Lf_Report/bind_Sd_echarts.js" type="text/javascript"></script>
    <%-- Echarts.js --%>

    <%--引用的css样式--%>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager2" AutoSizePanelID="TabStrip1" runat="server" />
        <f:TabStrip ID="TabStrip1" CssClass="mytabstrip" ShowInkBar="true" runat="server" ShowBorder="true" ActiveTabIndex="0">
            <Tabs>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Chart_Fico_Expense%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:Panel ID="Panel2" IsFluid="true" CssClass="blockpanel" runat="server" ShowBorder="false" Margin="00px"
                            Layout="VBox" ShowHeader="false" Title="" BodyPadding="0">
                            <Items>
                                <f:Panel ID="Panel3" BoxFlex="1" runat="server" ShowBorder="false" ShowHeader="false" Title="<%$ Resources:GlobalResource,MainTitle1%>" EnableIFrame="true" IFrameUrl="~/Lf_Accounting/expense.aspx">
                                </f:Panel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,sys_Charts%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:Panel ID="Panel1" IsFluid="true" CssClass="blockpanel" runat="server" ShowBorder="false" Margin="00px"
                            Layout="VBox" ShowHeader="false" Title="" BodyPadding="0">
                            <Items>
                                <f:Panel ID="Panel4" BoxFlex="1" runat="server" ShowBorder="false" ShowHeader="false" Title="<%$ Resources:GlobalResource,MainTitle1%>" EnableIFrame="true" IFrameUrl="~/Lf_Accounting/expense_contrast.aspx">
                                </f:Panel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Charts_Elements%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:ContentPanel runat="server" CssClass="mycontentpanel" ShowBorder="false" ShowHeader="false">
                            <div id="TreeBar">
                            </div>
                        </f:ContentPanel>
                    </Items>
                </f:Tab>
            </Tabs>
        </f:TabStrip>
    </form>
        <script type="text/javascript">
        var TabStrip1ClientID = '<%= TabStrip1.ClientID %>';
        // 更新选项卡中的图表
        function updateChartInTabStrip() {
            var tabIndex = F(TabStrip1ClientID).getActiveTabIndex();
            if (tabIndex === 2) {
                AjaxData_Expense_Tree();
            } 
        }

        F.ready(function () {            

            AjaxData_Expense_Tree();
            // 选项卡切换或者窗体大小改变时，更新当前选项卡内的图表
            F(TabStrip1ClientID).on('tabchange', function (event, tab) {
                updateChartInTabStrip();
            });
            F.windowResize(function () {
                updateChartInTabStrip();
            });

        });

    </script>
</body>
</html>

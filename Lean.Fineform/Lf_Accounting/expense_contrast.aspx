<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="expense_contrast.aspx.cs" Inherits="LeanFine.Lf_Accounting.expense_contrast" %>

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

        #ContrastBar {
            height: 100%;
            width: 100%;
        }

        #RequisitionBar {
            height: 100%;
            width: 100%;
        }

        #ActualExs {
            height: 100%;
            width: 100%;
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
            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                        <f:DatePicker ID="DPend" Readonly="false" DateFormatString="yyyyMM" AutoPostBack="true"
                            Label="<%$ Resources:GlobalResource,Query_Select_Date%>"
                            runat="server" ShowRedStar="True">
                        </f:DatePicker>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                    </Items>
                </f:Toolbar>

            </Toolbars>
            <Tabs>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Charts_Contrast%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:ContentPanel runat="server" CssClass="mycontentpanel" ShowBorder="false" ShowHeader="false">
                            <div id="ContrastBar">
                            </div>
                        </f:ContentPanel>

                    </Items>
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Charts_Requisition%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:ContentPanel runat="server" CssClass="mycontentpanel" ShowBorder="false" ShowHeader="false">
                            <div id="RequisitionBar">
                            </div>
                        </f:ContentPanel>
                    </Items>
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Charts_ActualExs%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:ContentPanel runat="server" CssClass="mycontentpanel" ShowBorder="false" ShowHeader="false">
                            <div id="ActualExs">
                            </div>
                        </f:ContentPanel>
                    </Items>
                </f:Tab>
            </Tabs>
        </f:TabStrip>

    </form>

    <script type="text/javascript">
        
        var TabStrip1ClientID = '<%= TabStrip1.ClientID %>';

        // 定义变量：日期（格式：202001）
        var TransDates = null;

        // 更新选项卡中的图表
        function updateChartInTabStrip() {

            var tabIndex = F(TabStrip1ClientID).getActiveTabIndex();
            if (tabIndex === 0) {
                AjaxData_Expense_Contrast();
            }
            else if (tabIndex === 1) {
                AjaxData_Expense_DeptRequests();
            }
            else if (tabIndex === 2) {
                AjaxData_Expense_Actual();
            }
        }
        //debugger;
        F.ready(function () {
            var DPendID = '<%= DPend.ClientID %>';

            //默认日期
            //F(DPendID).getText().substring(0, 4) + '年' + F(DPendID).getText().substring(4, 6) + '月'
            TransDates = F(DPendID).getText();

            //TabStrip1ClientID默认选择ActiveTabIndex=0
            //默认图表显示
            AjaxData_Expense_Contrast();

            //DatePicker选择事件
            F(DPendID).on('change', function () {
                //debugger;
                TransDates = F(DPendID).getText(); //文本失去光标时
                updateChartInTabStrip();

            });
            //DatePicker离开事件
            //F(DPendID).on('blur', function () {
            //    TransDates = F(DPendID).getText(); //文本失去光标时
            //    updateChartInTabStrip();
            //});


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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qm_chart.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.QM.qm_chart" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <style>
        .mycontentpanel .f-panel-body > div {
            width: 100%;
            height: 100%;
        }


        #PassLine {
            height: 100%;
            width: 100%;
        }

        #ReasonSunburst {
            height: 100%;
            width: 100%;
        }

        #AchieveLine {
            height: 100%;
            width: 100%;
        }



        #ActualBar {
            height: 100%;
            width: 100%;
        }

        #DefectBar {
            height: 100%;
            width: 100%;
        }

        #DirectLine {
            height: 100%;
            width: 100%;
        }

        #LotpassLine {
            height: 100%;
            width: 100%;
        }


        #ProgressBar {
            height: 100%;
            width: 100%;
        }
    </style>
    <%--<script src="/res/js/jquery.min.js" type="text/javascript"></script>--%>
    <%--jquery.js--%>
    <script type="text/javascript" src="/Lf_Resources/echarts/package/dist/echarts.min.js"></script>
    <script src="/Lf_Report/ajax_Fico_data.js" type="text/javascript"></script>
    <script src="/Lf_Report/ajax_pp_data.js" type="text/javascript"></script>
    <script src="/Lf_Report/ajax_Sd_data.js" type="text/javascript"></script>
    <script src="/Lf_Report/ajax_qm_data.js" type="text/javascript"></script>
    <script src="/Lf_Report/bind_Fico_echarts.js" type="text/javascript"></script>
    <script src="/Lf_Report/bind_pp_echarts.js" type="text/javascript"></script>
    <script src="/Lf_Report/bind_qm_echarts.js" type="text/javascript"></script>
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
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Charts_Defect%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:ContentPanel runat="server" CssClass="mycontentpanel" ShowBorder="false" ShowHeader="false">
                            <div id="DefectBar">
                            </div>

                        </f:ContentPanel>
                    </Items>
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Charts_Reason%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:ContentPanel runat="server" CssClass="mycontentpanel" ShowBorder="false" ShowHeader="false">
                            <div id="ReasonSunburst">
                            </div>
                        </f:ContentPanel>
                    </Items>
                </f:Tab>


                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Charts_Direct%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:ContentPanel runat="server" CssClass="mycontentpanel" ShowBorder="false" ShowHeader="false">
                            <div id="DirectLine">
                            </div>
                        </f:ContentPanel>
                    </Items>
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Charts_Achieve%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:ContentPanel runat="server" CssClass="mycontentpanel" ShowBorder="false" ShowHeader="false">
                            <div id="AchieveLine">
                            </div>
                        </f:ContentPanel>
                    </Items>
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Charts_Pass%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:ContentPanel runat="server" CssClass="mycontentpanel" ShowBorder="false" ShowHeader="false">
                            <div id="PassLine">
                            </div>
                        </f:ContentPanel>
                    </Items>
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Charts_LotPass%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:ContentPanel runat="server" CssClass="mycontentpanel" ShowBorder="false" ShowHeader="false">
                            <div id="LotpassLine">
                            </div>
                        </f:ContentPanel>
                    </Items>
                </f:Tab>                
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Charts_Progress%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:ContentPanel runat="server" CssClass="mycontentpanel" ShowBorder="false" ShowHeader="false">
                            <div id="ProgressBar">
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
                AjaxData_Pp_Defect();
            } else if (tabIndex === 1) {
                AjaxData_Pp_Reason();
            } else if (tabIndex === 2) {
                AjaxData_Pp_Direct();
            } else if (tabIndex === 3) {
                AjaxData_Pp_Achieve();
            } else if (tabIndex === 4) {
                AjaxData_Qm_Pass();
            } else if (tabIndex === 5) {
                AjaxData_LotPass();
            } else if (tabIndex === 6) {
                AjaxData_Pp_Progress();
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
            AjaxData_Pp_Defect();

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


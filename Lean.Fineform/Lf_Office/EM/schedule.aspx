<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="schedule.aspx.cs" Inherits="Lean.Fineform.Lf_Office.EM.schedule" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">

<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>日程管理</title>
    <style type="text/css">
        .LabDateCss .x-form-display-field-default {
            line-height: 23px;
        }

        .LabDateCss .x-form-arrow-trigger-default {
            padding-top: 6px;
        }

        .LabDateCss span {
            font-size: 25px;
            font-weight: 500;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="RegionPanel1" AjaxTimeout="30000"
            EnablePageLoading="True" EnableAjaxLoading="False" />
        <f:RegionPanel ID="RegionPanel1" ShowBorder="false" runat="server">
            <Regions>
                <f:Region ID="mainRegion" ShowHeader="false" Layout="Fit" ShowBorder="true" Position="Center"
                    runat="server">
                    <Items>
                        <f:TabStrip ID="TabStrip1" ShowBorder="false" TabPosition="Top" EnableTabCloseMenu="false"
                            ActiveTabIndex="0" runat="server">
                            <Tabs>
                                <f:Tab Title="事件日历" ID="tab_Schedule_full" runat="server" EnableIFrame="true" IFrameUrl="schedule_full.aspx">
                                    <Items>
                                    </Items>
                                </f:Tab>
                                <f:Tab Title="事件日志" ID="tab_Task_full" runat="server" EnableIFrame="true" IFrameUrl="event.aspx">
                                    <Items>
                                    </Items>
                                </f:Tab>
                            </Tabs>
                        </f:TabStrip>
                    </Items>
                </f:Region>
            </Regions>
        </f:RegionPanel>
    </form>

</body>
</html>


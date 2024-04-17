<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p2d_output_line.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.daily.P2D.p2d_output_line" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .x-grid-row-summary .x-grid-cell-inner {
            font-weight: bold;
            color: red;
        }

        .x-grid-row-summary .x-grid-cell {
            background-color: #fff !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="生产日报OPH">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ForceFit="true" ShowBorder="true" ShowHeader="false" EnableTextSelection="true"
                    EnableCheckBoxSelect="true"
                    DataKeyNames="Prodate" AllowSorting="true" OnSort="Grid1_Sort" SortField="Prodate"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound" EnableSummary="true" SummaryPosition="Bottom"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar runat="server">
                            <Items>
                                <f:DatePicker runat="server" Label="<%$ Resources:GlobalResource,Query_Startdate%>" DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Startdate_EmptyText%>" AutoPostBack="true"
                                    ID="DpStartDate" ShowRedStar="True" OnTextChanged="DpStartDate_TextChanged">
                                </f:DatePicker>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                </f:ToolbarFill>
                                <f:DatePicker ID="DpEndDate" Readonly="false" Width="300px" CompareControl="DpStartDate" DateFormatString="yyyyMMdd" AutoPostBack="true"
                                    CompareOperator="GreaterThan" CompareMessage="<%$ Resources:GlobalResource,Query_Enddate_EmptyText%>" Label="<%$ Resources:GlobalResource,Query_Enddate%>"
                                    runat="server" ShowRedStar="True" OnTextChanged="DpEndDate_TextChanged">
                                </f:DatePicker>
                                <f:ToolbarFill ID="ToolbarFill3" runat="server">
                                </f:ToolbarFill>
                                <f:DropDownList ID="DdlLine" Label="<%$ Resources:GlobalResource,Query_Pp_Line%>" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DdlLine_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:ToolbarFill ID="ToolbarFill4" runat="server">
                                </f:ToolbarFill>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Pp_EmptyText%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>

                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_SheetNormal%>" OnClick="BtnExport_Click" CssClass="marginr">
                                </f:Button>
                                <f:Button ID="BtnRepair" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_SheetRework%>" OnClick="BtnRepair_Click" CssClass="marginr">
                                </f:Button>

                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>">
                        </f:ToolbarText>
                        <f:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged"
                            runat="server">
                            <f:ListItem Text="1000" Value="1000" />
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                        <f:BoundField DataField="Prolinename" SortField="Prolinename" Width="100px" HeaderText="班别" />
                        <f:BoundField DataField="Prodate" SortField="Prodate" Width="100px" HeaderText="日期" />
                        <f:BoundField DataField="Prolot" SortField="Prolot" Width="100px" HeaderText="批次" />
                        <f:BoundField DataField="Promodel" SortField="Promodel" Width="100px" HeaderText="机种" />
                        <f:BoundField DataField="Propcbatype" SortField="Propcbatype" Width="100px" HeaderText="生产板别" />
                        <f:BoundField DataField="Propcbaside" SortField="Propcbaside" Width="100px" HeaderText="多面板" />

                        <f:BoundField DataField="Prorealqty" SortField="Prorealqty" ColumnID="Prorealqty" Width="80px" HeaderText="实绩" DataFormatString="{0:F0}" />
                        <f:BoundField DataField="Prorealtotal" SortField="Prorealtotal" ColumnID="Prorealtotal" Width="80px" HeaderText="累计生产" DataFormatString="{0:F0}" />
                        <f:BoundField DataField="Protime" SortField="Protime" ColumnID="Protime" Width="80px" HeaderText="生产工数" DataFormatString="{0:F0}" />
                        <f:BoundField DataField="Prohandoffnum" SortField="Prohandoffnum" ColumnID="Prohandoffnum" Width="80px" HeaderText="切换次数" />
                        <f:BoundField DataField="Prohandofftime" SortField="Prohandofftime" ColumnID="Prohandofftime" HeaderText="切换时间" DataFormatString="{0:F0}" />
                        <f:BoundField DataField="Prodowntime" SortField="Prodowntime" ColumnID="Prodowntime" HeaderText="切停机时间" DataFormatString="{0:F0}" />
                        <f:BoundField DataField="Prolosstime" SortField="Prolosstime" ColumnID="Prolosstime" HeaderText="损失工数" DataFormatString="{0:F0}" />
                        <f:BoundField DataField="Promaketime" SortField="Promaketime" ColumnID="Promaketime" HeaderText="投入工数" DataFormatString="{0:F0}" />



                    </Columns>
                </f:Grid>
                <f:HiddenField runat="server" ID="hfGrid1Summary"></f:HiddenField>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="800px"
            Height="500px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>


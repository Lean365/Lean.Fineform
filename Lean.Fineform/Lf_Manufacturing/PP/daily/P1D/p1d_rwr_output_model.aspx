<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p1d_rwr_output_model.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.daily.p1d_rpr_output_model" %>

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
            ShowBorder="false" Layout="Fit" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
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
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Rpr%>" OnClick="BtnExport_Click" CssClass="marginr">
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

                        <f:BoundField DataField="Prodate" SortField="Prodate" Width="100px" HeaderText="日期" />
                        <f:BoundField DataField="Promodel" SortField="Promodel" Width="100px" HeaderText="物料" />
                        <f:BoundField DataField="Prost" SortField="Prost" Width="100px" HeaderText="ST" DataFormatString="{0:F2}" />
                        <f:BoundField DataField="Proplanqty" SortField="Proplanqty" ColumnID="Proplanqty" Width="100px" HeaderText="计划台数" DataFormatString="{0:F2}" />
                        <f:BoundField DataField="Proworktime" SortField="Proworktime" ColumnID="Proworktime" Width="100px" HeaderText="投入工时" DataFormatString="{0:F0}" />
                        <f:BoundField DataField="Proworkqty" SortField="Proworkqty" ColumnID="Proworkqty" Width="100px" HeaderText="实绩台数" DataFormatString="{0:F0}" />
                        <f:BoundField DataField="Prodirect" SortField="Prodirect" Width="100px" HeaderText="直接人员" DataFormatString="{0:F0}" />
                        <f:BoundField DataField="Proworkst" SortField="Proworkst" Width="100px" HeaderText="实绩ST" DataFormatString="{0:F2}" />
                        <f:BoundField DataField="Prodiffst" SortField="Prodiffst" Width="100px" HeaderText="ST差异" DataFormatString="{0:F2}" />
                        <f:BoundField DataField="Prodiffqty" SortField="Prodiffqty" Width="100px" HeaderText="台数差异" DataFormatString="{0:F2}" />
                        <f:BoundField DataField="Proactivratio" SortField="Proactivratio" ColumnID="Proactivratio" Width="100px" HeaderText="达成率" DataFormatString="{0:p0}" />


                    </Columns>
                </f:Grid>
                <f:HiddenField runat="server" ID="hfGrid1Summary"></f:HiddenField>
            </Items>
        </f:Panel>

    </form>
</body>
</html>


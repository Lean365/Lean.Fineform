<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p2d_manufacturing_defect_monthly.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.poor.p2d_manufacturing_defect_monthly" %>

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

        .color1 {
            background-color: #0094ff;
            color: #fff;
        }

        .color2 {
            background-color: #0026ff;
            color: #fff;
        }

        .color3 {
            background-color: #b200ff;
            color: #fff;
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
                <f:Form ID="Form2" runat="server" Height="36px" BodyPadding="5px" ShowHeader="false"
                    ShowBorder="false" LabelAlign="Right">
                    <Rows>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ForceFit="true" ShowBorder="true" ShowHeader="false" EnableTextSelection="true"
                    EnableCheckBoxSelect="true"
                    DataKeyNames="Prodate,Prolot" AllowSorting="true" SortField="Prodate"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true"
                    OnSort="Grid1_Sort"
                    OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound"
                    EnableSummary="true"
                    SummaryPosition="Bottom"
                    OnRowCommand="Grid1_RowCommand"
                    OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:DatePicker ID="DpEndDate" Readonly="false" DateFormatString="yyyyMM" AutoPostBack="true"
                                    Label="<%$ Resources:GlobalResource,Query_Select_Date%>"
                                    runat="server" ShowRedStar="True" OnTextChanged="DpEndDate_TextChanged">
                                </f:DatePicker>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Pp_EmptyText%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnExport_Click" CssClass="marginr">
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
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="50" Value="50" />
                            <f:ListItem Text="100" Value="100" />
                            <f:ListItem Text="300" Value="300" />
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                        <f:BoundField DataField="Prodate" ExpandUnusedSpace="true" SortField="Prodate" MinWidth="100px" HeaderText="生产日期" />
                        <f:BoundField DataField="Promodel" ExpandUnusedSpace="true" SortField="Promodel" MinWidth="100px" HeaderText="生产机种" />
                        <f:BoundField DataField="Proorder" ExpandUnusedSpace="true" SortField="Proorder" MinWidth="100px" HeaderText="生产订单" />
                        <f:BoundField DataField="Prolot" ExpandUnusedSpace="true" SortField="Prolot" MinWidth="100px" HeaderText="生产LOT" />
                        <f:BoundField DataField="Proorderqty" ExpandUnusedSpace="true" SortField="Proorderqty" MinWidth="100px" HeaderText="订单台数" />
                        <f:BoundField DataField="Propcbtype" ExpandUnusedSpace="true" SortField="Propcbtype" MinWidth="100px" HeaderText="板别" />
                        <f:BoundField DataField="Prorealqty" ExpandUnusedSpace="true" SortField="Prorealqty" MinWidth="100px" HeaderText="生产实绩" />
                        <f:BoundField DataField="Prolinename" ExpandUnusedSpace="true" SortField="Prolinename" MinWidth="100px" HeaderText="生产组别" />
                        <f:BoundField DataField="Propcbcardno" ExpandUnusedSpace="true" SortField="Propcbcardno" MinWidth="100px" HeaderText="卡号" />
                        <f:BoundField DataField="Probadnote" ExpandUnusedSpace="true" SortField="Probadnote" MinWidth="100px" HeaderText="不良症状" />
                        <f:BoundField DataField="Propcbcheckout" ExpandUnusedSpace="true" SortField="Propcbcheckout" MinWidth="100px" HeaderText="检出工程" />
                        <f:BoundField DataField="Probadreason" ExpandUnusedSpace="true" SortField="Probadreason" MinWidth="100px" HeaderText="不良原因" />
                        <f:BoundField DataField="Probadqty" ExpandUnusedSpace="true" SortField="Probadqty" MinWidth="100px" HeaderText="不良数量" />
                        <f:BoundField DataField="Probadtotal" ExpandUnusedSpace="true" SortField="Probadtotal" MinWidth="100px" HeaderText="不良台数" />
                        <f:BoundField DataField="Probadresponsibility" ExpandUnusedSpace="true" SortField="Probadresponsibility" MinWidth="100px" HeaderText="责任归属" />
                        <f:BoundField DataField="Probadprop" ExpandUnusedSpace="true" SortField="Probadprop" MinWidth="100px" HeaderText="不良性质" />
                        <f:BoundField DataField="Probadserial" ExpandUnusedSpace="true" SortField="Probadserial" MinWidth="100px" HeaderText="序列号" />
                        <f:BoundField DataField="Probadrepairman" ExpandUnusedSpace="true" SortField="Probadrepairman" MinWidth="100px" HeaderText="修理" />









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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="operatelog.aspx.cs" Inherits="Fine.Lf_Admin.operatelog" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="日志管理">
            <Items>
                <f:Form ID="Form2" runat="server" Height="36px" BodyPadding="5px" ShowHeader="false"
                    ShowBorder="false" LabelAlign="Right">
                    <Rows>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Enter_Text%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                                <f:DropDownList ID="ddlSearchOperateUser" runat="server" Label="搜索用户" EnableEdit="true" AutoPostBack="true" ForceSelection="false"
                                    OnSelectedIndexChanged="ddlSearchOperateUser_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:DropDownList ID="ddlSearchRange" runat="server" Label="搜索范围" EnableEdit="true" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSearchRange_SelectedIndexChanged">
                                    <f:ListItem Text="ALL" Value="ALL" />
                                    <f:ListItem Text="今天" Value="TODAY" Selected="true" />
                                    <f:ListItem Text="最近三天" Value="LAST3DAYS" />
                                    <f:ListItem Text="最近七天" Value="LAST7DAYS" />
                                    <f:ListItem Text="最近一个月" Value="LASTMONTH" />
                                    <f:ListItem Text="最近一年" Value="LASTYEAR" />
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" DataKeyNames="GUID" AllowSorting="true" ForceFit="true" EnableTextSelection="true"
                    OnSort="Grid1_Sort" SortField="OperateTime" SortDirection="DESC" AllowPaging="true"
                    IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound" OnPageIndexChange="Grid1_PageIndexChange"
                    OnRowCommand="Grid1_RowCommand">
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator3" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>">
                        </f:ToolbarText>
                        <f:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="ddlGridPageSize_SelectedIndexChanged"
                            runat="server">
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="50" Value="50" />
                            <f:ListItem Text="100" Value="100" />
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                        <f:RowNumberField />
                        <f:BoundField DataField="OperateTime" SortField="OperateTime" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                            Width="120px" MinWidth="120px" HeaderText="时间" />
                        <f:BoundField DataField="OperateUserName" SortField="OperateUserName" Width="100px" MinWidth="100px" HeaderText="用户" />
                        <f:BoundField DataField="OperatePowers" SortField="OperatePowers" Width="100px" MinWidth="100px" HeaderText="类型" />
                        <f:BoundField DataField="OperateNotes" SortField="OperateNotes" Width="320px" MinWidth="320px" ExpandUnusedSpace="true" HeaderText="信息" />

                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_View%>" ColumnID="viewField"
                            Icon="Information" Width="70px" ToolTip="<%$ Resources:GlobalResource,sys_Button_View%>"
                            CommandName="View" Text="<%$ Resources:GlobalResource,sys_Button_View%>" />
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="800px"
            Height="500px">
        </f:Window>
    </form>
</body>
</html>

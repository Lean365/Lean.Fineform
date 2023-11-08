<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="log.aspx.cs" Inherits="Lean.Fineform.Lf_Admin.log" %>

<!DOCTYPE html>
<html>
<head runat="server">
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
                                <f:DropDownList ID="ddlSearchLevel" runat="server" Label="错误级别" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSearchLevel_SelectedIndexChanged">
                                    <f:ListItem Text="ALL" Value="ALL" Selected="true" />
                                    <f:ListItem Text="INFO" Value="INFO" />
                                    <f:ListItem Text="DEBUG" Value="DEBUG" />
                                    <f:ListItem Text="WARN" Value="WARN" />
                                    <f:ListItem Text="ERROR" Value="ERROR" />
                                    <f:ListItem Text="FATAL" Value="FATAL" />
                                </f:DropDownList>
                                <f:DropDownList ID="ddlSearchRange" runat="server" Label="搜索范围" AutoPostBack="true"
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
                    EnableCheckBoxSelect="true" DataKeyNames="ID" AllowSorting="true" ForceFit="true" EnableTextSelection="true"
                    OnSort="Grid1_Sort" SortField="Date" SortDirection="DESC" AllowPaging="true"
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
                        <f:BoundField DataField="Date" SortField="Date" DataFormatString="{0:yyyy-MM-dd HH:mm}"
                            Width="150px" HeaderText="时间" />
                        <f:BoundField DataField="Level" SortField="Level" Width="150px" HeaderText="级别" />
                        <f:BoundField DataField="Thread" SortField="Thread" Width="50px" HeaderText="线程" />
                        <f:BoundField DataField="Logger" SortField="Logger" Width="150px" HeaderText="源" />
                        <f:BoundField DataField="Message" ExpandUnusedSpace="true" HeaderText="信息" />
                        <f:BoundField DataField="Exception" Width="200px" HeaderText="异常信息" />
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

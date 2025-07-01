<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p2d_switch_note.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.daily.P2D.p2d_switch_note" %>

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
            ShowHeader="false" Title="机种ST">
            <Items>
                <f:Form ID="Form2" runat="server" Height="36px" BodyPadding="5px" ShowHeader="false"
                    ShowBorder="false" LabelAlign="Right">
                    <Rows>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:DatePicker runat="server" Label="<%$ Resources:GlobalResource,Query_Startdate%>" DateFormatString="yyyyMM" EmptyText="<%$ Resources:GlobalResource,Query_Startdate_EmptyText%>" AutoPostBack="true"
                                    ID="Prodate" OnTextChanged="Prodate_TextChanged">
                                </f:DatePicker>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Enter_Text%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" ForceFit="true"
                    DataKeyNames="GUID,Prodate" AllowSorting="true" OnSort="Grid1_Sort" SortField="Prodate"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound" EnableTextSelection="true"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="BtnList" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnList_Click" CssClass="marginr">
                                </f:Button>

                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                <f:Button ID="btnNew" runat="server" Icon="Add" EnablePostBack="false" Text="<%$ Resources:GlobalResource,sys_Button_New%>">
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
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                        <f:RowNumberField Width="70px" HeaderText="序号" EnablePagingNumber="true" />
                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_Edit%>" ColumnID="editField"
                            Icon="TableEdit" Width="100px" ToolTip="<%$ Resources:GlobalResource,sys_Button_Edit%>"
                            CommandName="Edit" Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" />
                        <f:BoundField DataField="Prodate" SortField="Prodate" Width="100px" HeaderText="生产日期" />
                        <f:BoundField DataField="ProSmtSwitchNum" SortField="ProSmtSwitchNum" Width="100px" HeaderText="SMT切换次数" />
                        <f:BoundField DataField="ProSmtSwitchTotalTime" SortField="ProSmtSwitchTotalTime" Width="100px" HeaderText="SMT总切换时间" />
                        <f:BoundField DataField="ProAitSwitchNum" SortField="ProAitSwitchNum" Width="100px" HeaderText="自插切换次数" />
                        <f:BoundField DataField="ProAiStopTime" SortField="ProAiStopTime" Width="100px" HeaderText="自插总切换时间" />
                        <f:BoundField DataField="ProHandSopTime" SortField="ProHandSopTime" Width="100px" HeaderText="手插读SOP时间" />
                        <f:BoundField DataField="ProHandPerson" SortField="ProHandPerson" Width="100px" HeaderText="手插人数" />
                        <f:BoundField DataField="ProHandSopTotalTime" SortField="ProHandSopTotalTime" Width="60px" HeaderText="手插读SOP总时间" />
                        <f:BoundField DataField="ProHandSwitchNum" SortField="ProHandSwitchNum" Width="100px" HeaderText="手插切换次数" />
                        <f:BoundField DataField="ProHandSwitchTime" SortField="ProHandSwitchTime" Width="60px" HeaderText="手插切换时间" />
                        <f:BoundField DataField="ProHandSwitchTotalTime" SortField="ProHandSwitchTotalTime" Width="100px" HeaderText="手插切换总时间" />
                        <f:BoundField DataField="ProRepairSopTime" SortField="ProRepairSopTime" Width="100px" HeaderText="修正读SOP时间" />
                        <f:BoundField DataField="ProRepairPerson" SortField="ProRepairPerson" Width="60px" HeaderText="修正人数" />
                        <f:BoundField DataField="ProRepairSopTotalTime" SortField="ProRepairSopTotalTime" Width="100px" HeaderText="修正读SOP总时间" />
                        <f:BoundField DataField="ProRepairSwitchNum" SortField="ProRepairSwitchNum" Width="60px" HeaderText="修正切换次数" />
                        <f:BoundField DataField="ProRepairSwitchTime" SortField="ProRepairSwitchTime" Width="100px" HeaderText="修正切换时间" />
                        <f:BoundField DataField="ProRepairSwitchTotalTime" SortField="ProRepairSwitchTotalTime" Width="100px" HeaderText="修正切换总时间" />
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete"
                            HeaderText="<%$ Resources:GlobalResource,sys_Button_Delete%>" Text="<%$ Resources:GlobalResource,sys_Button_Delete%>"
                            ToolTip="<%$ Resources:GlobalResource,sys_Button_Delete%>" ConfirmText="<%$ Resources:GlobalResource,sys_Button_DeleteConfirmText%>"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="950px"
            Height="550px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>


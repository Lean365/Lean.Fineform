<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="user.aspx.cs" Inherits="Lean.Fineform.Lf_Admin.user" %>

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
            ShowHeader="false" Title="UserManage">
            <Items>
                <f:Form ID="Form2" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                    <Rows>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Enter_Text%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                                <f:RadioButtonList ID="rblEnableStatus" AutoPostBack="true" OnSelectedIndexChanged="rblEnableStatus_SelectedIndexChanged"
                                    Label="<%$ Resources:GlobalResource,sys_Status%>" ColumnNumber="3" runat="server">
                                    <f:RadioItem Text="<%$ Resources:GlobalResource,sys_Status_All%>" Selected="true" Value="all" />
                                    <f:RadioItem Text="<%$ Resources:GlobalResource,sys_Status_Enable%>" Value="enabled" />
                                    <f:RadioItem Text="<%$ Resources:GlobalResource,sys_Status_Disable%>" Value="disabled" />
                                </f:RadioButtonList>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" ForceFit="true"  EnableTextSelection="true"
                    DataKeyNames="ID,Name" AllowSorting="true" OnSort="Grid1_Sort" SortField="Name"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:Button ID="btnDeleteSelected" Icon="Delete" runat="server" Text="删除选中记录" OnClick="btnDeleteSelected_Click">
                                </f:Button>
                                <f:ToolbarSeparator runat="server">
                                </f:ToolbarSeparator>
                                <f:Button ID="btnChangeEnableUsers" Icon="GroupEdit" EnablePostBack="false" runat="server"
                                    Text="设置启用状态">
                                    <Menu runat="server">
                                        <f:MenuButton ID="btnEnableUsers" OnClick="btnEnableUsers_Click" runat="server" Text="启用选中记录">
                                        </f:MenuButton>
                                        <f:MenuButton ID="btnDisableUsers" OnClick="btnDisableUsers_Click" runat="server"
                                            Text="禁用选中记录">
                                        </f:MenuButton>
                                    </Menu>
                                </f:Button>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnNew" runat="server" Icon="Add" EnablePostBack="false" Text="<%$ Resources:GlobalResource,sys_Button_New%>">
                                </f:Button>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
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
                        <f:RowNumberField Width="35px" EnablePagingNumber="true" />
                        <f:BoundField DataField="Name" SortField="Name" Width="80px" HeaderText="用户名" />
                        <f:BoundField DataField="ChineseName" SortField="ChineseName" Width="100px" HeaderText="中文名" />
                        <f:CheckBoxField DataField="Enabled" SortField="Enabled" HeaderText="启用" RenderAsStaticField="true"
                            Width="50px" />
                        <f:BoundField DataField="Email" SortField="Email" Width="150px" HeaderText="邮箱" />

                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_View%>" ColumnID="viewField"
                            Icon="Information" Width="70px" ToolTip="<%$ Resources:GlobalResource,sys_Button_View%>"
                            CommandName="View" Text="<%$ Resources:GlobalResource,sys_Button_View%>" />

                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_PwdEdit%>" ColumnID="pwdeditField"
                            Icon="Key" Width="120px" ToolTip="<%$ Resources:GlobalResource,sys_Button_PwdEdit%>"
                            CommandName="PwdEdit" Text="<%$ Resources:GlobalResource,sys_Button_PwdEdit%>" />

                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_Edit%>" ColumnID="editField"
                            Icon="Pencil" Width="70px" ToolTip="<%$ Resources:GlobalResource,sys_Button_Edit%>"
                            CommandName="Edit" Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" />

                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete"
                            HeaderText="<%$ Resources:GlobalResource,sys_Button_Delete%>" Text="<%$ Resources:GlobalResource,sys_Button_Delete%>"
                            ToolTip="<%$ Resources:GlobalResource,sys_Button_Delete%>" ConfirmText="<%$ Resources:GlobalResource,sys_Button_DeleteConfirmText%>"
                            ConfirmTarget="Top" CommandName="Delete" Width="70px" />
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank"
            Width="900px" Height="600px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>

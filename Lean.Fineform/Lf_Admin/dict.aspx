﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dict.aspx.cs" Inherits="LeanFine.Lf_Admin.dict" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Grid1" runat="server" />
        <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" CssClass="blockpanel"
            EnableCheckBoxSelect="true" DataKeyNames="GUID" AllowSorting="true" ForceFit="true" EnableTextSelection="true"
            OnSort="Grid1_Sort" SortField="DictType" SortDirection="DESC" AllowPaging="true"
            IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound" OnRowCommand="Grid1_RowCommand"
            OnPageIndexChange="Grid1_PageIndexChange">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Enter_Text%>"
                            Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                            OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                        </f:TwinTriggerBox>
                        <f:Label runat="server">
                        </f:Label>
                        <f:Label ID="Label1" runat="server">
                        </f:Label>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                        </f:ToolbarFill>
                        <f:Button ID="btnNew" runat="server" Icon="Add" EnablePostBack="false" Text="<%$ Resources:GlobalResource,sys_Button_New%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Columns>
                <f:RowNumberField HeaderText="序号" />
                <f:BoundField DataField="DictType" SortField="DictType" Width="200px" HeaderText="类型" />
                <f:BoundField DataField="DictName" SortField="DictName" MinWidth="200px" HeaderText="名称" />
                <f:BoundField DataField="DictLabel" SortField="DictLabel" MinWidth="100px" HeaderText="标签" />
                <f:BoundField DataField="DictValue" SortField="DictValue" MinWidth="100px" HeaderText="数值" />
                <f:BoundField DataField="DictSort" SortField="DictSort" MinWidth="50px" HeaderText="排序" />
                <%--<f:BoundField DataField="Remark" SortField="Remark" MinWidth="200px" HeaderText="备注" />--%>
                <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_Edit%>" ColumnID="editField"
                    Icon="Pencil" Width="70px" ToolTip="<%$ Resources:GlobalResource,sys_Button_Edit%>"
                    CommandName="Edit" Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" />
                <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete"
                    HeaderText="<%$ Resources:GlobalResource,sys_Button_Delete%>" Text="<%$ Resources:GlobalResource,sys_Button_Delete%>"
                    ToolTip="<%$ Resources:GlobalResource,sys_Button_Delete%>" ConfirmText="<%$ Resources:GlobalResource,sys_Button_DeleteConfirmText%>"
                    ConfirmTarget="Top" CommandName="Delete" Width="70px" />
            </Columns>
        </f:Grid>
        <f:Window ID="Window1" CloseAction="Hide" runat="server" IsModal="true" Hidden="true" Target="Top"
            EnableResize="true" EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank"
            Width="900px" Height="550px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>

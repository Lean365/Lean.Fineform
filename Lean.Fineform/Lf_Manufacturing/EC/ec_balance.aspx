<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ec_balance.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.EC.ec_balance" %>

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
            ShowHeader="false" Title="设计变更ECN">
            <Toolbars>
                <f:Toolbar runat="server">
                    <Items>
                        <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Pp_EC_EmptyText%>"
                            Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                            OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                        </f:TwinTriggerBox>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                            runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnExport_Click" CssClass="marginr">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>

                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCollapse="false" AllowColumnLocking="true"
                    DataKeyNames="Ec_no,Ec_model,Ec_olditem" AllowSorting="true" OnSort="Grid1_Sort" SortField="Ec_issuedate"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid1_PageIndexChange" OnPreDataBound="Grid1_PreDataBound" OnPreRowDataBound="Grid1_PreRowDataBound" OnRowCommand="Grid1_RowCommand" OnRowDataBound="Grid1_RowDataBound">
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
                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_Edit%>" ColumnID="editField"
                            Icon="TableEdit" Width="70px" ToolTip="<%$ Resources:GlobalResource,sys_Button_Edit%>"
                            CommandName="Edit" Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" />
                        <f:RowNumberField Width="35px" EnablePagingNumber="true" EnableLock="False" Locked="true" HeaderText="序号" />
                        <f:BoundField DataField="Ec_balancedate" SortField="Ec_balancedate " Width="100px" DataFormatString="{0}" EnableLock="true" Locked="true" HeaderText="更新日期"></f:BoundField>
                        <f:BoundField DataField="Ec_olditem" SortField="Ec_olditem " Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="旧物料"></f:BoundField>
                        <f:GroupField HeaderText="旧品在库管理" TextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="Ec_oldqty" SortField="Ec_oldqty " Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="在库"></f:BoundField>
                                <f:BoundField DataField="Ec_poqty" SortField="Ec_poqty " Width="100px" DataFormatString="{0}" EnableLock="true" HtmlEncode="false" HeaderText="PO残"></f:BoundField>
                                <f:BoundField DataField="Ec_balanceqty" SortField="Ec_balanceqty " Width="100px" DataFormatString="{0}" EnableLock="true" HtmlEncode="false" HeaderText="结余"></f:BoundField>
                            </Columns>
                        </f:GroupField>
                        <f:GroupField HeaderText="处理方法" TextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="Ec_precess" SortField="Ec_precess" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="处理方法"></f:BoundField>
                                <f:BoundField DataField="Ec_note" SortField="Ec_note" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="注意事项"></f:BoundField>
                                <f:BoundField DataField="Remark" SortField="Remark" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="备注事项"></f:BoundField>
                            </Columns>
                        </f:GroupField>
                        <f:BoundField DataField="Ec_newitem" SortField="Ec_newitem" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="新物料"></f:BoundField>
                        <f:GroupField HeaderText="设变信息" TextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="Ec_no" SortField="Ec_no" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="设变号码"></f:BoundField>
                                <f:BoundField DataField="Ec_issuedate" SortField="Ec_issuedate" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="设变日期"></f:BoundField>
                                <f:BoundField DataField="Ec_status" SortField="Ec_status" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="状态"></f:BoundField>
                                <f:BoundField DataField="Ec_model" SortField="Ec_model" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="机种"></f:BoundField>
                                <f:BoundField DataField="Ec_item" SortField="Ec_item" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="完成品"></f:BoundField>
                            </Columns>
                        </f:GroupField>
                        <f:BoundField DataField="Ec_status" SortField="Ec_status" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="状态"></f:BoundField>
                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="800px"
            Height="500px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>






<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pp_order.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.Master.Pp_order" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"  
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="机种ST">
            <Items>

                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" ForceFit="true" EnableTextSelection="true"
                    DataKeyNames="GUID,Porderdate" AllowSorting="true" OnSort="Grid1_Sort"  SortField="Porderdate"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid1_PageIndexChange" OnPreDataBound="Grid1_PreDataBound" OnPreRowDataBound="Grid1_PreRowDataBound" OnRowCommand="Grid1_RowCommand">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Enter_Text%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>

                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                <f:Button ID="btnNew" runat="server" Icon="TabAdd" EnablePostBack="false" Text="<%$ Resources:GlobalResource,sys_Button_New%>">
                                </f:Button>
                                
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>" >
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
                        <f:RowNumberField Width="70px" EnablePagingNumber="true" />  
                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_Edit%>" ColumnID="editField" 
                            Icon="TableEdit" Width="100px" ToolTip="<%$ Resources:GlobalResource,sys_Button_Edit%>" 
                            CommandName="Edit" Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" />                 
                        <f:BoundField DataField="Porderdate" SortField="Porderdate"  Width="100px" HeaderText="日程" />
                        <f:BoundField DataField="Porderlot" SortField="Porderlot"  Width="100px" HeaderText="批号" />
                        <f:BoundField DataField="Porderqty" SortField="Porderqty"  Width="100px" HeaderText="数量" />    
                        <f:BoundField DataField="Porderno" SortField="Porderno"  Width="100px" HeaderText="生产订单" />
                         <f:BoundField DataField="Porderreal" SortField="Porderreal"  Width="100px" HeaderText="已生产" />                                                                                                 
                        <f:BoundField DataField="Porderhbn" SortField="Porderhbn"  Width="100px" HeaderText="物料" />
                        <f:BoundField DataField="Porderserial" SortField="Porderserial"  Width="100px" HeaderText="序列号"/>
    
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" 
                            HeaderText="<%$ Resources:GlobalResource,sys_Button_Delete%>" Text="<%$ Resources:GlobalResource,sys_Button_Delete%>" 
                            ToolTip="<%$ Resources:GlobalResource,sys_Button_Delete%>" ConfirmText="<%$ Resources:GlobalResource,sys_Button_DeleteConfirmText%>" 
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />
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

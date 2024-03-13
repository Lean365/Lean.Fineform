<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pp_manhour.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.Master.Pp_manhour" %>

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
                    EnableCheckBoxSelect="true" ForceFit="true"
                    DataKeyNames="GUID,Promodel" AllowSorting="true" OnSort="Grid1_Sort"  SortField="Prodate"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound" EnableTextSelection="true"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Enter_Text%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                <f:Button ID="btnNew" runat="server" Icon="Add" EnablePostBack="false" Text="<%$ Resources:GlobalResource,sys_Button_New%>">
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
                        <f:RowNumberField Width="70px" HeaderText="序号" EnablePagingNumber="true" /> 
                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_Edit%>" ColumnID="editField" 
                            Icon="TableEdit" Width="100px" ToolTip="<%$ Resources:GlobalResource,sys_Button_Edit%>" 
                            CommandName="Edit" Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" />
                        
                        <f:BoundField DataField="Prodate" SortField="Prodate"  Width="100px" HeaderText="更新日期" />
                        <%--<f:BoundField DataField="Proplnt" SortField="Proplnt"  Width="100px" HeaderText="工厂" />--%>
                        <f:BoundField DataField="Promodel" SortField="Promodel"  Width="100px" HeaderText="机种" />
                        <f:BoundField DataField="Proitem" SortField="Proitem"  Width="100px" HeaderText="品号" />
                        <f:BoundField DataField="Prodesc" SortField="Prodesc"  Width="100px" HeaderText="仕向" />
                        <f:BoundField DataField="Prost" SortField="Prost"  Width="100px" HeaderText="ST" />    
                        <f:BoundField DataField="Proset" SortField="Proset"  Width="60px" HeaderText="单位" />
                         <f:BoundField DataField="Proshort" SortField="Proshort"  Width="100px" HeaderText="short" />    
                        <f:BoundField DataField="Propset" SortField="Propset"  Width="60px" HeaderText="单位" />  
                        <f:BoundField DataField="Prorate" SortField="Prorate"  Width="100px" HeaderText="Rate" />    
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


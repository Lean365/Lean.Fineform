<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="liaison_local.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.TL.liaison_local" %>

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
            ShowHeader="false" Title="Lean365">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" ForceFit="true"
                    DataKeyNames="GUID" AllowSorting="true" OnSort="Grid1_Sort" SortField="Ec_issuedate"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound" EnableTextSelection="true"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:DropDownList ID="DDLModel" Label="<%$ Resources:GlobalResource,Query_Model%>" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DDLModel_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
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
                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_Edit%>" ColumnID="editField"
                            Icon="TableEdit" Width="100px" ToolTip="<%$ Resources:GlobalResource,sys_Button_Edit%>"
                            CommandName="Edit" Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" />
                        <f:BoundField DataField="Ec_issuedate" ColumnID="Ec_issuedate" SortField="Ec_issuedate" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="发行"></f:BoundField>
                        <f:BoundField DataField="Ec_leader" ColumnID="Ec_leader" SortField="Ec_leader" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="担当"></f:BoundField>
                        <f:BoundField DataField="Ec_model" ColumnID="Ec_model" SortField="Ec_model" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="机种"></f:BoundField>
                        <f:BoundField DataField="Ec_modellist" ColumnID="Ec_modellist" SortField="Ec_modellist" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="明细"></f:BoundField>
                        <f:BoundField DataField="Ec_region" ColumnID="Ec_region" SortField="Ec_region" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="仕向"></f:BoundField>
                        <f:HyperLinkField DataTextField="Ec_letterno" Width="100px" HeaderText="技联No." MinWidth="100px" DataNavigateUrlFields="Ec_letterdoc" />
                        <f:HyperLinkField DataTextField="Ec_eppletterno" Width="100px" HeaderText="P番No." MinWidth="100px" DataNavigateUrlFields="Ec_eppletterdoc" />
                        <f:HyperLinkField DataTextField="Ec_teppletterno" Width="100px" HeaderText="TCJ技联No." MinWidth="100px" DataNavigateUrlFields="Ec_teppletterdoc" />
                        <f:BoundField DataField="Ec_enterdate" ColumnID="Ec_enterdate" SortField="Ec_enterdate" Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="实施"></f:BoundField>




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

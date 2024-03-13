<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="view_desc.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.EC.dept.view_desc" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" />
        <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" ShowBorder="true" TabPosition="Top" ActiveTabIndex="0" AutoScroll="true"
            runat="server">
                        <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="<%$ Resources:GlobalResource,WindowsForm_Close%>">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Tabs>
                <f:Tab Title="设变内容" BodyPadding="10px" runat="server">
                    <Items>
                        <f:Panel runat="server" ID="panelLeftRegion" RegionPosition="Left" RegionSplit="true" EnableCollapse="true"
                            RegionPercent="100%" Title="设变内容" ShowBorder="true" ShowHeader="false"
                            BodyPadding="10px" IconFont="_PullLeft">
                            <Items>
                                <f:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server"
                                    BodyPadding="0px" Title="SimpleForm">
                                    <Items>
                                        <f:TextArea runat="server" ID="EcnDesc"
                                            Height="530px" RegionPosition="Left" RegionSplit="true" RegionSplitWidth="100%">
                                        </f:TextArea>
                                    </Items>
                                </f:SimpleForm>
                            </Items>
                        </f:Panel>
                        </Items>
                    </f:Tab>
                <f:Tab Title="物料确认" BodyPadding="10px" runat="server">
                    <Items>
                        <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                            EnableCheckBoxSelect="true" 
                            DataKeyNames="D_SAP_ZPABD_S001" AllowSorting="true" OnSort="Grid1_Sort" SortField="D_SAP_ZPABD_S001"
                            SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" EnableTextSelection="true"
                            OnPageIndexChange="Grid1_PageIndexChange" OnRowDataBound="Grid1_RowDataBound"
                            OnPreRowDataBound="Grid1_PreRowDataBound">
                            <Toolbars>
                                <f:Toolbar ID="Toolbar2" runat="server">
                                    <Items>
                                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                        <f:DropDownList ID="DDL_Item" AutoPostBack="true" OnSelectedIndexChanged="DDL_Item_SelectedIndexChanged"
                                            runat="server">
                                        </f:DropDownList>
                                        <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
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
                                <f:BoundField DataField="D_SAP_ZPABD_S001" ColumnID="D_SAP_ZPABD_S001" SortField="D_SAP_ZPABD_S001" EnableLock="true" Width="100px" HeaderText="设变号码" />
                                <f:BoundField DataField="D_SAP_ZPABD_S002" ColumnID="D_SAP_ZPABD_S002" SortField="D_SAP_ZPABD_S002" EnableLock="true" Width="150px" HeaderText="完成品" />
                                <f:BoundField DataField="D_SAP_ZPABD_S003" ColumnID="D_SAP_ZPABD_S003" SortField="D_SAP_ZPABD_S003" EnableLock="true" Width="150px" HeaderText="上阶品号" />
                                <f:BoundField DataField="D_SAP_ZPABD_S004" ColumnID="D_SAP_ZPABD_S004" SortField="D_SAP_ZPABD_S004" EnableLock="true" Width="150px" HeaderText="旧品号" />
                                <f:BoundField DataField="D_SAP_ZPABD_S005" ColumnID="D_SAP_ZPABD_S005" SortField="D_SAP_ZPABD_S005" EnableLock="true" Width="100px" HeaderText="品名" />
                                <f:BoundField DataField="D_SAP_ZPABD_S006" ColumnID="D_SAP_ZPABD_S006" SortField="D_SAP_ZPABD_S006" EnableLock="true" Width="100px" HeaderText="数量" />
                                <f:BoundField DataField="D_SAP_ZPABD_S007" ColumnID="D_SAP_ZPABD_S007" SortField="D_SAP_ZPABD_S007" EnableLock="true" Width="100px" HeaderText="位置" />
                                <f:BoundField DataField="D_SAP_ZPABD_S008" ColumnID="D_SAP_ZPABD_S008" SortField="D_SAP_ZPABD_S008" EnableLock="true" Width="150px" HeaderText="新品号" />
                                <f:BoundField DataField="D_SAP_ZPABD_S009" ColumnID="D_SAP_ZPABD_S009" SortField="D_SAP_ZPABD_S009" EnableLock="true" Width="100px" HeaderText="品名" />
                                <f:BoundField DataField="D_SAP_ZPABD_S010" ColumnID="D_SAP_ZPABD_S010" SortField="D_SAP_ZPABD_S010" EnableLock="true" Width="100px" HeaderText="数量" />
                                <f:BoundField DataField="D_SAP_ZPABD_S011" ColumnID="D_SAP_ZPABD_S011" SortField="D_SAP_ZPABD_S011" EnableLock="true" Width="100px" HeaderText="位置" />
                                <f:BoundField DataField="D_SAP_ZPABD_S013" ColumnID="D_SAP_ZPABD_S013" SortField="D_SAP_ZPABD_S013" EnableLock="true" Width="100px" HeaderText="番号" />
                                


                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Tab>
                </Tabs>
            </f:TabStrip>
    </form>
</body>
</html>

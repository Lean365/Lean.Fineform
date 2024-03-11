<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="material.aspx.cs" Inherits="Fine.Lc_MM.material" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <!-- reference your copy Font Awesome here (from our CDN or by hosting yourself) -->
    <link href="~/Lf_Resources/fontawesome/css/fontawesome.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/fontawesome.min.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/brands.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/solid.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/svg-with-js.css" rel="stylesheet" />
    <link href="~/Lf_Resources/fontawesome/css/v4-shims.css" rel="stylesheet" />
    <link href="~/Lf_Resources/bootstrap/css/bootstrap.min.css" rel="stylesheet" />
    <link href="~/Lf_Resources/css/map.css" rel="stylesheet" />

</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Grid1" runat="server" />

        <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" EnableTextSelection="true"
            EnableCheckBoxSelect="true" ForceFit="true"
            DataKeyNames="GUID,MatItem" AllowSorting="true" OnSort="Grid1_Sort" SortField="MatItem"
            SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
            OnPreRowDataBound="Grid1_PreRowDataBound"
            OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Items%>"
                            Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                            OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                        </f:TwinTriggerBox>
                        <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                        <f:Button ID="btnYFDataView" runat="server" IconUrl="~/Lf_Resources/menu/yf.png" EnablePostBack="true" Text="<%$ Resources:GlobalResource,yf_DataView%>" OnClick="btnYFDataView_Click">
                        </f:Button>
                        <%--<f:Button ID="btnModelRegion" runat="server" IconUrl="~/Lf_Resources/menu/pro.png" EnablePostBack="true" Text="<%$ Resources:GlobalResource,Query_Model%>" OnClick="btnModelRegion_Click">
                        </f:Button>--%>
                        <%--<f:Button ID="btnNew" runat="server" Icon="Add" EnablePostBack="false" Text="<%$ Resources:GlobalResource,sys_Button_New%>">
                        </f:Button>--%>
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
                    Icon="TableEdit" Width="50px" ToolTip="<%$ Resources:GlobalResource,sys_Button_Edit%>"
                    CommandName="Edit" Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" />
                <f:BoundField DataField="isDate" SortField="isDate" Width="100px" HeaderText="更新日期" />
                <f:BoundField DataField="MatItem" SortField="MatItem" Width="100px" HeaderText="物料" />
                <f:BoundField DataField="Inventory" SortField="Inventory" Width="100px" HeaderText="库存" />
                <f:BoundField DataField="ProfitCenter" SortField="ProfitCenter" Width="100px" HeaderText="成本中心" />
                <f:BoundField DataField="MovingAvg" SortField="MovingAvg" Width="100px" HeaderText="移动价格" />
                <f:BoundField DataField="PriceUnit" SortField="PriceUnit" Width="100px" HeaderText="价格单位" />
                <f:BoundField DataField="MatDescription" SortField="MatDescription" Width="100px" HeaderText="品名" />
                <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete"
                    HeaderText="<%$ Resources:GlobalResource,sys_Button_Delete%>" Text="<%$ Resources:GlobalResource,sys_Button_Delete%>"
                    ToolTip="<%$ Resources:GlobalResource,sys_Button_Delete%>" ConfirmText="<%$ Resources:GlobalResource,sys_Button_DeleteConfirmText%>"
                    ConfirmTarget="Top" CommandName="Delete" Width="50px" />
            </Columns>
        </f:Grid>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="800px"
            Height="500px" OnClose="Window1_Close">
        </f:Window>
    </form>

</body>
</html>






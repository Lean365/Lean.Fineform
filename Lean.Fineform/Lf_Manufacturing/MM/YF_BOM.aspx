<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YF_BOM.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.MM.YF_BOM" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .color1 {
            background-color: #0094ff;
            color: #fff;
        }

        .color2 {
            background-color: #0026ff;
            color: #fff;
        }

        .color3 {
            background-color: #b200ff;
            color: #fff;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="YiFei Data">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" ForceFit="true"
                    DataKeyNames="Serialno" AllowSorting="true" SortField="Serialno" EnableTextSelection="true"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" 
                    OnPageIndexChange="Grid1_PageIndexChange"
                    OnSort="Grid1_Sort"
                    OnRowDataBound="Grid1_RowDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound">
                    <Toolbars>

                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:DropDownList runat="server" ID="SerialNo" Label="<%$ Resources:GlobalResource,Query_Goods%>" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" ShowRedStar="True" EnableEdit="true" ForceSelection="true" AutoPostBack="True" OnSelectedIndexChanged="SerialNo_SelectedIndexChanged" TabIndex="2">
                                </f:DropDownList>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnExport_Click" CssClass="marginr">
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
                            <f:ListItem Text="5000" Value="5000" />
                        </f:DropDownList>

                    </PageItems>
                    <Columns>
                        <f:BoundField DataField="Stratum" SortField="Stratum" Width="60px" HeaderText="层级" />
                        <f:BoundField DataField="Ecno" SortField="Ecno" Width="150px" HeaderText="设变" />
                        <f:BoundField DataField="SubMatAttribute" SortField="SubMatAttribute" Width="80px" MinWidth="80px" HeaderText="物料属性" />
                        <f:BoundField DataField="EngSequences" SortField="EngSequences" Width="80px" HeaderText="工程顺" />
                        <f:BoundField DataField="EngLocation" SortField="EngLocation" Width="100px" HeaderText="位置" />
                        <f:BoundField DataField="TopMaterial" SortField="TopMaterial" Width="100px" HeaderText="上阶物料" />
                        <f:BoundField DataField="SubMaterial" SortField="SubMaterial" Width="100px" HeaderText="子物料" />
                        <f:BoundField DataField="Usageqty" SortField="Usageqty" Width="60px" HeaderText="用量" />
                        <f:BoundField DataField="Supplier" SortField="Supplier" Width="100px" HeaderText="供应商" />


                        <f:BoundField DataField="SubMatText" SortField="SubMatText" Width="200px" HeaderText="物料描述" />
                         <%--<f:BoundField DataField="Serialno" SortField="Serialno" Width="200px" HeaderText="序号" />--%>

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

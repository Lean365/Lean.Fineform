<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fyperiod.aspx.cs" Inherits="LeanFine.Lf_Accounting.fyperiod" %>

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
            ShowHeader="false" Title="YiFei Data">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" ForceFit="true"
                    DataKeyNames="Btfy" AllowSorting="true" SortField="Btfy" EnableTextSelection="true"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true"
                    OnPageIndexChange="Grid1_PageIndexChange"
                    OnSort="Grid1_Sort"
                    OnRowDataBound="Grid1_RowDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound">
                    <Toolbars>

                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:DatePicker ID="DPend" Readonly="false" DateFormatString="yyyyMM" AutoPostBack="true"
                                    Label="<%$ Resources:GlobalResource,Query_Select_Date%>"
                                    runat="server" ShowRedStar="True" OnTextChanged="DPend_TextChanged">
                                </f:DatePicker>
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
                            <f:ListItem Text="10" Value="10" />
                            <f:ListItem Text="20" Value="20" />
                            <f:ListItem Text="50" Value="50" />
                            <f:ListItem Text="100" Value="100" />
                        </f:DropDownList>

                    </PageItems>
                    <Columns>
                        <f:RowNumberField Width="35px" HeaderText="序号" />
                        <f:BoundField DataField="Btfy" SortField="Btfy" Width="100px" HeaderText="财年(TCJ)" />
                        <f:BoundField DataField="Btfm" SortField="Btfm" Width="100px" HeaderText="日期" />
                        <f:BoundField DataField="UDF01" SortField="UDF01" Width="100px" HeaderText="年" />
                        <f:BoundField DataField="UDF02" SortField="UDF02" Width="100px" HeaderText="月" />
                        <f:BoundField DataField="UDF03" SortField="UDF03" Width="100px" HeaderText="季(TCJ)" />


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


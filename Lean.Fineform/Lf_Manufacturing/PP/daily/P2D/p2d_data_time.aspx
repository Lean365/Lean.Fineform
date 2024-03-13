<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p2d_data_time.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.daily.P2D.p2d_data_time" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .x-grid-row-summary .x-grid-cell-inner {
            font-weight: bold;
            color: red;
        }

        .x-grid-row-summary .x-grid-cell {
            background-color: #fff !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" EnableFStateValidation="false" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="1px"
            ShowBorder="false" Layout="Fit" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="OPH">
            <Items>

                <f:Grid ID="Grid1" ShowBorder="false" ShowHeader="false" AllowPaging="true" IsDatabasePaging="true" AllowSorting="true" SortField="Date" SortDirection="DESC"
                    OnPageIndexChange="Grid1_PageIndexChange" ForceFit="true" EnableTextSelection="true"
                    OnSort="Grid1_Sort"
                    runat="server">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:DatePicker ID="DPend" Readonly="false" DateFormatString="yyyyMMdd" AutoPostBack="true"
                                    Label="<%$ Resources:GlobalResource,Query_Select_Date%>"
                                    runat="server" ShowRedStar="True" OnTextChanged="DPend_TextChanged">
                                </f:DatePicker>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>

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
                </f:Grid>
            </Items>
        </f:Panel>
    </form>
</body>
</html>

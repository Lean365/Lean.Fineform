<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="forecast.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.SD.salesmanage.forecast" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <style>
        .f-grid-cell[data-color=color1] {
            /*background-color: #0094ff;*/
            color: #0026ff;
        }

        .f-grid-cell[data-color=color2] {
            /*background-color: #0026ff;*/
            color: #0094ff;
        }

        .f-grid-cell[data-color=color3] {
            /*background-color: #b200ff;*/
            color: #ff0000;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel1" />
        <f:Panel ID="Panel1" IsFluid="true" ShowBorder="false" ShowHeader="false" CssClass="blockpanel" runat="server" Title="" Layout="VBox">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" OnRowDataBound="Grid1_RowDataBound"
                    ForceFit="true" EnableTextSelection="true" DataKeyNames="FY" AllowSorting="true"
                    OnSort="Grid1_Sort" SortField="FY" SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true"
                    OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                <f:DatePicker ID="DPend" Readonly="false" DateFormatString="yyyyMM" AutoPostBack="true"
                                    Label="<%$ Resources:GlobalResource,Query_Select_Date%>"
                                    runat="server" ShowRedStar="True" OnTextChanged="DPend_TextChanged">
                                </f:DatePicker>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:DropDownList ID="DDLItem" Label="<%$ Resources:GlobalResource,Query_Items%>" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DDLItEm_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                </f:ToolbarFill>
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
                        <f:BoundField DataField="Item" Width="120px" HeaderText="品目" />
                        <f:GroupField ID="HT_FY" TextAlign="Center" runat="server">
                            <Columns>
                                <f:BoundField Width="80px" DataField="Qtya'04" HeaderText="04'Qty" TextAlign="Center" />
                                <f:BoundField Width="80px" DataField="Qtya'05" HeaderText="05'Qty" TextAlign="Center" />
                                <f:BoundField Width="80px" DataField="Qtya'06" HeaderText="06'Qty" TextAlign="Center" />
                                <f:BoundField Width="80px" DataField="Qtya'07" HeaderText="07'Qty" TextAlign="Center" />
                                <f:BoundField Width="80px" DataField="Qtya'08" HeaderText="08'Qty" TextAlign="Center" />
                                <f:BoundField Width="80px" DataField="Qtya'09" HeaderText="09'Qty" TextAlign="Center" />
                                <f:BoundField Width="80px" DataField="Qtya'10" HeaderText="10'Qty" TextAlign="Center" />
                                <f:BoundField Width="80px" DataField="Qtya'11" HeaderText="11'Qty" TextAlign="Center" />
                                <f:BoundField Width="80px" DataField="Qtya'12" HeaderText="12'Qty" TextAlign="Center" />
                                <f:BoundField Width="80px" DataField="Qtya'01" HeaderText="01'Qty" TextAlign="Center" />
                                <f:BoundField Width="80px" DataField="Qtya'02" HeaderText="02'Qty" TextAlign="Center" />
                                <f:BoundField Width="80px" DataField="Qtya'03" HeaderText="03'Qty" TextAlign="Center" />

                            </Columns>
                        </f:GroupField>
                    </Columns>


                </f:Grid>

            </Items>
        </f:Panel>
    </form>

</body>
</html>

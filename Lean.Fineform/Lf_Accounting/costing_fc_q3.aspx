<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="costing_fc_q3.aspx.cs" Inherits="Lean.Fineform.Lf_Accounting.costing_fc_q3" %>

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
                                
                                <f:DropDownList ID="DDLItem" Label="<%$ Resources:GlobalResource,Query_Select_Text%>" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DDLItEm_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>
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
                        <f:BoundField DataField="Item" Width="80px" HeaderText="品目" />
                        <f:GroupField ID="HT_FY" TextAlign="Center" runat="server">
                            <Columns>

                                <f:GroupField HeaderText="'10" TextAlign="Center">
                                    <Columns>


                                        <f:GroupField ID="HT_Vera04" runat="server" HeaderText="001" TextAlign="Center">
                                            <Columns>
                                                <f:BoundField Width="80px" DataField="Qtya'10" HeaderText="数量" TextAlign="Center" />
                                            </Columns>
                                        </f:GroupField>
                                        <f:GroupField ID="HT_Verb04" runat="server" HeaderText="999" TextAlign="Center">
                                            <Columns>
                                                <f:BoundField Width="80px" DataField="Qtyb'10" HeaderText="数量" TextAlign="Center" />
                                            </Columns>
                                        </f:GroupField>
                                        <f:BoundField Width="80px" ColumnID="DiffQty'10" DataField="DiffQty'10" HeaderText="增减" TextAlign="Center" />
                                    </Columns>
                                </f:GroupField>
                                <f:GroupField HeaderText="'11" TextAlign="Center">
                                    <Columns>
                                        <f:GroupField ID="HT_Vera05" runat="server" HeaderText="001" TextAlign="Center">
                                            <Columns>
                                                <f:BoundField Width="80px" DataField="Qtya'11" HeaderText="数量" TextAlign="Right" />
                                            </Columns>
                                        </f:GroupField>
                                        <f:GroupField ID="HT_Verb05" runat="server" HeaderText="999" TextAlign="Center">
                                            <Columns>
                                                <f:BoundField Width="80px" DataField="Qtyb'11" HeaderText="数量" TextAlign="Right" />
                                            </Columns>
                                        </f:GroupField>
                                        <f:BoundField Width="80px" ColumnID="DiffQty'11"  DataField="DiffQty'11" HeaderText="增减" TextAlign="Right" />
                                    </Columns>
                                </f:GroupField>
                                <f:GroupField HeaderText="'12" TextAlign="Center">
                                    <Columns>
                                        <f:GroupField ID="HT_Vera06" runat="server" HeaderText="001" TextAlign="Center">
                                            <Columns>
                                                <f:BoundField Width="80px" DataField="Qtya'12" HeaderText="数量" TextAlign="Right" />
                                            </Columns>
                                        </f:GroupField>
                                        <f:GroupField ID="HT_Verb06" runat="server" HeaderText="999" TextAlign="Center">
                                            <Columns>
                                                <f:BoundField Width="80px" DataField="Qtyb'12" HeaderText="数量" TextAlign="Right" />
                                            </Columns>
                                        </f:GroupField>
                                        <f:BoundField Width="80px" ColumnID="DiffQty'12" DataField="DiffQty'12" HeaderText="增减" TextAlign="Right" />
                                    </Columns>
                                </f:GroupField>
                            </Columns>
                        </f:GroupField>
                    </Columns>

                </f:Grid>

            </Items>
        </f:Panel>
    </form>

</body>
</html>

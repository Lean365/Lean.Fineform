<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fqc_query.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.QM.fqc.fqc_query" %>

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
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" EnableFStateValidation="false" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="生产日报OPH">
            <Items>
                <f:Form ID="Form2" runat="server" Height="36px" BodyPadding="5px" ShowHeader="false"
                    ShowBorder="false" LabelAlign="Right">
                    <Rows>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:DatePicker runat="server" Label="<%$ Resources:GlobalResource,Query_Startdate%>" DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Startdate_EmptyText%>" AutoPostBack="true"
                                    ID="DpStartDate" ShowRedStar="True" OnTextChanged="DpStartDate_TextChanged">
                                </f:DatePicker>
                                <f:DatePicker ID="DpEndDate" Readonly="false" CompareControl="DpStartDate" DateFormatString="yyyyMMdd" AutoPostBack="true"
                                    CompareOperator="GreaterThan" CompareMessage="<%$ Resources:GlobalResource,Query_Enddate_EmptyText%>" Label="<%$ Resources:GlobalResource,Query_Enddate%>"
                                    runat="server" ShowRedStar="True" OnTextChanged="DpEndDate_TextChanged">
                                </f:DatePicker>
                                <f:DropDownList ID="DdlLine" ShowRedStar="true" Label="<%$ Resources:GlobalResource,Query_Pp_Line%>" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DdlLine_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:DropDownList ID="DDLlot" ShowRedStar="true" Label="<%$ Resources:GlobalResource,Query_Pp_Lot%>" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DDLlot_SelectedIndexChanged">
                                </f:DropDownList>

                                <f:DropDownList ID="DDLhbn" ShowRedStar="true" Label="<%$ Resources:GlobalResource,Query_Items%>" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DDLhbn_SelectedIndexChanged">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" ForceFit="true"
                    DataKeyNames="ID,qmCheckdate" AllowSorting="true" OnSort="Grid1_Sort" SortField="qmCheckdate"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound" EnableSummary="true" SummaryPosition="Bottom"  EnableTextSelection="true"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>

                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
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
                        <f:LinkButtonField HeaderText="打印" ColumnID="printField" Icon="Printer" Width="100px" CommandName="PrintCheck" Text="打印" />
                        <f:BoundField DataField="qmProlot" SortField="qmProlot" Width="100px" HeaderText="批次" />
                        <f:BoundField DataField="qmLine" SortField="qmLine" Width="100px" HeaderText="班组" />

                        <f:BoundField DataField="qmCheckdate" SortField="qmCheckdate" Width="100px" HeaderText="日期" />
                        <f:BoundField DataField="qmAcceptqty" SortField="qmAcceptqty" ColumnID="qmAcceptqty" Width="100px" HeaderText="验收数" />
                        <f:BoundField DataField="qmMaterial" SortField="qmMaterial" Width="100px" HeaderText="物料" />
                        <f:BoundField DataField="qmRejectqty" SortField="qmRejectqty" ColumnID="qmRejectqty" Width="100px" HeaderText="验退数" />
                        <f:BoundField DataField="Rate" SortField="Rate" ColumnID="Rate" Width="100px" HeaderText="不合格率" />
                        <f:BoundField DataField="qmCheckNotes" SortField="qmCheckNotes" Width="100px" HeaderText="查验号码" />
                        <f:BoundField DataField="qmInspector" SortField="qmInspector" Width="100px" HeaderText="检查员" />



                    </Columns>

                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="1200px"
            Height="700px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>


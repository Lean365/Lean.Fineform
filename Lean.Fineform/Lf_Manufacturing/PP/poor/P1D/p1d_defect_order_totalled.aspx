<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p1d_defect_order_totalled.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.poor.p1d_defect_order_totalled" %>

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
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="LeanCloud">
            <Items>
                <f:Form ID="Form2" runat="server" Height="36px" BodyPadding="5px" ShowHeader="false"
                    ShowBorder="false" LabelAlign="Right">
                    <Rows>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:DatePicker runat="server" Label="开始日期" DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Startdate_EmptyText%>" AutoPostBack="true"
                                    ID="DPstart" ShowRedStar="True" OnTextChanged="DPstart_TextChanged">
                                </f:DatePicker>
                                <f:DatePicker ID="DPend" Readonly="false" CompareControl="DPstart" DateFormatString="yyyyMMdd" AutoPostBack="true"
                                    CompareOperator="GreaterThan" CompareMessage="<%$ Resources:GlobalResource,Query_Enddate_EmptyText%>" Label="<%$ Resources:GlobalResource,Query_Enddate%>"
                                    runat="server" ShowRedStar="True" OnTextChanged="DPend_TextChanged">
                                </f:DatePicker>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Pp_EmptyText%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>

                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" EnableTextSelection="true"
                    EnableCheckBoxSelect="true" ForceFit="true"
                    DataKeyNames="GUID,Prodate" AllowSorting="true" OnSort="Grid1_Sort" SortField="Prodate"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound" EnableSummary="true" SummaryPosition="Bottom"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
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
                            <f:ListItem Text="5000" Value="5000" />
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                        <f:RowNumberField Width="35px" EnablePagingNumber="true" />
                        <%--                        <f:WindowField ColumnID="editField" TextAlign="Center" Icon="Pencil" ToolTip="编辑"
                            WindowID="Window1" Title="编辑" DataIFrameUrlFields="ID" DataIFrameUrlFormatString="~/oneProduction/oneDefect/defect_edit.aspx?id={0}"
                            Width="50px" />--%>

                        <f:LinkButtonField HeaderText="编辑" ColumnID="editField" Icon="TableEdit" Width="100px" CommandName="Edit" Text="编辑" />
                        <f:BoundField DataField="Prolot" SortField="Prolot" Width="100px" HeaderText="Lot" />
                        <f:BoundField DataField="Prolinename" SortField="Prolinename" Width="100px" HeaderText="班组" />
                        <f:BoundField DataField="Proorder" SortField="Proorder" Width="100px" HeaderText="工单" />
                        <f:BoundField DataField="Proorderqty" SortField="Proorderqty" Width="100px" HeaderText="工单数量" />
                        <f:BoundField DataField="Prodate" SortField="Prodate" Width="200px" HeaderText="生产日期" />
                        <f:BoundField DataField="Prorealqty" SortField="Prorealqty" ColumnID="Prorealqty" Width="100px" HeaderText="生产数量" />
                        <f:BoundField DataField="Pronobadqty" SortField="Pronobadqty" ColumnID="Pronobadqty" Width="100px" HeaderText="无不良台数" />
                        <f:BoundField DataField="Probadtotal" SortField="Probadtotal" ColumnID="Probadtotal" Width="100px" HeaderText="不良总数" />
                        <f:BoundField DataField="Prodirectrate" SortField="Prodirectrate" ColumnID="Prodirectrate" Width="100px" HeaderText="直行率" HtmlEncode="false" DataFormatString="{0:p2}" />
                        <f:BoundField DataField="Probadrate" SortField="Probadrate" ColumnID="Probadrate" Width="100px" HeaderText="不良率" HtmlEncode="false" DataFormatString="{0:p2}" />

                        <%--<f:BoundField DataField="Prodirectrate" SortField="Prodirectrate"  Width="100px" HeaderText="直行率" DataFormatString="{0:p2}"/>--%>
                        <%--<f:BoundField DataField="Probadrate" SortField="Probadrate"  Width="100px" HeaderText="不良率" DataFormatString="{0:p2}"/>--%>
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

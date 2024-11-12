<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p2d_inspection_defect.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.poor.P2D.p2d_inspection_defect" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .f-grid-row.color1,
        .f-grid-row.color1 .f-icon,
        .f-grid-row.color1 a {
            background-color: #0094ff;
            color: #fff;
        }

        .f-grid-row.color3,
        .f-grid-row.color3 .f-icon,
        .f-grid-row.color3 a {
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
            ShowHeader="false" Title="LeanCloud">
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
                                <f:DropDownList ID="DdlLine" Label="<%$ Resources:GlobalResource,Query_Pp_Line%>" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DdlLine_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Pp_EmptyText%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>

                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" ForceFit="true" EnableTextSelection="true"
                    EnableCheckBoxSelect="true"
                    DataKeyNames="ID,Proinspdate" AllowSorting="true" OnSort="Grid1_Sort" SortField="Proinspdate"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound" EnableSummary="true" SummaryPosition="Bottom"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>

                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnExport_Click" CssClass="marginr">
                                </f:Button>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
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
                            <f:ListItem Text="5000" Value="5000" />
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                        <f:RowNumberField Width="35px" EnablePagingNumber="true" />
                        <%--<f:WindowField ColumnID="editField" TextAlign="Center" Icon="Pencil" ToolTip="编辑"
                            WindowID="Window1" Title="编辑" DataIFrameUrlFields="ID" DataIFrameUrlFormatString="~/oneProduction/oneDefect/defect_edit.aspx?id={0}"
                            Width="50px" />--%>

                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_Edit%>" ColumnID="editField"
                            Icon="TableEdit" Width="70px" ToolTip="<%$ Resources:GlobalResource,sys_Button_Edit%>"
                            CommandName="Edit" Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" />
                        <f:BoundField DataField="Proinspdate" SortField="Proinspdate" Width="80px" HeaderText="检查日" />
                        <f:BoundField DataField="Promodel" SortField="Promodel" Width="150px" HeaderText="机种" />
                        <f:BoundField DataField="Propcbtype" SortField="Propcbtype" Width="80px" HeaderText="板别" />
                        <%-- <f:BoundField DataField="Provisualtype" SortField="Provisualtype" Width="80px" HeaderText="目视" />--%>
                        <%--<f:BoundField DataField="Provctype" SortField="Provctype" Width="80px" HeaderText="VC线别" />--%>
                        <f:BoundField DataField="Prosideadate" SortField="Prosideadate" Width="80px" HeaderText="B面实装" />
                        <f:BoundField DataField="Prosidebdate" SortField="Prosidebdate" Width="80px" HeaderText="T面实装" />
                        <%--<f:BoundField DataField="Prodshiftname" SortField="Prodshiftname" Width="80px" HeaderText="生产班别" />--%>
                        <f:BoundField DataField="Procensor" SortField="Procensor" Width="80px" HeaderText="检查" />
                        <%--<f:BoundField DataField="Proorder" SortField="Proorder" Width="80px" HeaderText="订单" />--%>
                        <f:BoundField DataField="Prolot" SortField="Prolot" Width="100px" HeaderText="Lot" />
                        <f:BoundField DataField="Proorderqty" SortField="Proorderqty" Width="100px" HeaderText="订单数" />
                        <f:BoundField DataField="Prorealqty" SortField="Prorealqty" ColumnID="Prorealqty" Width="80px" HeaderText="生产数" />
                        <f:BoundField DataField="Proispqty" SortField="Proispqty" ColumnID="Proispqty" Width="80px" HeaderText="检查数" />
                        <f:BoundField DataField="Propcbchecktype" SortField="Propcbchecktype" Width="80px" HeaderText="检查状况" />
                        <%--<f:BoundField DataField="Prolinename" SortField="Prolinename" Width="60px" HeaderText="线别" />--%>

                        <%--<f:BoundField DataField="Proinsqtime" SortField="Proinsqtime" Width="80px" HeaderText="检查工数" />--%>
                        <%--<f:BoundField DataField="Proaoitime" SortField="Proaoitime" Width="60px" HeaderText="AOI工数" />--%>
                        <f:BoundField DataField="Probadqty" SortField="Probadqty" ColumnID="Probadqty" Width="80px" HeaderText="不良" />
                        <%--<f:BoundField DataField="Probadamount" SortField="Probadamount"  Width="100px" HeaderText="不良总数" />--%>
                        <%--<f:BoundField DataField="Prohandle" SortField="Prohandle" Width="120px" HeaderText="手贴" />--%>
                        <%--<f:BoundField DataField="Probadserial" SortField="Probadserial" Width="120px" HeaderText="流水" />--%>
                        <%--<f:BoundField DataField="Probadcontent" SortField="Probadcontent" Width="150px" HeaderText="内容" />--%>
                        <f:BoundField DataField="Probadtype" SortField="Probadtype" Width="150px" HeaderText="个所" />
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete"
                            HeaderText="<%$ Resources:GlobalResource,sys_Button_Delete%>" Text="<%$ Resources:GlobalResource,sys_Button_Delete%>"
                            ToolTip="<%$ Resources:GlobalResource,sys_Button_Delete%>" ConfirmText="<%$ Resources:GlobalResource,sys_Button_DeleteConfirmText%>"
                            ConfirmTarget="Top" CommandName="Delete" Width="70px" />
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p1d_defect_query.aspx.cs" Inherits="Fine.Lf_Manufacturing.PP.poor.p1d_defect_query" %>

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
                                <f:DropDownList ID="DDLlot" Label="<%$ Resources:GlobalResource,Query_Pp_Lot%>" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DDLlot_SelectedIndexChanged">
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
                    DataKeyNames="ID,Prodate,Prolot" AllowSorting="true" OnSort="Grid1_Sort" SortField="Prodate"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound" EnableSummary="true" SummaryPosition="Bottom"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>


                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnExport_Click" CssClass="marginr">
                                </f:Button>
                                <f:Button ID="BtnModel" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_SheetModel%>" OnClick="BtnModel_Click" CssClass="marginr">
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
                        <f:BoundField DataField="Proorder" SortField="Proorder" Width="100px" HeaderText="工单" />
                        <f:BoundField DataField="Prolot" SortField="Prolot" Width="100px" HeaderText="Lot" />
                        <f:BoundField DataField="Prolinename" SortField="Prolinename" Width="60px" HeaderText="班组" />

                        <f:BoundField DataField="Prodate" SortField="Prodate" Width="80px" HeaderText="日期" />
                        <f:BoundField DataField="Prorealqty" SortField="Prorealqty" ColumnID="Prorealqty" Width="80px" HeaderText="台数" />
                        <f:BoundField DataField="Prongdept" SortField="Prongdept" Width="80px" HeaderText="区分" />
                        <f:BoundField DataField="Pronobadqty" SortField="Pronobadqty" ColumnID="Pronobadqty" Width="100px" HeaderText="无不良" />
                        <f:BoundField DataField="Probadqty" SortField="Probadqty" ColumnID="Probadqty" Width="80px" HeaderText="不良" />
                        <f:BoundField DataField="Probadnote" SortField="Probadnote" Width="120px" HeaderText="症状" />
                        <f:BoundField DataField="Probadset" SortField="Probadset" Width="150px" HeaderText="个所" />
                        <f:BoundField DataField="Probadreason" SortField="Probadreason" Width="150px" HeaderText="原因" />



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

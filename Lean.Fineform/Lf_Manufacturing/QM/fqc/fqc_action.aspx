<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fqc_action.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.QM.fqc.fqc_action" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="分析对策及检证">
            <Items>
                <f:Form ID="Form2" runat="server" Height="36px" BodyPadding="5px" ShowHeader="false"
                    ShowBorder="false" LabelAlign="Right">
                    <Rows>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:DatePicker runat="server" Label="<%$ Resources:GlobalResource,Query_Startdate%>" DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Startdate_EmptyText%>" AutoPostBack="true"
                                    ID="DPstart" ShowRedStar="True" OnTextChanged="DPstart_TextChanged">
                                </f:DatePicker>
                                <f:DatePicker ID="DPend" Readonly="false" CompareControl="DPstart" DateFormatString="yyyyMMdd" AutoPostBack="true"
                                    CompareOperator="GreaterThan" CompareMessage="<%$ Resources:GlobalResource,Query_Enddate_EmptyText%>" Label="<%$ Resources:GlobalResource,Query_Enddate%>"
                                    runat="server" ShowRedStar="True" OnTextChanged="DPend_TextChanged">
                                </f:DatePicker>
                                <f:DropDownList ID="DDLline" Label="<%$ Resources:GlobalResource,Query_Pp_Line%>" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DDLline_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Qm_EmptyText%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>

                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true"
                    DataKeyNames="GUID" AllowSorting="true" OnSort="Grid1_Sort" SortField="qmCheckdate"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound" ForceFit="true"  EnableTextSelection="true"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
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
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_Print%>" ColumnID="printField"
                            Icon="Printer" Width="70px" ToolTip="<%$ Resources:GlobalResource,sys_Button_Print%>"
                            CommandName="Pint" Text="<%$ Resources:GlobalResource,sys_Button_Print%>" />
                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_Edit%>" ColumnID="editField"
                            Icon="TableEdit" Width="70px" ToolTip="<%$ Resources:GlobalResource,sys_Button_Edit%>"
                            CommandName="Edit" Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" />

                        <f:BoundField DataField="qmIssueno" SortField="qmIssueno" Width="100px" HeaderText="发行NO" />
                        <f:BoundField DataField="qmLotserial" SortField="qmLotserial" Width="100px" HeaderText="生产批号" />
                        <f:BoundField DataField="qmDate" SortField="qmDate" Width="100px" HeaderText="对应日期" />
                        <f:BoundField DataField="qmPersonnel" SortField="qmPersonnel" Width="100px" HeaderText="对策人员" />
                        <f:BoundField DataField="qmDate" SortField="qmDate" Width="100px" HeaderText="直接发生原因" />

                        <f:BoundField DataField="qmCarryoutdate" SortField="qmCarryoutdate" Width="100px" HeaderText="检证日期" />
                        <f:BoundField DataField="qmVerify" SortField="qmVerify" Width="100px" HeaderText="检证人员" />
                        <f:BoundField DataField="qmSolutionsverify" SortField="qmSolutionsverify" Width="100px" HeaderText="对策实施检证" />

                        <f:BoundField DataField="qmProlot" SortField="qmProlot" Width="100px" HeaderText="查检日期" />
                        <f:BoundField DataField="qmInspector" SortField="qmInspector" Width="60px" HeaderText="检查员" />
                        <f:BoundField DataField="qmLine" SortField="qmLine" Width="100px" HeaderText="班别" />
                        <f:BoundField DataField="qmOrder" SortField="qmOrder" Width="100px" HeaderText="生产工单" />
                        <f:BoundField DataField="qmCheckNotes" SortField="qmCheckNotes" Width="100px" HeaderText="说明" />
                        <f:BoundField DataField="qmRejectqty" SortField="qmRejectqty" Width="100px" HeaderText="验退数" />
                        <f:BoundField DataField="qmJudgmentlevel" SortField="qmJudgmentlevel" Width="100px" HeaderText="不良级别" />
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete"
                            HeaderText="<%$ Resources:GlobalResource,sys_Button_Delete%>" Text="<%$ Resources:GlobalResource,sys_Button_Delete%>"
                            ToolTip="<%$ Resources:GlobalResource,sys_Button_Delete%>" ConfirmText="<%$ Resources:GlobalResource,sys_Button_DeleteConfirmText%>"
                            ConfirmTarget="Top" CommandName="Delete" Width="50px" />

                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="850px"
            Height="770px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>

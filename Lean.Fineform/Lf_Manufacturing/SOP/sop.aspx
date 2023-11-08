<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="sop.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.SOP.sop" %>

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
            ShowHeader="false" Title="设计变更ECN">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" ForceFit="true" EnableTextSelection="true"
                    DataKeyNames="ID" AllowSorting="true" SortField="Ec_issuedate"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true"
                    OnPageIndexChange="Grid1_PageIndexChange"
                    OnSort="Grid1_Sort"
                    OnRowDataBound="Grid1_RowDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                <f:DatePicker runat="server" Label="<%$ Resources:GlobalResource,Query_Startdate%>" DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Startdate_EmptyText%>" AutoPostBack="true"
                                    ID="DPstart" ShowRedStar="True" OnTextChanged="DPstart_TextChanged">
                                </f:DatePicker>
                                <f:ToolbarFill ID="ToolbarFill4" runat="server"></f:ToolbarFill>
                                <f:DatePicker ID="DPend" Readonly="false" CompareControl="DPstart" DateFormatString="yyyyMMdd" AutoPostBack="true"
                                    CompareOperator="GreaterThan" CompareMessage="<%$ Resources:GlobalResource,Query_Enddate_EmptyText%>" Label="<%$ Resources:GlobalResource,Query_Enddate%>"
                                    runat="server" ShowRedStar="True" OnTextChanged="DPend_TextChanged">
                                </f:DatePicker>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Pp_EC_EmptyText%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>

                            </Items>
                        </f:Toolbar>

                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>

                                <f:RadioButton ID="rbtnFirstAuto" Label="" Checked="true" GroupName="MyRadioGroup2"
                                    Text="<%$ Resources:GlobalResource, sys_Status_Pp_EC_Unenforced %>" runat="server" OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                </f:RadioButton>
                                <f:RadioButton ID="rbtnSecondAuto" GroupName="MyRadioGroup2" ShowEmptyLabel="true" Text="<%$ Resources:GlobalResource,sys_Status_Pp_EC_Implemented%>" runat="server"
                                    OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                </f:RadioButton>
                                <f:RadioButton ID="rbtnThirdAuto" GroupName="MyRadioGroup2" ShowEmptyLabel="true" Text="<%$ Resources:GlobalResource,sys_Status_Pp_EC_All%>" runat="server"
                                    OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                </f:RadioButton>
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
                        <f:RowNumberField EnableLock="true" Width="35px" EnablePagingNumber="true" HeaderText="序号" />
                        <f:BoundField DataField="Ec_issuedate" ColumnID="Ec_issuedate" SortField="Ec_issuedate" EnableLock="true" Width="100px" HeaderText="发行日期" />
                        <f:BoundField DataField="Ec_leader" ColumnID="Ec_leader" SortField="Ec_leader" EnableLock="true" Width="100px" HeaderText="技术担当" />
                        <f:HyperLinkField DataTextField="Ec_no" SortField="Ec_no" ColumnID="Ec_no" Width="100px" HeaderText="设变号码" MinWidth="100px" DataNavigateUrlFields="Ec_documents" />
                        <f:BoundField DataField="Ec_model" ColumnID="Ec_model" SortField="Ec_model" EnableLock="true" Width="100px" HeaderText="机种" />
                        <f:BoundField DataField="Ec_entrydate" ColumnID="Ec_entrydate" SortField="Ec_entrydate" EnableLock="true" Width="100px" HeaderText="登录日期" />
                        <%--<f:BoundField DataField="Ec_qadate" ColumnID="Ec_qadate" SortField="Ec_qadate" EnableLock="true" Width="100px" HeaderText="实施日期" />--%>
                        <%--<f:BoundField DataField="Ec_pmcdate" ColumnID="Ec_pmcdate" SortField="Ec_pmcdate" EnableLock="true" Width="100px" HeaderText="生管课" />--%>
                        <%--<f:BoundField DataField="Ec_bomitem" ColumnID="Ec_bomitem" SortField="Ec_bomitem" EnableLock="true" Width="150px" HeaderText="成品物料" />--%>
                        <%--<f:BoundField DataField="Ec_bomsubitem" ColumnID="Ec_bomsubitem" SortField="Ec_bomsubitem" EnableLock="true" Width="150px" HeaderText="上阶物料" />--%>
                        <f:GroupField HeaderText="制技课SOP确认" TextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="Ec_pengadate" ColumnID="Ec_pengadate" SortField="Ec_pengadate" EnableLock="true" Width="100px" HeaderText="组立" />
                                <f:BoundField DataField="Ec_pengpdate" ColumnID="Ec_pengpdate" SortField="Ec_pengpdate" EnableLock="true" Width="100px" HeaderText="PCB" />
                            </Columns>
                        </f:GroupField>
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


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p2d_daily.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.daily.P2D.p2d_daily" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" EnableFStateValidation="false" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="生产日报">
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
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" IsFluid="true" FixedRowHeight="true" EnableCollapse="false" ForceFit="true" EnableTextSelection="true"
                    DataKeyNames="Parent,Prolinename,Prodate,Prolot,Promodel,Prost,Prostdcapacity,Proorder" AllowSorting="true" OnSort="Grid1_Sort" SortField="ID"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="BtnList" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnList_Click" CssClass="marginr">
                                </f:Button>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="btnP1dNew" runat="server" Icon="Add" EnablePostBack="false" Text="<%$ Resources:GlobalResource,sys_Button_New%>" CssClass="marginr" Pressed="True">
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
                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Button_Edit%>" ColumnID="editField"
                            Icon="ReportEdit" Width="70px" CommandName="EditOph" Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" />
                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,sys_Actual_Edit%>" ColumnID="subeditField"
                            Icon="NoteEdit" Width="70px" CommandName="EditOphsub" Text="<%$ Resources:GlobalResource,sys_Actual_Edit%>" />
                        <f:BoundField DataField="ID" SortField="ID" ColumnID="ID" Width="60px" Hidden="True" HeaderText="ID" />
                        <f:BoundField DataField="GUID" SortField="GUID" ColumnID="GUID" Width="60px" Hidden="True" HeaderText="GUID" />
                        <f:BoundField DataField="Parent" SortField="Parent" ColumnID="Parent" Width="60px" Hidden="True" HeaderText="Parent" />
                        <f:BoundField DataField="Proordertype" SortField="Proordertype" ColumnID="Proordertype" Width="60px" Hidden="True" HeaderText="工单类别" />
                        <f:BoundField DataField="Proorder" SortField="Proorder" ColumnID="Proorder" Width="60px" Hidden="False" HeaderText="工单号" />

                        <f:BoundField DataField="Prolinename" SortField="Prolinename" ColumnID="Prolinename" Width="60px" Hidden="False" HeaderText="班组" />
                        <f:BoundField DataField="Prodate" SortField="Prodate" ColumnID="Prodate" Width="60px" Hidden="False" HeaderText="生产日期" />
                        <f:BoundField DataField="Prodirect" SortField="Prodirect" ColumnID="Prodirect" Width="60px" Hidden="True" HeaderText="直接" />
                        <f:BoundField DataField="Proindirect" SortField="Proindirect" ColumnID="Proindirect" Width="60px" Hidden="True" HeaderText="间接" />
                        <f:BoundField DataField="Prolot" SortField="Prolot" ColumnID="Prolot" Width="60px" Hidden="False" HeaderText="批次LOT" />
                        <f:BoundField DataField="Promodel" SortField="Promodel" ColumnID="Promodel" Width="60px" Hidden="True" HeaderText="机种" />
                        <f:BoundField DataField="Prohbn" SortField="Prohbn" ColumnID="Prohbn" Width="60px" Hidden="True" HeaderText="物料" />
                        <f:BoundField DataField="Propcbatype" SortField="Propcbatype" ColumnID="Propcbatype" Width="60px" Hidden="False" HeaderText="Pcb类别" />
                        <f:BoundField DataField="Propcbaside" SortField="Propcbaside" ColumnID="Propcbaside" Width="60px" Hidden="False" HeaderText="板面" />
                        <f:BoundField DataField="Prost" SortField="Prost" ColumnID="Prost" Width="60px" Hidden="True" HeaderText="ST" />
                        <f:BoundField DataField="Proshort" SortField="Proshort" ColumnID="Proshort" Width="60px" Hidden="True" HeaderText="点数" />
                        <f:BoundField DataField="Prorate" SortField="Prorate" ColumnID="Prorate" Width="60px" Hidden="True" HeaderText="汇率" />
                        <f:BoundField DataField="Prostdcapacity" SortField="Prostdcapacity" ColumnID="Prostdcapacity" Width="60px" Hidden="True" HeaderText="标产" />
                        <f:BoundField DataField="Totaltag" SortField="Totaltag" ColumnID="Totaltag" Width="60px" Hidden="True" HeaderText="标记" />
                        <f:BoundField DataField="Prostime" SortField="Prostime" ColumnID="Prostime" Width="60px" Hidden="True" HeaderText="生产时段" />
                        <f:BoundField DataField="Proetime" SortField="Proetime" ColumnID="Proetime" Width="60px" Hidden="True" HeaderText="生产时段" />
                        <f:BoundField DataField="Proorderqty" SortField="Proorderqty" ColumnID="Proorderqty" Width="60px" Hidden="True" HeaderText="工单数" />
                        <f:BoundField DataField="UDF54" SortField="UDF54" ColumnID="UDF54" Width="60px" Hidden="False" HeaderText="Lot数" />
                        <f:BoundField DataField="Prorealqty" SortField="Prorealqty" ColumnID="Prorealqty" Width="60px" Hidden="False" HeaderText="生产实绩" />
                        <f:BoundField DataField="Prorealtotal" SortField="Prorealtotal" ColumnID="Prorealtotal" Width="60px" Hidden="False" HeaderText="累计生产" />
                        <f:BoundField DataField="Propcbserial" SortField="Propcbserial" ColumnID="Propcbserial" Width="60px" Hidden="True" HeaderText="序列号" />
                        <f:BoundField DataField="Prolinestopmin" SortField="Prolinestopmin" ColumnID="Prolinestopmin" Width="60px" Hidden="True" HeaderText="停线时间" />
                        <f:BoundField DataField="Prostopcou" SortField="Prostopcou" ColumnID="Prostopcou" Width="60px" Hidden="True" HeaderText="停线原因" />
                        <f:BoundField DataField="Prostopmemo" SortField="Prostopmemo" ColumnID="Prostopmemo" Width="60px" Hidden="True" HeaderText="停线说明" />
                        <f:BoundField DataField="Probadcou" SortField="Probadcou" ColumnID="Probadcou" Width="60px" Hidden="True" HeaderText="未达成" />
                        <f:BoundField DataField="Probadmemo" SortField="Probadmemo" ColumnID="Probadmemo" Width="60px" Hidden="True" HeaderText="说明" />
                        <f:BoundField DataField="Prolinemin" SortField="Prolinemin" ColumnID="Prolinemin" Width="60px" Hidden="True" HeaderText="生产工数" />
                        <f:BoundField DataField="Prorealtime" SortField="Prorealtime" ColumnID="Prorealtime" Width="60px" Hidden="True" HeaderText="实际工数" />
                        <f:BoundField DataField="Propcbastated" SortField="Propcbastated" ColumnID="Propcbastated" Width="60px" Hidden="False" HeaderText="完成状态" />
                        <f:BoundField DataField="Protime" SortField="Protime" ColumnID="Protime" Width="60px" Hidden="False" HeaderText="生产工数" />
                        <f:BoundField DataField="Prohandoffnum" SortField="Prohandoffnum" ColumnID="Prohandoffnum" Width="60px" Hidden="False" HeaderText="切换次数" />
                        <f:BoundField DataField="Prohandofftime" SortField="Prohandofftime" ColumnID="Prohandofftime" Width="60px" Hidden="False" HeaderText="切换时间" />
                        <f:BoundField DataField="Prodowntime" SortField="Prodowntime" ColumnID="Prodowntime" Width="60px" Hidden="False" HeaderText="切停机时间" />
                        <f:BoundField DataField="Prolosstime" SortField="Prolosstime" ColumnID="Prolosstime" Width="60px" Hidden="False" HeaderText="损失工数" />
                        <f:BoundField DataField="Promaketime" SortField="Promaketime" ColumnID="Promaketime" Width="60px" Hidden="False" HeaderText="投入工数" />
                        <f:BoundField DataField="Proworkst" SortField="Proworkst" ColumnID="Proworkst" Width="60px" Hidden="True" HeaderText="实绩生产工时" />
                        <f:BoundField DataField="Prostdiff" SortField="Prostdiff" ColumnID="Prostdiff" Width="60px" Hidden="True" HeaderText="工时差异" />
                        <f:BoundField DataField="Proqtydiff" SortField="Proqtydiff" ColumnID="Proqtydiff" Width="60px" Hidden="True" HeaderText="预计投入台数" />
                        <f:BoundField DataField="Proratio" SortField="Proratio" ColumnID="Proratio" Width="60px" Hidden="True" HeaderText="稼动率" />
                        <f:BoundField DataField="UDF01" SortField="UDF01" ColumnID="UDF01" Width="60px" Hidden="True" HeaderText="自定义A" />
                        <f:BoundField DataField="UDF02" SortField="UDF02" ColumnID="UDF02" Width="60px" Hidden="True" HeaderText="自定义B" />
                        <f:BoundField DataField="UDF03" SortField="UDF03" ColumnID="UDF03" Width="60px" Hidden="True" HeaderText="自定义C" />
                        <f:BoundField DataField="UDF04" SortField="UDF04" ColumnID="UDF04" Width="60px" Hidden="True" HeaderText="自定义D" />
                        <f:BoundField DataField="UDF05" SortField="UDF05" ColumnID="UDF05" Width="60px" Hidden="True" HeaderText="自定义E" />
                        <f:BoundField DataField="UDF06" SortField="UDF06" ColumnID="UDF06" Width="60px" Hidden="True" HeaderText="自定义F" />
                        <f:BoundField DataField="UDF51" SortField="UDF51" ColumnID="UDF51" Width="60px" Hidden="False" HeaderText="不良台数" />
                        <f:BoundField DataField="UDF52" SortField="UDF52" ColumnID="UDF52" Width="60px" Hidden="False" HeaderText="修正仕损" />
                        <f:BoundField DataField="UDF53" SortField="UDF53" ColumnID="UDF53" Width="60px" Hidden="False" HeaderText="手插仕损" />
                        <%--<f:BoundField DataField="UDF54" SortField="UDF54" ColumnID="UDF54" Width="60px" Hidden="True" HeaderText="自定义4" />--%>
                        <f:BoundField DataField="UDF55" SortField="UDF55" ColumnID="UDF55" Width="60px" Hidden="True" HeaderText="自定义5" />
                        <f:BoundField DataField="UDF56" SortField="UDF56" ColumnID="UDF56" Width="60px" Hidden="True" HeaderText="自定义6" />
                        <f:BoundField DataField="isDeleted" SortField="isDeleted" ColumnID="isDeleted" Width="60px" Hidden="True" HeaderText="软删除" />
                        <f:BoundField DataField="Remark" SortField="Remark" ColumnID="Remark" Width="60px" Hidden="True" HeaderText="备注" />
                        <f:BoundField DataField="Creator" SortField="Creator" ColumnID="Creator" Width="60px" Hidden="True" HeaderText="创建者" />
                        <f:BoundField DataField="CreateDate" SortField="CreateDate" ColumnID="CreateDate" Width="60px" Hidden="True" HeaderText="创建时间" />
                        <f:BoundField DataField="Modifier" SortField="Modifier" ColumnID="Modifier" Width="60px" Hidden="True" HeaderText="更新者" />
                        <f:BoundField DataField="ModifyDate" SortField="ModifyDate" ColumnID="ModifyDate" Width="60px" Hidden="True" HeaderText="更新时间" />




                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete"
                            HeaderText="<%$ Resources:GlobalResource,sys_Button_Delete%>" Text="<%$ Resources:GlobalResource,sys_Button_Delete%>"
                            ToolTip="<%$ Resources:GlobalResource,sys_Button_Delete%>" ConfirmText="<%$ Resources:GlobalResource,sys_Button_DeleteConfirmText%>"
                            ConfirmTarget="Top" CommandName="Delete" Width="70px" />
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

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ec_report.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.EC.ec_report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"  
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="设计变更ECN">
            <Items>
                <f:Form ID="Form2" runat="server" Height="36px" BodyPadding="5px" ShowHeader="false"
                    ShowBorder="false" LabelAlign="Right" >
                    <Rows>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:DatePicker runat="server" Label="<%$ Resources:GlobalResource,Query_Startdate%>" DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Startdate_EmptyText%>" AutoPostBack="true"
                                    ID="DpStartDate" ShowRedStar="True" OnTextChanged="DpStartDate_TextChanged">
                                </f:DatePicker>
                                <f:DatePicker ID="DpEndDate" Readonly="false" Width="300px" CompareControl="DpStartDate" DateFormatString="yyyyMMdd" AutoPostBack="true"
                                    CompareOperator="GreaterThan" CompareMessage="<%$ Resources:GlobalResource,Query_Enddate_EmptyText%>" Label="<%$ Resources:GlobalResource,Query_Enddate%>"
                                    runat="server" ShowRedStar="True" OnTextChanged="DpEndDate_TextChanged">
                                </f:DatePicker>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Pp_EC_EmptyText%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>

                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCollapse="false" AllowColumnLocking="true" ForceFit="true" EnableTextSelection="true"
                    DataKeyNames="ID" AllowSorting="true" OnSort="Grid1_Sort"  SortField="Ec_issuedate"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false" 
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnExport_Click" CssClass="marginr">  </f:Button>
                                <f:Button ID="Btn2003" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"  
                                     runat="server" Text="2003" OnClick="Btn2003_Click" >  </f:Button> 
                                
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <PageItems>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:ToolbarText ID="ToolbarText1" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>" >
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
<%--                            <f:WindowField ColumnID="editField" TextAlign="Center" Icon="Pencil" ToolTip="编辑"
                                 WindowID="Window1" Title="编辑" DataIFrameUrlFields="ID" DataIFrameUrlFormatString="~/oneProduction/oneTimesheet/ecn_eng_edit.aspx?id={0}"
                                 Width="50px" />--%>
                            <f:RowNumberField Width="35px" EnablePagingNumber="true" EnableLock="true" Locked="true" HeaderText="序号" />
                            <f:BoundField DataField="Prostatus" SortField="Prostatus "    Width="100px" DataFormatString="{0}" EnableLock="true" Locked="true" HeaderText="实施状态"></f:BoundField>
                        <f:GroupField HeaderText="品管课" TextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="Ec_qadate" SortField="Ec_qadate "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="检查日期"></f:BoundField>
                                <f:BoundField DataField="Ec_qalot" SortField="Ec_qalot "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="生产批次"></f:BoundField>
                                <f:BoundField DataField="Ec_qalotsn" SortField="Ec_qalotsn "    Width="100px" DataFormatString="{0}" EnableLock="true" HtmlEncode="false" HeaderText="检验批次"></f:BoundField>
                                <f:BoundField DataField="Ec_qanote" SortField="Ec_qanote "    Width="100px" DataFormatString="{0}" EnableLock="true" HtmlEncode="false" HeaderText="备注说明"></f:BoundField>
                            </Columns>
                        </f:GroupField>
                        <f:GroupField HeaderText="部管课" TextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="Ec_bstock" SortField="Ec_bstock "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="旧品在库"></f:BoundField>
                                <f:BoundField DataField="Ec_mmdate" SortField="Ec_mmdate "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="出库日期"></f:BoundField>
                                <f:BoundField DataField="Ec_mmlot" SortField="Ec_mmlot "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="批次"></f:BoundField>
                                <f:BoundField DataField="Ec_mmnote" SortField="Ec_mmnote "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="注意事项"></f:BoundField>
                            </Columns>
                        </f:GroupField>
                        <f:GroupField HeaderText="生管课" TextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="Ec_pmcdate" SortField="Ec_pmcdate "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="投入日期"></f:BoundField>
                                <f:BoundField DataField="Ec_pmclot" SortField="Ec_pmclot "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="投入批次"></f:BoundField>
                                <f:BoundField DataField="Ec_pmcnotedate" SortField="Ec_pmcnotedate "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="更新日期"></f:BoundField>
                                <f:BoundField DataField="Ec_pmcsn" SortField="Ec_pmcsn "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="处理说明"></f:BoundField>
                                <f:BoundField DataField="Ec_pmcnote" SortField="Ec_pmcnote "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="注意事项"></f:BoundField>
                            </Columns>
                        </f:GroupField>
                        <f:GroupField HeaderText="设变信息" TextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="Ec_no" SortField="Ec_no "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="设变号码"></f:BoundField>
                                <f:BoundField DataField="Ec_issuedate" SortField="Ec_issuedate "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="发行日期"></f:BoundField>
                                <f:BoundField DataField="Ec_status" SortField="Ec_status "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="SAP状态"></f:BoundField>
                                <f:BoundField DataField="Ec_model" SortField="Ec_model "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="机种"></f:BoundField>
                                <f:BoundField DataField="Ec_bomitem" SortField="Ec_bomitem "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="完成品"></f:BoundField>
                                <f:BoundField DataField="Ec_bomsubitem" SortField="Ec_bomsubitem "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="上阶品号"></f:BoundField>
                                <f:BoundField DataField="Ec_olditem" SortField="Ec_olditem "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="旧品号"></f:BoundField>
                                <f:BoundField DataField="Ec_newitem" SortField="Ec_newitem "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="新品号"></f:BoundField>
                                <f:BoundField DataField="Ec_bomno" SortField="Ec_bomno "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="BOM番号"></f:BoundField>
                                <f:BoundField DataField="Ec_change" SortField="Ec_change "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="互換性"></f:BoundField>
                                <f:BoundField DataField="Ec_local" SortField="Ec_local "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="セカンド区分"></f:BoundField>
                                <f:BoundField DataField="Ec_note" SortField="Ec_note "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="手配指示"></f:BoundField>
                                <f:BoundField DataField="Ec_process" SortField="Ec_process "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="旧部品処"></f:BoundField>
                                <f:BoundField DataField="Ec_bomdate" SortField="Ec_bomdate "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="生效日期"></f:BoundField>
                            </Columns>
                        </f:GroupField>
                        <f:GroupField HeaderText="技术部" TextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="Ec_details" SortField="Ec_details "    Width="300px" DataFormatString="{0}" EnableLock="true" HtmlEncode="false" HeaderText="内容"></f:BoundField>
                                <f:BoundField DataField="Ec_leader" SortField="Ec_leader "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="担当"></f:BoundField>
                                <f:BoundField DataField="Ec_distinction" SortField="Ec_distinction "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="管理区分"></f:BoundField>
                                <f:BoundField DataField="Ec_distinction" SortField="Ec_distinction "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="实施区分"></f:BoundField>
                                <f:BoundField DataField="Ec_letterno" SortField="Ec_letterno "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="技联No."></f:BoundField>
                                <f:BoundField DataField="Ec_eppletterno" SortField="Ec_eppletterno "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="P番No."></f:BoundField>
                            </Columns>
                        </f:GroupField>
                        <f:GroupField HeaderText="资材部" TextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="Ec_issuedate" SortField="Ec_issuedate "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="订单日期"></f:BoundField>
                                <f:BoundField DataField="Ec_issueno" SortField="Ec_issueno "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="订单号码"></f:BoundField>
                                <f:BoundField DataField="Ec_issue" SortField="Ec_issue "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="供应商"></f:BoundField>
                                <f:BoundField DataField="Ec_purnote" SortField="Ec_purnote "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="注意事项"></f:BoundField>
                            </Columns>
                        </f:GroupField>
                        <f:GroupField HeaderText="受检课" TextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="Ec_iqcdate" SortField="Ec_iqcdate "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="检查日期"></f:BoundField>
                                <f:BoundField DataField="Ec_iqclot" SortField="Ec_iqclot "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="检查批次"></f:BoundField>
                                <f:BoundField DataField="Ec_iqcorder" SortField="Ec_iqcorder "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="采购单"></f:BoundField>
                                <f:BoundField DataField="Ec_iqcnote" SortField="Ec_iqcnote "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="说明"></f:BoundField>
                            </Columns>
                        </f:GroupField>
                        <f:GroupField HeaderText="制一课" TextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="Ec_p1ddate" SortField="Ec_p1ddate "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="生产日期"></f:BoundField>
                                <f:BoundField DataField="Ec_p1dline" SortField="Ec_p1dline "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="生产班组"></f:BoundField>
                                <f:BoundField DataField="Ec_p1dlot" SortField="Ec_p1dlot "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="生产批次"></f:BoundField>
                                <f:BoundField DataField="Ec_p1dlotsn" SortField="Ec_p1dlotsn "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="批次说明"></f:BoundField>
                                <f:BoundField DataField="Ec_p1dnote" SortField="Ec_p1dnote "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="备注说明"></f:BoundField>
                            </Columns>
                        </f:GroupField>
                        <f:GroupField HeaderText="制二课" TextAlign="Center">
                            <Columns>
                                <f:BoundField DataField="Ec_p2ddate" SortField="Ec_p2ddate "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="生产日期"></f:BoundField>
                                <f:BoundField DataField="Ec_p2dline" SortField="Ec_p2dline "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="生产班组"></f:BoundField>
                                <f:BoundField DataField="Ec_p2dlot" SortField="Ec_p2dlot "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="生产批次"></f:BoundField>
                                <f:BoundField DataField="Ec_p2dlotsn" SortField="Ec_p2dlotsn "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="批次说明"></f:BoundField>
                                <f:BoundField DataField="Ec_p2dnote" SortField="Ec_p2dnote "    Width="100px" DataFormatString="{0}" EnableLock="true" HeaderText="备注说明"></f:BoundField>
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

﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="qa.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.EC.dept.qa" %>

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
                <f:Form ID="Form2" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
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
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"  ForceFit="true"                   
                    DataKeyNames="ID" AllowSorting="true" SortField="Ec_issuedate" EnableTextSelection="true"
                    SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true"
                    OnPageIndexChange="Grid1_PageIndexChange"                      
                    OnSort="Grid1_Sort"
                    OnRowDataBound="Grid1_RowDataBound" 
                    OnPreRowDataBound="Grid1_PreRowDataBound">

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
                        <f:RowNumberField  EnableLock="true" Width="35px" EnablePagingNumber="true" HeaderText="序号" />
                        <f:BoundField DataField="Ec_issuedate" ColumnID="Ec_issuedate" SortField="Ec_issuedate"  EnableLock="true" Width="100px" HeaderText="发行日期"/>
                        <f:HyperLinkField DataTextField="Ec_no"  SortField="Ec_no" ColumnID="Ec_no" Width="100px" HeaderText="设变号码" MinWidth="100px" DataNavigateUrlFields="Ec_documents"/>
                        <f:BoundField DataField="Ec_model" ColumnID="Ec_model" SortField="Ec_model"  EnableLock="true" Width="100px" HeaderText="机种"/>             
                        <f:BoundField DataField="Ec_bomitem" ColumnID="Ec_bomitem" SortField="Ec_bomitem"  EnableLock="true" Width="100px" HeaderText="成品料号"/>
                        <f:BoundField DataField="Ec_bomsubitem" ColumnID="Ec_bomsubitem" SortField="Ec_bomsubitem"  EnableLock="true" Width="100px" HeaderText="上阶物料"/>  
                        <f:BoundField DataField="Ec_olditem" ColumnID="Ec_olditem" SortField="Ec_olditem"  EnableLock="true" Width="100px" HeaderText="旧品"/>
                        <f:BoundField DataField="Ec_newitem" ColumnID="Ec_newitem" SortField="Ec_newitem"  EnableLock="true" Width="100px" HeaderText="新品"/>    

                        <f:BoundField DataField="Ec_qadate" ColumnID="Ec_qadate" SortField="Ec_qadate"  EnableLock="true" Width="100px" HeaderText="抽样日期"/>  
                        <f:BoundField DataField="Ec_qalot" ColumnID="Ec_qalot" SortField="Ec_qalot"  EnableLock="true" Width="100px" HeaderText="抽样批次"/>
                        <f:BoundField DataField="Ec_qanote" ColumnID="Ec_qanote" SortField="Ec_qanote"  EnableLock="true" Width="100px" HeaderText="说明"/> 

                        <f:BoundField DataField="qaModifier" ColumnID="qaModifier" SortField="qaModifier"  EnableLock="true" Width="100px" HeaderText="登录人员"/>
                        <f:BoundField DataField="qaModifyDate" ColumnID="qaModifyDate" SortField="qaModifyDate"  EnableLock="true" Width="100px" HeaderText="登录日期"/> 

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

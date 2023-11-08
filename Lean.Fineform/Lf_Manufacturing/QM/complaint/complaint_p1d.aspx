<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="complaint_p1d.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.QM.complaint.complaint_p1d" %>

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
            ShowHeader="false" Title="LeanCloud">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true"
                    DataKeyNames="GUID,Cc_IssuesNo" AllowSorting="true" OnSort="Grid1_Sort" SortField="Cc_IssuesNo"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound" EnableTextSelection="true"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Enter_Text%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                            </Items>
                        </f:Toolbar>
                        <f:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                <f:RadioButton ID="rbtnFirstAuto" Label="" Checked="true" GroupName="MyRadioGroup2"
                                    Text="<%$ Resources:GlobalResource, sys_Status_Unprocessed %>" runat="server" OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                </f:RadioButton>
                                <f:RadioButton ID="rbtnSecondAuto" GroupName="MyRadioGroup2" ShowEmptyLabel="true" Text="<%$ Resources:GlobalResource,sys_Status_Processed%>" runat="server"
                                    OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                </f:RadioButton>
                                <f:RadioButton ID="rbtnThirdAuto" GroupName="MyRadioGroup2" ShowEmptyLabel="true" Text="<%$ Resources:GlobalResource,sys_Status_All%>" runat="server"
                                    OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                </f:RadioButton>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>
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
                        <f:RowNumberField Width="35px" EnablePagingNumber="true" />
                        <f:LinkButtonField HeaderText="<%$ Resources:GlobalResource,co_Dept_P1D%>" ColumnID="p1deditField"
                            Icon="TableEdit" Width="100px" ToolTip="<%$ Resources:GlobalResource,sys_Button_Edit%>"
                            CommandName="PEdit" Text="<%$ Resources:GlobalResource,sys_Button_Edit%>" />


                        <f:BoundField DataField="Cc_ReceivingDate" SortField="Cc_ReceivingDate" Width="100px" HeaderText="日期" />
                        <f:BoundField DataField="Cc_Customer" SortField="Cc_Customer" Width="100px" HeaderText="客户" />
                        <f:HyperLinkField DataTextField="Cc_IssuesNo" SortField="Cc_IssuesNo" Width="100px" HeaderText="客诉" DataNavigateUrlFields="Cc_Reference" />
                        <f:BoundField DataField="Cc_Model" SortField="Cc_Model" Width="100px" HeaderText="机种名" />
                        <f:BoundField DataField="Cc_DefectsQty" SortField="Cc_DefectsQty" Width="100px" HeaderText="数量" />
                        <f:BoundField DataField="Cc_Issues" SortField="Cc_Issues" Width="200px" HeaderText="投诉事项" />

                        <f:BoundField DataField="Cc_Line" SortField="Cc_Line" Width="80px" HeaderText="班组" />
                        <f:BoundField DataField="Cc_ProcessDate" SortField="Cc_ProcessDate" Width="120px" HeaderText="处理日期" />
                        <f:BoundField DataField="Cc_Ddescription" SortField="Cc_Ddescription" Width="350px" HeaderText="症状" />
                        <f:BoundField DataField="Cc_Reasons" SortField="Cc_Reasons" Width="350px" HeaderText="原因" />
                        <f:BoundField DataField="Cc_Operator" SortField="Cc_Operator" Width="100px" HeaderText="作业员" />
                        <f:BoundField DataField="Cc_Station" SortField="Cc_Station" Width="100px" HeaderText="工位" />
                        <f:BoundField DataField="Cc_Lot" SortField="Cc_Lot" Width="150px" HeaderText="批次" />
                        <f:BoundField DataField="Cc_CorrectActions" SortField="Cc_CorrectActions" Width="350px" HeaderText="对策" />


                    </Columns>
                </f:Grid>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="800px"
            Height="700px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>


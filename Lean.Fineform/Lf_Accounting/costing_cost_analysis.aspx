<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="costing_cost_analysis.aspx.cs" Inherits="Lean.Fineform.Lf_Accounting.costing_cost_analysis" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .f-grid-row-summary .f-grid-cell-inner {
            font-weight: bold;
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" EnableFStateValidation="false" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="费用预算">
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" EnableTextSelection="true"
                    EnableCheckBoxSelect="true" ForceFit="true"
                    DataKeyNames="Bc_Item" AllowSorting="true" OnSort="Grid1_Sort" SortField="Bc_Item"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound" EnableSummary="true" SummaryPosition="Bottom"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:DatePicker ID="DPend" Readonly="false" CompareControl="DPstart" DateFormatString="yyyyMM" AutoPostBack="true"
                                    CompareOperator="GreaterThan" CompareMessage="<%$ Resources:GlobalResource,Query_Enddate_EmptyText%>" Label="<%$ Resources:GlobalResource,Query_Select_Date%>"
                                    runat="server" ShowRedStar="True" OnTextChanged="DPend_TextChanged">
                                </f:DatePicker>
                                <f:TwinTriggerBox ID="ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Enter_Text%>"
                                    Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="ttbSearchMessage_Trigger2Click"
                                    OnTrigger1Click="ttbSearchMessage_Trigger1Click">
                                </f:TwinTriggerBox>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server"></f:ToolbarFill>


                                <f:RadioButton ID="rbtnFirstAuto" Label="" Checked="true" GroupName="MyRadioGroup2" Text="<%$ Resources:GlobalResource,rpt_Chart_Pcost%>" runat="server"
                                    OnCheckedChanged="rbtnAuto_CheckedChanged" AutoPostBack="true">
                                </f:RadioButton>
                                <f:RadioButton ID="rbtnSecondAuto" GroupName="MyRadioGroup2" ShowEmptyLabel="true" Text="<%$ Resources:GlobalResource,rpt_Chart_Fob%>" runat="server"
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
                            <f:ListItem Text="1000" Value="1000" />
                            <f:ListItem Text="5000" Value="5000" />
                        </f:DropDownList>
                    </PageItems>
                    <Columns>
                        <f:BoundField DataField="Bc_Item" HeaderText="品目" />
                        <f:BoundField DataField="Bc_ItemText" HeaderText="品目Text" />
                        <f:BoundField DataField="Bc_MovingCost" HeaderText="材料费" />
                        <f:BoundField DataField="Bc_MovingAverage" HeaderText="移动平均" DataFormatString="{0:F5}" />
                        <f:BoundField DataField="Bc_Diff" HeaderText="生产成本" DataFormatString="{0:F5}" />
                        <f:BoundField DataField="Bc_TcjFob" HeaderText="TACFOB" DataFormatString="{0:F5}" />
                        <f:BoundField DataField="Bc_TcjCurrency" HeaderText="币种T" />
                        <f:BoundField DataField="Bc_DtaFob" HeaderText="DTAFOB" DataFormatString="{0:F5}" />
                        <f:BoundField DataField="Bc_DtaCurrency" HeaderText="币种D" />



                    </Columns>

                </f:Grid>
                <f:HiddenField runat="server" ID="hfGrid1Summary"></f:HiddenField>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="1200px"
            Height="700px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>


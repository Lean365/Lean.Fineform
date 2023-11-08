<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="lot_tracking.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.PP.tracking.lot_tracking" %>

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
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false"
                    EnableCheckBoxSelect="true" ForceFit="true" EnableTextSelection="true"
                    DataKeyNames="Pro_Date" AllowSorting="true" OnSort="Grid1_Sort" SortField="Pro_Date,Pro_Process"
                    SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" OnPreDataBound="Grid1_PreDataBound"
                    OnPreRowDataBound="Grid1_PreRowDataBound"
                    OnRowCommand="Grid1_RowCommand" OnPageIndexChange="Grid1_PageIndexChange">
                    <Toolbars>
                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <f:DropDownList ID="DDLline" Label="<%$ Resources:GlobalResource,Query_Pp_Line%>" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DDLline_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:DropDownList ID="DDLModel" Label="<%$ Resources:GlobalResource,Query_Model%>" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DDLModel_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:DropDownList ID="DDLLot" Label="<%$ Resources:GlobalResource,Query_Pp_Lot%>" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" runat="server" OnSelectedIndexChanged="DDLLot_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                </f:ToolbarFill>
                                <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                </f:ToolbarFill>
                                <f:Button ID="Button1" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" CssClass="marginr">
                                </f:Button>
                                <f:Button ID="BtnList" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnList_Click" CssClass="marginr">
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
                        <f:BoundField DataField="Pro_Line" SortField="Pro_Line" Width="60px" HeaderText="班别" />
                        <f:BoundField DataField="Pro_Date" SortField="Pro_Date" Width="80px" HeaderText="日期" />
                        <f:BoundField DataField="Pro_Item" SortField="Pro_Item" Width="100px" HeaderText="物料" />
                        <f:BoundField DataField="Pro_Model" SortField="Pro_Model" Width="100px" HeaderText="机种" />
                        <f:BoundField DataField="Pro_Region" SortField="Pro_Region" Width="100px" HeaderText="仕向" />
                        <f:BoundField DataField="Pro_Lot" SortField="Pro_Lot" Width="80px" HeaderText="LOT" />
                        <f:BoundField DataField="Pro_Process" SortField="Pro_Process" Width="80px" HeaderText="Process" />
                        <f:BoundField DataField="Pro_MaxTime" SortField="Pro_MaxTime" Width="80px" HeaderText="Max_Sec" />
                        <f:BoundField DataField="Pro_MinTime" SortField="Pro_MinTime" Width="80px" HeaderText="Min_Sec" />
                        <f:BoundField DataField="ProcessQty" SortField="ProcessQty" Width="80px" HeaderText="ProcessQty" />
                        <f:BoundField DataField="Pro_StdDev" SortField="Pro_StdDev" Width="80px" HeaderText="StdDeviation" DataFormatString="{0:f2}" />
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


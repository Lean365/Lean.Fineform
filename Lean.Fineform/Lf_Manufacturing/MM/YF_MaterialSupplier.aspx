<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YF_MaterialSupplier.aspx.cs" Inherits="Fine.Lf_Manufacturing.MM.YF_MaterialSupplier" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .highlight {
            font-weight: bold;
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="TabStrip1" />
        <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ActiveTabIndex="0"
            runat="server">
            <Tabs>
                <f:Tab Title="H100" BodyPadding="10px" Layout="VBox" runat="server">
                    <Items>

                        <f:Form ID="Form2" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                            <Rows>
                                <f:FormRow ID="FormRow2" runat="server">
                                    <Items>
                                        <f:DatePicker runat="server" Label="<%$ Resources:GlobalResource,Query_Startdate%>" DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Startdate_EmptyText%>" AutoPostBack="true"
                                            ID="H_DPstart" ShowRedStar="True" OnTextChanged="H_DPstart_TextChanged">
                                        </f:DatePicker>
                                        <f:DatePicker ID="H_DPend" Readonly="false" Width="300px" CompareControl="H_DPstart" DateFormatString="yyyyMMdd" AutoPostBack="true"
                                            CompareOperator="GreaterThan" CompareMessage="<%$ Resources:GlobalResource,Query_Enddate_EmptyText%>" Label="<%$ Resources:GlobalResource,Query_Enddate%>"
                                            runat="server" ShowRedStar="True" OnTextChanged="H_DPend_TextChanged">
                                        </f:DatePicker>
                                        <f:TwinTriggerBox ID="H_ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Enter_EmptyText%>"
                                            Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="H_ttbSearchMessage_Trigger2Click"
                                            OnTrigger1Click="H_ttbSearchMessage_Trigger1Click">
                                        </f:TwinTriggerBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                        <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" ForceFit="true"
                            DataKeyNames="MB014" AllowSorting="true" SortField="MB014" EnableTextSelection="true"
                            SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true"
                            OnPageIndexChange="Grid1_PageIndexChange"
                            EnableMultiSelect="false" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid1_RowDoubleClick"
                            OnSort="Grid1_Sort"
                            OnRowDataBound="Grid1_RowDataBound"
                            OnPreRowDataBound="Grid1_PreRowDataBound">
                            <%--<Toolbars>

                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>                                
                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>

                                <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false" 
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnExport_Click" CssClass="marginr">  </f:Button>
                                
                            </Items>
                        </f:Toolbar>
                    </Toolbars>--%>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText1" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>">
                                </f:ToolbarText>
                                <f:DropDownList ID="ddlGridPageSize" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="H_ddlGridPageSize_SelectedIndexChanged"
                                    runat="server">
                                    <f:ListItem Text="10" Value="10" />
                                    <f:ListItem Text="20" Value="20" />
                                    <f:ListItem Text="50" Value="50" />
                                    <f:ListItem Text="100" Value="100" />
                                </f:DropDownList>

                            </PageItems>
                            <Columns>

                                <f:BoundField DataField="MB001" SortField="MB001" Width="80px" HeaderText="品号" />
                                <f:BoundField DataField="MB002" SortField="MB002" Width="80px" HeaderText="供应商编号" />
                                <f:BoundField DataField="MB003" SortField="MB003" Width="80px" HeaderText="币种" />
                                <f:BoundField DataField="MB004" SortField="MB004" Width="80px" HeaderText="计价单位" />
                                <f:BoundField DataField="MB005" SortField="MB005" Width="80px" HeaderText="初次交易日" />
                                <f:BoundField DataField="MB007" SortField="MB007" Width="80px" HeaderText="供应商品号" />
                                <f:BoundField DataField="MB008" SortField="MB008" Width="80px" HeaderText="核价日" />
                                <f:BoundField DataField="MB009" SortField="MB009" Width="80px" HeaderText="上次进货日" />
                                <f:BoundField DataField="MB010" SortField="MB010" Width="80px" HeaderText="分量计价" />
                                <f:BoundField DataField="MB011" SortField="MB011" Width="80px" HeaderText="采购单价" />
                                <f:BoundField DataField="MB013" SortField="MB013" Width="80px" HeaderText="含税" />
                                <f:BoundField DataField="MB014" SortField="MB014" Width="80px" HeaderText="生效日" />
                                <f:BoundField DataField="MB015" SortField="MB015" Width="80px" HeaderText="失效日" />





                            </Columns>
                        </f:Grid>

                    </Items>
                </f:Tab>
                <f:Tab Title="C100" BodyPadding="10px" Layout="VBox" runat="server">
                    <Items>
                        <f:Form ID="Form3" runat="server" ShowHeader="false" ShowBorder="false" LabelAlign="Right">
                            <Rows>
                                <f:FormRow ID="FormRow1" runat="server">
                                    <Items>
                                        <f:DatePicker runat="server" Label="<%$ Resources:GlobalResource,Query_Startdate%>" DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Startdate_EmptyText%>" AutoPostBack="true"
                                            ID="C_DPstart" ShowRedStar="True" OnTextChanged="C_DPstart_TextChanged">
                                        </f:DatePicker>
                                        <f:DatePicker ID="C_DPend" Readonly="false" Width="300px" CompareControl="C_DPstart" DateFormatString="yyyyMMdd" AutoPostBack="true"
                                            CompareOperator="GreaterThan" CompareMessage="<%$ Resources:GlobalResource,Query_Enddate_EmptyText%>" Label="<%$ Resources:GlobalResource,Query_Enddate%>"
                                            runat="server" ShowRedStar="True" OnTextChanged="C_DPend_TextChanged">
                                        </f:DatePicker>
                                        <f:TwinTriggerBox ID="C_ttbSearchMessage" runat="server" ShowLabel="false" EmptyText="<%$ Resources:GlobalResource,Query_Enter_EmptyText%>"
                                            Trigger1Icon="Clear" Trigger2Icon="Search" ShowTrigger1="false" OnTrigger2Click="C_ttbSearchMessage_Trigger2Click"
                                            OnTrigger1Click="C_ttbSearchMessage_Trigger1Click">
                                        </f:TwinTriggerBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                        <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" ForceFit="true"
                            DataKeyNames="MB014" AllowSorting="true" SortField="MB014" EnableTextSelection="true"
                            SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true"
                            OnPageIndexChange="Grid2_PageIndexChange"
                            EnableMultiSelect="false" EnableRowDoubleClickEvent="true" OnRowDoubleClick="Grid2_RowDoubleClick"
                            OnSort="Grid2_Sort"
                            OnRowDataBound="Grid2_RowDataBound"
                            OnPreRowDataBound="Grid2_PreRowDataBound">
                            <%--<Toolbars>

                        <f:Toolbar ID="Toolbar1" runat="server">
                            <Items>                                
                                <f:ToolbarFill ID="ToolbarFill2" runat="server"></f:ToolbarFill>

                                <f:Button ID="BtnExport" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false" 
                                    runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="BtnExport_Click" CssClass="marginr">  </f:Button>
                                
                            </Items>
                        </f:Toolbar>
                    </Toolbars>--%>
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                                <f:ToolbarText ID="ToolbarText2" runat="server" Text="<%$ Resources:GlobalResource,sys_Grid_Pagecount%>">
                                </f:ToolbarText>
                                <f:DropDownList ID="DropDownList1" Width="80px" AutoPostBack="true" OnSelectedIndexChanged="C_ddlGridPageSize_SelectedIndexChanged"
                                    runat="server">
                                    <f:ListItem Text="10" Value="10" />
                                    <f:ListItem Text="20" Value="20" />
                                    <f:ListItem Text="50" Value="50" />
                                    <f:ListItem Text="100" Value="100" />
                                </f:DropDownList>

                            </PageItems>
                            <Columns>
                                <f:BoundField DataField="MB001" SortField="MB001" Width="80px" HeaderText="品号" />
                                <f:BoundField DataField="MB002" SortField="MB002" Width="80px" HeaderText="供应商编号" />
                                <f:BoundField DataField="MB003" SortField="MB003" Width="80px" HeaderText="币种" />
                                <f:BoundField DataField="MB004" SortField="MB004" Width="80px" HeaderText="计价单位" />
                                <f:BoundField DataField="MB005" SortField="MB005" Width="80px" HeaderText="初次交易日" />
                                <f:BoundField DataField="MB007" SortField="MB007" Width="80px" HeaderText="供应商品号" />
                                <f:BoundField DataField="MB008" SortField="MB008" Width="80px" HeaderText="核价日" />
                                <f:BoundField DataField="MB009" SortField="MB009" Width="80px" HeaderText="上次进货日" />
                                <f:BoundField DataField="MB010" SortField="MB010" Width="80px" HeaderText="分量计价" />
                                <f:BoundField DataField="MB011" SortField="MB011" Width="80px" HeaderText="采购单价" />
                                <f:BoundField DataField="MB013" SortField="MB013" Width="80px" HeaderText="含税" />
                                <f:BoundField DataField="MB014" SortField="MB014" Width="80px" HeaderText="生效日" />
                                <f:BoundField DataField="MB015" SortField="MB015" Width="80px" HeaderText="失效日" />

                            </Columns>
                        </f:Grid>

                    </Items>
                </f:Tab>

            </Tabs>
        </f:TabStrip>
        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="800px"
            Height="500px" OnClose="Window1_Close">
        </f:Window>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Yf_SalesOrder.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.MM.Yf_SalesOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style type="text/css">
        .f-grid-row .f-grid-cell-inner {
            font-size:small;
            white-space: normal;
            word-break: break-all;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="TabStrip1" />
        <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" ShowBorder="true" ActiveTabIndex="0"
            runat="server">
            <Tabs>
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
                                        <f:ToolbarFill ID="ToolbarFill2" runat="server">
                                        </f:ToolbarFill>
                                        <f:Button ID="Btn_dta" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                            runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" CssClass="marginr" OnClick="Btn_dta_Click">
                                        </f:Button>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                        <f:Grid ID="Grid2" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" ForceFit="true"
                            DataKeyNames="TD004" AllowSorting="true" SortField="TD004" EnableTextSelection="true"
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

                                <f:RowNumberField EnableLock="False" Width="35px" EnablePagingNumber="true" HeaderText="序号" />
                                <f:BoundField DataField="MB120" SortField="MB120" Width="100px" HeaderText="工厂" />
                                <f:BoundField DataField="TC001" SortField="TC001" Width="100px" HeaderText="单号" />
                                <f:BoundField DataField="TC003" SortField="TC003" Width="100px" HeaderText="日期" />
                                <f:BoundField DataField="MA002" SortField="MA002" Width="100px" HeaderText="客户" />
                                <f:BoundField DataField="TC008" SortField="TC008" Width="100px" HeaderText="币种" />
                                <f:BoundField DataField="TD004" SortField="TD004" Width="100px" HeaderText="物料" />
                                <f:BoundField DataField="TD005" SortField="TD005" Width="100px" HeaderText="描述" />
                                <f:BoundField DataField="TD008" SortField="TD008" Width="100px" HeaderText="数量" />
                                <f:BoundField DataField="TD009" SortField="TD009" Width="100px" HeaderText="已交" />
                                <f:BoundField DataField="TD011" SortField="TD011" Width="100px" HeaderText="单价" />
                                <f:BoundField DataField="TD012" SortField="TD012" Width="100px" HeaderText="金额" />
                                <f:BoundField DataField="UDF01" SortField="UDF01" Width="100px" HeaderText="机种" />
                                <f:BoundField DataField="UDF02" SortField="UDF02" Width="100px" HeaderText="仕向" />
                                <f:BoundField DataField="UDF03" SortField="UDF03" Width="100px" HeaderText="出口" />
                                <f:BoundField DataField="UDF04" SortField="UDF04" Width="100px" HeaderText="需求" />
                                <f:BoundField DataField="UDF05" SortField="UDF05" Width="100px" HeaderText="批次" />
                            </Columns>
                        </f:Grid>

                    </Items>
                </f:Tab>
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
                                        <f:ToolbarFill ID="ToolbarFill1" runat="server">
                                        </f:ToolbarFill>
                                        <f:Button ID="Btn_tac" IconUrl="~/res/icon/Eexcel.png" EnableAjax="false" DisableControlBeforePostBack="false"
                                            runat="server" Text="<%$ Resources:GlobalResource,sys_Export_Sheet%>" OnClick="Btn_tac_Click" CssClass="marginr">
                                        </f:Button>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                        <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" ForceFit="true"
                            DataKeyNames="TD004" AllowSorting="true" SortField="TD004" EnableTextSelection="true"
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

                                <f:RowNumberField EnableLock="False" Width="35px" EnablePagingNumber="true" HeaderText="序号" />
                                <f:BoundField DataField="MB120" SortField="MB120" Width="100px" HeaderText="工厂" />
                                <f:BoundField DataField="TC001" SortField="TC001" Width="100px" HeaderText="单号" />
                                <f:BoundField DataField="TC003" SortField="TC003" Width="100px" HeaderText="日期" />
                                <f:BoundField DataField="MA002" SortField="MA002" Width="100px" HeaderText="客户" />
                                <f:BoundField DataField="TC008" SortField="TC008" Width="100px" HeaderText="币种" />
                                <f:BoundField DataField="TD004" SortField="TD004" Width="100px" HeaderText="物料" />
                                <f:BoundField DataField="TD005" SortField="TD005" Width="100px" HeaderText="描述" />
                                <f:BoundField DataField="TD008" SortField="TD008" Width="100px" HeaderText="数量" />
                                <f:BoundField DataField="TD009" SortField="TD009" Width="100px" HeaderText="已交" />
                                <f:BoundField DataField="TD011" SortField="TD011" Width="100px" HeaderText="单价" />
                                <f:BoundField DataField="TD012" SortField="TD012" Width="100px" HeaderText="金额" />
                                <f:BoundField DataField="UDF01" SortField="UDF01" Width="100px" HeaderText="机种" />
                                <f:BoundField DataField="UDF02" SortField="UDF02" Width="100px" HeaderText="仕向" />
                                <f:BoundField DataField="UDF03" SortField="UDF03" Width="100px" HeaderText="出口" />
                                <f:BoundField DataField="UDF04" SortField="UDF04" Width="100px" HeaderText="需求" />
                                <f:BoundField DataField="UDF05" SortField="UDF05" Width="100px" HeaderText="批次" />
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

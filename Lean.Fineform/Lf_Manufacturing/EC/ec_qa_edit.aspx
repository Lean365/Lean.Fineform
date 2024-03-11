<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ec_qa_edit.aspx.cs" Inherits="Fine.Lf_Manufacturing.EC.ec_qa_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="TabStrip1" runat="server" />
        <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" ShowBorder="true" TabPosition="Top" ActiveTabIndex="0" AutoScroll="true"
            runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="<%$ Resources:GlobalResource,WindowsForm_Close%>">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btnSaveClose" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveClose_Click"
                            runat="server" Text="<%$ Resources:GlobalResource,WindowsForm_SaveMail%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Tabs>
                <f:Tab Title="设变信息" BodyPadding="10px" runat="server">
                    <Items>
                        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                            Title="SimpleForm">
                            <Rows>
                                <f:FormRow ID="FormRow5" runat="server">
                                    <Items>
                                        <f:Label runat="server" ID="Ec_no" Label="设变号码" ShowRedStar="True">
                                        </f:Label>
                                        <f:Label runat="server" ID="Ec_p1dlotsn" Label="事项说明" ShowRedStar="True">
                                        </f:Label>
                                        <f:Label runat="server" ID="Ec_p1dnote" Label="注意事项" ShowRedStar="True">
                                        </f:Label>
                                        <f:Label runat="server" ID="Ec_leader" Label="技术担当" ShowRedStar="True">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow2" runat="server">
                                    <Items>

                                        <f:Label Label="生管批次" ID="Ec_pmclot" runat="server"></f:Label>
                                        <f:Label runat="server" ID="Ec_mmlot" Label="部管批次">
                                        </f:Label>
                                        <f:Label runat="server" ID="Ec_p2dlot" Label="制二批次">
                                        </f:Label>
                                        <f:Label runat="server" ID="Ec_p1dlot" Label="制一批次">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow7" runat="server">
                                    <Items>
                                        <f:DatePicker runat="server" Label="检验日期" ID="Ec_qadate" DateFormatString="yyyyMMdd" ShowRedStar="True" Required="True">
                                        </f:DatePicker>
                                        <f:TextBox runat="server" ID="Ec_qalot" Label="检查批次" ShowRedStar="True" Required="True" EmptyText="与生管预定批次不同时,请输入">
                                        </f:TextBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow3" runat="server">
                                    <Items>
                                        <f:TextArea runat="server" Label="检查号码" ID="Ec_qalotsn" Height="100px" Required="True" MaxLength="200" ShowRedStar="True" Text="--">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow4" runat="server">
                                    <Items>
                                        <f:Label ID="Ec_detailstent" runat="server" Label="SAP设变内容" Height="100px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>

                            </Rows>
                        </f:Form>

                    </Items>
                </f:Tab>
                <f:Tab Title="物料确认" BodyPadding="10px" runat="server">
                    <Items>
                        <f:Grid ID="Grid1" runat="server" BoxFlex="1" ShowBorder="true" ShowHeader="false" ForceFit="true"
                            DataKeyNames="Ec_model" AllowSorting="true" OnSort="Grid1_Sort" SortField="Ec_model"
                            SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" OnPageIndexChange="Grid1_PageIndexChange" OnRowDataBound="Grid1_RowDataBound">
                            <PageItems>
                                <f:ToolbarSeparator ID="ToolbarSeparator3" runat="server">
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
                                <f:BoundField DataField="Ec_no" ColumnID="Ec_no" SortField="Ec_no" EnableLock="true" Width="100px" HeaderText="设变号码" />
                                <f:BoundField DataField="Ec_issuedate" ColumnID="Ec_issuedate" SortField="Ec_issuedate" EnableLock="true" Width="100px" HeaderText="日期" />
                                <f:BoundField DataField="Ec_model" ColumnID="Ec_model" SortField="Ec_model" EnableLock="true" Width="100px" HeaderText="机种" />
                                <f:BoundField DataField="Ec_bomitem" SortField="Ec_bomitem " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="上阶品号"></f:BoundField>
                                <f:BoundField DataField="D_SAP_ZCA1D_Z005" SortField="D_SAP_ZCA1D_Z005 " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="文本"></f:BoundField>
                                <f:BoundField DataField="Ec_olditem" SortField="Ec_olditem " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="旧品号"></f:BoundField>
                                <f:BoundField DataField="Ec_oldset" SortField="Ec_oldset " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="位置"></f:BoundField>
                                <f:BoundField DataField="Ec_newitem" SortField="Ec_newitem " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="新品号"></f:BoundField>
                                <f:BoundField DataField="Ec_newset" SortField="Ec_newset " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="位置"></f:BoundField>
                                <f:BoundField DataField="Ec_bomno" SortField="Ec_bomno " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="BOM番号"></f:BoundField>
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Tab>
            </Tabs>
        </f:TabStrip>

    </form>
</body>
</html>

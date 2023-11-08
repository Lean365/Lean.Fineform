<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ec_pm_edit.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.EC.ec_pm_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="TabStrip1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
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
                        <f:Button ID="btnIrrelevant" ValidateForms="SimpleForm1" Icon="TagsRed" OnClick="btnIrrelevant_Click"
                            runat="server" Text="EOL">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Tabs>
                <f:Tab Title="设变信息" BodyPadding="10px" Layout="VBox" runat="server">
                    <Items>
                        <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                            Title="SimpleForm">
                            <Rows>
                                <f:FormRow ID="FormRow5" runat="server">
                                    <Items>
                                        <f:Label runat="server" ID="Ec_no" Label="设变号码" ShowRedStar="True">
                                        </f:Label>
                                        <f:DatePicker runat="server" Label="投入日期" ID="Ec_pmcdate" DateFormatString="yyyyMMdd" ShowRedStar="True" Required="True" FocusOnPageLoad="true" EmptyText="预定投入日期">
                                        </f:DatePicker>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow7" runat="server">
                                    <Items>
                                        <f:TextBox runat="server" ID="Ec_pmclot" Label="预定批次"></f:TextBox>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow8" runat="server">
                                    <Items>
                                        <f:NumberBox runat="server" Label="PO残" Text="0" ID="Ec_poqty" NoDecimal="true" ShowRedStar="True" Required="True" EmptyText="PO残">
                                        </f:NumberBox>
                                        <f:NumberBox runat="server" Label="结余" Text="0" ID="Ec_balanceqty" NoDecimal="true" ShowRedStar="True" Required="True" EmptyText="结余">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>

                                <f:FormRow ID="FormRow3" runat="server">
                                    <Items>
                                        <f:TextArea runat="server" ID="Ec_pmcsn" Label="处理方式" Height="100px" MaxLength="200" ShowRedStar="True" Required="True" Text="--">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow10" runat="server">
                                    <Items>
                                        <f:TextArea runat="server" ID="Ec_pmcnote" Label="注意事项" Height="100px" MaxLength="200" ShowRedStar="True" Text="--">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow1" runat="server">
                                    <Items>
                                        <f:Label runat="server" Label="发行日期" ID="Ec_issuedate" ShowRedStar="True">
                                        </f:Label>
                                        <f:Label ID="Ec_model" runat="server" Label="机种名" ShowRedStar="True" Text="-">
                                        </f:Label>
                                        <f:Label runat="server" ID="Ec_leader" Label="技术担当" ShowRedStar="True">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>

                                <f:FormRow ID="FormRow4" runat="server">
                                    <Items>
                                        <f:Label ID="Ec_detailstent" runat="server" Label="SAP设变内容" Height="150px">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>

                            </Rows>
                        </f:Form>
                    </Items>


                </f:Tab>
                <f:Tab Title="物料确认" BodyPadding="10px" Layout="VBox" runat="server">
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
                                <f:BoundField DataField="Ec_model" ColumnID="Ec_model" SortField="Ec_model" EnableLock="true" Width="150px" HeaderText="机种" />
                                <f:BoundField DataField="Ec_bomitem" ColumnID="Ec_bomitem" SortField="Ec_bomitem" EnableLock="true" Width="120px" HeaderText="上阶物料" />
                                <f:BoundField DataField="Ec_olditem" ColumnID="Ec_olditem" SortField="Ec_olditem" EnableLock="true" Width="120px" HeaderText="旧物料" />
                                <f:BoundField DataField="Oldstock" ColumnID="Oldstock" SortField="Oldstock" EnableLock="true" Width="100px" HeaderText="旧品在库" />
                                <f:BoundField DataField="Ec_newitem" ColumnID="Ec_newitem" SortField="Ec_newitem" EnableLock="true" Width="120px" HeaderText="新物料" />
                                <f:BoundField DataField="Newstock" ColumnID="Newstock" SortField="Newstock" EnableLock="true" Width="100px" HeaderText="新品在库" />
                                <f:BoundField DataField="Ec_location" ColumnID="Ec_location" SortField="Ec_location" EnableLock="true" Width="100px" HeaderText="存储位置" />
                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Tab>
            </Tabs>
        </f:TabStrip>
    </form>
</body>
</html>

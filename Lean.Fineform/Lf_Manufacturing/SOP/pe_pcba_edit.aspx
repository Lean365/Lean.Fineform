<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pe_pcba_edit.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.SOP.pe_pcba_edit" %>

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
                            runat="server" Text="<%$ Resources:GlobalResource,WindowsForm_SaveClose%>">
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
                                <f:FormRow ID="FormRow10" runat="server">
                                    <Items>
                                        <f:Label runat="server" ID="Ec_no" Label="设变号码" ShowRedStar="True">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow7" runat="server">
                                    <Items>
                                        <f:DatePicker runat="server" Label="确认日期" ID="Ec_pengdate" FocusOnPageLoad="true" DateFormatString="yyyyMMdd" ShowRedStar="True" Required="True">
                                        </f:DatePicker>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow8" runat="server">
                                    <Items>
                                        <f:RadioButtonList ID="isengModifysop" Label="SOP变更否变更否" runat="server">
                                            <f:RadioItem Text="是" Value="1" Selected="true" />
                                            <f:RadioItem Text="否" Value="0" />
                                        </f:RadioButtonList>
                                    </Items>
                                </f:FormRow>

                                <f:FormRow ID="FormRow1" runat="server">
                                    <Items>
                                        <f:Label runat="server" Label="发行日期" ID="Ec_issuedate" ShowRedStar="True">
                                        </f:Label>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow2" runat="server">
                                    <Items>
                                        <f:Label ID="Ec_model" runat="server" Label="机种名" ShowRedStar="True" Text="-">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow3" runat="server">
                                    <Items>
                                        <f:Label runat="server" ID="pengpModifier" Label="SOP担当" ShowRedStar="True">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow6" runat="server">
                                    <Items>
                                        <f:Label runat="server" ID="Ec_leader" Label="技术担当" ShowRedStar="True">
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
                                <f:BoundField DataField="Ec_bomitem" SortField="Ec_bomitem " DataFormatString="{0}" ExpandUnusedSpace="true" HeaderText="成品物料"></f:BoundField>

                            </Columns>
                        </f:Grid>
                    </Items>
                </f:Tab>
            </Tabs>
        </f:TabStrip>
    </form>
</body>
</html>

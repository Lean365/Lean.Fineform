<%@ Page Language="C#" ValidateRequest="false" AutoEventWireup="true" CodeBehind="liaison_new.aspx.cs" Inherits="Fine.Lf_Manufacturing.TL.liaison_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AjaxTimeout="3600" />
        <f:Panel ID="Panel2" ShowBorder="false" ShowHeader="false" AutoScroll="true" runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar2" runat="server">
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
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                    Title="SimpleForm">
                    <Rows>
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:DatePicker runat="server" Label="发行日期" ID="Ec_issuedate" DateFormatString="yyyyMMdd" ShowRedStar="True" Required="True">
                                </f:DatePicker>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:DropDownList ID="Ec_model" runat="server" Label="机种" ShowRedStar="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" AutoSelectFirstItem="true">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:DropDownList ID="Ec_modellist" runat="server" Label="明细" ShowRedStar="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" EnableMultiSelect="true">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                <f:DropDownList ID="Ec_region" runat="server" Label="仕向" ShowRedStar="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" EnableMultiSelect="true">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:DropDownList runat="server" ID="Ec_leader" Label="技术担当" ShowRedStar="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:DatePicker runat="server" Label="实施日期" ID="Ec_enterdate" DateFormatString="yyyyMMdd" ShowRedStar="True" Required="True">
                                </f:DatePicker>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:DropDownList runat="server" ID="ddlPbook" Label="编码" ShowRedStar="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" AutoPostBack="True" OnSelectedIndexChanged="ddlPbook_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:TextBox ID="Ec_letterno" Label="技联NO" ShowRedStar="True" runat="server" EmptyText="9999">
                                </f:TextBox>

                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:FileUpload runat="server" ID="Ec_letterdoc" ButtonText="选择技联PDF" EmptyText="选择附件" Label="添加PDF"
                                    ShowRedStar="true">
                                </f:FileUpload>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:DropDownList runat="server" ID="ddlp" Label="编码" ShowRedStar="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>">
                                    <f:ListItem Text="P-" Value="1" Selected="true" />
                                </f:DropDownList>
                                <f:TextBox ID="Ec_eppletterno" Label="P番(DTA)" ShowRedStar="True" runat="server" EmptyText="9999" RegexPattern="ALPHA_NUMERIC" MaxLength="9999">
                                </f:TextBox>

                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:FileUpload runat="server" ID="Ec_eppletterdoc" ButtonText="选择P番PDF" EmptyText="选择附件" Label="添加PDF"
                                    ShowRedStar="true">
                                </f:FileUpload>
                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:DropDownList runat="server" ID="ddlptcj" Label="编码" ShowRedStar="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>">
                                    <f:ListItem Text="P-" Value="1" Selected="true" />
                                </f:DropDownList>
                                <f:TextBox ID="Ec_teppletterno" Label="P番(TCJ)" ShowRedStar="True" runat="server" EmptyText="9999" RegexPattern="ALPHA_NUMERIC" MaxLength="9999">
                                </f:TextBox>

                            </Items>
                        </f:FormRow>
                        <f:FormRow runat="server">
                            <Items>
                                <f:FileUpload runat="server" ID="Ec_teppletterdoc" ButtonText="选择P番PDF" EmptyText="选择附件" Label="添加PDF"
                                    ShowRedStar="true">
                                </f:FileUpload>
                            </Items>
                        </f:FormRow>

                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
    </form>
</body>
</html>

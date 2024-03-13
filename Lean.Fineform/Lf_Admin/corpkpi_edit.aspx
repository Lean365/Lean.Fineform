<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="corpkpi_edit.aspx.cs" Inherits="LeanFine.Lf_Admin.corpkpi_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false" AutoScroll="true" runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="<%$ Resources:GlobalResource,WindowsForm_Close%>">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btnSaveClose" ValidateForms="SimpleForm1" Icon="SystemSaveClose"
                            OnClick="btnSaveClose_Click" runat="server" Text="<%$ Resources:GlobalResource,WindowsForm_SaveClose%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:SimpleForm ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server"
                    BodyPadding="10px" Title="SimpleForm">
                    <Items>
                        <f:TextBox ID="tbxCorpAbbrName" runat="server" Label="公司名称" Required="true" ShowRedStar="true">
                        </f:TextBox>
                        <f:TextBox ID="tbxCorpAnnual" runat="server" Label="年度" Required="true" ShowRedStar="true">
                        </f:TextBox>
                        <f:TextArea ID="tbxCorpTarget_CN" Label="CN目标内容" runat="server" Required="true" ShowRedStar="true">
                        </f:TextArea>
                        <f:TextArea ID="tbxCorpTarget_EN" Label="EN目标内容" runat="server" Required="true" ShowRedStar="true">
                        </f:TextArea>
                        <f:TextArea ID="tbxCorpTarget_JA" Label="JA目标内容" runat="server" Required="true" ShowRedStar="true">
                        </f:TextArea>
                    </Items>
                </f:SimpleForm>
            </Items>
        </f:Panel>
    </form>
</body>
</html>



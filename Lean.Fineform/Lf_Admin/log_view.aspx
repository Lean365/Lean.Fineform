<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="log_view.aspx.cs" Inherits="LeanFine.Lf_Admin.log_view" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel2" IsFluid="true" CssClass="blockpanel" runat="server" Height="450px" ShowBorder="true" EnableCollapse="false"
            Layout="Fit" BodyPadding="10px"
            BoxConfigChildMargin="0 5 0 0" ShowHeader="false"
            Title="内容">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="<%$ Resources:GlobalResource,WindowsForm_Close%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:TextArea ID="LoginLogger" runat="server" Width="700px" Height="400px" Readonly="true"></f:TextArea>
            </Items>
        </f:Panel>
    </form>
</body>
</html>

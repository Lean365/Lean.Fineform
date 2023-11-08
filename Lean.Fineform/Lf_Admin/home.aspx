<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="home.aspx.cs" Inherits="Lean.Fineform.Lf_Admin.home" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form2" runat="server">
        <f:PageManager ID="PageManager2" runat="server" AutoSizePanelID="Panel2" />
        <f:Panel ID="Panel1" IsFluid="true" CssClass="blockpanel" runat="server" ShowBorder="false" Margin="20px"
            Layout="VBox" ShowHeader="false" Title="" BodyPadding="0">
            <Items>
                <f:Panel ID="Panel3" BoxFlex="1" runat="server"
                    ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigChildMargin="0 5 0 0" MarginBottom="5">
                    <Items>
                        <f:Panel ID="Panel4" BoxFlex="1" runat="server" ShowBorder="true" ShowHeader="true" Title="<%$ Resources:GlobalResource,MainTitle1%>" EnableIFrame="true" IFrameUrl="~/Lf_Report/default_echarts.aspx">
                        </f:Panel>
                        <f:Panel ID="Panel5" BoxFlex="1" runat="server" ShowBorder="true" ShowHeader="true" Title="<%$ Resources:GlobalResource,MainTitle2%>" EnableIFrame="true" IFrameUrl="~/Lf_Report/cube_echarts_all.aspx">
                        </f:Panel>

                    </Items>
                </f:Panel>
                <f:Panel ID="Panel6" BoxFlex="1" runat="server"
                    ShowBorder="false" ShowHeader="false" Layout="HBox" BoxConfigChildMargin="0 5 0 0">
                    <Items>
                        <f:Panel ID="Panel7" BoxFlex="1" runat="server" ShowBorder="true" ShowHeader="true" Title="<%$ Resources:GlobalResource,MainTitle4%>" EnableIFrame="true" IFrameUrl="~/Lf_Report/default_echarts.aspx">
                        </f:Panel>
                        <f:Panel ID="Panel8" BoxFlex="1" runat="server" ShowBorder="true" ShowHeader="true" Title="<%$ Resources:GlobalResource,MainTitle5%>" EnableIFrame="true" IFrameUrl="~/Lf_Report/cube_echarts_all.aspx">
                        </f:Panel>

                    </Items>
                </f:Panel>

            </Items>
        </f:Panel>

    </form>
</body>
</html>

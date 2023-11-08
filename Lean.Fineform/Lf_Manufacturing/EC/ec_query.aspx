<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ec_query.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.EC.ec_query" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="TabStrip1"/>
        <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" Height="500px" ShowBorder="true" ActiveTabIndex="0"
            runat="server">
            <Tabs>
                <f:Tab ID="Tab1" EnableIFrame="true" BodyPadding="10px"  IFrameUrl="~/Lf_Manufacturing/EC/dept/te.aspx"
                    Title="技术" runat="server">
                </f:Tab>
                <f:Tab ID="Tab2" EnableIFrame="true" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/EC/dept/pd.aspx"
                    Title="采购" runat="server">
                </f:Tab>
                <f:Tab ID="Tab3" EnableIFrame="true" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/EC/dept/pm.aspx"
                    Title="生管" runat="server">
                </f:Tab>
                <f:Tab ID="Tab4" EnableIFrame="true" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/EC/dept/qc.aspx"
                    Title="受检" runat="server">
                </f:Tab>
                <f:Tab ID="Tab5" EnableIFrame="true" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/EC/dept/mm.aspx"
                    Title="部管" runat="server">
                </f:Tab>
                <f:Tab ID="Tab6" EnableIFrame="true" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/EC/dept/p2d.aspx"
                    Title="制二" runat="server">
                </f:Tab>
                <f:Tab ID="Tab7" EnableIFrame="true" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/EC/dept/p1d.aspx"
                    Title="制一" runat="server">
                </f:Tab>
                <f:Tab ID="Tab8" EnableIFrame="true" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/EC/dept/qa.aspx"
                    Title="品管" runat="server">
                </f:Tab>
            </Tabs>
        </f:TabStrip>
    </form>
</body>
</html>

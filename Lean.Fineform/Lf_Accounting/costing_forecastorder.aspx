<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="costing_forecastorder.aspx.cs" Inherits="Lean.Fineform.Lf_Accounting.costing_forecastorder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="TabStrip1" />
        <f:TabStrip ID="TabStrip1" CssClass="mytabstrip" ShowInkBar="true" runat="server" ShowBorder="true" ActiveTabIndex="0">
            <Tabs>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Chart_Fc_Q1%>" Layout="Fit" BodyPadding="10px" EnableIFrame="true" IFrameUrl="~/Lf_Accounting/costing_fc_q1.aspx">
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Chart_Fc_Q2%>" Layout="Fit" BodyPadding="10px" EnableIFrame="true" IFrameUrl="~/Lf_Accounting/costing_fc_q2.aspx">
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Chart_Fc_Q3%>" Layout="Fit" BodyPadding="10px" EnableIFrame="true" IFrameUrl="~/Lf_Accounting/costing_fc_q3.aspx">
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Chart_Fc_Q4%>" Layout="Fit" BodyPadding="10px" EnableIFrame="true" IFrameUrl="~/Lf_Accounting/costing_fc_q4.aspx">
                </f:Tab>
            </Tabs>
        </f:TabStrip>
    </form>

</body>
</html>

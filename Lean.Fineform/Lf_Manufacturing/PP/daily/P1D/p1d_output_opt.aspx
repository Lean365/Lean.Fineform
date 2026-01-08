<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p1d_output_opt.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.daily.p1d_output_opt" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="TabStrip1" />
        <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" Height="500px" ShowBorder="true" ActiveTabIndex="0"
            runat="server">
            <Tabs>
                <f:Tab ID="Tab1" EnableIFrame="true" Layout="Fit" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/PP/daily/P1D/p1d_output_line.aspx"
                    Title="<%$ Resources:GlobalResource,sys_Button_Line%>" runat="server">
                </f:Tab>
                <f:Tab ID="Tab2" EnableIFrame="true" Layout="Fit" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/PP/daily/P1D/p1d_output_model.aspx"
                    Title="<%$ Resources:GlobalResource,sys_Button_Model%>" runat="server">
                </f:Tab>
                <f:Tab ID="Tab3" EnableIFrame="true" Layout="Fit" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/PP/daily/P1D/p1d_rpr_output_line.aspx"
                    Title="<%$ Resources:GlobalResource,sys_Button_Line_B%>" runat="server">
                </f:Tab>
                <f:Tab ID="Tab4" EnableIFrame="true" Layout="Fit" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/PP/daily/P1D/p1d_rpr_output_model.aspx"
                    Title="<%$ Resources:GlobalResource,sys_Button_Model_B%>" runat="server">
                </f:Tab>
                <f:Tab ID="Tab5" EnableIFrame="true" Layout="Fit" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/PP/daily/P1D/p1d_rwr_output_line.aspx"
                    Title="<%$ Resources:GlobalResource,sys_Button_Line_N%>" runat="server">
                </f:Tab>
                <f:Tab ID="Tab6" EnableIFrame="true" Layout="Fit" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/PP/daily/P1D/p1d_rwr_output_model.aspx"
                    Title="<%$ Resources:GlobalResource,sys_Button_Model_N%>" runat="server">
                </f:Tab>
                <f:Tab ID="Tab7" EnableIFrame="true" Layout="Fit" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/PP/daily/P1D/p1d_epp_output_line.aspx"
                    Title="<%$ Resources:GlobalResource,sys_Button_Line_E%>" runat="server">
                </f:Tab>
                <f:Tab ID="Tab8" EnableIFrame="true" Layout="Fit" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/PP/daily/P1D/p1d_epp_output_model.aspx"
                    Title="<%$ Resources:GlobalResource,sys_Button_Model_E%>" runat="server">
                </f:Tab>

            </Tabs>
        </f:TabStrip>
    </form>
</body>
</html>

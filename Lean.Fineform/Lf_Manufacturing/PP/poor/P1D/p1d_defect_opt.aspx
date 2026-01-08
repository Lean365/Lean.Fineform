<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p1d_defect_opt.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.daily.p1d_defect_opt" %>

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
                <f:Tab ID="Tab1" EnableIFrame="true" Layout="Fit" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/PP/poor/P1D/p1d_defect_lot_finished.aspx"
                    Title="批次不良" runat="server">
                </f:Tab>
                <f:Tab ID="Tab2" EnableIFrame="true" Layout="Fit" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/PP/poor/P1D/p1d_defect_moc_finished.aspx"
                    Title="工单不良" runat="server">
                </f:Tab>
                <f:Tab ID="Tab3" EnableIFrame="true" Layout="Fit" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/PP/poor/P1D/p1d_modify_defect_lot_finished.aspx"
                    Title="改修不良" runat="server">
                </f:Tab>
                <f:Tab ID="Tab4" EnableIFrame="true" Layout="Fit" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/PP/poor/P1D/p1d_epp_defect_lot_finished.aspx"
                    Title="EPP不良" runat="server">
                </f:Tab>


            </Tabs>
        </f:TabStrip>
    </form>
</body>
</html>

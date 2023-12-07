<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p1d_data_kanban.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.PP.daily.P1D.p1d_data_kanban" %>

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
                <f:Tab ID="Tab1" EnableIFrame="true" Layout="Fit" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/PP/daily/p1d_data_time.aspx"
                    Title="按时段" runat="server">
                </f:Tab>
                <f:Tab ID="Tab2" EnableIFrame="true" Layout="Fit" BodyPadding="10px" IFrameUrl="~/Lf_Manufacturing/PP/daily/p1d_data_line.aspx"
                    Title="按班组" runat="server">
                </f:Tab>

            </Tabs>
        </f:TabStrip>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="Lean.Fineform.Lf_Admin._default" %>

<!DOCTYPE html>
<html>
<head runat="server">
    <title></title>
    <style type="text/css">
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" runat="server" AutoSizePanelID="Panel2" />
        <f:Panel ID="Panel2" BoxFlex="1" runat="server" ShowBorder="false" ShowHeader="false" Title="<%$ Resources:GlobalResource,MainTitle1%>" EnableIFrame="true" IFrameUrl="~/Lf_Admin/default_echarts.aspx">
        </f:Panel>

    </form>
</body>
</html>

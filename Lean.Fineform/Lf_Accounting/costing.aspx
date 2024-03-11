<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="costing.aspx.cs" Inherits="Fine.Lf_Accounting.costing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager2" AutoSizePanelID="TabStrip1" runat="server" />
        <f:TabStrip ID="TabStrip1" CssClass="mytabstrip" ShowInkBar="true" runat="server" ShowBorder="true" ActiveTabIndex="0">
            <Tabs>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Chart_Materialcost%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:Panel ID="Panel2" IsFluid="true" CssClass="blockpanel" runat="server" ShowBorder="false" Margin="00px"
                            Layout="VBox" ShowHeader="false" Title="" BodyPadding="0">
                            <Items>
                                <f:Panel ID="Panel3" BoxFlex="1" runat="server" ShowBorder="false" ShowHeader="false" Title="<%$ Resources:GlobalResource,MainTitle1%>" EnableIFrame="true" IFrameUrl="~/Lf_Accounting/costing_salesinvoice.aspx">
                                </f:Panel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Chart_NeedQty%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:Panel ID="Panel5" IsFluid="true" CssClass="blockpanel" runat="server" ShowBorder="false" Margin="00px"
                            Layout="VBox" ShowHeader="false" Title="" BodyPadding="0">
                            <Items>
                                <f:Panel ID="Panel1" BoxFlex="1" runat="server" ShowBorder="false" ShowHeader="false" Title="<%$ Resources:GlobalResource,MainTitle1%>" EnableIFrame="true" IFrameUrl="~/Lf_Accounting/costing_demandqty.aspx">
                                </f:Panel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Charts_Operating%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:Panel ID="Panel9" IsFluid="true" CssClass="blockpanel" runat="server" ShowBorder="false" Margin="00px"
                            Layout="VBox" ShowHeader="false" Title="" BodyPadding="0">
                            <Items>
                                <f:Panel ID="Panel4" BoxFlex="1" runat="server" ShowBorder="false" ShowHeader="false" Title="<%$ Resources:GlobalResource,MainTitle1%>" EnableIFrame="true" IFrameUrl="~/Lf_Accounting/costing_grossmargin.aspx">
                                </f:Panel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Tab>


                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Chart_Forecast%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:Panel ID="Panel13" IsFluid="true" CssClass="blockpanel" runat="server" ShowBorder="false" Margin="00px"
                            Layout="VBox" ShowHeader="false" Title="" BodyPadding="0">
                            <Items>
                                <f:Panel ID="Panel6" BoxFlex="1" runat="server" ShowBorder="false" ShowHeader="false" Title="<%$ Resources:GlobalResource,MainTitle1%>" EnableIFrame="true" IFrameUrl="~/Lf_Accounting/costing_forecastorder.aspx">
                                </f:Panel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Chart_Pcost%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:Panel ID="Panel21" IsFluid="true" CssClass="blockpanel" runat="server" ShowBorder="false" Margin="00px"
                            Layout="VBox" ShowHeader="false" Title="" BodyPadding="0">
                            <Items>
                                <f:Panel ID="Panel8" BoxFlex="1" runat="server" ShowBorder="false" ShowHeader="false" Title="<%$ Resources:GlobalResource,MainTitle1%>" EnableIFrame="true" IFrameUrl="~/Lf_Accounting/costing_cost_analysis.aspx">
                                </f:Panel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Tab>
                <f:Tab runat="server" Title="<%$ Resources:GlobalResource,rpt_Chart_buAmount%>" Layout="Fit" BodyPadding="10px">
                    <Items>
                        <f:Panel ID="Panel12" IsFluid="true" CssClass="blockpanel" runat="server" ShowBorder="false" Margin="00px"
                            Layout="VBox" ShowHeader="false" Title="" BodyPadding="0">
                            <Items>
                                <f:Panel ID="Panel14" BoxFlex="1" runat="server" ShowBorder="false" ShowHeader="false" Title="<%$ Resources:GlobalResource,MainTitle1%>" EnableIFrame="true" IFrameUrl="~/Lf_Accounting/costing_inventoryamt_chart.aspx">
                                </f:Panel>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Tab>
            </Tabs>
        </f:TabStrip>
    </form>
</body>
</html>


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p2d_smt_daily_new.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.daily.P2D.p2d_smt_daily_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
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
                        <f:Button ID="btnSaveClose" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveClose_Click"
                            runat="server" Text="<%$ Resources:GlobalResource,WindowsForm_SaveClose%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                    Title="SimpleForm">
                    <Rows>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:DropDownList runat="server" Label="生产线别" ID="Proline" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" OnSelectedIndexChanged="Proline_SelectedIndexChanged" LabelAlign="Right">
                                </f:DropDownList>
                                <f:DatePicker runat="server" Label="生产日期" FocusOnPageLoad="true" ID="Prodate" ShowRedStar="True" DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" LabelAlign="Right" Required="True" Readonly="True">
                                </f:DatePicker>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:DropDownList runat="server" Label="生产机种" ID="Promodel" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" OnSelectedIndexChanged="Promodel_SelectedIndexChanged" LabelAlign="Right">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:DropDownList runat="server" Label="完成品" ID="Proitem" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" OnSelectedIndexChanged="Proitem_SelectedIndexChanged" LabelAlign="Right">
                                </f:DropDownList>
                                <f:Label runat="server" Label="文本" ID="Proitemtext" LabelWidth="120" LabelAlign="Right">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:DropDownList runat="server" ID="Propcbitem" Label="SAP物料" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" OnSelectedIndexChanged="Propcbitem_SelectedIndexChanged" LabelAlign="Right">
                                </f:DropDownList>
                                <f:Label runat="server" ID="Propcbtext" Label="SAP物料文本" LabelWidth="120" LabelAlign="Right">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:NumberBox runat="server" Label="SAP Short数" ID="Propcbshort" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="2" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:DropDownList runat="server" Label="PCB品号" ID="Propcba" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True" OnSelectedIndexChanged="Propcba_SelectedIndexChanged" LabelAlign="Right">
                                </f:DropDownList>
                                <f:Label runat="server" Label="PCB品名" ID="Propcbatext" ShowRedStar="True" LabelAlign="Right">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:NumberBox runat="server" Label="Short数" ID="Proconvertshort" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="2" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow10" runat="server">
                            <Items>
                                <f:NumberBox runat="server" Label="机器点数" ID="Promachineshort" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="2" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow11" runat="server">
                            <Items>
                                <f:NumberBox runat="server" Label="手贴点数" ID="Promanualshort" NoDecimal="false" NoNegative="True" DecimalPrecision="5" Required="True" MinValue="0" MaxValue="9999" ShowRedStar="True" LabelAlign="Right" RegexPattern="NUMBER" EmptyText="0">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow9" runat="server">
                            <Items>
                                <f:NumberBox runat="server" ID="Proplanqty" Label="SMT计划数" ShowRedStar="True" NoDecimal="false" NoNegative="True" MinValue="0" MaxValue="9999" DecimalPrecision="2" Required="True" LabelAlign="Right">
                                </f:NumberBox>
                                <f:NumberBox runat="server" Label="SMT实际数" ID="Prorealqty" ShowRedStar="True" NoDecimal="false" NoNegative="True" MinValue="0" MaxValue="9999" DecimalPrecision="2" Required="True" LabelAlign="Right">
                                </f:NumberBox>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                <f:NumberBox runat="server" ID="Prosmtshortqty" Label="SMT完成点数" ShowRedStar="True" NoDecimal="false" NoNegative="True" MinValue="0" MaxValue="999" DecimalPrecision="2" Required="True" LabelWidth="120" LabelAlign="Right">
                                </f:NumberBox>
                                <f:NumberBox ID="Promachineshortqty" runat="server" Label="机器完成点数" ShowRedStar="True" NoDecimal="false" NoNegative="True" MinValue="0" MaxValue="999" DecimalPrecision="2" Required="True" LabelWidth="120" LabelAlign="Right">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="700px"
            Height="650px">
        </f:Window>
    </form>

</body>
</html>



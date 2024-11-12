<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p2d_switch_note_edit.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.daily.P2D.p2d_switch_note_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false"  AutoScroll="true" runat="server">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="<%$ Resources:GlobalResource,WindowsForm_Close%>" >
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
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:DatePicker FocusOnPageLoad="true" runat="server" Required="true" Label="生产日期" DateFormatString="yyyyMMdd" EmptyText="请选择生产日期"
                                    ID="Prodate" ShowRedStar="True" Readonly="true">
                                </f:DatePicker>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow9" runat="server">
                            <Items>
                                <f:NumberBox runat="server" Label="SMT切换次数" ID="ProSmtSwitchNum" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="0" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                                <f:NumberBox runat="server" Label="SMT总切换时间" ID="ProSmtSwitchTotalTime" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="0" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:NumberBox runat="server" Label="自插切换次数" ID="ProAitSwitchNum" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="0" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                                <f:NumberBox runat="server" Label="自插总切换时间" ID="ProAiStopTime" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="0" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:NumberBox runat="server" Label="手插读SOP时间" ID="ProHandSopTime" NoDecimal="false" NoNegative="true" MinValue="6" MaxValue="6" DecimalPrecision="0" LabelAlign="Right" Readonly="true">
                                </f:NumberBox>
                                <f:NumberBox runat="server" Label="手插人数" ID="ProHandPerson" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="0" Required="True" ShowRedStar="True" LabelAlign="Right" OnTextChanged="ProHandPerson_TextChanged" AutoPostBack="true">
                                </f:NumberBox>
                                <f:NumberBox runat="server" Label="手插读SOP总时间" ID="ProHandSopTotalTime" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="0" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>

                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:NumberBox runat="server" Label="手插切换次数" ID="ProHandSwitchNum" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="0" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                                <f:NumberBox runat="server" Label="手插切换时间" ID="ProHandSwitchTime" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="0" Required="True" ShowRedStar="True" LabelAlign="Right" OnTextChanged="ProHandPerson_TextChanged" AutoPostBack="true">
                                </f:NumberBox>
                                <f:NumberBox runat="server" Label="手插切换总时间" ID="ProHandSwitchTotalTime" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="0" Required="True" LabelAlign="Right" Readonly="true">
                                </f:NumberBox>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:NumberBox runat="server" Label="修正读SOP时间" ID="ProRepairSopTime" NoDecimal="false" NoNegative="true" MinValue="6" MaxValue="6" DecimalPrecision="0" LabelAlign="Right" Readonly="true">
                                </f:NumberBox>
                                <f:NumberBox runat="server" Label="修正人数" ID="ProRepairPerson" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="0" Required="True" ShowRedStar="True" LabelAlign="Right" OnTextChanged="ProRepairPerson_TextChanged" AutoPostBack="true">
                                </f:NumberBox>
                                <f:NumberBox runat="server" Label="修正读SOP总时间" ID="ProRepairSopTotalTime" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="0" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:NumberBox runat="server" Label="修正切换次数" ID="ProRepairSwitchNum" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="0" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                                <f:NumberBox runat="server" Label="修正切换时间" ID="ProRepairSwitchTime" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="0" Required="True" ShowRedStar="True" LabelAlign="Right" OnTextChanged="ProRepairPerson_TextChanged" AutoPostBack="true">
                                </f:NumberBox>
                                <f:NumberBox runat="server" Label="修正切换总时间" ID="ProRepairSwitchTotalTime" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="9999" DecimalPrecision="0" Required="True" LabelAlign="Right" Readonly="true">
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





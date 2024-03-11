<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="waste_cost_edit.aspx.cs" Inherits="Fine.Lf_Manufacturing.QM.cost.waste_cost_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="TabStrip1" runat="server" />
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
                <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="10px"
                    Title="基本资料">
                    <Rows>
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:Label runat="server" Label="年月" ID="Qcwd001" ShowRedStar="True">
                                </f:Label>
                                <f:Label ID="Qcwd003" runat="server" Label="间接赁率" ShowRedStar="True">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:Label runat="server" ID="Qcwd002" Label="机种" ShowRedStar="True">
                                </f:Label>
                                <f:Label runat="server" ID="Qcwd004" Label="物料" ShowRedStar="True">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow10" runat="server">
                            <Items>
                                <f:Label ID="Qcwd005" runat="server" Label="TEXT" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow9" runat="server">
                            <Items>
                            </Items>
                        </f:FormRow>


                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:TextArea runat="server" FocusOnPageLoad="true" ID="Qcwd006" Label="事故内容"
                                    AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" Text="*" MaxLength="300">
                                </f:TextArea>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Form ID="Form2" ShowBorder="True" ShowHeader="True" runat="server" BodyPadding="10px"
                    Title="处理业务费用">
                    <Rows>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:Label runat="server" ID="Qcwd007" Label="废弃费用" ShowRedStar="True" Text="0">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:NumberBox runat="server" ID="Qcwd008" Label="废弃数量" ShowRedStar="True" Required="True" MaxValue="99999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Text="0" OnTextChanged="Qcwd008_TextChanged" AutoPostBack="True">
                                </f:NumberBox>
                                <f:Label runat="server" ID="Qcwd009" Label="部品单价" ShowRedStar="True" Text="0">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                <f:NumberBox runat="server" ID="Qcwd010" Label="废弃处理费用" ShowRedStar="True" Required="True" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Text="0" OnTextChanged="Qcwd010_TextChanged" AutoPostBack="True">
                                </f:NumberBox>
                                <f:NumberBox runat="server" ID="Qcwd011" Label="运费" ShowRedStar="True" Required="True" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Text="0" OnTextChanged="Qcwd011_TextChanged" AutoPostBack="True">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Form ID="Form3" ShowBorder="True" ShowHeader="True" runat="server" BodyPadding="10px"
                    Title="其它业务费用">
                    <Rows>
                        <f:FormRow ID="FormRow11" runat="server">
                            <Items>
                                <f:Label runat="server" ID="Qcwd012" Label="其它费用" ShowRedStar="True" Text="0">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:NumberBox runat="server" ID="Qcwd013" Label="处理时间" ShowRedStar="True" Required="True" MaxValue="9999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Text="0" OnTextChanged="Qcwd013_TextChanged" AutoPostBack="True">
                                </f:NumberBox>
                                <f:NumberBox runat="server" ID="Qcwd014" Label="关税" ShowRedStar="True" Required="True" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Text="0" OnTextChanged="Qcwd014_TextChanged" AutoPostBack="True">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:NumberBox runat="server" ID="Qcwd015" Label="其他费用" ShowRedStar="True" Required="True" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Text="0" OnTextChanged="Qcwd015_TextChanged" AutoPostBack="True">
                                </f:NumberBox>
                                <f:Label runat="server" ID="Qcwdrec" Label="处理担当" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="900px"
            Height="750px">
        </f:Window>
    </form>

</body>
</html>


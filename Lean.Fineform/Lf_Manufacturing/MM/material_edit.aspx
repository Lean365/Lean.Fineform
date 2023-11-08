<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="material_edit.aspx.cs" Inherits="Lean.Fineform.Lc_MM.material_edit" %>

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
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:Label runat="server" Label="工厂" ID="Plnt" ShowRedStar="True">
                                </f:Label>
                                <f:Label runat="server" Label="物料" ID="MatItem" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:Label runat="server" Label="描述" ID="MatDescription" ShowRedStar="True">
                                </f:Label>
                                <f:Label runat="server" Label="币种" ID="Currency" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:Label runat="server" Label="评估类" ID="EvaluationType" ShowRedStar="True">
                                </f:Label>

                                <f:Label runat="server" Label="差异码" ID="DiffCode" ShowRedStar="True">
                                </f:Label>
                                <f:Label runat="server" Label="价格单位" ID="PriceUnit" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:DatePicker runat="server" Label="日期" FocusOnPageLoad="true" ID="isDate" ShowRedStar="True">
                                </f:DatePicker>
                                <f:NumberBox runat="server" Label="移动价格" ID="MovingAvg" NoDecimal="false" NoNegative="true" MinValue="0" MaxValue="999999" DecimalPrecision="2" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                <f:TextBox ID="remark" runat="server" Label="备注">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Label ID="labResult" EncodeText="false" runat="server">
                </f:Label>
            </Items>
        </f:Panel>
        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="700px"
            Height="650px">
        </f:Window>
    </form>

</body>
</html>

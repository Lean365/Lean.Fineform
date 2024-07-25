<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pp_order_edit.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.Master.Pp_order_edit" %>

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
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:DatePicker runat="server" Label="计划日期" FocusOnPageLoad="true" ID="Porderdate" ShowRedStar="True" DateFormatString="yyyyMMdd" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" LabelAlign="Right">
                                </f:DatePicker>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:Label runat="server" Label="生产物料" LabelAlign="Right" ID="Porderhbn" ShowRedStar="true">
                                </f:Label>
                                <f:TextBox runat="server" Label="生产LOT" ID="Porderlot" Required="true" ShowRedStar="True" LabelAlign="Right">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>

                                <f:Label runat="server" Label="订单台数" ID="Porderqty">
                                </f:Label>
                                <f:NumberBox runat="server" Label="已生产台数" ID="Porderreal" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" LabelAlign="Right">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:DropDownList runat="server" Label="工单类别" ID="Pordertype" ShowRedStar="true" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" EnableEdit="true" ForceSelection="true" AutoPostBack="True"  LabelAlign="Right">
                                </f:DropDownList>
                                <f:Label runat="server" Label="生产工单" ID="Porderno" ShowRedStar="True" LabelAlign="Right">
                                </f:Label>


                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:Label runat="server" Label="工艺" ID="Porderroute" ShowRedStar="True" LabelAlign="Right">
                                </f:Label>
                                <f:TextBox runat="server" ID="Porderserial" Label="序列号SN" Required="true" ShowRedStar="True" LabelAlign="Right">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>

                                <f:TextBox ID="Remark" runat="server" Label="备注" LabelAlign="Right">
                                </f:TextBox>
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

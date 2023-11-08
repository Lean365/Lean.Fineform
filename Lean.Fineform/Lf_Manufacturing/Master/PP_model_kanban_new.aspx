<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pp_model_kanban_new.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.Master.Pp_model_kanban_new" %>

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
                                <f:DatePicker runat="server" Label="投入日期" FocusOnPageLoad="true" DateFormatString="yyyy-MM-dd" ID="dpkP_Kanban_Date" ShowRedStar="True" Required="True">
                                </f:DatePicker>
                                <f:DropDownList runat="server" ID="ddlP_Kanban_Line" Label="生产班组" EnableEdit="true" ShowRedStar="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" AutoPostBack="true" OnSelectedIndexChanged="ddlP_Kanban_Line_SelectedIndexChanged">
                                </f:DropDownList>



                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>

                                <f:DropDownList runat="server" ID="ddlP_Kanban_Order" Label="生产工单" EnableEdit="true"  AutoPostBack="true" ShowRedStar="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" OnSelectedIndexChanged="ddlP_Kanban_Order_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:Label runat="server" ID="lblP_Kanban_Lot" Label="生产批次" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:Label runat="server" ID="lblP_Kanban_Item" Label="物料" ShowRedStar="True">
                                </f:Label>
                                <f:Label runat="server" ID="lblD_SAP_ZCA1D_Z005" Label="品名">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:Label runat="server" ID="lblP_Kanban_Model" Label="机种" ShowRedStar="True">
                                </f:Label>
                                <f:Label ID="lblP_Kanban_Region" runat="server" Label="仕向" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:NumberBox ID="numP_Kanban_Process" AutoPostBack="true" runat="server" Label="工序" ShowRedStar="True" Required="True" MaxValue="30" MinValue="1" NoDecimal="true" NoNegative="true" OnTextChanged="numP_Kanban_Process_TextChanged">
                                </f:NumberBox>
                                <f:TextBox ID="tbxRemark" runat="server" Label="备注">
                                </f:TextBox>

                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>

                                <f:Image ID="imgModelQrcode" runat="server" Label="QRCode" Height="86px" Width="86px"></f:Image>

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

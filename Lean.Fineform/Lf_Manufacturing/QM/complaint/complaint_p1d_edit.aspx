<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="complaint_p1d_edit.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.QM.complaint.complaint_p1d_edit" %>

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
                                <f:Label runat="server" Label="内部编号" ID="Cc_DocNo" ShowRedStar="True">
                                </f:Label>
                                <f:Label runat="server" ID="Cc_IssuesNo" Label="客诉编号" ShowRedStar="True">
                                </f:Label>
                                <f:Label runat="server" ID="Cc_Customer" Label="客户代码" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:Label runat="server" ID="Cc_Model" Label="机种" ShowRedStar="True">
                                </f:Label>
                                <f:Label runat="server" ID="Cc_Serialno" Label="序列号" ShowRedStar="True">
                                </f:Label>
                                <f:Label ID="Cc_Order" runat="server" Label="订单" ShowRedStar="True">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>

                                <f:Label runat="server" Label="接收日期" ID="Cc_ReceivingDate" ShowRedStar="True">
                                </f:Label>
                                <f:Label runat="server" ID="Cc_Discover" Label="承认" ShowRedStar="True">
                                </f:Label>
                                <f:Label runat="server" Label="承认日期" ID="Cc_ReceivedDate" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:Label ID="Cc_Issues" runat="server" Label="投诉事项" Height="100px">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:DropDownList runat="server" ID="Cc_Line" Label="班组" ShowRedStar="True" EnableEdit="true">
                                </f:DropDownList>

                                <f:DatePicker runat="server" Required="true" Label="日期" DateFormatString="yyyyMMdd" EmptyText="请输入日期"
                                    ID="Cc_ProcessDate" ShowRedStar="True">
                                </f:DatePicker>
                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow12" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="Cc_Ddescription" Label="症状" ShowRedStar="True" Required="True" Height="100px">
                                </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="Cc_Reasons" Label="原因" ShowRedStar="True" Required="True" Height="100px">
                                </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow14" runat="server">
                            <Items>
                                <f:TextBox ID="Cc_Operator" runat="server" Label="作业员">
                                </f:TextBox>
                                <f:TextBox ID="Cc_Station" runat="server" Label="工位">
                                </f:TextBox>
                                <f:TextBox ID="Cc_Lot" runat="server" Label="批次">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow15" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="Cc_CorrectActions" Label="对策" ShowRedStar="True" Required="True" Height="100px">
                                </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="remark" Label="备注" ShowRedStar="True" Height="40px">
                                </f:TextArea>

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


﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="complaint_qa_new.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.QM.complaint.complaint_qa_new" %>

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
                                <f:DatePicker runat="server" Required="true" Label="接收日期" DateFormatString="yyyyMMdd" EmptyText="请输入日期"
                                    ID="Cc_ReceivingDate" ShowRedStar="True">
                                </f:DatePicker>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:TextBox runat="server" ID="Cc_IssuesNo" FocusOnPageLoad="true" Label="客诉编号" ShowRedStar="True" Required="True">
                                </f:TextBox>
                                <f:DropDownList runat="server" ID="Cc_Customer" Label="客户代码" ShowRedStar="True" EnableEdit="true">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:DropDownList runat="server" ID="Cc_Model" Label="机种" ShowRedStar="True" EnableEdit="true">
                                </f:DropDownList>
                                <f:NumberBox Label="数量" ID="Cc_DefectsQty" NoDecimal="true" ShowTrigger="false" runat="server" Required="True" />
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:TextBox ID="Cc_Order" runat="server" Label="订单" ShowRedStar="True" Required="True">
                                </f:TextBox>
                                <f:TextBox runat="server" ID="Cc_Serialno" Label="序列号" ShowRedStar="True" Required="True">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:TextArea ID="Cc_Issues" runat="server" Label="投诉事项" ShowRedStar="True" Required="True" Height="100px">
                                </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow9" runat="server">
                            <Items>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow10" runat="server">
                            <Items>
                                <f:FileUpload runat="server" ID="Cc_Reference" ButtonText="参考文件" EmptyText="参考文件PDF" Label="参考文件"
                                    ShowRedStar="true">
                                </f:FileUpload>
                                <f:DatePicker runat="server" Required="true" Label="承认日期" DateFormatString="yyyyMMdd" EmptyText="请输入日期"
                                    ID="Cc_ReceivedDate" ShowRedStar="True">
                                </f:DatePicker>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                <f:TextArea ID="remark" runat="server" Label="备注" ShowRedStar="True" Height="100px">
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


<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fqc_notice_edit.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.QM.fqc.fqc_notice_edit" %>

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
                        <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:Label ID="qmIssueno" runat="server" Label="Issueno" ShowRedStar="true"></f:Label>
                            </Items>

                        </f:FormRow>
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:Label runat="server" ID="qmInspector" Label="检查员" ShowRedStar="True">
                                </f:Label>
                                <f:Label runat="server" ID="qmLine" Label="班别" ShowRedStar="True">
                                </f:Label>
                            </Items>

                        </f:FormRow>

                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:Label runat="server" ID="qmOrder" Label="生产订单" ShowRedStar="True">
                                </f:Label>

                                <f:Label ID="qmModels" runat="server" Label="机种" Text="*">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow10" runat="server">
                            <Items>
                                <f:Label ID="qmMaterial" runat="server" Label="品号" Text="*">
                                </f:Label>
                                <f:Label ID="qmRegion" runat="server" Label="仕向" Text="*">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:Label runat="server" Label="检查日期" ID="qmCheckdate" ShowRedStar="True">
                                </f:Label>
                                <f:Label ID="qmProlot" runat="server" Label="生产批次" Text="*">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:Label runat="server" ID="qmLotserial" Label="批次说明">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:Label ID="qmRejectqty" runat="server" Label="验退数" Text="*">
                                </f:Label>
                                <f:Label ID="qmJudgmentlevel" runat="server" Label="不良级别" Text="*">
                                </f:Label>


                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow11" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="qmCheckNotes" Label="不良内容"
                                    AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True">
                                </f:TextArea>


                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:TextArea runat="server" FocusOnPageLoad="true" ID="Remark" Label="备注"
                                    AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" ShowRedStar="True">
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


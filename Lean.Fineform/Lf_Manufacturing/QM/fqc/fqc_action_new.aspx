<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fqc_action_new.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.QM.fqc.fqc_action_new" %>

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
                <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="10px"
                    Title="基本信息">
                    <Rows>
                        <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:Label ID="Qaadguid" runat="server" Label="GUID" ShowRedStar="true"></f:Label>
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
                                <f:Label ID="qmOrder" runat="server" Label="生产订单" Text="*">
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
                                <f:Label ID="qmCheckdate" runat="server" Label="检查日期" Text="*">
                                </f:Label>
                                <f:Label runat="server" ID="qmProlot" Label="生产批号">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:Label ID="qmLotserial" runat="server" Label="批号说明" Text="*">
                                </f:Label>

                                <f:Label ID="qmRejectqty" ShowRedStar="True" runat="server" Label="验退数" Text="0">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:Label runat="server" ID="qmJudgmentlevel" Label="不良级别" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="qmCheckNotes" FocusOnPageLoad="true" Label="不良内容"
                                    AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" Readonly="True">
                                </f:TextArea>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="10px"
                    Title="分析对策[制造部]">
                    <Rows>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:TextBox runat="server" ID="qmPersonnel" Label="对策担当" ShowRedStar="true" Required="true"></f:TextBox>
                                <f:DatePicker runat="server" Required="true" Label="对策日期" DateFormatString="yyyyMMdd" EmptyText="Please select"
                                    ID="qmDate" ShowRedStar="True">
                                </f:DatePicker>
                            </Items>

                        </f:FormRow>
                        <f:FormRow ID="FormRow9" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="qmDirectreason" Label="直接发生原因"
                                    AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="200" AutoPostBack="True" OnTextChanged="qmDirectreason_TextChanged">
                                </f:TextArea>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow15" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="qmIndirectreason" Label="间接发生原因"
                                    AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="200" AutoPostBack="True" OnTextChanged="qmIndirectreason_TextChanged">
                                </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow11" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="qmDisposal" Label="处置"
                                    AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="200" AutoPostBack="True" OnTextChanged="qmDisposal_TextChanged">
                                </f:TextArea>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow12" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="qmDirectsolutions" Label="直接对策"
                                    AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="200" AutoPostBack="True" OnTextChanged="qmDirectsolutions_TextChanged">
                                </f:TextArea>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow14" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="qmIndirectsolutions" Label="间接对策"
                                    AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="200" AutoPostBack="True" OnTextChanged="qmIndirectsolutions_TextChanged">
                                </f:TextArea>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Form ID="Form3" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="10px"
                    Title="实施检证[品证部]">
                    <Rows>
                        <f:FormRow ID="FormRow17" runat="server">
                            <Items>
                                <f:TextBox runat="server" ID="qmVerify" Label="检证担当" ShowRedStar="true" Required="true"></f:TextBox>
                                <f:DatePicker runat="server" Required="true" Label="检证日期" DateFormatString="yyyyMMdd" EmptyText="Please select"
                                    ID="qmCarryoutdate" ShowRedStar="True">
                                </f:DatePicker>

                            </Items>

                        </f:FormRow>
                        <f:FormRow ID="FormRow18" runat="server">
                            <Items>
                                <f:CheckBox runat="server" ID="qmCarryoutverify" Label="实施检证" ShowRedStar="true"></f:CheckBox>

                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow19" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="qmSolutionsverify" Label="检证说明"
                                    AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="200" AutoPostBack="True" OnTextChanged="qmSolutionsverify_TextChanged">
                                </f:TextArea>


                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow13" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="qmNotes" Label="特记事项"
                                    AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="200" AutoPostBack="True" OnTextChanged="qmNotes_TextChanged">
                                </f:TextArea>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow24" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="Remark" Label="说明"
                                    AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True">
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


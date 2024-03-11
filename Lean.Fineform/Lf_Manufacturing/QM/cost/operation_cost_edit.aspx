<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="operation_cost_edit.aspx.cs" Inherits="Fine.Lf_Manufacturing.QM.cost.operation_cost_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="TabStrip1" runat="server" />
        <f:TabStrip ID="TabStrip1" ShowBorder="true" TabPosition="Top" runat="server" Width="970px" ActiveTabIndex="0">
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
            <Tabs>
                <f:Tab Title="<%$ Resources:GlobalResource,co_Dept_IQC%>" BodyPadding="5px" runat="server" Layout="VBox" AutoScroll="true">
                    <Items>

                        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px" Title="受入检查业务费用">
                            <Rows>
                                <f:FormRow ID="FormRow27" runat="server">
                                    <Items>
                                        <f:Label ID="Qcodqcr" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="记录担当" ShowRedStar="true">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow2" runat="server">
                                    <Items>
                                        <f:Label  LabelWidth="200px" LabelAlign="Right"  runat="server"  Label="年月" ID="Qcod001" ShowRedStar="True" >
                                        </f:Label>
                                        <f:Label ID="Qcod002" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="间接人員赁率" ShowRedStar="True">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow1" runat="server">
                                    <Items>
                                        <f:Label ID="Qcod003" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="受入检查业务费用" ShowRedStar="True" Text="0">
                                        </f:Label>
                                        <f:NumberBox ID="Qcod004" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="检查时间(分)" MaxValue="99999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod004_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow3" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcod005" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="交通费、旅费" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod005_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcod006" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="检查其他费用" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod006_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow4" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right"  runat="server" Text="*" ID="Qcod007" Label="检查备注"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>

                            </Rows>
                        </f:Form>
                        <f:Form ID="Form5" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="初期检定.定期检定业务费用">
                            <Rows>
                                <f:FormRow ID="FormRow5" runat="server">
                                    <Items>
                                        <f:Label ID="Qcod008" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="初期检定.定期检定业务费用" ShowRedStar="True" Text="0">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow28" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcod009" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="检定作业时间(分)" MaxValue="99999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod009_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcod010" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="检定其他费用" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod010_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow8" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right"  runat="server" Text="*" ID="Qcod011" Label="检定备注"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                        <f:Form ID="Form9" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="测定器校正业务费用">
                            <Rows>
                                <f:FormRow ID="FormRow9" runat="server">
                                    <Items>
                                        <f:Label ID="Qcod012" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="测定器校正业务费用" ShowRedStar="True" Text="0">
                                        </f:Label>
                                        <f:NumberBox ID="Qcod013" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="校正作业时间(分)" MaxValue="99999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod013_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow10" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcod014" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="外部委托费、运搬费" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod014_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcod015" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="校正其他费用" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod015_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow12" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right"  runat="server" Text="*" ID="Qcod016" Label="校正备注"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                        <f:Form ID="Form13" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="其他通常业务费用">
                            <Rows>
                                <f:FormRow ID="FormRow13" runat="server">
                                    <Items>
                                        <f:Label ID="Qcod017" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="其他通常业务费用" ShowRedStar="True" Text="0">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow14" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcod018" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="通常业务作业时间(分)" MaxValue="99999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod018_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcod019" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="通常业务其他费用" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod019_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow15" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right"  runat="server" Text="*" ID="Qcod020" Label="其他备注"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                    </Items>

                </f:Tab>
                <f:Tab Title="<%$ Resources:GlobalResource,co_Dept_QA%>" BodyPadding="5px" Layout="VBox" AutoScroll="true"
                    runat="server">
                    <Items>

                        <f:Form ID="Form11" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="出荷检查业务费用">
                            <Rows>
                                <f:FormRow ID="FormRow6" runat="server">
                                    <Items>
                                        <f:Label ID="Qcodqar" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="记录担当" ShowRedStar="true">
                                        </f:Label>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow11" runat="server">
                                    <Items>
                                        <f:Label ID="Qcod021" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="出荷检查业务费用" ShowRedStar="True" Text="0">
                                        </f:Label>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow21" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcod022" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="检查时间(分)" MaxValue="99999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod022_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcod023" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="检查其他费用" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod023_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow16" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right"  runat="server" Text="*" ID="Qcod024" Label="检查备注"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                        <f:Form ID="Form17" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="信赖性评价・ORT业务费用">
                            <Rows>
                                <f:FormRow ID="FormRow17" runat="server">
                                    <Items>
                                        <f:Label ID="Qcod025" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="信赖性评价・ORT业务费用" ShowRedStar="True" Text="0">
                                        </f:Label>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow18" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcod026" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="评价作业时间(分)" MaxValue="9999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod026_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcod027" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="评价其他费用" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod027_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow19" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right"  runat="server" Text="*" ID="Qcod028" Label="评价备注"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                        <f:Form ID="Form22" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="顾客品质要求对应业务费用">
                            <Rows>
                                <f:FormRow ID="FormRow22" runat="server">
                                    <Items>
                                        <f:Label ID="Qcod029" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="顾客品质要求对应业务费用" ShowRedStar="True" Text="0">
                                        </f:Label>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow23" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcod030" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="评价作业时间(分)" MaxValue="9999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod030_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcod031" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="评价其他费用" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod031_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow25" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right"  runat="server" Text="*" ID="Qcod032" Label="评价备注"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                        <f:Form ID="Form24" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="其他通常业务费用">
                            <Rows>
                                <f:FormRow ID="FormRow24" runat="server">
                                    <Items>
                                        <f:Label ID="Qcod033" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="其他通常业务费用" ShowRedStar="True" Text="0">
                                        </f:Label>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow26" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcod034" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="通常业务作业时间(分)" MaxValue="99999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod034_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcod035" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="通常业务其他费用" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcod035_TextChanged" AutoPostBack="True">
                                        </f:NumberBox>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow20" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right"  runat="server" Text="*" ID="Qcod036" Label="通常其他备注"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow7" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right"  runat="server" Text="*" ID="Remark" Label="备注说明"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>

                            </Rows>
                        </f:Form>
                    </Items>
                </f:Tab>
            </Tabs>
        </f:TabStrip>
        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="1024px"
            Height="750px">
        </f:Window>
    </form>

</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="rework_cost_edit.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.QM.cost.rework_cost_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="TabStrip1" runat="server" />
        <f:TabStrip ID="TabStrip1" ShowBorder="true" TabPosition="Top" runat="server">
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
                <f:Tab Title="品质问题对应" ID="Tab1" BodyPadding="5px" runat="server" Layout="VBox" AutoScroll="true">
                    <Items>

                        <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px" Title="品质业务对应">
                            <Rows>
                                <f:FormRow ID="FormRow1" runat="server">
                                    <Items>
                                        <f:Label LabelWidth="200px" LabelAlign="Right"  runat="server" Label="日期" ID="Qcrd001" ShowRedStar="True">
                                        </f:Label>

                                        <f:Label ID="Qcrdqarec" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="对应担当" ShowRedStar="true">
                                        </f:Label>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow3" runat="server">
                                    <Items>
                                        <f:Label LabelWidth="200px" LabelAlign="Right"  runat="server" ID="Qcrd002" Label="机种" ShowRedStar="True">
                                        </f:Label>
                                        <f:Label LabelWidth="200px" LabelAlign="Right"  runat="server" ID="Qcrd003" Label="Lot" ShowRedStar="True">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow4" runat="server">
                                    <Items>
                                        <f:Label ID="Qcrd004" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="直接人员赁率" ShowRedStar="True" Text="0">
                                        </f:Label>
                                        <f:Label ID="Qcrd005" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="间接人员赁率" ShowRedStar="True" Text="0">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>

                            </Rows>
                        </f:Form>
                        <f:Form ID="SimpleForm2" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="检讨・调查・试验">
                            <Rows>
                                <f:FormRow ID="FormRow5" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right"  runat="server" Text="*" ID="Qcrd006" Label="检讨・调查・试验内容"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow6" runat="server">
                                    <Items>
                                        <f:Label ID="Qcrd007" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="检讨・调查・试验费用" ShowRedStar="true">
                                        </f:Label>
                                        <f:Label ID="Qcrd013" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="其他费用" ShowRedStar="True" Text="0">
                                        </f:Label>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow7" runat="server">
                                    <Items>

                                        <f:NumberBox ID="Qcrd009" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="直接人员参加人数" MaxValue="99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd009_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd010" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="间接人员参加人数" MaxValue="99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd010_TextChanged">
                                        </f:NumberBox>

                                    </Items>

                                </f:FormRow>
                                <f:FormRow ID="FormRow8" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcrd008" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="检讨会使用时间(分)" MaxValue="9999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd008_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd011" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="调查评价试验工作时间(分)" MaxValue="9999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd011_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd012" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="交通费、旅费" MaxValue="99999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd012_TextChanged">
                                        </f:NumberBox>

                                    </Items>

                                </f:FormRow>

                            </Rows>
                        </f:Form>
                        <f:Form ID="SimpleForm3" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="其他业务费用">
                            <Rows>
                                <f:FormRow ID="FormRow10" runat="server">
                                    <Items>

                                        <f:NumberBox ID="Qcrd014" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="其他作业時間(分)" MaxValue="99999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd014_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd015" LabelWidth="220px" LabelAlign="Right"  runat="server" Label="其他设备购入费,工程费,搬运费" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd015_TextChanged">
                                        </f:NumberBox>
                                    </Items>

                                </f:FormRow>
                                <f:FormRow ID="FormRow11" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right" runat="server" Text="*" ID="Qcrd017" Label="备注"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow12" runat="server">
                                    <Items>
                                        <f:CheckBox ID="Qcrd016" runat="server" Label="改修对应否" AutoPostBack="True" OnCheckedChanged="Qcrd016_CheckedChanged"></f:CheckBox>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>

                    </Items>

                </f:Tab>
                <f:Tab Title="<%$ Resources:GlobalResource,co_Dept_PM%>" ID="Tab2" BodyPadding="5px" Layout="VBox" AutoScroll="true"
                    runat="server">
                    <Items>

                        <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px" Title="改修业务对应">
                            <Rows>
                                <f:FormRow ID="FormRow13" runat="server">
                                    <Items>
                                        <f:DatePicker LabelWidth="200px" LabelAlign="Right"  runat="server" Required="true" Label="日期" DateFormatString="yyyyMMdd" EmptyText="请输入日期"
                                            ID="Qcrd018" ShowRedStar="True">
                                        </f:DatePicker>
                                        <f:Label ID="Qcrdmcrec" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="对应担当" ShowRedStar="true">
                                        </f:Label>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow15" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right" runat="server" Text="*" ID="Qcrd019" Label="不良内容"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                        <f:Form ID="Form3" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="选别・改修">
                            <Rows>
                                <f:FormRow ID="FormRow18" runat="server">
                                    <Items>
                                        <f:Label ID="Qcrd020" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="选别・改修费用" ShowRedStar="true">
                                        </f:Label>
                                        <f:Label ID="Qcrd027" LabelWidth="200px" LabelAlign="Right"  runat="server" Label="向顾客的费用请求" ShowRedStar="true">
                                        </f:Label>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow19" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcrd021" LabelWidth="200px" LabelAlign="Right" runat="server" Label="选别・改修时间(分)" MaxValue="9999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd021_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd022" LabelWidth="200px" LabelAlign="Right" runat="server" Label="再检查时间(分)" MaxValue="9999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd022_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd023" LabelWidth="200px" LabelAlign="Right" runat="server" Label="交通费、旅费" MaxValue="99999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd023_TextChanged">
                                        </f:NumberBox>

                                    </Items>

                                </f:FormRow>
                                <f:FormRow ID="FormRow20" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcrd024" LabelWidth="200px" LabelAlign="Right" runat="server" Label="仓库管理费" MaxValue="99999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd024_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd025" LabelWidth="200px" LabelAlign="Right" runat="server" Label="选别・改修其他费用" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd025_TextChanged">
                                        </f:NumberBox>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow16" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right" runat="server" Text="*" ID="Qcrd026" Label="备注"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>

                                    </Items>
                                </f:FormRow>

                            </Rows>
                        </f:Form>
                        <f:Form ID="Form4" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="费用请求">
                            <Rows>
                                <f:FormRow ID="FormRow21" runat="server">
                                    <Items>
                                        <f:TextBox ID="Qcrd028" LabelWidth="200px" LabelAlign="Right" runat="server" Label="顾客名" Required="true" ShowRedStar="True" Text="0">
                                        </f:TextBox>
                                        <f:TextBox ID="Qcrd029" LabelWidth="200px" LabelAlign="Right" runat="server" Label="Debit Note No" Required="true" ShowRedStar="True" Text="0">
                                        </f:TextBox>
                                    </Items>

                                </f:FormRow>
                                <f:FormRow ID="FormRow22" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcrd030" LabelWidth="200px" LabelAlign="Right" runat="server" Label="请求费用" MaxValue="99999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd030_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd031" LabelWidth="200px" LabelAlign="Right" runat="server" Label="其他费用" MaxValue="99999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd031_TextChanged">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow23" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right" runat="server" Text="*" ID="Qcrd032" Label="备注"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>

                            </Rows>
                        </f:Form>

                    </Items>
                </f:Tab>
                <f:Tab Title="<%$ Resources:GlobalResource,co_Dept_P1D%>" ID="Tab3" BodyPadding="5px" Layout="VBox" AutoScroll="true"
                    runat="server">
                    <Items>

                        <f:Form ID="Form5" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px" Title="改修业务对应">
                            <Rows>
                                <f:FormRow ID="FormRow24" runat="server">
                                    <Items>
                                        <f:DatePicker LabelWidth="200px" LabelAlign="Right" runat="server" Required="true" Label="日期" DateFormatString="yyyyMMdd" EmptyText="请输入日期"
                                            ID="Qcrd033" ShowRedStar="True">
                                        </f:DatePicker>
                                        <f:Label ID="Qcrdassrec" LabelWidth="200px" LabelAlign="Right" runat="server" Label="对应担当" ShowRedStar="true">
                                        </f:Label>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow26" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right" runat="server" Text="*" ID="Qcrd034" Label="不良内容"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                        <f:Form ID="Form6" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="选别・改修">
                            <Rows>

                                <f:FormRow ID="FormRow28" runat="server">
                                    <Items>
                                        <f:Label ID="Qcrd035" LabelWidth="200px" LabelAlign="Right" runat="server" Label="选别・改修费用" ShowRedStar="true">
                                        </f:Label>
                                        <f:Label ID="Qcrd042" LabelWidth="200px" LabelAlign="Right" runat="server" Label="向顾客的费用请求" ShowRedStar="true">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow29" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcrd036" LabelWidth="200px" LabelAlign="Right" runat="server" Label="选别・改修时间(分)" MaxValue="9999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd036_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd037" LabelWidth="200px" LabelAlign="Right" runat="server" Label="再检查时间(分)" MaxValue="9999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd037_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd038" LabelWidth="200px" LabelAlign="Right" runat="server" Label="交通费、旅费" MaxValue="99999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd038_TextChanged">
                                        </f:NumberBox>

                                    </Items>

                                </f:FormRow>
                                <f:FormRow ID="FormRow30" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcrd039" LabelWidth="200px" LabelAlign="Right" runat="server" Label="仓库管理费" MaxValue="99999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd039_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd040" LabelWidth="200px" LabelAlign="Right" runat="server" Label="选别・改修其他费用" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd040_TextChanged">
                                        </f:NumberBox>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow31" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right" runat="server" Text="*" ID="Qcrd041" Label="备注"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>

                                    </Items>
                                </f:FormRow>

                            </Rows>
                        </f:Form>
                        <f:Form ID="Form7" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="费用请求">
                            <Rows>

                                <f:FormRow ID="FormRow33" runat="server">
                                    <Items>
                                        <f:TextBox ID="Qcrd043" LabelWidth="200px" LabelAlign="Right" runat="server" Label="顾客名" Required="true" ShowRedStar="True" Text="0">
                                        </f:TextBox>
                                        <f:TextBox ID="Qcrd044" LabelWidth="200px" LabelAlign="Right" runat="server" Label="Debit Note No" Required="true" ShowRedStar="True" Text="0">
                                        </f:TextBox>
                                    </Items>

                                </f:FormRow>
                                <f:FormRow ID="FormRow34" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcrd045" LabelWidth="200px" LabelAlign="Right" runat="server" Label="请求费用" MaxValue="99999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd045_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd046" LabelWidth="200px" LabelAlign="Right" runat="server" Label="其他费用" MaxValue="99999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd046_TextChanged">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow36" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right" runat="server" Text="*" ID="Qcrd047" Label="备注"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>

                            </Rows>
                        </f:Form>

                    </Items>
                </f:Tab>
                <f:Tab Title="<%$ Resources:GlobalResource,co_Dept_P2D%>" ID="Tab4" BodyPadding="5px" Layout="VBox" AutoScroll="true"
                    runat="server">
                    <Items>

                        <f:Form ID="Form8" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px" Title="改修业务对应">
                            <Rows>
                                <f:FormRow ID="FormRow37" runat="server">
                                    <Items>
                                        <f:DatePicker LabelWidth="200px" LabelAlign="Right" runat="server" Required="true" Label="日期" DateFormatString="yyyyMMdd" EmptyText="请输入日期"
                                            ID="Qcrd048" ShowRedStar="True">
                                        </f:DatePicker>
                                        <f:Label ID="Qcrdpcbrec" LabelWidth="200px" LabelAlign="Right" runat="server" Label="对应担当" ShowRedStar="true">
                                        </f:Label>

                                    </Items>
                                </f:FormRow>

                                <f:FormRow ID="FormRow39" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right" runat="server" Text="*" ID="Qcrd049" Label="不良内容"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>
                                    </Items>
                                </f:FormRow>
                            </Rows>
                        </f:Form>
                        <f:Form ID="Form9" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="选别・改修">
                            <Rows>
                                <f:FormRow ID="FormRow41" runat="server">
                                    <Items>
                                        <f:Label ID="Qcrd050" LabelWidth="200px" LabelAlign="Right" runat="server" Label="选别・改修费用" ShowRedStar="true">
                                        </f:Label>
                                        <f:Label ID="Qcrd057" LabelWidth="200px" LabelAlign="Right" runat="server" Label="向顾客的费用请求" ShowRedStar="true">
                                        </f:Label>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow42" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcrd051" LabelWidth="200px" LabelAlign="Right" runat="server" Label="选别・改修时间(分)" MaxValue="9999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd051_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd052" LabelWidth="200px" LabelAlign="Right" runat="server" Label="再检查时间(分)" MaxValue="9999" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd052_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd053" LabelWidth="200px" LabelAlign="Right" runat="server" Label="交通费、旅费" MaxValue="99999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd053_TextChanged">
                                        </f:NumberBox>

                                    </Items>

                                </f:FormRow>
                                <f:FormRow ID="FormRow43" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcrd054" LabelWidth="200px" LabelAlign="Right" runat="server" Label="仓库管理费" MaxValue="99999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd054_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd055" LabelWidth="200px" LabelAlign="Right" runat="server" Label="选别・改修其他费用" MaxValue="999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd055_TextChanged">
                                        </f:NumberBox>

                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow44" runat="server">
                                    <Items>
                                        <f:TextArea LabelWidth="200px" LabelAlign="Right" runat="server" Text="*" ID="Qcrd056" Label="备注"
                                            AutoGrowHeight="true" AutoGrowHeightMin="60px" AutoGrowHeightMax="60px" Required="True" ShowRedStar="True" MaxLength="300">
                                        </f:TextArea>

                                    </Items>
                                </f:FormRow>

                            </Rows>
                        </f:Form>
                        <f:Form ID="Form10" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="5px"
                            Title="费用请求">
                            <Rows>
                                <f:FormRow ID="FormRow46" runat="server">
                                    <Items>
                                        <f:TextBox ID="Qcrd058" LabelWidth="200px" LabelAlign="Right" runat="server" Label="顾客名" Required="true" ShowRedStar="True" Text="0">
                                        </f:TextBox>
                                        <f:TextBox ID="Qcrd059" LabelWidth="200px" LabelAlign="Right" runat="server" Label="Debit Note No" Required="true" ShowRedStar="True" Text="0">
                                        </f:TextBox>
                                    </Items>

                                </f:FormRow>
                                <f:FormRow ID="FormRow47" runat="server">
                                    <Items>
                                        <f:NumberBox ID="Qcrd060" LabelWidth="200px" LabelAlign="Right" runat="server" Label="请求费用" MaxValue="99999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd060_TextChanged">
                                        </f:NumberBox>
                                        <f:NumberBox ID="Qcrd061" LabelWidth="200px" LabelAlign="Right" runat="server" Label="其他费用" MaxValue="99999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="True" OnTextChanged="Qcrd061_TextChanged">
                                        </f:NumberBox>
                                    </Items>
                                </f:FormRow>
                                <f:FormRow ID="FormRow48" runat="server">
                                    <Items>
                                        <f:TextArea  LabelWidth="200px" LabelAlign="Right" runat="server" Text="*" ID="Qcrd062" Label="备注"
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


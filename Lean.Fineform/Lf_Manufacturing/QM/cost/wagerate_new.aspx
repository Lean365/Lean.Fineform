<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="wagerate_new.aspx.cs" Inherits="Fine.Lf_Manufacturing.QM.cost.wagerate_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="../Lf_Resources/css/main.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../Lf_Resources/ueditor/themes/default/css/ueditor.min.css" />
    <script type="text/javascript" src="../Lf_Resources/ueditor/ckeditor.js"></script>
</head>

<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false"  AutoScroll="true" runat="server">
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
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                 <f:Label runat="server" ID="Qcsd002" Label="工厂" ShowRedStar="True" Text="C100">
                                </f:Label>                                                                
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:DatePicker runat="server" Required="true" Label="年月" DateFormatString="yyyyMM" EmptyText="请输入日期"
                                    ID="Qcsd001" ShowRedStar="True">
                                </f:DatePicker>
                                <f:NumberBox ID="Qcsd005" runat="server" Label="总工作时日"  MaxValue="21.75" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0"  AutoPostBack="true" OnTextChanged="Qcsd005_TextChanged">
                                </f:NumberBox>                                                                                                          
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow10" runat="server">
                            <Items>
                                <f:Textbox runat="server" Label="币种" ID="Qcsd003" ShowRedStar="True" Required="True" Text="CNY">
                                </f:Textbox> 
                                <f:NumberBox ID="Qcsd004" runat="server" Label="月销售额" MaxValue="99999999.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="true">
                                </f:NumberBox> 
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                 <f:Label ID="Qcsdrec" runat="server" Label="财务担当">
                                </f:Label>  
                                                                                          
                                </Items>
                        </f:FormRow>
                  </Rows>
                </f:Form>
                                <f:Form ID="Form2" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="10px"
                     Title="直接人员">
                    <Rows>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:NumberBox ID="Qcsd006" runat="server" Label="直接赁率" Text="0" MaxValue="5.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True">
                                </f:NumberBox>
                                <f:NumberBox ID="Qcsd007" runat="server" Label="直接人员" MaxValue="500" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcsd007_TextChanged" AutoPostBack="true">
                                </f:NumberBox>
                                </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>

                                <f:NumberBox ID="Qcsd008" runat="server" Label="直接加班" MaxValue="999999.9" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="1" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcsd008_TextChanged" AutoPostBack="true">
                                </f:NumberBox>
                                <f:NumberBox ID="Qcsd009" runat="server" Label="直接工资" MaxValue="3000000.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcsd009_TextChanged" AutoPostBack="true">
                                </f:NumberBox> 
                                </Items>
                        </f:FormRow>

                  </Rows>
                </f:Form>
                <f:Form ID="Form3" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="10px"
                     Title="间接人员">
                    <Rows>
                        <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:NumberBox ID="Qcsd010" runat="server" Label="间接赁率" Text="0" MaxValue="5.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True">
                                </f:NumberBox>
                                <f:NumberBox ID="Qcsd011" runat="server" Label="间接人员" MaxValue="200" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="0" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcsd012_TextChanged" AutoPostBack="true">
                                </f:NumberBox>  
                                </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow11" runat="server">
                            <Items>

                                <f:NumberBox ID="Qcsd012" runat="server" Label="间接加班" MaxValue="999999.9" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="1" Required="True" ShowRedStar="True" Text="0" OnTextChanged="Qcsd011_TextChanged" AutoPostBack="true">
                                </f:NumberBox>
                                <f:NumberBox ID="Qcsd013" runat="server" Label="间接工资" MaxValue="3000000.99" MinValue="0" NoDecimal="false" NoNegative="True" DecimalPrecision="2" Required="True" ShowRedStar="True" Text="0" AutoPostBack="true" OnTextChanged="Qcsd013_TextChanged">
                                </f:NumberBox> 
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

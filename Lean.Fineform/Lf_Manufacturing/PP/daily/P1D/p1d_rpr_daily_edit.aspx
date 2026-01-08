<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p1d_rpr_daily_edit.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.daily.p1d_rpr_daily_edit" %>

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
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="1px"
                    Title="SimpleForm">
                    <Rows>
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:Label  runat="server"  Label="生产日期" ID="prodate" >
                                </f:Label>
                                <f:Label runat="server" ID="prolinename" Label="生产班别">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:NumberBox ID="prodirect" FocusOnPageLoad="true" runat="server" Label="直接人数" Text="0" OnTextChanged="prodirect_TextChanged" AutoPostBack="true" MaxValue="30" MinValue="1">
                                </f:NumberBox>
                                <f:NumberBox ID="proindirect" runat="server" Label="间接人数" Text="0" MaxValue="10" MinValue="0">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:Label ID="proorder" runat="server" Label="SAP订单">
                                </f:Label>
                                <f:Label runat="server" ID="prolot" Label="生产LOT">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow10" runat="server">
                            <Items>
                                <f:Label runat="server" ID="prohbn" Label="品号">
                                </f:Label>
                                <f:Label ID="promodel" runat="server" Label="机种">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:Label ID="prost" runat="server" Label="工时" Text="0.00" >
                                </f:Label>
                                <f:Label ID="prostdcapacity" runat="server" Label="标准产能" Text="0.00" >
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:Label ID="prolotqty" runat="server" Label="订单台数" Text="0.0" >
                                </f:Label>
                                <f:Label ID="prosn" runat="server" Label="生产序号" Text="*">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow9" runat="server">
                            <Items>
                                <f:TextBox ID="promodifynotes" runat="server" Label="改修内容">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>

            </Items>
        </f:Panel>
    </form>
</body>
</html>
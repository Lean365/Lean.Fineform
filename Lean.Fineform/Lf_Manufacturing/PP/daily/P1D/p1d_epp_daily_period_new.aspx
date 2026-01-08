<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p1d_epp_daily_period_new.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.daily.p1d_epp_daily_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" EnableFStateValidation="false" />
        <f:Panel ID="Panel5" runat="server" ShowBorder="True" EnableCollapse="true"
            Layout="VBox" ShowHeader="True" Title=""
            BoxConfigChildMargin="0 0 5 0" BodyPadding="5">
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
                                <f:DatePicker FocusOnPageLoad="true" runat="server" Required="true" Label="生产日期" DateFormatString="yyyyMMdd" EmptyText="请选择生产日期"
                                    ID="pikProdate" ShowRedStar="True">
                                </f:DatePicker>
                                <f:DropDownList runat="server" ID="ddlProlinename" Label="生产班别" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" ForceSelection="true" ShowRedStar="True" Required="True">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:NumberBox ID="numProdirect" runat="server" Label="直接人数" Text="0" OnTextChanged="prodirect_TextChanged" AutoPostBack="true" MaxValue="30" MinValue="1">
                                </f:NumberBox>
                                <f:NumberBox ID="numProindirect" runat="server" Label="间接人数" Text="0" MaxValue="10" MinValue="0">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:DropDownList ID="ddlProorder" runat="server" Label="SAP订单" ShowRedStar="True" EnableEdit="true" ForceSelection="true" AutoPostBack="True" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" OnSelectedIndexChanged="proorder_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:Label runat="server" ID="lblProlot" Label="生产LOT" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow10" runat="server">
                            <Items>
                                <f:Label runat="server" ID="lblProhbn" Label="品号" ShowRedStar="True">
                                </f:Label>
                                <f:Label ID="lblPromodel" runat="server" Label="机种">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:NumberBox ID="numProst" runat="server" Label="工时" Text="0.00" OnTextChanged="numProst_TextChanged" AutoPostBack="true" MaxValue="999" MinValue="1" NoDecimal="false" NoNegative="true" DecimalPrecision="2" Required="true" ShowRedStar="True">
                                </f:NumberBox>
                                <f:Label ID="lblProstdcapacity" runat="server" Label="标准产能"  ShowRedStar="True">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:Label ID="lblProlotqty" runat="server" Label="订单台数" Text="0.0" ShowRedStar="True">
                                </f:Label>
                                <f:Label ID="lblProsn" runat="server" Label="生产序号" Text="*">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow9" runat="server">
                            <Items>

                                <f:TextBox ID="txtRemark" runat="server" Label="备注">
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

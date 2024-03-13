<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ec_pd_edit.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.EC.ec_pd_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false"  AutoScroll="true" runat="server" Title="资材部">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="<%$ Resources:GlobalResource,WindowsForm_Close%>" >
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btnSaveClose" ValidateForms="SimpleForm1" Icon="SystemSaveClose" OnClick="btnSaveClose_Click"
                            runat="server" Text="<%$ Resources:GlobalResource,WindowsForm_SaveMail%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                     Title="SimpleForm">
                    <Rows>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                <f:Label runat="server" ID="Ec_no" Label="设变号码" ShowRedStar="True">
                                </f:Label>
                                <f:Label ID="ProInv"  Label="当前在库"  runat="server" ShowRedStar="True" Text="0">
                                </f:Label> 
                                <f:NumberBox  runat="server"  Label="PO残" Text="0"  ID="Ec_poqty" NoDecimal="true" ShowRedStar="True" Required="True" EmptyText="PO残">
                                </f:NumberBox> 
                                <f:NumberBox  runat="server"  Label="结余" Text="0"  ID="Ec_balanceqty" NoDecimal="true" ShowRedStar="True" Required="True" EmptyText="结余">
                                </f:NumberBox>

                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:DatePicker runat="server"  Label="PO日期"  ID="Ec_purdate" DateFormatString="yyyyMMdd" ShowRedStar="True" Required="True" FocusOnPageLoad="true">
                                </f:DatePicker>  
                                <f:TextBox runat="server" ID="Ec_pursupplier" Label="供应商" ShowRedStar="True" Required="True" EmptyText="供应商代码">
                                </f:TextBox>
                                <f:NumberBox Label="采购订单" ID="Ec_purorder" runat="server" EmptyText="订单号码"
                                                NoDecimal="True" NoNegative="True" Required="True"
                                            ShowRedStar="True" ></f:NumberBox>

                                </Items>


                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>

                                <f:TextArea runat="server" ID="Ec_purnote" Label="处理方式" Height="100px" ShowRedStar="True" Required="True" MaxLength="200" Text="--">
                                </f:TextArea>                                
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:Label runat="server"  Label="发行日期"  ID="Ec_issuedate" ShowRedStar="True">
                                </f:Label>  
                               <f:Label ID="Ec_model" runat="server" Label="机种名" ShowRedStar="True" Text="-">
                                </f:Label>
                               <f:Label ID="Ec_bomitem" runat="server" Label="完成品" ShowRedStar="True" Text="-">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow9" runat="server">
                            <Items>
                               <f:Label ID="Ec_olditem" runat="server" Label="旧品号" ShowRedStar="True" Text="-">
                                </f:Label>
                                <f:Label ID="Ec_bstock"  Label="发行在库"  runat="server" ShowRedStar="True" Text="0">
                                </f:Label> 
                                <f:Label ID="Ec_newitem" runat="server" Label="新品号" ShowRedStar="True" Text="-">
                                </f:Label>

                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>                                
                                <f:Label ID="Ec_detailstent" runat="server" Label="SAP设变内容" Height="150px">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:Label runat="server" ID="Ec_pmclot" Label="预定LOT" ShowRedStar="True" >
                                </f:Label>

                                <f:Label runat="server" ID="Ec_leader" Label="技术担当" ShowRedStar="True" >
                                </f:Label>
                                  <f:Label runat="server" ID="Ec_pur" Label="采购担当" ShowRedStar="True" >
                                </f:Label>                                                                                              
                            </Items>
                        </f:FormRow> 
                  </Rows>
                </f:Form>
            </Items>
        </f:Panel>
    </form>        
</body>
</html>

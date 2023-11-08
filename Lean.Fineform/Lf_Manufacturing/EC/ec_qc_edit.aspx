<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ec_qc_edit.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.EC.ec_qc_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" OnCustomEvent="PageManager1_CustomEvent" />
        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false"  AutoScroll="true" runat="server"  Title="受检课">
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
                        <f:Button ID="btnIrrelevant" ValidateForms="SimpleForm1" Icon="TagsRed" OnClick="btnIrrelevant_Click"
                            runat="server" Text="<%$ Resources:GlobalResource,WindowsForm_Exemption%>">
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
                          
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:DatePicker runat="server"  Label="检查日期"  ID="Ec_iqcdate" DateFormatString="yyyyMMdd" ShowRedStar="True" Required="True">
                                </f:DatePicker>   
<%--                                <f:TextBox runat="server" ID="Ec_iqclot" Label="检查批次" ShowRedStar="True" Required="True" EmptyText="与采购PO不同时,请输入">
                                </f:TextBox>   --%>                             
                                <f:NumberBox Label="订单号码" ID="Ec_iqcorder" runat="server" EmptyText="采购订单"
                                                NoDecimal="True" NoNegative="True" Required="True"
                                            ShowRedStar="True" ></f:NumberBox>              

                      
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <%--<f:TextArea runat="server" ID="Ec_iqcnote" Label="检查说明" MaxLength="200" Height="100px" ShowRedStar="True" Required="True">
                                </f:TextArea>  --%>                              
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
                                <f:Label ID="Ec_bstock"  Label="旧品在库"  runat="server" ShowRedStar="True" Text="0">
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
                                <f:Label runat="server" ID="Ec_pursupplier" Label="采购PO" ShowRedStar="True" >
                                </f:Label>
                                <f:Label runat="server" ID="Ec_leader" Label="技术担当" ShowRedStar="True" >
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
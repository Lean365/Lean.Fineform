<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ec_balance_edit.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.EC.ec_balance_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false"  AutoScroll="true" runat="server"  Title="生管课">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="<%$ Resources:GlobalResource,WindowsForm_Close%>" >
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
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                <f:Label runat="server" ID="Ec_no" Label="设变号码" ShowRedStar="True">
                                </f:Label>   
                             
                                </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:DatePicker runat="server"  Label="更新日期"  ID="Ec_balancedate" DateFormatString="yyyyMMdd" ShowRedStar="True" Required="True" EmptyText="领料单更新或邮件通知日期">
                                </f:DatePicker>                               
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:Label runat="server"  Label="发行在库" Text="0" ID="Ec_oldqty" ShowRedStar="True">
                                </f:Label> 
                                <f:NumberBox  runat="server"  Label="PO残" Text="0"  ID="Ec_poqty" NoDecimal="true" ShowRedStar="True" Required="True" EmptyText="PO残">
                                </f:NumberBox> 
                                <f:NumberBox  runat="server"  Label="结余" Text="0"  ID="Ec_balanceqty" NoDecimal="true" ShowRedStar="True" Required="True" EmptyText="结余">
                                </f:NumberBox>
                                <f:Label runat="server"  Label="当前在库" Text="0"  ID="NowQty" ShowRedStar="True">
                                </f:Label>                                                                                               
                                </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="Ec_precess" Label="处理方式" Height="100px" ShowRedStar="True" Required="True">
                                </f:TextArea>                                
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow10" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="Ec_note" Label="注意事项" Height="100px" ShowRedStar="True">
                                </f:TextArea>                                  
                                </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:Label runat="server"  Label="发行日期"  ID="Ec_issuedate" ShowRedStar="True">
                                </f:Label>  
                               <f:Label ID="Ec_model" runat="server" Label="机种名" ShowRedStar="True" Text="-">
                                </f:Label>
                               <f:Label ID="Ec_item" runat="server" Label="完成品" ShowRedStar="True" Text="-">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow9" runat="server">
                            <Items>
                               <f:Label ID="Ec_olditem" runat="server" Label="旧品号" ShowRedStar="True" Text="-">
                                </f:Label>
                               <f:Label ID="Ec_newitem" runat="server" Label="新品号" ShowRedStar="True" Text="-">
                                </f:Label>
                            </Items>
                        </f:FormRow>
 



                        
                  </Rows>
                </f:Form>
            </Items>
        </f:Panel>
        <f:HiddenField ID="hfSelectedDhbn" runat="server">
        </f:HiddenField>
        <f:HiddenField ID="hfSelectedWhbn" runat="server">
        </f:HiddenField>
        <f:HiddenField ID="hfSelectedModel" runat="server">
        </f:HiddenField>
        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="1000px"
            Height="800px">
        </f:Window>
    </form>
        
</body>
</html>
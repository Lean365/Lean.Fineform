<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pp_transport_new.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.Master.pp_transport_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false"  AutoScroll="true" runat="server">
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

                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:TextBox ID="Transportype" runat="server" Label="运输类别" ShowRedStar="True" Required="True" LabelAlign="Right">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:TextBox ID="Transportcntext" runat="server" Label="目的地CN" ShowRedStar="True" Required="True" LabelAlign="Right">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:TextBox ID="Transportentext" runat="server" Label="目的地EN" ShowRedStar="True" Required="True" LabelAlign="Right">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>  
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:TextBox ID="Transportjptext" runat="server" Label="目的地JP" ShowRedStar="True" Required="True" LabelAlign="Right">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>  
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                           
                                <f:TextBox ID="remark" runat="server" Label="备注" LabelAlign="Right">
                                </f:TextBox>                                                                                                                         
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



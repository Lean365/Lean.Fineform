<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dict_new.aspx.cs" Inherits="LeanFine.Lf_Admin.dict_new" %>

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
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                    Title="SimpleForm">
                    <Rows>

                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:TextBox ID="txtDictType" runat="server" Label="字典类型" ShowRedStar="True" Required="True" LabelAlign="Right">
                                </f:TextBox>
                                <f:TextBox ID="txtDictName" runat="server" Label="字典名称" ShowRedStar="True" Required="True" LabelAlign="Right">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:TextBox ID="txtDictLabel" runat="server" Label="字典标签" ShowRedStar="True" Required="True" LabelAlign="Right">
                                </f:TextBox>

                                <f:TextBox ID="txtDictValue" runat="server" Label="数值" ShowRedStar="True" Required="True" LabelAlign="Right">
                                </f:TextBox>


                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:NumberBox ID="numDictSort" runat="server" Label="排序" ShowRedStar="True"  Required="True" LabelAlign="Right">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>



                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                <f:TextArea ID="txtRemark" runat="server" Label="备注" ShowRedStar="True" Required="True" LabelAlign="Right">
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

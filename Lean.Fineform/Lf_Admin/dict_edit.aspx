<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dict_edit.aspx.cs" Inherits="LeanFine.Lf_Admin.dict_edit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="TabStrip1" runat="server" />

        <f:TabStrip ID="TabStrip1" runat="server" ActiveTabIndex="0" ShowBorder="True">
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
                <f:Tab ID="Tab2" runat="server" BodyPadding="5px" Title="Tab1">
                    <Items>
                        <f:Panel ID="Panel3" ShowHeader="false" ShowBorder="false" runat="server">
                            <Items>
                                <f:Label ID="txtDictType" runat="server" Label="类型" LabelAlign="Right">
                                </f:Label>
                                <f:TextBox ID="txtDictLabel" runat="server" Label="标签" ShowRedStar="True" Required="True" LabelAlign="Right">
                                </f:TextBox>
                                <f:TextBox ID="txtDictName" runat="server" Label="名称" ShowRedStar="True" Required="True" LabelAlign="Right">
                                </f:TextBox>
                                <f:TextBox ID="txtDictValue" runat="server" Label="数值" ShowRedStar="True" Required="True" LabelAlign="Right">
                                </f:TextBox>
                                <f:NumberBox ID="numDictSort" runat="server" Label="排序" ShowRedStar="True" Required="True" LabelAlign="Right">
                                </f:NumberBox>
                                <f:TextArea ID="txtRemark" runat="server" Label="备注" ShowRedStar="True" Required="True" LabelAlign="Right">
                                </f:TextArea>
                            </Items>
                        </f:Panel>
                    </Items>
                </f:Tab>
            </Tabs>
        </f:TabStrip>
        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="700px"
            Height="650px">
        </f:Window>
    </form>
</body>
</html>

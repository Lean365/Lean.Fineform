<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dict_new.aspx.cs" Inherits="LeanFine.Lf_Admin.dict_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="TabStrip1" runat="server" />
        <f:TabStrip ID="TabStrip1" IsFluid="true" CssClass="blockpanel" Height="350px" ShowBorder="true" TabPosition="Top"
            EnableTabCloseMenu="false" ActiveTabIndex="0" runat="server">
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
                <f:Tab Title="数据字典" BodyPadding="10px" Layout="Fit" runat="server">
                    <Items>
                        <f:SimpleForm ID="SimpleForm1" ShowBorder="false"
                            ShowHeader="false" Title="SimpleForm1" LabelWidth="120px" runat="server">
                            <Items>
                                <f:GroupPanel ID="GroupPanel1" Layout="Anchor" Title="字典信息" runat="server">
                                    <Items>
                                        <f:Panel ID="Panel1" ShowHeader="false" ShowBorder="false" runat="server">
                                            <Items>
                                                <f:TextBox ID="txtDictType" runat="server" Label="类型" ShowRedStar="True" Required="True" LabelAlign="Right">
                                                </f:TextBox>
                                                <f:TextBox ID="txtDictName" runat="server" Label="名称" ShowRedStar="True" Required="True" LabelAlign="Right">
                                                </f:TextBox>
                                                <f:TextBox ID="txtDictLabel" runat="server" Label="标签" ShowRedStar="True" Required="True" LabelAlign="Right">
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
                                </f:GroupPanel>
                            </Items>
                        </f:SimpleForm>
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

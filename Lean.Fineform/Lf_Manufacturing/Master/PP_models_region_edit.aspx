<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pp_models_region_edit.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.Master.Pp_models_region_edit" %>

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

                                <f:Label runat="server" ID="lblD_SAP_DEST_Z001" Label="物料">
                                </f:Label>
                                <f:Label runat="server" ID="lblD_SAP_ZCA1D_Z005" Label="品名">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:TextBox runat="server" ID="tbxD_SAP_DEST_Z002" FocusOnPageLoad="true" Label="机种" ShowRedStar="True" Required="True">
                                </f:TextBox>
                                <f:TextBox ID="tbxD_SAP_DEST_Z003" runat="server" Label="仕向" ShowRedStar="True" Required="True">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                <f:TextBox ID="tbxRemark" runat="server" Label="备注">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>
                <f:Label ID="labResult" EncodeText="false" runat="server">
                </f:Label>
            </Items>
        </f:Panel>
        <f:HiddenField ID="hfSelectedDept" runat="server">
        </f:HiddenField>

        <f:Window ID="Window1" Title="编辑" Hidden="true" EnableIFrame="true" runat="server"
            EnableMaximize="true" EnableResize="true" Target="Top" IsModal="True" Width="700px"
            Height="650px">
        </f:Window>
    </form>

</body>
</html>

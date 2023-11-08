<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="schedule_view.aspx.cs" Inherits="Lean.Fineform.Lf_Office.EM.schedule_view" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="../Lf_Resources/css/main.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../Lf_Resources/ueditor/themes/default/css/ueditor.min.css" />
    <script type="text/javascript" src="../Lf_Resources/ueditor/ckeditor.js"></script>
    <script src="/res/js/jquery.min.js" type="text/javascript"></script>
    <script src="/res/My97DatePicker/WdatePicker.js" type="text/javascript"></script>

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
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Form ID="SimpleForm1" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                    Title="SimpleForm">
                    <Rows>
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:Label runat="server" ID="attypename" Label="事件类型">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:Label runat="server" ID="attitle" Label="标题">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:Label ID="atsdate" ShowRedStar="True" Label="日期和时间" runat="server">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:Label ID="atedate" ShowRedStar="True" Label="日期和时间" runat="server">
                                </f:Label>
                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:TextArea runat="server" ID="atcontent" Label="内容" Readonly="true">
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


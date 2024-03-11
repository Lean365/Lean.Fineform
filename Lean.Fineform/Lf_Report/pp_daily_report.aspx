<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pp_daily_report.aspx.cs" Inherits="Fine.Lf_Report.Pp_daily_report" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script src="http://localhost:18000/CLodopfuncs.js?priority=0"></script>
    <script src="http://localhost:8000/CLodopfuncs.js?priority=1"></script>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <object class="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0"></object>

        <script lang="javascript" src="LodopFuncs.js"></script>
        <script lang="javascript" type="text/javascript"> 
            var LODOP; //声明为全局变量
            function CreatePrintPage() {
                LODOP = getLodop();
                LODOP.PRINT_INIT("套打机种模板");
            };
        </script>
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" IsFluid="false" CssClass="blockpanel" runat="server" ShowBorder="false" EnableCollapse="false"
            BodyPadding="1px" ShowHeader="false" Title="" AutoScroll="false" Margin="1px">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="<%$ Resources:GlobalResource,WindowsForm_Close%>">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btn_PrintPreview" ValidateForms="SimpleForm1" IconUrl="~/Lf_Resources/PrintView.png"
                            OnClick="btn_PrintPreview_Click" runat="server" Text="<%$ Resources:GlobalResource,sys_Button_Preview%>">
                        </f:Button>
                        <f:Button ID="btn_PrintDesign" ValidateForms="SimpleForm1" IconUrl="~/Lf_Resources/PrintDesign.png"
                            OnClick="btn_PrintDesign_Click" runat="server" Text="<%$ Resources:GlobalResource,sys_Button_Design%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
            </Items>
        </f:Panel>
    </form>
</body>
</html>

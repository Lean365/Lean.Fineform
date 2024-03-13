<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pp_models_region_view.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.Master.Pp_models_region_view" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script lang="javascript" src="/res/lodop/LodopFuncs.js"></script>
    <%--<script src='http://dcube.teac.com.cn:8001/CLodopfuncs.js'></script>--%>
    <%--<script src='http://dcube.teac.com.cn:8001/CLodopfuncs.js?priority=1'></script>--%>
    <%--<script src="http://localhost:8000/CLodopfuncs.js?priority=1"></script>--%>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form3" runat="server">
        <object class="clsid:2105C259-1E0C-4534-8141-A753534CB4CA" width="0" height="0">
            <embed id="LODOP_EM" type="application/x-print-lodop" width="0" height="0" />
        </object>



        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" />
        <f:Panel ID="Panel1" runat="server" BodyPadding="5px"
            ShowBorder="false" Layout="VBox" BoxConfigAlign="Stretch" BoxConfigPosition="Start"
            ShowHeader="false" Title="LeanCloud">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="<%$ Resources:GlobalResource,WindowsForm_Close%>">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btn_PrintPreview" ValidateForms="SimpleForm1" IconUrl="~/Lf_Resources/icon/PrintView.png"
                            EnablePostBack="false" OnClientClick="javascript:CreatePrintPage();LODOP.PREVIEW();" runat="server" Text="<%$ Resources:GlobalResource,sys_Button_Preview%>">
                        </f:Button>
                        <f:Button ID="btn_PrintDesign" ValidateForms="SimpleForm1" IconUrl="~/Lf_Resources/icon/PrintDesign.png"
                            EnablePostBack="false" OnClientClick="javascript:CreatePrintPage();LODOP.PRINT_DESIGN();" runat="server" Text="<%$ Resources:GlobalResource,sys_Button_Design%>">
                        </f:Button>
                        <f:Button ID="btn_PrintSetup" ValidateForms="SimpleForm1" IconUrl="~/Lf_Resources/icon/PrintDesign.png"
                            EnablePostBack="false" OnClientClick="javascript:CreatePrintPage();LODOP.PRINT_SETUP();" runat="server" Text="<%$ Resources:GlobalResource,sys_Button_Setup%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                    Title="SimpleForm" ColumnWidth="100%">
                    <Rows>
                        <f:FormRow ID="FormRow1" runat="server" ColumnWidths="100%">
                            <Items>
                                <f:Label runat="server" ID="lblD_SAP_DEST_Z001" Label="物料">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server" ColumnWidths="100%">
                            <Items>
                                <f:Label runat="server" ID="lblD_SAP_DEST_Z003" Label="机种">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server" ColumnWidths="100%">
                            <Items>
                                <f:Label runat="server" ID="lblD_SAP_DEST_Z004" Label="仕向">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow2" runat="server" ColumnWidths="100%">
                            <Items>

                                <f:Label runat="server" ID="lblUdf004" Label="工序">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server" ColumnWidths="100%">
                            <Items>
                                <f:Image ID="imgModelQrcode" runat="server" Height="86px" Width="86px" Label="QRCode"></f:Image>
                            </Items>
                        </f:FormRow>

                    </Rows>
                </f:Form>
            </Items>
        </f:Panel>

        <f:Window ID="Window1" runat="server" IsModal="true" Hidden="true" Target="Top" EnableResize="true"
            EnableMaximize="true" EnableIFrame="true" IFrameUrl="about:blank" Width="800px"
            Height="500px" OnClose="Window1_Close">
        </f:Window>

    </form>
    <%--<script lang="javascript" src="LodopFuncs.js"></script>--%>
    <script lang="javascript" type="text/javascript">

        var strPline = '<%=lblD_SAP_DEST_Z001.Text%>';
        var strPmodel = '<%=lblD_SAP_DEST_Z003.Text%>';
        var strPlot = '<%=lblD_SAP_DEST_Z004.Text%>';
        var strPdate = '<%=lblD_SAP_DEST_Z004.Text%>';
        var strPregion = '<%=lblD_SAP_DEST_Z004.Text%>';
        var strPqrcode = '<%=lblD_SAP_DEST_Z001.Text%>';

        var LODOP; //声明为全局变量
        //var LODOP; //声明为全局变量
        //function CreatePrintPage() {
        //    LODOP = getLodop();
        //    LODOP.PRINT_INIT("套打模板");


        //};
        function CreatePrintPage() {
            //debugger;
            LODOP = getLodop();
            //LODOP = getLodop(document.getElementById('LODOP_OB'), document.getElementById('LODOP_EM'));
            //LODOP.PRINT_INITA(2, 1, 800, 600, "套打机种模板");
            //LODOP.SET_PRINT_PAGESIZE(1, "186mm", "114mm", "");//设置纸张高度
            //LODOP.SET_PRINT_MODE("PRINT_NOCOLLATE", 1);
            ////LODOP.ADD_PRINT_SETUP_BKIMG("res/lodop/modelcard.jpg");
            //LODOP.ADD_PRINT_TEXT(98, 110, 60, 20, strPline);
            //LODOP.SET_PRINT_STYLEA(0, "AlignJustify", 1);
            //LODOP.ADD_PRINT_TEXT(98, 240, 90, 20, strPmodel);
            //LODOP.ADD_PRINT_TEXT(98, 414, 90, 20, strPlot);
            //LODOP.ADD_PRINT_TEXT(50, 684, 90, 20, strPdate);
            //LODOP.ADD_PRINT_TEXT(140, 135, 75, 26, strPregion);
            //LODOP.ADD_PRINT_BARCODE(7, 14, 96, 96, "QRCode", strPqrcode);
            LODOP.PRINT_INITA(0, 0, 703, 431, "套打模板");
           // LODOP.ADD_PRINT_SETUP_BKIMG("H:\\VS2019\\设变DB项目\\modelcard.jpg");
           // LODOP.SET_SHOW_MODE("BKIMG_IN_PREVIEW", true);
            LODOP.ADD_PRINT_TEXT(90, 70, 60, 20, strPdate);
            LODOP.ADD_PRINT_TEXT(90, 177, 40, 20, strPline);
            LODOP.ADD_PRINT_TEXT(90, 270, 80, 20, strPmodel);
            LODOP.ADD_PRINT_TEXT(90, 400, 100, 20, strPlot);
            LODOP.ADD_PRINT_TEXT(123, 112, 80, 20, strPregion);
            LODOP.ADD_PRINT_BARCODE(10, 39, 64, 64, "QRCode", strPdate + strPline + strPmodel + strPlot + strPregion);
            LODOP.SET_PRINT_STYLEA(0, "QRCodeVersion", 3);
            LODOP.SET_PRINT_STYLEA(0, "DataCharset", "UTF-8");

        };
    </script>
</body>
</html>

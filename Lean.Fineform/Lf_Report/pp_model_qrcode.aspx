<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pp_model_qrcode.aspx.cs" Inherits="LeanFine.Lf_Report.pp_model_qrcode" %>

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
            ShowHeader="false" Title="Lean365">
            <Toolbars>
                <f:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <f:Button ID="btnClose" Icon="SystemClose" EnablePostBack="false" runat="server"
                            Text="<%$ Resources:GlobalResource,WindowsForm_Close%>">
                        </f:Button>
                        <f:ToolbarSeparator ID="ToolbarSeparator2" runat="server">
                        </f:ToolbarSeparator>
                        <f:Button ID="btn_PrintPreview" ValidateForms="SimpleForm1" IconUrl="~/LB_Resources/icon/PrintView.png"
                            EnablePostBack="false" OnClientClick="CreatePages_A5_PREVIEW()" runat="server" Text="<%$ Resources:GlobalResource,sys_Button_Preview_A5%>">
                        </f:Button>
                        <f:Button ID="btn_PrintDesign" ValidateForms="SimpleForm1" IconUrl="~/LB_Resources/icon/PrintDesign.png"
                            EnablePostBack="false" OnClientClick="CreatePages_A114_PREVIEW()" runat="server" Text="<%$ Resources:GlobalResource,sys_Button_Preview_A4%>">
                        </f:Button>
                        <f:Button ID="btn_PrintSetup" ValidateForms="SimpleForm1" IconUrl="~/LB_Resources/icon/PrintDesign.png"
                            EnablePostBack="false" OnClientClick="CreateAllPages_SETUP()" runat="server" Text="<%$ Resources:GlobalResource,sys_Button_Setup%>">
                        </f:Button>
                    </Items>
                </f:Toolbar>
            </Toolbars>
            <Items>
                <f:Form ID="Form2" ShowBorder="false" ShowHeader="false" runat="server" BodyPadding="10px"
                    Title="SimpleForm" ColumnWidth="100%">
                    <Rows>
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:Label runat="server" Label="投入日期" ID="lblP_Kanban_Date" ShowRedStar="True">
                                </f:Label>

                                <f:Label runat="server" ID="lblP_Kanban_Line" Label="生产班组" ShowRedStar="True">
                                </f:Label>


                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:Label runat="server" ID="lblP_Kanban_Order" Label="生产工单" ShowRedStar="True">
                                </f:Label>

                                <f:Label runat="server" ID="lblP_Kanban_Lot" Label="生产批次" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:Label runat="server" ID="lblP_Kanban_Item" Label="生产物料" ShowRedStar="True">
                                </f:Label>
                                <f:Label runat="server" ID="lblD_SAP_ZCA1D_Z005" Label="品名">
                                </f:Label>

                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:Label runat="server" ID="lblP_Kanban_Model" Label="机种" ShowRedStar="True">
                                </f:Label>
                                <f:Label ID="lblP_Kanban_Region" runat="server" Label="仕向" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:Label ID="lblP_Kanban_Process" runat="server" Label="工序" ShowRedStar="True">
                                </f:Label>

                                <f:TextBox ID="tbxRemark" runat="server" Label="备注">
                                </f:TextBox>
                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>

                                <f:Image ID="imgModelQrcode" runat="server" Label="QRCode" Height="200px" Width="200px"></f:Image>

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
    <script lang="javascript" type="text/javascript">

        var strPline = '<%=lblP_Kanban_Line.Text%>';
        var strPmodel = '<%=lblP_Kanban_Model.Text%>';
        var strPlot = '<%=lblP_Kanban_Lot.Text%>';
        var strPdate = '<%=lblP_Kanban_Date.Text%>';
        var strPregion = '<%=lblP_Kanban_Region.Text%>';
        var strPitem = '<%=lblP_Kanban_Item.Text%>';
        var strPageCount = '<%=lblP_Kanban_Process.Text%>';

        var LODOP; //声明为全局变量
        //var LODOP; //声明为全局变量
        //function CreatePrintPage() {
        //    LODOP = getLodop();
        //    LODOP.PRINT_INIT("套打模板");


        //};

        function CreateNewPages() {
            LODOP = getLodop();

            LODOP.PRINT_INITA(0, 0, 703, 431, "套打模板");
            // LODOP.ADD_PRINT_SETUP_BKIMG("H:\\VS2019\\设变DB项目\\modelcard.jpg");
            // LODOP.SET_SHOW_MODE("BKIMG_IN_PREVIEW", true);
            LODOP.ADD_PRINT_TEXT(90, 70, 60, 20, strPdate);
            LODOP.ADD_PRINT_TEXT(90, 177, 40, 20, strPline);
            LODOP.ADD_PRINT_TEXT(90, 270, 80, 20, strPmodel);
            LODOP.ADD_PRINT_TEXT(90, 400, 100, 20, strPlot);
            LODOP.ADD_PRINT_TEXT(123, 112, 80, 20, strPregion);
            LODOP.ADD_PRINT_BARCODE(10, 39, 64, 64, "QRCode", strPdate + "," + strPline + "," + strPmodel + "," + strPlot + "," +  strPregion);
            LODOP.SET_PRINT_STYLEA(0, "QRCodeVersion", 5);
            LODOP.SET_PRINT_STYLEA(0, "DataCharset", "UTF-8");
            LODOP.PRINT_DESIGN();


        };

        function CreatePages_A114_PREVIEW() {    //一个任务中循环多页，每页内容不同
            LODOP = getLodop();
            //LODOP.PRINT_INIT("");//初始化在循环外
            //LODOP.SET_PRINT_PAGESIZE(1, 703, 431, "套打模板");
            var headtext = "套打" + strPlot + "模板"
            LODOP.PRINT_INITA(0, 0, "194mm", "122mm", headtext);
            LODOP.SET_PRINT_PAGESIZE(1, "194mm", "122mm", "");
            var count = parseInt(strPageCount) + 1;
            for (i = 1; i < count; i++) {
                LODOP.NewPage();
                //LODOP.PRINT_INITA(0, 0, 703, 431, "套打模板");
                LODOP.ADD_PRINT_SETUP_BKIMG("<img src='/oneFile/qrcode/kanbanA114.jpg'/>");
                LODOP.SET_SHOW_MODE("BKIMG_PRINT", true);
                LODOP.ADD_PRINT_BARCODE(2, 19, 64, 64, "QRCode", strPdate + "," + strPline + "," + strPmodel + "," + strPlot + "," + strPregion + "," + i + "SEQ");

                //打印LOGO
                //LODOP.ADD_PRINT_HTM(10, 85, 83, 20, "<img src='/LB_Resources/images/FileLogo.png'/>");
                //LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);
                //打印标题
                //LODOP.ADD_PRINT_TEXT(12, 200, 300, 30, "东莞蒂雅克电子有限公司");
                //LODOP.SET_PRINT_STYLEA(0, "FontName", "微软雅黑");
                //LODOP.SET_PRINT_STYLEA(0, "FontSize", 16);
                //LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
                //LODOP.ADD_PRINT_TEXT(6, 520, 60, 20, "投入时间");

                LODOP.ADD_PRINT_TEXT(65, 70, 60, 20, strPdate);
                LODOP.ADD_PRINT_TEXT(65, 190, 40, 20, strPline);
                LODOP.ADD_PRINT_TEXT(65, 285, 80, 20, strPmodel);
                LODOP.ADD_PRINT_TEXT(65, 430, 90, 20, strPlot);
                LODOP.ADD_PRINT_TEXT(90, 94, 80, 20, strPregion);
                LODOP.SET_PRINT_STYLEA(0, "QRCodeVersion", 5);
                LODOP.SET_PRINT_STYLEA(0, "DataCharset", "UTF-8");

                // LODOP.ADD_PRINT_SETUP_BKIMG("H:\\VS2019\\设变DB项目\\modelcard.jpg");
                // LODOP.SET_SHOW_MODE("BKIMG_IN_PREVIEW", true);
                //LODOP.ADD_PRINT_TEXT("18mm", "20mm",60, 20, strPdate);
                //LODOP.ADD_PRINT_TEXT("18mm", "50mm",40, 20, strPline);
                //LODOP.ADD_PRINT_TEXT("18mm", "75mm",80, 20, strPmodel);
                //LODOP.ADD_PRINT_TEXT("18mm", "114mm",100, 20, strPlot);
                //LODOP.ADD_PRINT_TEXT("27mm", "26mm", 80, 20, strPregion);
                //LODOP.ADD_PRINT_BARCODE("2mm", "5mm", 64, 64, "QRCode", strPdate + strPline + strPmodel + strPlot + strPregion + i);
                //LODOP.SET_PRINT_STYLEA(0, "QRCodeVersion", 3);
                //LODOP.SET_PRINT_STYLEA(0, "DataCharset", "UTF-8");
            }
            //LODOP.PRINT_DESIGN();
            //LODOP.SET_PRINT_MODE("POS_BASEON_PAPER", true);//设置以纸张边缘为基点
            //LODOP.SET_PRINT_MODE("FULL_WIDTH_FOR_OVERFLOW", true);//超出宽度自动缩小
            //LODOP.PRINTSETUP_LEFTMARGIN("0");
            //LODOP.PRINTSETUP_TOPMARGIN("0");

            LODOP.PREVIEW();
        };
        function CreatePages_A5_PREVIEW() {    //一个任务中循环多页，每页内容不同
            LODOP = getLodop();
            //LODOP.PRINT_INIT("");//初始化在循环外
            //LODOP.SET_PRINT_PAGESIZE(1, 703, 431, "套打模板");
            var headtext = "套打" + strPlot +"模板"
            LODOP.PRINT_INITA(0, 0, "210mm", "148mm", headtext);
            LODOP.SET_PRINT_PAGESIZE(1, "210mm", "148mm", "");
            var count = parseInt(strPageCount) + 1;
            for (i = 1; i < count; i++) {
                LODOP.NewPage();
                //LODOP.PRINT_INITA(0, 0, 703, 431, "套打模板");
                LODOP.ADD_PRINT_SETUP_BKIMG("<img src='/oneFile/qrcode/kanbanA5.jpg'/>");
                LODOP.SET_SHOW_MODE("BKIMG_PRINT", true);
                LODOP.ADD_PRINT_BARCODE(2, 19, 96, 96, "QRCode", strPdate + "," + strPline + "," + strPmodel + "," + strPlot + "," + strPregion + "," + strPitem + "," + i + "SEQ");

                //打印LOGO
                //LODOP.ADD_PRINT_HTM(10, 85, 83, 20, "<img src='/LB_Resources/images/FileLogo.png'/>");
                //LODOP.SET_PRINT_STYLEA(0, "Stretch", 1);
                //打印标题
                //LODOP.ADD_PRINT_TEXT(12, 200, 300, 30, "东莞蒂雅克电子有限公司");
                //LODOP.SET_PRINT_STYLEA(0, "FontName", "微软雅黑");
                //LODOP.SET_PRINT_STYLEA(0, "FontSize", 16);
                //LODOP.SET_PRINT_STYLEA(0, "Alignment", 2);
                //LODOP.ADD_PRINT_TEXT(6, 520, 60, 20, "投入时间");
                LODOP.ADD_PRINT_TEXT(18, 690, 60, 20, "SEQ：" + i );
                LODOP.ADD_PRINT_TEXT(90, 100, 60, 20, strPdate);
                LODOP.ADD_PRINT_TEXT(90, 210, 40, 20, strPline);
                LODOP.ADD_PRINT_TEXT(90, 300, 100, 20, strPmodel);
                LODOP.ADD_PRINT_TEXT(90, 490, 100, 20, strPlot);
                LODOP.ADD_PRINT_TEXT(125, 94, 80, 20, strPregion);
                LODOP.SET_PRINT_STYLEA(0, "QRCodeVersion", 5);
                LODOP.SET_PRINT_STYLEA(0, "DataCharset", "UTF-8");

                // LODOP.ADD_PRINT_SETUP_BKIMG("H:\\VS2019\\设变DB项目\\modelcard.jpg");
                // LODOP.SET_SHOW_MODE("BKIMG_IN_PREVIEW", true);
                //LODOP.ADD_PRINT_TEXT("18mm", "20mm",60, 20, strPdate);
                //LODOP.ADD_PRINT_TEXT("18mm", "50mm",40, 20, strPline);
                //LODOP.ADD_PRINT_TEXT("18mm", "75mm",80, 20, strPmodel);
                //LODOP.ADD_PRINT_TEXT("18mm", "114mm",100, 20, strPlot);
                //LODOP.ADD_PRINT_TEXT("27mm", "26mm", 80, 20, strPregion);
                //LODOP.ADD_PRINT_BARCODE("2mm", "5mm", 64, 64, "QRCode", strPdate + strPline + strPmodel + strPlot + strPregion + i);
                //LODOP.SET_PRINT_STYLEA(0, "QRCodeVersion", 3);
                //LODOP.SET_PRINT_STYLEA(0, "DataCharset", "UTF-8");
            }
            //LODOP.PRINT_DESIGN();
            //LODOP.SET_PRINT_MODE("POS_BASEON_PAPER", true);//设置以纸张边缘为基点
            //LODOP.SET_PRINT_MODE("FULL_WIDTH_FOR_OVERFLOW", true);//超出宽度自动缩小
            //LODOP.PRINTSETUP_LEFTMARGIN("0");
            //LODOP.PRINTSETUP_TOPMARGIN("0");

            LODOP.PREVIEW();
        };
        function CreatePages_A114_DESIGN() {    //一个任务中循环多页，每页内容不同
            LODOP = getLodop();
            LODOP.PRINT_INIT("");//初始化在循环外
            LODOP.SET_PRINT_PAGESIZE(0, "186mm", "114mm", "");
            LODOP.PRINT_INITA(0, 0, "186mm", "114mm", "套打模板");
            var count = 2;// parseInt(strPageCount) + 1;
            for (i = 1; i < count; i++) {
                LODOP.NewPage();

                // LODOP.ADD_PRINT_SETUP_BKIMG("H:\\VS2019\\设变DB项目\\modelcard.jpg");
                // LODOP.SET_SHOW_MODE("BKIMG_IN_PREVIEW", true);
                LODOP.ADD_PRINT_TEXT(90, 70, 60, 20, strPdate);
                LODOP.ADD_PRINT_TEXT(90, 177, 40, 20, strPline);
                LODOP.ADD_PRINT_TEXT(90, 270, 80, 20, strPmodel);
                LODOP.ADD_PRINT_TEXT(90, 400, 100, 20, strPlot);
                LODOP.ADD_PRINT_TEXT(123, 112, 80, 20, strPregion);
                LODOP.ADD_PRINT_BARCODE(10, 39, 64, 64, "QRCode", strPdate + strPline + strPmodel + strPlot + strPregion + i + "SEQ");
                LODOP.SET_PRINT_STYLEA(0, "QRCodeVersion", 3);
                LODOP.SET_PRINT_STYLEA(0, "DataCharset", "UTF-8");
            }
            LODOP.PRINT_DESIGN();
            //LODOP.PREVIEW();
        };
        function CreatePages_A5_DESIGN() {    //一个任务中循环多页，每页内容不同
            LODOP = getLodop();
            LODOP.PRINT_INIT("");//初始化在循环外
            LODOP.SET_PRINT_PAGESIZE(2, "210mm", "148mm", "A5");
            LODOP.PRINT_INITA(0, 0, "210mm", "148mm", "套打模板");
            var count = 2;// parseInt(strPageCount) + 1;
            for (i = 1; i < count; i++) {
                LODOP.NewPage();

                // LODOP.ADD_PRINT_SETUP_BKIMG("H:\\VS2019\\设变DB项目\\modelcard.jpg");
                // LODOP.SET_SHOW_MODE("BKIMG_IN_PREVIEW", true);
                LODOP.ADD_PRINT_TEXT(90, 70, 60, 20, strPdate);
                LODOP.ADD_PRINT_TEXT(90, 177, 40, 20, strPline);
                LODOP.ADD_PRINT_TEXT(90, 270, 80, 20, strPmodel);
                LODOP.ADD_PRINT_TEXT(90, 400, 100, 20, strPlot);
                LODOP.ADD_PRINT_TEXT(123, 112, 80, 20, strPregion);
                LODOP.ADD_PRINT_BARCODE(10, 39, 64, 64, "QRCode", strPdate + strPline + strPmodel + strPlot + strPregion + i + "SEQ");
                LODOP.SET_PRINT_STYLEA(0, "QRCodeVersion", 3);
                LODOP.SET_PRINT_STYLEA(0, "DataCharset", "UTF-8");
            }
            LODOP.PRINT_DESIGN();
            //LODOP.PREVIEW();
        };
        function CreateAllPages_SETUP() {    //一个任务中循环多页，每页内容不同
            LODOP = getLodop();
            LODOP.PRINT_INIT("");//初始化在循环外
            LODOP.SET_PRINT_PAGESIZE(1, 703, 431, "");
            LODOP.PRINT_INITA(0, 0, 703, 431, "套打模板");
            var count = parseInt(strPageCount) + 1;
            for (i = 1; i < count; i++) {
                LODOP.NewPage();

                // LODOP.ADD_PRINT_SETUP_BKIMG("H:\\VS2019\\设变DB项目\\modelcard.jpg");
                // LODOP.SET_SHOW_MODE("BKIMG_IN_PREVIEW", true);
                LODOP.ADD_PRINT_TEXT(90, 70, 60, 20, strPdate);
                LODOP.ADD_PRINT_TEXT(90, 177, 40, 20, strPline);
                LODOP.ADD_PRINT_TEXT(90, 270, 80, 20, strPmodel);
                LODOP.ADD_PRINT_TEXT(90, 400, 100, 20, strPlot);
                LODOP.ADD_PRINT_TEXT(123, 112, 80, 20, strPregion);
                LODOP.ADD_PRINT_BARCODE(10, 39, 64, 64, "QRCode", strPdate + strPline + strPmodel + strPlot + strPregion + i + "SEQ");
                LODOP.SET_PRINT_STYLEA(0, "QRCodeVersion", 3);
                LODOP.SET_PRINT_STYLEA(0, "DataCharset", "UTF-8");
            }
            LODOP.PRINT_SETUP();
            //LODOP.PRINT_DESIGN();
            //LODOP.PREVIEW();
        };
    </script>
    </body>
</html>
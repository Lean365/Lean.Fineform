<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p2d_defect_order_totalled_edit.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.PP.poor.p2d_defect_order_totalled_edit" %>

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
                                <f:Label runat="server" ID="DefDate" Label="生产日期" ShowRedStar="True">
                                </f:Label>


                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:Label runat="server" ID="prolinename" Label="生产班组" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow4" runat="server">
                            <Items>
                                <f:Label runat="server" ID="prolot" Label="生产LOT" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>

                        <f:FormRow ID="FormRow10" runat="server">
                            <Items>
                                <f:Label ID="prorealqty" runat="server" Label="生产台数" Text="0">
                                </f:Label>


                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow5" runat="server">
                            <Items>
                                <f:NumberBox ID="pronobadqty" runat="server" Label="无不良台数" Text="0" NoDecimal="true" NoNegative="true" Required="true" ShowRedStar="true">
                                </f:NumberBox>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:Label ID="promodel" runat="server" Label="机种名称"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:Label ID="promodelqty" runat="server" Label="机种台数"></f:Label>


                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow7" runat="server">
                            <Items>
                                <f:Label ID="proorder" runat="server" Label="生产订单"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:Label ID="proorderqty" runat="server" Label="订单台数"></f:Label>
                            </Items>
                        </f:FormRow>
                    </Rows>
                </f:Form>

            </Items>

        </f:Panel>
    </form>
    <script>

        // 注意：专业版中改事件名为：afteredit，开源版中为：edit
        function onGridAfterEdit(editor, params) {
            var me = this, columnId = params.column.id, rowId = params.record.getId();
            if (columnId === 'ChineseScore' || columnId === 'MathScore') {
                var chineseScore = me.f_getCellValue(rowId, 'ChineseScore');
                var mathScore = me.f_getCellValue(rowId, 'MathScore');

                me.f_updateCellValue(rowId, 'TotalScore', chineseScore + mathScore);
            }
        }


    </script>
</body>
</html>

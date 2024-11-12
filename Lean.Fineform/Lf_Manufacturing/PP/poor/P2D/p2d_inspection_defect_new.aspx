<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p2d_inspection_defect_new.aspx.cs" Inherits="LeanFine.Lf_Manufacturing.PP.poor.P2D.p2d_inspection_defect_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <style>
        .x-grid-row-summary .x-grid-cell-inner {
            font-weight: bold;
            color: red;
        }

        .x-grid-row-summary .x-grid-cell {
            background-color: #fff !important;
        }
    </style>
    <link href="../Lf_Resources/css/main.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="../Lf_Resources/ueditor/themes/default/css/ueditor.min.css" />
    <script type="text/javascript" src="../Lf_Resources/ueditor/ckeditor.js"></script>
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
                                <f:DatePicker runat="server" Label="生产日期" DateFormatString="yyyyMMdd" EmptyText="请选择开始日期" AutoPostBack="true" ID="dpProinspdate" ShowRedStar="True" TabIndex="1" FocusOnPageLoad="true">
                                </f:DatePicker>
                                <%--<f:DropDownList runat="server" ID="prolinename" Label="生产班组" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" ShowRedStar="True" Required="true" EnableEdit="true" ForceSelection="true" AutoPostBack="True" OnSelectedIndexChanged="prolinename_SelectedIndexChanged" TabIndex="2">
                                </f:DropDownList>--%>
                                <f:DropDownList runat="server" ID="ddpProorder" Label="生产订单" EmptyText="<%$ Resources:GlobalResource,Query_Select%>" ShowRedStar="True" Required="true" EnableEdit="true" ForceSelection="true" AutoPostBack="True" OnSelectedIndexChanged="ddpProorder_SelectedIndexChanged" TabIndex="3">
                                </f:DropDownList>
                            </Items>
                        </f:FormRow>


                        <f:FormRow ID="FormRow10" runat="server">
                            <Items>
                                <%--<f:NumberBox ID="prorealqty" runat="server" Label="生产台数" Text="0" NoDecimal="true" NoNegative="true" Required="true" ShowRedStar="true">
                                </f:NumberBox>--%>
                                <%--<f:Label ID="pronobadqty" runat="server" Label="无不良台数" Text="0">
                                </f:Label>--%>
                                <f:Label ID="lblPromodel" runat="server" Label="机种名称"></f:Label>
                                <f:Label ID="lblProlot" runat="server" Label="生产LOT"></f:Label>
                                <f:Label ID="lblProorderqty" runat="server" Label="订单台数"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                            </Items>
                        </f:FormRow>

                    </Rows>
                </f:Form>
            </Items>
            <Items>
                <f:Grid ID="Grid1" runat="server" BoxFlex="1" ForceFit="true" ShowBorder="true" ShowHeader="true" AllowCellEditing="true" ClicksToEdit="2" EnableCheckBoxSelect="true"
                    DataKeyNames="ID" AllowSorting="true" SortField="ID" SortDirection="ASC" AllowPaging="true" IsDatabasePaging="true" EnableSummary="true" SummaryPosition="Bottom"
                    OnPreDataBound="Grid1_PreDataBound" OnRowCommand="Grid1_RowCommand" EnableMultiSelect="false">
                    <Toolbars>
                        <f:Toolbar runat="server" Position="Top">
                            <Items>
                                <f:Button ID="btnNew" Icon="Add" EnablePostBack="false" runat="server">
                                </f:Button>
                                <%--                                <f:Button ID="btnDel" Icon="DatabaseDelete" EnablePostBack="false" runat="server">
                                </f:Button>--%>
                                <f:Button ID="btnReset" Icon="Reload" EnablePostBack="false" runat="server">
                                </f:Button>
                                <f:ToolbarSeparator ID="ToolbarSeparator1" runat="server">
                                </f:ToolbarSeparator>
                            </Items>
                        </f:Toolbar>
                    </Toolbars>
                    <Columns>
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete" ToolTip="删除"
                            ConfirmText="确定删除此记录？" ConfirmTarget="Top" CommandName="Delete" Width="50px" />
                        <f:RenderField Width="200px" ColumnID="Propcbtype" SortField="Propcbtype" DataField="Propcbtype" HeaderText="板别" FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlPropcbtype" Required="true" runat="server" EnableEdit="true" ForceSelection="true">
                                </f:DropDownList>

                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="Provisualtype" SortField="Provisualtype" DataField="Provisualtype" HeaderText="目视" FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlProvisualtype"  runat="server" EnableEdit="true" ForceSelection="true">
                                </f:DropDownList>

                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="Provctype" SortField="Provctype" DataField="Provctype" HeaderText="VC" FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlProvctype"  runat="server" EnableEdit="true" ForceSelection="true">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="Prosideadate" SortField="Prosideadate" DataField="Prosideadate" HeaderText="B面实装" Renderer="Date" RendererArgument="yyyyMMdd" FieldType="String">
                            <Editor>
                                <f:DatePicker ID="dpProsideadate" Required="true" runat="server" >
                                </f:DatePicker>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="Prosidebdate" SortField="Prosidebdate" DataField="Prosidebdate" HeaderText="T面实装" Renderer="Date" RendererArgument="yyyyMMdd" FieldType="String">
                            <Editor>
                                <f:DatePicker ID="dpProsidebdate" Required="true" runat="server" >
                                </f:DatePicker>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="Prodshiftname" DataField="Prodshiftname" HeaderText="生产班别" FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlProdshiftname" Required="true" runat="server" EnableEdit="true" ForceSelection="true" AutoPostBack="True">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="Procensor" DataField="Procensor" HeaderText="检查员" FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlProcensor" Required="true" runat="server" EnableEdit="true" ForceSelection="true" AutoPostBack="True">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="Prorealqty" SortField="Prorealqty" DataField="Prorealqty" HeaderText="生产数量" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numProrealqty" Required="true" runat="server" Text="">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="Proispqty" SortField="Proispqty" DataField="Proispqty" HeaderText="检查台数" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numProispqty" Required="true" runat="server" Text="">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="Propcbchecktype" SortField="Propcbchecktype" DataField="Propcbchecktype" HeaderText="检查状况" FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlPropcbchecktype" Required="true" runat="server" EnableEdit="true" ForceSelection="true" AutoPostBack="True">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="Prolinename" DataField="Prolinename" HeaderText="生产线别" FieldType="String">
                            <Editor>

                                <f:TextBox ID="txtProlinename" Required="true" runat="server">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="Proinsqtime" SortField="Proinsqtime" DataField="Proinsqtime" HeaderText="检查工数" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numProinsqtime" Required="true" runat="server" Text="">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="Proaoitime" SortField="Proaoitime" DataField="Proaoitime" HeaderText="AOI工数" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numProaoitime" Required="true" runat="server" Text="">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="Probadqty" DataField="Probadqty" HeaderText="不良数量" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numProbadqty" NoDecimal="true" NoNegative="true" MinValue="1" MaxValue="999" runat="server" AutoPostBack="true">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="Prohandle" DataField="Prohandle" HeaderText="手贴" FieldType="String">
                            <Editor>
                                <f:TextBox ID="txtProhandle" Required="true" runat="server">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>

                        <f:RenderField Width="200px" ColumnID="Probadserial" SortField="Probadserial" DataField="Probadserial" HeaderText="流水" FieldType="String">
                            <Editor>
                                <f:TextBox ID="txtProbadserial" Required="true" runat="server" Text="">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>

                        <f:RenderField Width="300px" ColumnID="Probadcontent" DataField="Probadcontent" HeaderText="内容" FieldType="String">
                            <Editor>
                                <f:TextBox ID="txtProbadcontent" Required="true" runat="server" Text="NG">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="200px" ColumnID="Probadtype" SortField="Probadtype" DataField="Probadtype" HeaderText="不良个所" FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlProbadtype" runat="server" EnableEdit="true" ForceSelection="true">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>

                    </Columns>
                </f:Grid>
                <f:Panel Height="150px" ShowHeader="false" BodyPadding="10px"
                    ShowBorder="true" runat="server">
                    <Items>
                        <f:Label ID="MemoText" runat="server" EncodeText="false">
                        </f:Label>
                    </Items>
                </f:Panel>
            </Items>
        </f:Panel>
    </form>
    <script>

        // 注意：专业版中改事件名为：Contectext，开源版中为：edit
        function onGridContectext(editor, params) {
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

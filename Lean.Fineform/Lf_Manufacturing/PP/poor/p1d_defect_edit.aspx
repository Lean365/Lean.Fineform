<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="p1d_defect_edit.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.PP.poor.p1d_defect_edit" %>

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
                                <f:Label runat="server" ID="DefDate" Label="生产日期" ShowRedStar="True">
                                </f:Label>
                                <f:Label runat="server" ID="prolinename" Label="生产班组" ShowRedStar="True">
                                </f:Label>
                                <f:Label runat="server" ID="prolot" Label="生产LOT" ShowRedStar="True">
                                </f:Label>
                            </Items>
                        </f:FormRow>


                        <f:FormRow ID="FormRow10" runat="server">
                            <Items>
                                <f:Label ID="prorealqty" runat="server" Label="生产台数" Text="0">
                                </f:Label>
                                <f:NumberBox ID="pronobadqty" runat="server" Label="无不良台数" Text="0" NoDecimal="true" NoNegative="true" Required="true" ShowRedStar="true">
                                </f:NumberBox>
                                <f:Label ID="promodel" runat="server" Label="机种名称"></f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:Label ID="promodelqty" runat="server" Label="机种台数"></f:Label>
                                <f:Label ID="proorder" runat="server" Label="生产订单"></f:Label>
                                <f:Label ID="proorderqty" runat="server" Label="订单台数"></f:Label>
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

                        <f:RenderField Width="200px" ColumnID="Prongdept" SortField="Prongdept" DataField="Prongdept" HeaderText="不良区分" FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlProngdept" Required="true" runat="server" EnableEdit="true" ForceSelection="true">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="Probadqty" SortField="Probadqty" DataField="Probadqty" HeaderText="不具合数量" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numProbadqty" NoDecimal="true" NoNegative="true" MinValue="1" MaxValue="999" runat="server" AutoPostBack="true">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="300px" ColumnID="Probadnote" SortField="Probadnote" DataField="Probadnote" HeaderText="不良症状" FieldType="String">
                            <Editor>
                                <f:TextBox ID="txtProbadnote" Required="true" runat="server" Text="NG">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="300px" ColumnID="Probadset" SortField="Probadset" DataField="Probadset" HeaderText="不良个所" FieldType="String">
                            <Editor>
                                <f:TextBox ID="txtProbadset" Required="true" runat="server" Text="NG">
                                </f:TextBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="300px" ColumnID="Probadreason" SortField="Probadreason" DataField="Probadreason" HeaderText="不良原因" FieldType="String">
                            <Editor>
                                <f:TextBox ID="txtProbadreason" Required="true" runat="server" Text="NG">
                                </f:TextBox>
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

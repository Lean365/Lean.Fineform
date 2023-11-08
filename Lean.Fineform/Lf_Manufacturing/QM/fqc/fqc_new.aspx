<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="fqc_new.aspx.cs" Inherits="Lean.Fineform.Lf_Manufacturing.QM.fqc.fqc_new" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>

    <style>
        .redlabel span {
            color: red;
            font-weight: bold;
        }

        .bluelabel span {
            color: blue;
            font-weight: bold;
        }

        .brownlabel span {
            color: brown;
            font-weight: bold;
        }

        .x-grid-row-summary .x-grid-cell-inner {
            font-weight: bold;
            color: red;
        }

        .x-grid-row-summary .x-grid-cell {
            background-color: #fff !important;
        }
    </style>
</head>

<body>
    <form id="form1" runat="server">
        <f:PageManager ID="PageManager1" AutoSizePanelID="Panel1" runat="server" EnableAjax="false" EnableAjaxLoading="false" />
        <f:Panel ID="Panel1" ShowBorder="false" ShowHeader="false" AutoScroll="true" runat="server" Layout="VBox">
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
                <f:Form ID="SimpleForm1" ShowBorder="true" ShowHeader="true" runat="server" BodyPadding="10px"
                    Title="基本信息">
                    <Rows>
                        <f:FormRow ID="FormRow8" runat="server">
                            <Items>
                                <f:Label ID="qmCheckout" runat="server" CssClass="brownlabel" Label="检验次数" Text="*">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow2" runat="server">
                            <Items>
                                <f:DropDownList runat="server" ID="qmOrder" Label="生产工单" ShowRedStar="True" EnableEdit="true" ForceSelection="true" FocusOnPageLoad="true" AutoPostBack="True" OnSelectedIndexChanged="qmOrder_SelectedIndexChanged">
                                </f:DropDownList>
                                <f:DropDownList runat="server" FocusOnPageLoad="true" ID="qmLine" Label="班别" ShowRedStar="True" EnableEdit="true" ForceSelection="true" AutoPostBack="true" OnSelectedIndexChanged="qmLinename_SelectedIndexChanged">
                                </f:DropDownList>
                            </Items>

                        </f:FormRow>

                        <f:FormRow ID="FormRow6" runat="server">
                            <Items>
                                <f:DropDownList runat="server" ID="qmInspector" Label="检查员" ShowRedStar="True">
                                </f:DropDownList>
                                <f:DatePicker runat="server" Required="true" Label="检查日期" DateFormatString="yyyyMMdd" ID="qmCheckdate" ShowRedStar="True">
                                </f:DatePicker>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow10" runat="server">
                            <Items>
                                <f:Label ID="qmModels" runat="server" Label="机种" Text="*">
                                </f:Label>
                                <f:Label ID="LaOrgin" runat="server" Label="仕向" Text="*">
                                </f:Label>
                                <f:Label ID="qmMaterial" runat="server" Label="品号" Text="*">
                                </f:Label>


                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow1" runat="server">
                            <Items>
                                <f:Label ID="LaText" runat="server" Label="品名" Text="*">
                                </f:Label>
                                <f:Label ID="qmProlot" runat="server" Label="生产批次" Text="*">
                                </f:Label>
                                <f:Label runat="server" ID="qmLotserial" Label="序列号">
                                </f:Label>
                            </Items>
                        </f:FormRow>
                        <f:FormRow ID="FormRow3" runat="server">
                            <Items>
                                <f:Label ID="qmOrderqty" runat="server" Label="订单数量" Text="*">
                                </f:Label>
                                <f:Label ID="StockNum" runat="server" CssClass="bluelabel" Label="已入库数量" Text="0">
                                </f:Label>
                                <f:Label ID="SurplusNum" runat="server" CssClass="redlabel" Label="未入库数量" Text="0">
                                </f:Label>
                            </Items>
                        </f:FormRow>

                    </Rows>
                </f:Form>
            </Items>
            <Items>
                <f:Grid ID="Grid1" runat="server" EnableAjax="false" EnableAjaxLoading="false" BoxFlex="1" ForceFit="true" ShowBorder="false" ShowHeader="false" AllowCellEditing="true" ClicksToEdit="1" EnableCheckBoxSelect="true"
                    DataKeyNames="ID" AllowSorting="true" SortField="ID" SortDirection="DESC" AllowPaging="true" IsDatabasePaging="true" EnableSummary="true" SummaryPosition="Bottom"
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
                        <f:LinkButtonField ColumnID="deleteField" TextAlign="Center" Icon="Delete"
                            HeaderText="<%$ Resources:GlobalResource,sys_Button_Delete%>" Text="<%$ Resources:GlobalResource,sys_Button_Delete%>"
                            ToolTip="<%$ Resources:GlobalResource,sys_Button_Delete%>" ConfirmText="<%$ Resources:GlobalResource,sys_Button_DeleteConfirmText%>"
                            ConfirmTarget="Top" CommandName="Delete" Width="70px" />

                        <f:RenderField Width="100px" ColumnID="qmSamplinglevel" EnableAjaxLoading="false" DataField="qmSamplinglevel" HeaderText="抽样水准" FieldType="String">
                            <Editor>
                                <f:DropDownList runat="server" ID="ddlqmSamplinglevel" EnableAjaxLoading="false" ShowRedStar="True">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="qmCheckmethod" EnableAjaxLoading="false" DataField="qmCheckmethod" HeaderText="抽样方式" FieldType="String">
                            <Editor>
                                <f:DropDownList runat="server" ID="ddlqmCheckmethod" ShowRedStar="True" EnableAjaxLoading="false">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="qmProqty" EnableAjaxLoading="false" DataField="qmProqty" HeaderText="送检数量" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numqmProqty" EnableAjaxLoading="false" NoDecimal="true" NoNegative="true" MinValue="1" MaxValue="999" runat="server">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="qmSamplingQty" EnableAjaxLoading="false" DataField="qmSamplingQty" HeaderText="抽样数量" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numqmSamplingQty" EnableAjaxLoading="false" NoDecimal="true" NoNegative="true" MinValue="1" MaxValue="999" runat="server">
                                </f:NumberBox>

                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="qmJudgment" EnableAjaxLoading="false" DataField="qmJudgment" HeaderText="判定" FieldType="String">
                            <Editor>
                                <f:DropDownList ID="ddlqmJudgment" runat="server" EnableAjaxLoading="false" ShowRedStar="True">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="qmAcceptqty" EnableAjaxLoading="false" DataField="qmAcceptqty" HeaderText="验收数量" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numqmAcceptqty" EnableAjaxLoading="false" NoDecimal="true" NoNegative="true" MinValue="1" MaxValue="999" runat="server">
                                </f:NumberBox>

                            </Editor>
                        </f:RenderField>

                        <f:RenderField Width="100px" ColumnID="qmJudgmentlevel" EnableAjaxLoading="false" DataField="qmJudgmentlevel" HeaderText="不良级别" FieldType="String">
                            <Editor>
                                <f:DropDownList runat="server" ID="ddlqmJudgmentlevel" EnableAjaxLoading="false" ShowRedStar="True">
                                </f:DropDownList>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="qmRejectqty" EnableAjaxLoading="false" DataField="qmRejectqty" HeaderText="验退数" FieldType="Int">
                            <Editor>
                                <f:NumberBox ID="numqmRejectqty" ShowRedStar="True" EnableAjaxLoading="false" MinValue="0" runat="server" Text="0" Required="true">
                                </f:NumberBox>
                            </Editor>
                        </f:RenderField>

                        <f:RenderField Width="100px" ColumnID="qmCheckNotes" EnableAjaxLoading="false" ExpandUnusedSpace="true" HtmlEncode="false" DataField="qmCheckNotes" HeaderText="检查号码" FieldType="String">
                            <Editor>
                                <f:TextArea ID="htmlqmCheckNotes" runat="server" EnableAjaxLoading="false" Height="150px" Text="-">
                                </f:TextArea>
                            </Editor>
                        </f:RenderField>
                        <f:RenderField Width="100px" ColumnID="qmSpecialNotes" EnableAjaxLoading="false" ExpandUnusedSpace="true" HtmlEncode="false" DataField="qmSpecialNotes" HeaderText="特记事项" FieldType="String">
                            <Editor>
                                <f:TextArea ID="htmlqmSpecialNotes" runat="server" EnableAjaxLoading="false" Height="150px" Text="-">
                                </f:TextArea>
                            </Editor>
                        </f:RenderField>

                    </Columns>
                </f:Grid>
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

